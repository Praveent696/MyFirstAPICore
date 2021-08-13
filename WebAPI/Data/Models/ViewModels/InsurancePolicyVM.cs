using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models.ViewModels
{
    public class InsurancePolicyVM
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public float SumInsured { get; set; }
        public float GrossPremium { get; set; }
        public float NetPremium { get; set; }
        public float ODPremium { get; set; }
        public string BusinessType { get; set; }
        public string VehicleNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        // Navigational properties
        public int InsuranceCompanyId { get; set; }

        public int VehicleTypeId { get; set; }

        public int InsuranceTypeId { get; set; }

        public int DealerUserId { get; set; }

        public int StaffUserId { get; set; }

        public int? PaymentId { get; set; }
    }
}
