using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_GOPR_SERVICE.Models
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime DateToAdd { get; set; }
        public string Body { get; set; } = null!;
        public bool IsRead { get; set; }
        public int ClientId { get; set; }
        [JsonIgnore]
        public virtual Client Client { get; set; } = null!;
    }
}
