﻿using System.Text.Json.Serialization;

namespace API_GOPR_SERVICE.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientsDevices = new HashSet<ClientsDevice>();

            Notifications = new HashSet<Notifications>();
        }
        
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string? Name { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public virtual ICollection<ClientsDevice> ClientsDevices { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }

    }
}
