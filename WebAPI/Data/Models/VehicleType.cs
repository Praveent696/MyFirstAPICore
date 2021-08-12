using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Data.Models
{
    public class VehicleType
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? UpdatedOn { get; set; }

        [ForeignKey("ParentId")]
        public VehicleType Parent { get; set; }
        public HashSet<VehicleType> Children { get; set; }
        public List<Commision> Commisions { get; set; }

        public List<InsurancePolicy> InsurancePolicies { get; set; }

    }
}
