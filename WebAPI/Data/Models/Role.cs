using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigational Property

        public List<User> Users { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}
