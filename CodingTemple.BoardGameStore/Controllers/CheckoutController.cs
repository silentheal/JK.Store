using CodingTemple.BoardGameStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.BoardGameStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            CheckoutModel model = new CheckoutModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutModel model)
        {
            Order newOrder = null;
            if (ModelState.IsValid)
            {
                using (var entities = new JKBoardGameStoreDataEntities())
                {
                    AspNetUser currentUser = entities.AspNetUsers.Single(x => x.UserName == User.Identity.Name);
                    newOrder = currentUser.Orders.FirstOrDefault(x => x.Completed == null);
                    if (newOrder == null)
                    {
                        newOrder = new Order();
                        newOrder.OrderNumber = Guid.NewGuid();
                        currentUser.Orders.Add(newOrder);
                        entities.SaveChanges();
                    }
                    else
                    {
                        if (Request.Cookies.AllKeys.Contains("orderNumber"))
                        {
                            Guid orderNumber = Guid.Parse(Request.Cookies["orderNumber"].Value);
                            newOrder = entities.Orders.FirstOrDefault(x => x.Completed == null && x.OrderNumber == orderNumber);
                        }
                        if (newOrder == null)
                        {
                            newOrder = new Order();
                            newOrder.OrderNumber = Guid.NewGuid();
                            entities.Orders.Add(newOrder);
                            Response.Cookies.Add(new HttpCookie("orderNumber", newOrder.OrderNumber.ToString()));
                            entities.SaveChanges();
                        }
                    }
                    if (newOrder.OrderProduct.Sum(x => x.Quantity) == 0)
                    {
                        return RedirectToAction("Index", "Cart");
                    }

                    newOrder.PurchaserEmail = User.Identity.Name;
                    var newShippingAddress = new Address();
                    newShippingAddress.Line1 = model.ShippingAddress1;
                    newShippingAddress.Line2 = model.ShippingAddress2;
                    newShippingAddress.City = model.ShippingCity;
                    newShippingAddress.Country = model.ShippingCountry;
                    newShippingAddress.State = model.ShippingState;
                    newShippingAddress.Zip = model.ZipCode;

                    newOrder.Address1 = newShippingAddress;

                    entities.sp_CompleteOrder(newOrder.Id);
                }

                string sendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];

                var client = new SendGrid.SendGridClient(sendGridApiKey);
                var message = new SendGrid.Helpers.Mail.SendGridMessage();
                message.SetTemplateId("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");
                message.Subject = string.Format("Receipt for order {0}", newOrder.Id);
                message.From = new SendGrid.Helpers.Mail.EmailAddress("admin@JKboardGameStore.com", "Administrator");
                message.AddTo(new SendGrid.Helpers.Mail.EmailAddress(model.EmailAddress));
                var contents = new SendGrid.Helpers.Mail.Content("text/plain", "Thank you for placing an order");

                message.AddSubstitution("%order%", newOrder.Id.ToString());
                message.AddContent(contents.Type, contents.Value);

                SendGrid.Response response = await client.SendEmailAsync(message);

                //Comment this out, but leaving for reference to compare ADO.NET to Entity Framework
                //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLTEST"].ConnectionString;
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();
                //    SqlTransaction transaction = connection.BeginTransaction();
                //    try
                //    {
                //        string uniqueName = Guid.NewGuid().ToString();
                //        SqlCommand command = connection.CreateCommand();
                //        command.CommandText = string.Format("INSERT INTO Address(Line1, Line2, City, State, PostalCode, Country) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", model.ShippingAddress1, model.ShippingAddress2, model.ShippingCity, model.ShippingState, model.ZipCode, model.ShippingCountry);

                //        SqlCommand command2 = connection.CreateCommand();
                //        command2.CommandText = string.Format("");
                //        transaction.Commit();

                //        SqlCommand command3 = connection.CreateCommand();
                //        command3.CommandType = System.Data.CommandType.StoredProcedure;
                //        command3.CommandText = "sp_CompleteOrder";
                //    }
                //    catch (Exception ex)
                //    {
                //        transaction.Rollback();
                //        throw ex;
                //    }
                //    finally
                //    {
                //        connection.Close();
                //    }
                //}
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult States(string country)
        {
            using (var entities = new JKBoardGameStoreDataEntities())
            {
                var getCountry = entities.Countries.FirstOrDefault(x => x.Abbreviation == country);
                if (getCountry != null)
                {
                    return Json(getCountry.State.Select(x => new StateModel { ID = x.ID, Text = x.Name, Value = x.Abbreviation }).ToArray());
                }
                else
                {
                    return Json(new StateModel[0]);
                }
            }
        }

        [HttpPost]
        public ActionResult Countries()
        {
            using (var entities = new JKBoardGameStoreDataEntities())
            {
                return Json(entities.Countries.Select(x => new { text = x.Name, value = x.Abbreviation }).ToArray());
            }
        }
    }
}