using System.Text.Json.Serialization;

namespace API_GOPR_SERVICE.Models
{
    public class Notifications
    {        
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime DateToAdd { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public int ClientId { get; set; }

        
        public virtual Client Client { get; set; } = null!;
    }
}
