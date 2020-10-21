using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListWeb.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "帳號為必填欄位")]
        [Display(Name = "帳號")]
        public string userAccount { get; set; }

        [Required(ErrorMessage = "密碼為必填欄位")]
        [Display(Name = "密碼")]
        [DataType(DataType.Password)]
        public string userPassword { get; set; }

        [Required(ErrorMessage = "確認密碼為必填欄位")]
        [Display(Name = "確認密碼")]
        [Compare("userPassword", ErrorMessage = "確認密碼與密碼不相符")]
        [DataType(DataType.Password)]
        public string ConfirmUserPassword { get; set; }

        [Required(ErrorMessage = "使用者名稱為必填欄位")]
        [Display(Name = "使用者名稱")]
        [MaxLength(50)]
        public string userName { get; set; }

        [Display(Name = "性別")]
        public bool sex { get; set; }

        [Required(ErrorMessage = "信箱為必填欄位")]
        [Display(Name = "信箱")]
        [EmailAddress]
        public string mail { get; set; }
    }
}
