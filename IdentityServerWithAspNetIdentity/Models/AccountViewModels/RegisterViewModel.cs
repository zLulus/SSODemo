using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "账号")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "再次确认密码")]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致。")]
        public string ConfirmPassword { get; set; }
    }
}
