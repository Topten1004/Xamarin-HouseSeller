using Immowert4You.Application.Contracts.API;
using Immowert4You.Application.Contracts.Storage;
using Immowert4You.Domain.Users;
using System;
using System.Threading.Tasks;

namespace Immowert4You.Application.Users.Queries
{
    public class GetCurrentUserQuery : IGetCurrentUserQuery
    {
        private readonly IUsersApiService _usersApiService;
        private readonly ICurrentUserRepository _currentUserRepository;
        private readonly IBrokersApiService _brokersApiService;
        private readonly IBrokerRepository _brokerRepository;

        public GetCurrentUserQuery(
            IUsersApiService usersApiService,
            ICurrentUserRepository currentUserRepository,
            IBrokersApiService brokersApiService,
            IBrokerRepository brokerRepository)
        {
            _usersApiService = usersApiService;
            _currentUserRepository = currentUserRepository;
            _brokersApiService = brokersApiService;
            _brokerRepository = brokerRepository;
        }

        public event EventHandler<GetUserEventArgs> GetUserExecuted;

        public async Task<UserDto> Execute()
        {
            try
            {
                var userDto = await _usersApiService.GetCurrentUser();

                if (userDto == null)
                    return null;

                await _currentUserRepository.SetUser(userDto);

                if (userDto.IsBroker)
                {
                    var broker = await _brokersApiService.GetBroker(userDto.Id);

                    await _brokerRepository.SetBroker(broker);
                }

                GetUserExecuted?.Invoke(this, new GetUserEventArgs { User = userDto });

                return userDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
