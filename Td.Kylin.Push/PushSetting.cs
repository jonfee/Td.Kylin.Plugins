using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Push
{
    /// <summary>
    /// 表示一个推送配置。
    /// </summary>
    public class PushSetting
    {
        #region 私有字段

        private static string _connectionString;
        private IDatabase _database;
        private string _key;
        private int _storeIndex;

        #endregion

        #region 静态属性

        /// <summary>
        /// 获取或设置 Redis 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException();

                _connectionString = value;
            }
        }

        #endregion

        #region 公共属性

        public string Key
        {
            get
            {
                return _key;
            }
        }

        public int StoreIndex
        {
            get
            {
                return _storeIndex;
            }
        }

        public IDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    if (string.IsNullOrWhiteSpace(ConnectionString))
                        throw new InvalidOperationException("The redis connectionstring is null or empty.");

                    var options = ConfigurationOptions.Parse(ConnectionString);

                    _database = ConnectionMultiplexer.Connect(options).GetDatabase(this.StoreIndex);
                }

                return _database;
            }
        }


        #endregion

        #region 构造方法

        internal PushSetting(string key, int storeIndex)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            this._key = key;
            this._storeIndex = storeIndex;
        }

        #endregion

    }

    /// <summary>
    /// 推送配置工厂类。
    /// </summary>
    public static class PushSettingFactory
    {
        #region 私有字段

        private static Dictionary<PushType, PushSetting> _settings;

        #endregion

        #region 公共属性

        public static Dictionary<PushType, PushSetting> Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new Dictionary<PushType, PushSetting>();

                    /*推送配置*/
                    _settings.Add(PushType.LegworkUserAddOrder, new PushSetting("LegworkUserAddOrder", 1));
                    _settings.Add(PushType.LegworkConfirmDelivery, new PushSetting("LegworkConfirmDelivery", 1));
                    _settings.Add(PushType.LegworkDownPay, new PushSetting("LegworkDownPay", 1));
                    _settings.Add(PushType.LegworkMessageBuy, new PushSetting("LegworkMessageBuy", 1));
                    _settings.Add(PushType.LegworkOffer, new PushSetting("LegworkOffer", 1));
                    _settings.Add(PushType.LegworkUserConfirmOrder, new PushSetting("LegworkUserConfirmOrder", 1));
                    _settings.Add(PushType.LegworkUserTopPay, new PushSetting("LegworkUserTopPay", 1));
                }

                return _settings;
            }
        }

        #endregion

    }
}
