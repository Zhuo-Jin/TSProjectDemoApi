using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models.Interfaces;

namespace TotalSynergyWebApi.Models.DataModels
{
    public class Project : IProject
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }
        public string ProjectName { get;set; }
        public string ProjectDescription { get;set; }
        public DateTime CreateTime { get;set; }
        public DateTime CloseTime { get;set; }
        public string ImageNumber { get; set; }


        [JsonConverter(typeof(ConcreteConverter<IEnumerable<ProjectContactItem>>))]
        public IEnumerable<IProjectContactItem> ProjectContactItems { get;set;}
    }
}
