namespace API_GOPR_SERVICE.Models
{
    public partial class ClientsDevice
    {
        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DeviceId { get; set; }
        public int? NotificationId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Device Device { get; set; } = null!;
        public virtual Notifications Notification { get; set; } = null!;
    }
}
