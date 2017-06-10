using Cobra_onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cobra_onboarding.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult OrderList()
        {
            using (var db = new CobraOnboardingEntities())
            {
                var customers = db.Person.ToList();
                var orderDetail = db.OrderDetails.ToList();
                var products = db.Products.ToList();
                var orderHeader = db.OrderHeaders.ToList();
                var listOfOrder = from od in orderDetail
                                  join oh in orderHeader on od.OrderId equals oh.OrderId
                                  join c in customers on oh.PersonId equals c.Id
                                  join p in products on od.ProductId equals p.Id
                                  select new {
                                      Id = od.Id,
                                      OrderId = oh.OrderId,
                                      OrderDate = oh.OrderDate,
                                      CustId = c.Id,
                                      CustName = c.Name,
                                      ProdId = p.Id,
                                      ProdName = p.Name,
                                      ProdPrice = p.Price
                                  }; 
                                                                                                                                                                                                               
                //  db.Configuration.LazyLoadingEnabled = false;
                return Json(listOfOrder, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CustomerList()
        {
            using (var db = new CobraOnboardingEntities())
            {
                var custNameList = db.Person.Select(x => new { Text = x.Name, Value = x.Id }).ToList();
               

                //  db.Configuration.LazyLoadingEnabled = false;
                return Json(custNameList, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ProductList()
        {
            using (var db = new CobraOnboardingEntities())
            {
                var prodList = db.Products.Select(x => new { Text = x.Name, Value = x.Id, Price = x.Price }).ToList();
               
                //  db.Configuration.LazyLoadingEnabled = false;
                return Json(prodList, JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult GetPrice(int pId)
        //{
        //    //int _pId = Int32.Parse(prodId);
        //    using (var db = new CobraOnboardingEntities())
        //    {
        //        var prod = db.Products.Select(x => new { Price = x.Price, Id = x.Id}).Where(x => x.Id == pId).ToList();
        //        //var price = prod;
        //        //  db.Configuration.LazyLoadingEnabled = false;
        //        return Json(prod, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public string DeleteOrder(string orderId)
        {

            if (!String.IsNullOrEmpty(orderId))
            {
                try
                {
                    int _oId = Int32.Parse(orderId);
                    using (var db = new CobraOnboardingEntities())
                    {
                        var doId = db.OrderDetails.Find(_oId);
                        db.OrderDetails.Remove(doId);
                        db.SaveChanges();
                        return "Selected customer record deleted sucessfully";
                    }
                }
                catch (Exception)
                {
                    return "Customer details not found";
                }
            }
            else
            {
                return "Fail to delete data";
            }
        }


        public string SaveOrder(OrderList orderList)
        {
            using (var db = new CobraOnboardingEntities())
            {
                if (orderList.Id != 0)
                {                   
                    var dbProd = db.OrderDetails.FirstOrDefault(x => x.Id == orderList.Id);
                    dbProd.ProductId = orderList.ProductId;                 
                    var dbOrder = db.OrderHeaders.FirstOrDefault(x => x.OrderId == orderList.OrderId);                 
                    dbOrder.PersonId = orderList.CustomerId;
                    if (orderList.OrderDate != DateTime.MinValue)
                    {
                        dbOrder.OrderDate = orderList.OrderDate;
                    }
                    var dbPrice = db.Products.FirstOrDefault(x => x.Id == orderList.ProductId);
                    dbPrice.Price = Convert.ToDecimal(orderList.ProdPrice);
                    try
                    {
                        db.SaveChanges();
                        return "success";
                    }
                    catch
                    {
                        return "fail";
                    }
                }
                else
                {
                    var newOrder = new OrderHeader
                    {
                        OrderDate = orderList.OrderDate,
                        PersonId = orderList.CustomerId
                    };
                    db.OrderHeaders.Add(newOrder);
                    int oId = newOrder.OrderId;
                    var newOrderDetail = new OrderDetail
                    {
                        OrderId = oId,
                        ProductId = orderList.ProductId
                    };
                    db.OrderDetails.Add(newOrderDetail);
                    var dbPrice = db.Products.FirstOrDefault(x => x.Id == orderList.ProductId);
                    dbPrice.Price = Convert.ToDecimal(orderList.ProdPrice);
                    try
                    {
                        db.SaveChanges();
                        return "success";
                    }
                    catch
                    {
                        return "fail";
                    }
                }
            }
        }
    }
}