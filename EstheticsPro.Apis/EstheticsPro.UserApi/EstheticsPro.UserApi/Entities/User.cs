using Dapper.Contrib.Extensions;
using EstheticsPro.Core.BaseApi;
using EstheticsPro.Core.Extensions;

namespace EstheticsPro.UserApi.Entities
{
    [Table("Users")]
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public bool HasId() => Id > 0;
        public void SetId(long id) => Id = id.ToInt();
    }
}