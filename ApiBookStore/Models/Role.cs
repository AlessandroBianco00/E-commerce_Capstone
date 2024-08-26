using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaWebApp.Models.Entities
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [Required]
        [StringLength(30)]
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
