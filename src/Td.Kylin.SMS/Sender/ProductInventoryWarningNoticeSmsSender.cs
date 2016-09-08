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
    /// 库存预警通知发送器
    /// </summary>
    public class ProductInventoryWarningNoticeSmsSender : BaseSender
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
        /// SKUID
        /// </summary>
        private readonly long _skuId;

        private readonly OperatorAssetsService _operatorAssetsService;

        /// <summary>
        /// 初始化库存预警通知发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="skuId">SKU ID</param>
        /// <param name="skuName">SKU 商品名</param>
        /// <param name="inventory">库存量</param>
        public ProductInventoryWarningNoticeSmsSender(int areaId, long skuId, string skuName, int inventory) : base(SmsTemplateOption.ProductInventoryWarningNotice)
        {
            _skuId = skuId;

            _operatorAssetsService = new OperatorAssetsService();

            var operatorService = new AreaOperatorService();

            _operatorId = operatorService.GetOperator(areaId);

            _mobiles = operatorService.GetNoticeMobiles(_operatorId, OperatorBusinessNoticeType.LegworkBusinessApplyAudit);

            var balance = _operatorAssetsService.GetAssetsBalance(_operatorId, EnumLibrary.Operator.OperatorAssetsType.Sms);

            if (balance < _mobiles.Length)
            {
                _mobiles = _mobiles.Take(balance).ToArray();
            }

            base.ContentFactory(new { SkuName = skuName, Inventory = inventory });
        }

        public override async Task<bool> SendAsync()
        {
            if (_mobiles == null || _mobiles.Length < 1) return false;

            var result = await ConfigRoot.SendProvider.SendSmsAsync(_mobiles, Content, _skuId.ToString());

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
                    SmsType = (int)SmsTemplateOption.ProductInventoryWarningNotice,
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
