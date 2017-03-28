using CodingTemple.BoardGameStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CodingTemple.BoardGameStore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (var entities = new IdentityModels())
                {
                    var userStore = new UserStore<User>(entities);
                    var manager = new UserManager<User>(userStore);
                    manager.UserTokenProvider = new EmailTokenProvider<User>();

                    var user = new User()
                    {
                        UserName = model.EmailAddress,
                        Email = model.EmailAddress,
                        //EmailConfirmed = true
                    };

                    IdentityResult result = manager.Create(user, model.Password);

                    if (result.Succeeded)
                    {
                        FormsAuthentication.SetAuthCookie(model.EmailAddress, true);

                        //Added
                        User u = manager.FindByName(model.EmailAddress);

                        //Create a customer record in braintree
                        /*
                        string mechantId = ConfigurationManager.AppSettings["Braintree.MerchantID"];
                        string publicKey = ConfigurationManager.AppSettings["Braintree.PublicKey"];
                        string privateKey = ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                        string environment = ConfigurationManager.AppSettings["Braintree.environment"];
                        var brainTree = new Braintree.BraintreeGateway(environment, mechantId, publicKey, privateKey);
                        var customer = new Braintree.CustomerRequest();
                        customer.CustomerId = u.Id;
                        customer.Email = u.Email;
                        */

                        string confirmationToken = manager.GenerateEmailConfirmationToken(u.Id);

                        //change password
                        //manager.ResetPassword(u.Id, u.PasswordHash);
                        //string PasswordToken = manager.GeneratePasswordResetToken(u.PasswordHash);


                        string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                        var client = new SendGrid.SendGridClient(sendGridApiKey);
                        var message = new SendGrid.Helpers.Mail.SendGridMessage();
                        message.Subject = string.Format("Please confirm your Account");
                        message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@JKboardGames.com", "JK board game Admin");
                        message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.EmailAddress));
                        var contents = new SendGrid.Helpers.Mail.Content("text/html", String.Format("<a href=\'{0}\'>Confirm Account</a>", Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/Confirm/" + confirmationToken + "?email=" + model.EmailAddress));

                        message.AddContent(contents.Type, contents.Value);
                        SendGrid.Response response = await client.SendEmailAsync(message);

                        return RedirectToAction("index", "ConfirmSent");
                    }   
                    else
                    {
                        ModelState.AddModelError("EmailAddress", "Unable to register with this email address");
                    }
                }
            }
            return View(model);
        }

        public ActionResult ConfirmSent()
        {
            return View();
        }

        public ActionResult Confirm(string id, string email)
        {
            using (var entities = new JKBoardGameStoreDataEntities())
            {
                var userStore = new UserStore<User>(entities);
                var manager = new UserManager<User>(userStore);
                manager.UserTokenProvider = new EmailTokenProvider<User>();

                var user = manager.FindByName(email);
                if (user != null)
                {
                    var result = manager.ConfirmEmail(user.Id, id);
                    if (result.Succeeded)
                    {
                        TempData.Add("AccountConfirmed", true);
                        return RedirectToAction("Login");
                    }
                }
            }
            return RedirectToAction("index", "home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var entities = new IdentityModels())
                {
                    try
                    {
                        var userStore = new UserStore<User>(entities);
                        var mananger = new UserManager<User>(userStore);
                        var user = mananger.FindByEmail(model.EmailAddress);
                        if (mananger.CheckPassword(user, model.Password))
                        {
                            FormsAuthentication.SetAuthCookie(model.EmailAddress, true);
                            return RedirectToAction("Index", "Home");
                        }
                        ModelState.AddModelError("EmailAddress", "Could not sign in with this name and/or password");
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                    
                }
            }
            return View(model);
        }
    }
}