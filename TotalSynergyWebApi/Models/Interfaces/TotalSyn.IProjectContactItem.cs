using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TotalSynergyWebApi.Models.Interfaces
{
    public interface IProjectContactItem
    {

        int Id { get; set; }
        int ProjectId { get; set; }
        IProject Project { get; set; }
        int ContactId { get; set; }
        IContact Contact { get; set; }


    }
}
