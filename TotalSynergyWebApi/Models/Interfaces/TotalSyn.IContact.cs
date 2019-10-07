using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalSynergyWebApi.Models.Interfaces
{
    public interface IContact
    {
        int Id { get; set; }
        string Name { get; set; }

        string Mobile { get; set; }

        string Email { get; set; }
        string ImageNumber { get; set; }

        [JsonIgnore]
        IEnumerable<IProjectContactItem> ProjectContactItems { get; set; }
    }
}
