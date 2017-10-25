using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class ResetPasswordByGetViewModel
    {
        [Required]
        [Phone]
        public string UserName { get; set; }
        public string Code { get; set; }
    }
}
