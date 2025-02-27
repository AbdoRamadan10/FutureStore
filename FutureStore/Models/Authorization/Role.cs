using FutureStore.Enums;

namespace FutureStore.Models.Authorization
{
    public class Role
    {
        public RoleEnum RoleId { get; set; }
        public string Code { get; set; }

        public string? Description { get; set; }

    }
}
