using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.AspNet.Utils;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Push.Model;
using Td.Kylin.Push.Services;
using Td.Kylin.Redis;

namespace Td.Kylin.Push.Handle
{
    public class PushHandle
    {
        public static void AssignOrderPushHandle(long orderID)
        {
            try
            {
                var pushRedis = PushSettingFactory.Settings[PushType.LegworkUserAddOrder];
                if (pushRedis != null)
                {
                    var assignOrder = OrderServices.PushAddAssignOrderPushContent(orderID);
                    pushRedis.Database.ListRightPush<AssignOrderPushContent>(pushRedis.Key, assignOrder);

                }
            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(orderID));
            }
        }

        public static void LegworkOffer(long orderID, int Status, long UserID, long RequiredDeliveryAddressID, long RequiredPickAddressID, long offerID)
        {
            #region//11、消息推送

            try
            {
                if (Status == (int)LegworkOrderStatus.WaitingHandle)
                {


                    var pushRedis = PushSettingFactory.Settings[PushType.LegworkOffer];
                    var orderModel = OrderServices.GetLegworkOrder(orderID);
                    var offerRecord = OrderServices.GetLegworkOfferRecord(offerID);
                    var userModel = UserServices.GetUserModel(UserID);
                    var deliveryModel = UserServices.GetUserAddressModel(RequiredDeliveryAddressID);
                    var pickModel = UserServices.GetUserAddressModel(RequiredPickAddressID);
                    if (null != pushRedis)
                    {
                        var msgContent = new OrderOfferPushContent()
                        {
                            OrderID = orderModel.OrderID,
                            OrderCode = orderModel.OrderCode,
                            Charge = offerRecord.Charge,
                            CreateTime = offerRecord.CreateTime,
                            Distance = orderModel.OrderType == (int)LegworkOrderType.BuyGoods ? Locator.GetDistance(offerRecord.Latitude, offerRecord.Longitude, deliveryModel.Latitude, deliveryModel.Longitude) : Locator.GetDistance(offerRecord.Latitude, offerRecord.Longitude, pickModel.Latitude, pickModel.Longitude),
                            PushCode = userModel?.PushCode,
                        };
                        pushRedis.Database.ListRightPush<OrderOfferPushContent>(pushRedis.Key, msgContent);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            #endregion
        }


        public static void LegworkUserConfirmOrder(long orderID, long workerID)
        {
            try
            {
                var model = UserServices.GetWorkerAccount(workerID);
                var orderModel = OrderServices.GetLegworkOrder(orderID);
                //购买物品的时候推送给工作端
                if (orderModel.OrderType == ((int)LegworkOrderType.BuyGoods))
                {
                    var pushRedis = PushSettingFactory.Settings[PushType.LegworkUserConfirmOrder];
                    if (null != orderModel)
                    {
                        if (null != pushRedis)
                        {
                            var msgContent = new OrderConfirmPushContent()
                            {
                                OrderID = orderModel.OrderID,
                                CreateTime = orderModel.SubmitTime,
                                OrderCode = orderModel.OrderCode,
                                PushCode = model?.PushCode,
                                OrderStatus = orderModel.Status,
                            };
                            pushRedis.Database.ListRightPush<OrderConfirmPushContent>(pushRedis.Key, msgContent);
                        }
                    }
                }
            }
            catch
            {
            }

        }

        public static void LegworkConfirmDelivery(long orderID, long workerID)
        {
            try
            {

                var orderModel = OrderServices.GetLegworkOrder(orderID);
                var model = UserServices.GetUserModel(orderModel.UserID);
                var pushRedis = PushSettingFactory.Settings[PushType.LegworkConfirmDelivery];
                if (null != orderModel)
                {
                    if (null != pushRedis)
                    {
                        var msgContent = new OrderConfirmPushContent()
                        {
                            OrderID = orderModel.OrderID,
                            OrderCode = orderModel.OrderCode,
                            CreateTime = orderModel.SubmitTime,
                            PushCode = model?.PushCode,
                            OrderStatus = orderModel.Status,
                        };
                        pushRedis.Database.ListRightPush<OrderConfirmPushContent>(pushRedis.Key, msgContent);
                    }
                }
            }
            catch
            {
            }
        }

        public static void LegworkDownPay(long orderID)
        {
            try
            {
                var orderModel = OrderServices.GetLegworkOrder(orderID);
                var model = UserServices.GetUserModel(orderModel.UserID);

                var pushRedis = PushSettingFactory.Settings[PushType.LegworkDownPay];
                if (null != orderModel)
                {
                    if (null != pushRedis)
                    {
                        var msgContent = new RequestPaymentPushContent()
                        {
                            OrderID = orderModel.OrderID,
                            OrderCode = orderModel.OrderCode,
                            PushCode = model?.PushCode,
                            CreateTime = orderModel.SubmitTime,
                            GoodsAmount = orderModel.GoodsAmount,
                            ServiceCharge = orderModel.ServiceCharge,
                            ActualAmount = orderModel.ActualAmount,
                            OrderStatus = orderModel.Status,
                        };
                        pushRedis.Database.ListRightPush<RequestPaymentPushContent>(pushRedis.Key, msgContent);
                    }
                }
            }
            catch
            {
            }
        }

        public static void LegworkUserTopPay(long orderID)
        {
            try
            {
                var orderModel = OrderServices.GetLegworkOrder(orderID);
                var workModel = UserServices.GetWorkerAccount(orderModel.WorkerID.HasValue ? orderModel.WorkerID.Value : 0);
                //购买物品为已完成
                if (orderModel.OrderType == (int)LegworkOrderType.BuyGoods)
                {

                    var PushCode = workModel?.PushCode;
                    var userModel = UserServices.GetUserModel(orderModel.UserID);
                    var pushRedis = PushSettingFactory.Settings[PushType.LegworkUserTopPay];
                    if (null != orderModel)
                    {
                        if (null != pushRedis)
                        {
                            var msgContent = new PaymentCompletePushContent()
                            {
                                OrderID = orderModel.OrderID,
                                OrderCode = orderModel.OrderCode,
                                PushCode = PushCode,
                                UserName = userModel.Username,
                                Amount = orderModel.ActualAmount,
                                CreateTime = orderModel.SubmitTime,
                            };
                            pushRedis.Database.ListRightPush<PaymentCompletePushContent>(pushRedis.Key, msgContent);
                        }
                    }
                }
                else
                {
                    //去送物品为待取货
                    var PushCode = workModel?.PushCode;
                    var pushRedis = PushSettingFactory.Settings[PushType.LegworkUserConfirmOrder];
                    if (null != orderModel)
                    {
                        if (null != pushRedis)
                        {
                            var msgContent = new OrderConfirmPushContent()
                            {
                                OrderID = orderModel.OrderID,
                                CreateTime = orderModel.SubmitTime,
                                OrderCode = orderModel.OrderCode,
                                PushCode = PushCode,
                                OrderStatus = orderModel.Status,
                            };
                            pushRedis.Database.ListRightPush<OrderConfirmPushContent>(pushRedis.Key, msgContent);
                        }
                    }
                }
            }
            catch
            {
            }
        }


        public static void LegworkMessageBuy(long orderID)
        {
            try
            {
                var orderModel = OrderServices.GetLegworkOrder(orderID);
                var model = UserServices.GetWorkerAccount(orderModel.WorkerID.HasValue ? orderModel.WorkerID.Value : 0);
                var pushRedis = PushSettingFactory.Settings[PushType.LegworkMessageBuy];
                if (null != orderModel)
                {
                    if (null != pushRedis)
                    {
                        var msgContent = new MessageBuyPushContent()
                        {
                            OrderID = orderModel.OrderID,
                            OrderCode = orderModel.OrderCode,
                            PushCode = model.PushCode,
                            CreateTime = DateTime.Now
                        };
                        pushRedis.Database.ListRightPush<MessageBuyPushContent>(pushRedis.Key, msgContent);
                    }
                }
            }
            catch
            {
            }

        }

    }
}
