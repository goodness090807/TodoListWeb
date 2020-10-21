using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListWeb.Shared;
using Microsoft.AspNetCore.Mvc;
using TodoListWeb.Models;
using TodoListWeb.ViewModels.Account;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TodoListWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ApiRequest apiRequest = new ApiRequest();

            AccountModel accountModel = apiRequest.GetAccount(loginViewModel);

            if (accountModel == null)
            {
                ModelState.AddModelError("", "帳號或密碼錯誤!");
                return View();
            }


            #region 登入使用者
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, accountModel.userInfo.userId),
                new Claim("UserName", accountModel.userInfo.userName),
                new Claim("Token", accountModel.AccessToken),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //登入使用者
            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            //防止跨網域攻擊
            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            #endregion


            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ApiRequest apiRequest = new ApiRequest();

            AccountModel accountModel = apiRequest.RegisterAccount(registerViewModel);

            if (accountModel == null)
            {
                ModelState.AddModelError("", "資料內容發生錯誤!");
                return View();
            }

            return RedirectToAction("RegisterSuccess");
        }

        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");//導至登入頁
        }
    }
}
