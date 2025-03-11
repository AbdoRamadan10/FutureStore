namespace FutureStore.Models.Audit
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string Changes { get; set; }

    }
}
