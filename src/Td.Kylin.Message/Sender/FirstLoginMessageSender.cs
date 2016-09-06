using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 用户首次登录时消息发送器
    /// </summary>
    public class FirstLoginMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 登录用户ID
        /// </summary>
        private readonly long _userID;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="areaID">当前登录时的区域</param>
        /// <param name="userID">登录用户ID</param>
        public FirstLoginMessageSender(int areaID, long userID) : base(MessageTemplateOption.FirstLogin)
        {
            _userID = userID;

            var areaName = CacheData.GetAreaName(areaID);

            base.ContentFactory(new { AreaName = areaName });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _userID.ToString(), Title, Content, "");
        }
    }
}
