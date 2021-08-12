using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Commision
    {
        [JsonIgnore]
        public int Id { get; set; }
        public float FixedPercentage { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int VehicleTypeId { get; set; }
        public int InsuranceTypeId { get; set; }
        public int InsuranceCompanyId { get; set; }

        // Navigational Properties

        public VehicleType VehicleType { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public InsuranceCompany InsuranceCompany { get; set; }

        
    }
}
