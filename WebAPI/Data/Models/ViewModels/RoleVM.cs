using System.ComponentModel.DataAnnotations;

namespace WebAPI.Data.Models.ViewModels
{
    public class RoleVM
    {
       [Required]
       [MaxLength(50)]
       [MinLength(3)]
       public string RoleName { get; set; }
       
       [Required]
       [MinLength(1)]
       public int[] PermissionIds { get; set; }
    }
}
