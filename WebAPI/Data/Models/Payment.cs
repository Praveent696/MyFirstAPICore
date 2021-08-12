using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Payment
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsLatest { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int PaymentModeId { get; set; }
        public PaymentMode PaymentMode { get; set; }

        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        public int PaymentStatusId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public List<InsurancePolicy> InsurancePolicies { get; set; }
    }
}
