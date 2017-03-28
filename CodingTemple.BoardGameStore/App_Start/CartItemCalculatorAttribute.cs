using CodingTemple.BoardGameStore.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CodingTemple.BoardGameStore
{
    internal class CartItemCalculatorAttribute : FilterAttribute, IActionFilter
    {
        public CartItemCalculatorAttribute()
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            List<ProductModel> cart = filterContext.HttpContext.Session["Cart"] as List<ProductModel>;
            if (cart == null)
            {
                cart = new List<ProductModel>();
            }
            filterContext.Controller.ViewBag.ItemsInCart = cart.Count;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}