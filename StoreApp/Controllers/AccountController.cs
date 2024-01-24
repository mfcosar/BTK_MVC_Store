using Entities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class AccountController: Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            }) ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //user'ı okuyabilmek için _userManager inject edilir, built-in class
                IdentityUser user = await _userManager.FindByNameAsync(model.Name);

                if (user is not null) { 

                    //oturum açma işlemleri
                    await _signInManager.SignOutAsync(); //logout existing user

                if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    return Redirect(model?.ReturnUrl ?? "/");
                }
            }
            ModelState.AddModelError("Error", "Invalid username or pwd");
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl);
        }



        public IActionResult Register()
        {
            //Burası default olarak controller'ın sayfayı ilk geldiğinde yakaladığı yer..
            //İknici register ise post'tan sonra  post'la gelen datayı işlediği yer
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            
            // if  ModelState.IsValid  eklenecek
            if (ModelState.IsValid) { 

                    //kullanıcı oluştur
                    var user = new IdentityUser
                    {
                    UserName = model.UserName,
                    Email = model.Email
                    //Pasword'u açık vermiyoruz ki db'de şifreli tutulsun
                    };

                    //kullanıcıyı kaydet; şifre gelip gelmediğini kontrol et, yoksa exception veriyor
                    var result = await _userManager.CreateAsync(user, model.Password);

                    //kullanıcı oluştuysa rol bilgisini ekle
                    if (result.Succeeded)
                    {
                        var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    // yeni kayıt role olarak: User kaydedilir

                    if (roleResult.Succeeded)
                        return RedirectToAction("Login", new { ReturnUrl = "/" }); //Returnurl verilmezse Login sayfasında required field hatası verir
                    else {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                    } 
                    }
            }

            else
            {
                ModelState.AddModelError("Error", "Please enter your username, pwd and email");

                /*foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                } */
            }
            return View();
        }

        public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View();

        }
    }
}
