using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWeb.ViewModels.Account
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "帳號為必填欄位")]
        [Display(Name = "帳號")]
        public string userAccount { get; set; }

        [Required(ErrorMessage = "密碼為必填欄位")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string userPassword { get; set; }
    }
}
