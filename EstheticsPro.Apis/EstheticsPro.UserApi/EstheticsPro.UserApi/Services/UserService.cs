using EstheticsPro.Core.ADO;
using EstheticsPro.Core.BaseApi;
using EstheticsPro.UserApi.Entities;

namespace EstheticsPro.UserApi.Services
{
    public class UserService : BaseService<User>
    {
        public UserService(UnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}