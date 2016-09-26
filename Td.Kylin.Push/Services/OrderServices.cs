using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Td.Kylin.Entity;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Push.Data;
using Td.Kylin.Push.Model;
using Td.LBS;
using Td.Common;

namespace Td.Kylin.Push.Services
{
    public class OrderServices : DataContext
    {
        public static AssignOrderPushContent PushAddAssignOrderPushContent(long orderID)
        {
            using (var db = new DataContext())
            {
                var legworkOrder = db.Legwork_Order.Where(q => q.OrderID == orderID).FirstOrDefault();
                if (legworkOrder != null)
                {
                    var deliveryModel = GetAddress(legworkOrder.RequiredDeliveryAddressID);

                    #region 计算区域ID

                    var listAreaID = db.Legwork_AreaConfig.Select(q => q.AreaID).ToArray();
                    bool result = false;
                    int AreaID = 0;

                    foreach (var i in listAreaID)
                    {
                        //获取开通跑腿区域
                        var openAreas = db.Legwork_AreaConfig.Where(p => p.AreaID == i).Select(t => t.OpenAreas).FirstOrDefault().Split(',').Select(p => p.ToInt32());

                        if (!openAreas.HasValue())
                            return null;

                        //获取区域经纬度
                        try
                        {
                            var listArea = db.System_Area.Where(q => openAreas.Contains(q.AreaID)).Select(t => t.Points).Select(p => Newtonsoft.Json.JsonConvert.DeserializeObject<List<List<AreaModel>>>(p));

                            if (!listArea.HasValue())
                                return null;

                            foreach (var list1 in listArea)
                            {
                                foreach (var list2 in list1)
                                {
                                    result = LocationUtility.IsInPolygon(new Location(deliveryModel.Latitude, deliveryModel.Longitude), list2.Select(p => new Location(p.lat, p.lng)).ToList());

                                    if (result)
                                    {
                                        AreaID = i;
                                        break;
                                        ;
                                    }
                                }
                                if (result)
                                    break;
                            }
                            if (result)
                                break;
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    #endregion

                    string PushCode = "";
                    var userList = (from a in db.Worker_Account
                                    join b in db.Worker_BusinessRelation on a.WorkerID equals b.WorkerID
                                    where a.AccountStatus == (short)WorkerAccountStatus.Normal && a.WorkingState == (short)WorkingState.Enabled && a.DefaultAreaID == AreaID && b.AuditStatus == (int)WorkerBusinessAuditStatus.Passed && b.BusinessType == (int)BusinessType.LegworkService
                                    select new
                                    {
                                        a.PushCode
                                    }).ToArray();

                    if (!userList.Any())
                        return null;
                    foreach (var item in userList)
                    {
                        if (!string.IsNullOrWhiteSpace(item.PushCode))
                            PushCode += item.PushCode + ",";
                    }
                    AssignOrderPushContent assignOrder = new AssignOrderPushContent()
                    {
                        PushCode = PushCode.TrimEnd(new char[]
                        {
                            ','
                        }),
                        CreateTime = legworkOrder.SubmitTime,
                        OrderID = legworkOrder.OrderID,
                        OrderType = legworkOrder.OrderType,
                        OrderCode = legworkOrder.OrderCode,
                    };

                    return assignOrder;
                }
                return null;
            }
        }


        public static User_Address GetAddress(long addressID)
        {
            using (var db = new DataContext())
            {
                var model = db.User_Address.FirstOrDefault(q => q.AddressID == addressID);
                if (model == null)
                    return model = new User_Address();
                return model;
            }
        }


        public static Legwork_Order GetLegworkOrder(long orderID)
        {
            using (var db=new DataContext())
            {
                return db.Legwork_Order.Where(q => q.OrderID == orderID).FirstOrDefault();
            }
        }

        public static Legwork_OfferRecord GetLegworkOfferRecord(long offerID)
        {
            using ( var db=new DataContext())
            {
                return db.Legwork_OfferRecord.Where(q => q.OfferID == offerID).FirstOrDefault();
            }
        }
    }
}
