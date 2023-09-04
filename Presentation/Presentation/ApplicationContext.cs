using Immowert4You.Application.Contracts.Storage;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Immowert4You.Domain.Users;
using System.Collections.Generic;
using Immowert4You.Domain.Properties;
using Immowert4You.Domain.Chats;
using Immowert4You.Domain.Brokers;

namespace Immowert4You.Presentation
{
    public class ApplicationContext : IRepoManager, ICurrentUserRepository, IBrokerRepository, 
        IChatRepository, IPropertyRepository, ITokenRepository, ITempStorage
    {
        private UserDto _currentUser;
        private static readonly string _currentUserKey = "CurrentUser";

        private BrokerDto _broker;
        private static readonly string _brokerKey = "Broker";

        private List<ChatDto> _chats;
        private static readonly string _chatsKey = "Chats";

        private List<PropertyDto> _properties;
        private static readonly string _propertiesKey = "Properties";

        private string _accessToken;
        private static readonly string _accessTokenKey = "AccessToken";

        public static ApplicationContext Instance = new ApplicationContext();

        private ApplicationContext()
        {
        }

        public Task FetchRepositories()
        {
            throw new System.NotImplementedException();
        }

        public async Task SetUser(UserDto currentUser)
        {
            await SetValueToRepository(_currentUserKey, _currentUser = currentUser);
        }

        public UserDto GetUser()
        {
            return _currentUser ?? GetValueFromRepository<UserDto>(_currentUserKey);
        }

        public async Task SetBroker(BrokerDto broker)
        {
            await SetValueToRepository(_brokerKey, _broker = broker);
        }

        public BrokerDto GetBroker()
        {
            return _broker ?? GetValueFromRepository<BrokerDto>(_brokerKey);
        }

        public async Task SetProperties(List<PropertyDto> properties)
        {
            await SetValueToRepository(_propertiesKey, _properties = properties);
        }

        public List<PropertyDto> GetProperties()
        {
            return _properties ?? GetValueFromRepository<List<PropertyDto>>(_propertiesKey);
        }

        public async Task SetChats(List<ChatDto> chats)
        {
            await SetValueToRepository(_chatsKey, _chats = chats);
        }

        public List<ChatDto> GetChats()
        {
            return _chats ?? GetValueFromRepository<List<ChatDto>>(_chatsKey);
        }

        public async Task SetToken(string token)
        {
            await Xamarin.Essentials.SecureStorage.SetAsync(_accessTokenKey, _accessToken = token);
        }

        public string GetToken()
        {
            return string.IsNullOrEmpty(_accessToken) ? GetValueFromRepository(_accessTokenKey) : _accessToken;
        }

        public void Save<T>(T property) where T : class
        {
            var success = Xamarin.Forms.Application.Current.Properties.TryGetValue(typeof(T).Name, out _);
            if (success)
            {
                Xamarin.Forms.Application.Current.Properties.Remove(typeof(T).Name);
            }
            Xamarin.Forms.Application.Current.Properties.Add(typeof(T).Name, property);
        }

        public T Read<T>(bool cleanAfter = false) where T : class
        {
            var success = Xamarin.Forms.Application.Current.Properties.TryGetValue(typeof(T).Name, out object value);
            if (success)
            {
                if (cleanAfter)
                {
                    Xamarin.Forms.Application.Current.Properties.Remove(typeof(T).Name);
                }
                return (T)value;
            }
            return default;
        }

        private async Task SetValueToRepository(string key, object value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);

            await Xamarin.Essentials.SecureStorage.SetAsync(key, serializedValue);
        }

        private T GetValueFromRepository<T>(string key) where T : class
        {
            var serializedValue = Xamarin.Essentials.SecureStorage.GetAsync(key);

            serializedValue.Wait();

            if (serializedValue.Result != null)
                return JsonConvert.DeserializeObject<T>(serializedValue.Result);

            return null;
        }

        private string GetValueFromRepository(string key)
        {
            var serializedProperties = Xamarin.Essentials.SecureStorage.GetAsync(key);

            serializedProperties.Wait();

            return serializedProperties.Result;
        }
    }
}
