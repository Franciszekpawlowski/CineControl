using System.ComponentModel.DataAnnotations;

namespace CineControl.OperatorPanel.Models.UserLogin
{
    public class UserLoginRequest
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set;}

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
