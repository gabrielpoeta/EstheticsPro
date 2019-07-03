using System.Configuration;
using EstheticsPro.Core.ADO;
using EstheticsPro.UserApi.Entities;
using EstheticsPro.UserApi.Services;
using Nancy;
using Nancy.Extensions;
using Nancy.Json;
using Nancy.ModelBinding;

namespace EstheticsPro.UserApi.Modules
{
    public class UserModule : NancyModule
    {
        public UserModule()
        : base("/api/user")
        {
            Post("/post", _ =>
            {
                using (var unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["UserDb"].ConnectionString))
                {
                    var user = this.Bind<User>();
                    var service = new UserService(unitOfWork);
                    return Response.AsJson(service.Post(user));
                }
            });
            Get("/", _ => Response.AsText("Funcionando"));
        }
    }
}