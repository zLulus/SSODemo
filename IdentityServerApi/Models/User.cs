using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerApi.Models
{
    public class User
    {
        //[Key]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
