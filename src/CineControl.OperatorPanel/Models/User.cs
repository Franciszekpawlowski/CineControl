using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models
{
    public class User
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "User Name")]
        public string UserName { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}
