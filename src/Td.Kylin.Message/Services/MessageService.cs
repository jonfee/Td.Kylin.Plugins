using System;
using Td.Kylin.Entity;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Data;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 消息数据服务
    /// </summary>
    internal class MessageService
    {
        /// <summary>
        /// 添加用户系统消息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="option">消息类型，<see cref="MessageTemplateOption"/></param>
        /// <param name="relateDataID">消息关联的数据ID，如:某某订单ID</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="sign">消息发送方签名（如：XXX公司）</param>
        /// <returns></returns>
        public bool AddUserMessage(long userID, MessageTemplateOption option, string relateDataID, string title, string content, string sign)
        {
            return AddUserMessage(new[] { userID }, option, relateDataID, title, content, sign);
        }

        /// <summary>
        /// 添加用户系统消息
        /// </summary>
        /// <param name="userIDs">用户ID集合</param>
        /// <param name="option">消息类型，<see cref="MessageTemplateOption"/></param>
        /// <param name="relateDataID">消息关联的数据ID，如:某某订单ID</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="sign">消息发送方签名（如：XXX公司）</param>
        /// <returns></returns>
        public bool AddUserMessage(long[] userIDs, MessageTemplateOption option, string relateDataID, string title, string content, string sign)
        {
            using (var db = new DataContext())
            {
                if (userIDs == null || userIDs.Length < 1) return false;

                foreach (var userId in userIDs)
                {
                    var item = new User_Message
                    {
                        MessageID = IDProvider.NewId(),
                        Content = content ?? string.Empty,
                        CreateTime = DateTime.Now,
                        IsDelete = false,
                        IsRead = false,
                        MessageType = (int)option,
                        RefDataID = relateDataID,
                        Sign = sign ?? string.Empty,
                        Title = title,
                        UserID = userId
                    };

                    db.User_Message.Add(item);
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 添加商户系统消息
        /// </summary>
        /// <param name="merchantID">商家ID</param>
        /// <param name="option">消息类型，<see cref="MessageTemplateOption"/></param>
        /// <param name="relateDataID">消息关联的数据ID，如:某某订单ID</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="sign">消息发送方签名（如：XXX公司）</param>
        /// <returns></returns>
        public bool AddMerchantMessage(long merchantID, MessageTemplateOption option, string relateDataID, string title, string content, string sign)
        {
            using (var db = new DataContext())
            {
                var item = new Merchant_Message
                {
                    MessageID = IDProvider.NewId(),
                    Content = content,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    IsRead = false,
                    MessageType = (int)option,
                    MerchantID = merchantID,
                    RefDataID = relateDataID,
                    Sign = sign,
                    Title = title
                };

                db.Merchant_Message.Add(item);

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// 添加工作人员系统消息
        /// </summary>
        /// <param name="workerID">工作人员ID</param>
        /// <param name="option">消息类型，<see cref="MessageTemplateOption"/></param>
        /// <param name="relateDataID">消息关联的数据ID，如:某某订单ID</param>
        /// <param name="title">消息标题</param>
        /// <param name="content">消息内容</param>
        /// <param name="sign">消息发送方签名（如：XXX公司）</param>
        /// <returns></returns>
        public bool AddWorkerMessage(long workerID, MessageTemplateOption option, string relateDataID, string title, string content, string sign)
        {
            using (var db = new DataContext())
            {
                var item = new Worker_Message
                {
                    MessageID = IDProvider.NewId(),
                    Content = content,
                    CreateTime = DateTime.Now,
                    IsDelete = false,
                    IsRead = false,
                    MessageType = (int)option,
                    WorkerID = workerID,
                    RefDataID = relateDataID,
                    Sign = sign,
                    Title = title
                };

                db.Worker_Message.Add(item);

                return db.SaveChanges() > 0;
            }
        }
    }
}
