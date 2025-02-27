using FutureStore.Authorization.CustomRoleBased;
using FutureStore.Enums;

namespace FutureStore.Models.Authorization
{
    public class User 
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleEnum? RoleId { get; set; }


    }
}
