using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Cache;

namespace Td.Kylin.SMS.Sender
{
    public abstract class AreaSender : BaseSender
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        protected readonly int areaId;

        /// <summary>
        /// 区域短信资源
        /// </summary>
        internal AreaAssets Assets { get;  set; }

        /// <summary>
        /// 计划发送的目标手机号
        /// </summary>
        protected string[] planSendMobiles;        

        /// <summary>
        /// 计划发送数量
        /// </summary>
        private int planSendNumber
        {
            get
            {
                return planSendMobiles != null ? planSendMobiles.Count() : 0;
            }
        }

        /// <summary>
        /// 实际发送数量
        /// </summary>
        private int realSendNumber
        {
            get
            {
                return realSendMobiles != null ? realSendMobiles.Count() : 0;
            }
        }

        public AreaSender(int areaId, SmsTemplateOption option) : base(option)
        {
            this.areaId = areaId;
        }

        /// <summary>
        /// 区域专属短信发送
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        protected async Task Send(object uid)
        {
            //向区域运营商申请短信资源
            Assets = AreaAssetsCache.Instance.GetSmsAssets(areaId, planSendNumber);

            if (Assets.Balance < planSendNumber)
            {
                realSendMobiles = planSendMobiles.Take(Assets.Balance).ToArray();
            }
            else
            {
                realSendMobiles = planSendMobiles;
            }

            if (realSendNumber > 0)
            {
                string strId = (uid ?? string.Empty).ToString();

                var result = await base.SendSms(IdentityType.AreaOperator, Assets.OperatorId, realSendMobiles, strId);

                //更新资产
                AreaAssetsCache.Instance.UseAssets(Assets.AreaId, Assets.OperatorId, planSendNumber, realSendNumber, result.IsSuccess);
            }
            else
            {
                //更新资产
                AreaAssetsCache.Instance.UseAssets(Assets.AreaId, Assets.OperatorId, planSendNumber, realSendNumber, false);
            }
        }
    }
}
