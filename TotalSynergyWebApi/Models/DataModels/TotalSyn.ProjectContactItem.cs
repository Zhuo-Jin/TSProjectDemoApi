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
    public class ProjectContactItem : IProjectContactItem
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set; }
        public int ProjectId { get;set; }

        [JsonIgnore]
        public IProject Project { get;set; }
        public int ContactId { get;set; }

        [JsonConverter(typeof(ConcreteConverter<Contact>))]
        public IContact Contact { get;set; }
    }
}
