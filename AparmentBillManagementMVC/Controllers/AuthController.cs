using Bussiness.Abstract;
using Entity;
using Entity.DTOs;
using Entity.enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AparmentBillManagementMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITenantService tenantService;
        private readonly IManagerService managerService;

        public AuthController(ITenantService tenantService, IManagerService managerService)
        {
            this.tenantService = tenantService;
            this.managerService = managerService;
        }

        public IActionResult Index()
        {
            String? mail = Request.Cookies["userMailCookie"];
            if (mail != null)
            {
                String password = Request.Cookies["userPasswordCookie"] ?? "";
                String userType = Request.Cookies["userTypeCookie"] ?? "";
                UserType type = (userType == "tenant") ? UserType.tenant : UserType.manager;

                LoginDTO user = new LoginDTO { Mail = mail, Password = password, UserType = type, RememberMe = true };
                return View(user);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO loginDTO)
        {
            CredentialsCookieOperations(loginDTO);

            dynamic userResult = (loginDTO.UserType == UserType.manager) ?
                managerService.Login(loginDTO) :
                tenantService.Login(loginDTO);

            if (userResult.Success == false)
            {
                ModelState.AddModelError("Password", userResult.Message);
                return View(loginDTO);
            }


            string role = (loginDTO.UserType == UserType.tenant) ? "tenant" : "manager";
            string nameIdentifier = (loginDTO.UserType == UserType.tenant) ? userResult.Data.Id.ToString() : userResult.Data.ApartmentComplexId.ToString();


            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
                        new Claim(ClaimTypes.Email, loginDTO.Mail),
                       new Claim(ClaimTypes.Role, role),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);



            if (loginDTO.UserType == UserType.manager)
                return RedirectToAction("Index", "Apartment");
            else
                return RedirectToAction("Index", "Home", new { area = "TenantUser" });

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Manager manager)
        {

            if (ModelState.IsValid)
            {
                var result = managerService.AddManagerWithApartmentComplex(manager);
                if (result.Success)
                {
                    TempData["message"] = "Registration successful. You can login now.";
                    return RedirectToAction("Index");
                }

                ViewBag.error = result.Message;
            }

            return View(manager);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }

        private void CredentialsCookieOperations(LoginDTO loginDTO)
        {
            if (loginDTO.RememberMe)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Append("userMailCookie", loginDTO.Mail, options);
                Response.Cookies.Append("userPasswordCookie", loginDTO.Password, options);
                Response.Cookies.Append("userTypeCookie", loginDTO.UserType.ToString(), options);
            }
            else
            {
                Response.Cookies.Delete("userMailCookie");
                Response.Cookies.Delete("userPasswordCookie");
                Response.Cookies.Delete("userTypeCookie");
            }
        }

        public async Task<IActionResult> ManagerTestLogin()
        {

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, "1"),//ApartmentComplexId
                        new Claim(ClaimTypes.Email, "test@test.com"),
                       new Claim(ClaimTypes.Role, "manager"),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            return RedirectToAction("Index", "Apartment");
        }

        public async Task<IActionResult> TenantTestLogin()
        {

            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, "1"),//tenantId
                        new Claim(ClaimTypes.Email, "test@test.com"),
                       new Claim(ClaimTypes.Role, "tenant"),
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);

            return RedirectToAction("Index", "Home", new { area = "TenantUser" });
        }

        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }
    }
}
