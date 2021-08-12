using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class PaymentMode
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public List<Payment> Payments { get; set; }
    }
}
