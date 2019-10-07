using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TotalSynergyWebApi.Models.Interfaces
{
    public interface IProject
    {
        
        int Id { get; set; }
        string ProjectName { get; set; }
        string ProjectDescription { get; set; }
        DateTime CreateTime { get; set; }
        DateTime CloseTime { get; set; }

        string ImageNumber { get; set; }

        IEnumerable<IProjectContactItem> ProjectContactItems { get; set; }
    }
}
