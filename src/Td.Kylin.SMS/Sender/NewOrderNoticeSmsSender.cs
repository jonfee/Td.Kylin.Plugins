using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Entity;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 直营新订单通知消息发送器
    /// </summary>
    public class NewOrderNoticeSmsSender : BaseSender
    {
        /// <summary>
        /// 运营商ID
        /// </summary>
        private readonly long _operatorId;

        /// <summary>
        /// 需要通知的手机号集合
        /// </summary>
        private readonly string[] _mobiles;

        /// <summary>
        /// 订单编号
        /// </summary>
        private readonly string _orderCode;

        private readonly OperatorAssetsService _operatorAssetsService;

        /// <summary>
        /// 初始化直营新订单通知消息发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="orderCode">订单编号</param>
        /// <param name="orderTime">下单时间</param>
        public NewOrderNoticeSmsSender(int areaId, string orderCode, DateTime orderTime) : base(SmsTemplateOption.NewOrderNotice)
        {
            _orderCode = orderCode;

            _operatorAssetsService = new OperatorAssetsService();
            
            _operatorId = new AreaOperatorService().GetOperatorId(areaId);

            _mobiles = CacheData.GetMobiles(areaId, OperatorBusinessNoticeType.OrderDispatch);

            var balance = _operatorAssetsService.GetAssetsBalance(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms);

            if (balance < _mobiles.Length)
            {
                _mobiles = _mobiles.Take(balance).ToArray();
            }

            base.ContentFactory(new { OrderCode = orderCode, OrderTime = orderTime.ToString("yyyy/MM/dd HH:mm") });
        }

        public override async Task<bool> SendAsync()
        {
            if (_mobiles == null || _mobiles.Length < 1) return false;

            var result = await ConfigRoot.SendProvider.SendSmsAsync(_mobiles, Content, _orderCode);

            bool success = result.IsSuccess;

            //发送成功，则更新资产
            if (success)
            {
                try
                {
                    _operatorAssetsService.UseAssets(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms,
                        _mobiles.Length);
                }
                catch
                {
                    //TODO
                }
            }

            List<SmsSendRecords> records = new List<SmsSendRecords>();

            foreach (var m in _mobiles)
            {
                SmsSendRecords record = new SmsSendRecords
                {
                    IsSuccess = success,
                    Message = Content,
                    Mobile = m,
                    Remark = result.Remark,
                    SenderId = 0,
                    SenderType = (int)IdentityType.Platform,
                    SmsType = (int)SmsTemplateOption.NewOrderNotice,
                    SendID = IDProvider.NewId(),
                    SendTime = DateTime.Now
                };

                records.Add(record);
            }

            new SmsSendRecordsService().AddRecord(records);

            return success;
        }
    }
}
