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
    /// 工作人员申请开通跑腿业务审核通知发送器
    /// </summary>
    public class LegworkBussinessApplyNoticeSmsSender : BaseSender
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
        /// 申请的工作人员ID
        /// </summary>
        private readonly long _workerId;

        private readonly OperatorAssetsService _operatorAssetsService;

        /// <summary>
        /// 初始化工作人员申请开通跑腿业务审核通知发送器实例
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="workerId"></param>
        public LegworkBussinessApplyNoticeSmsSender(int areaId, long workerId) : base(SmsTemplateOption.WorkerLegworkBussinessApplyNotice)
        {
            _workerId = workerId;

            _operatorAssetsService = new OperatorAssetsService();
            
            _operatorId = new AreaOperatorService().GetOperatorId(areaId);

            _mobiles = CacheData.GetMobiles(areaId, OperatorBusinessNoticeType.LegworkBusinessApplyAudit);

            var balance = _operatorAssetsService.GetAssetsBalance(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms);

            if (balance < _mobiles.Length)
            {
                _mobiles = _mobiles.Take(balance).ToArray();
            }

            string name = string.Empty;
            string mobile = string.Empty;

            var userInfo = new UserService().GetUseInfo(workerId);

            if (null != userInfo)
            {
                name = userInfo.Name;
                mobile = userInfo.Mobile;
            }

            base.ContentFactory(new { Name = name, Mobile = mobile });
        }

        public override async Task<bool> SendAsync()
        {
            if (_mobiles == null || _mobiles.Length < 1) return false;

            var result = await ConfigRoot.SendProvider.SendSmsAsync(_mobiles, Content, _workerId.ToString());

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
                    SmsType = (int)SmsTemplateOption.WorkerLegworkBussinessApplyNotice,
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
