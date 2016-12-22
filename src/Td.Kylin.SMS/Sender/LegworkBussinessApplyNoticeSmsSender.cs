using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 工作人员申请开通跑腿业务审核通知发送器
    /// </summary>
    public class LegworkBussinessApplyNoticeSmsSender : AreaSender
    {
        /// <summary>
        /// 申请的工作人员ID
        /// </summary>
        private readonly long _workerId;

        /// <summary>
        /// 初始化工作人员申请开通跑腿业务审核通知发送器实例
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="workerId"></param>
        public LegworkBussinessApplyNoticeSmsSender(int areaId, long workerId) : base(areaId,SmsTemplateOption.WorkerLegworkBussinessApplyNotice)
        {
            _workerId = workerId;
            
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
            //计划发送的目标手机号
            planSendMobiles = CacheData.GetMobiles(areaId, OperatorBusinessNoticeType.LegworkBusinessApplyAudit);

            //发送短信
            await Send(_workerId);

            return true;
        }
    }
}
