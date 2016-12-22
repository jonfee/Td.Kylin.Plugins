using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 库存预警通知发送器
    /// </summary>
    public class ProductInventoryWarningNoticeSmsSender : AreaSender
    {
        /// <summary>
        /// SKUID
        /// </summary>
        private readonly long _skuId;

        /// <summary>
        /// 初始化库存预警通知发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="skuId">SKU ID</param>
        /// <param name="skuName">SKU 商品名</param>
        /// <param name="inventory">库存量</param>
        public ProductInventoryWarningNoticeSmsSender(int areaId, long skuId, string skuName, int inventory) : base(areaId,SmsTemplateOption.ProductInventoryWarningNotice)
        {
            _skuId = skuId;

            base.ContentFactory(new { SkuName = skuName, Inventory = inventory });
        }

        public override async Task<bool> SendAsync()
        {
            //计划发送的目标手机号
            planSendMobiles = CacheData.GetMobiles(areaId, OperatorBusinessNoticeType.ProductInventoryWarning);

            //发送短信
            await Send(_skuId);

            return true;
        }
    }
}
