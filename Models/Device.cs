using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_GOPR_SERVICE.Models
{
    public partial class Device
    {
        
        public Device()
        {
            ClientsDevices = new HashSet<ClientsDevice>();
        }
        
        public int Id { get; set; }
        public string DeviceName { get; set; } = null!;
        
        public DateTime DateToAdd { get; set; } = DateTime.Now;
        public DateTime DateToReturn { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<ClientsDevice> ClientsDevices { get; set; }
    }
}
