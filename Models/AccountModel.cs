using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWeb.Models
{
    public class AccountModel
    {
        public UserInfoReadDto userInfo { get; set; }

        public string AccessToken { get; set; }
    }


    public class UserInfoReadDto
    {
        public string userId { get; set; }

        public string userAccount { get; set; }

        public string userName { get; set; }

        public bool sex { get; set; }

        public string mail { get; set; }
    }
}
