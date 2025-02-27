namespace FutureStore.Authorization.CustomRoleBased
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CheckRoleAttribute : Attribute
    {
        private readonly Role _role;

        public CheckRoleAttribute(Role role)
        {
            _role = role;
        }
    }
}
