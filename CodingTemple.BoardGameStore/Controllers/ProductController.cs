using CodingTemple.BoardGameStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingTemple.BoardGameStore.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [OutputCache(Duration = 300)]
        public ActionResult Index(int? id)
        {
            using (var entities = new JKBoardGameStoreDataEntities())
            {
                var product = entities.Products.Find(id);
                if (product != null)
                {
                    ProductModel model = new ProductModel();
                    model.ID = product.ID;
                    model.Description = product.Description;
                    model.Name = product.Name;
                    model.Price = product.Price;
                    model.Images = product.ProductImage.Select(x => x.Path).ToArray();

                    return View(model);
                }
            }
            //using (SqlConnection connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BoardGameStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            //{
            //    connection.Open();

            //    SqlCommand command2 = connection.CreateCommand();
            //    command2.CommandText = string.Format("SELECT * FROM [ProductImage] WHERE ProductID = {0}", id);

            //    List<string> images = new List<string>();
            //    using (SqlDataReader reader2 = command2.ExecuteReader())
            //    {
            //        while (reader2.Read())
            //        {
            //            images.Add(reader2.GetString(reader2.GetOrdinal("path")));
            //        }
            //    }

            //    SqlCommand command = connection.CreateCommand();
            //    command.CommandText = string.Format("SELECT * FROM [Product] WHERE ID = {0}", id);

            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            ProductModel model = new ProductModel();
            //            model.ID = id;
            //            model.Description = reader.GetString(reader.GetOrdinal("description"));
            //            model.Price = reader.GetDecimal(reader.GetOrdinal("price"));
            //            model.Name = reader.GetString(reader.GetOrdinal("name"));
            //            model.Images = images.ToArray();
            //            return View(model);
            //        }
            //    }
            //}
            return HttpNotFound(string.Format("ID {0} Not Found", id));
        }

        // POST: Product
        [HttpPost]
        public ActionResult Index(ProductModel model)
        {

            //TODO: Collect information about the selected product
            //persist it in some sort of "Cart/Basket/ShoppingBag" in a database
            List<ProductModel> cart = this.Session["Cart"] as List<ProductModel>;
            if (cart == null)
            {
                cart = new List<ProductModel>();
            }

            cart.Add(model);

            this.Session.Add("Cart", cart);

            TempData.Add("AddedToCart", true);

            return RedirectToAction("Index", "Cart");
        }
    }
}