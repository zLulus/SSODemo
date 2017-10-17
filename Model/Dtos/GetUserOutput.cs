using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Dtos
{
    public class GetUserOutput
    {
        public bool IsExist { get; set; }
        public string UserId { get; set; }
        public UserDto User { get; set; }
    }
}
