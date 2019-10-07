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
    public class Contact : IContact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set;}
        public string Name { get;set;}

        [Column(TypeName = "varchar(10)")]
        public string Mobile { get;set;}
        public string Email { get;set;}
        public string ImageNumber { get; set; }

        [JsonIgnore]
        public IEnumerable<IProjectContactItem> ProjectContactItems { get;set; }

    }
}
