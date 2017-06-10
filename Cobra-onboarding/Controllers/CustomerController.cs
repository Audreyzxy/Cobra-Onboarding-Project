using Cobra_onboarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cobra_onboarding.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CustomerList()
        {
            using (var db = new CobraOnboardingEntities())
            {
                var listOfCustomer = db.Person.Select(x => new CustomerListVM()
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Address1 = x.Address1,
                    Address2 = x.Address2

                }).ToList();// new List<CustomerListVM>();
                
                //  db.Configuration.LazyLoadingEnabled = false;
                return Json(listOfCustomer, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCustomerById(string customerId)
        {
            using (var db = new CobraOnboardingEntities())
            {
                var Id = Convert.ToInt32(customerId);
                var getCustomerById = db.Person.FirstOrDefault(x => x.Id == Id);
                return Json(getCustomerById, JsonRequestBehavior.AllowGet);
            }
        }

        public string DeleteCustomer(string customerId)
        {

            if (!String.IsNullOrEmpty(customerId))
            {
                try
                {
                    int _cusId = Int32.Parse(customerId);
                    using (var db = new CobraOnboardingEntities())
                    {
                        var dcusId = db.Person.Find(_cusId);
                        var dorderId = db.OrderHeaders.Where(x => x.PersonId == dcusId.Id).ToList();
                        foreach (var order in dorderId)
                        {
                            var doId = db.OrderDetails.Where(x => x.OrderId == order.OrderId).ToList();
                            foreach (var oId in doId)
                            {
                                db.OrderDetails.Remove(oId);
                                db.SaveChanges();
                            }
                            db.OrderHeaders.Remove(order);
                            db.SaveChanges();
                        }

                        db.Person.Remove(dcusId);
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


        public string SaveCustomer(Person customer)
        {
            using (var db = new CobraOnboardingEntities())
            {
               if(customer.Id != 0){

                    var dbCustomer = db.Person.FirstOrDefault(x => x.Id == customer.Id);
                    dbCustomer.Name = customer.Name;
                    dbCustomer.Address1 = customer.Address1;
                    dbCustomer.Address2 = customer.Address2;
                    dbCustomer.City = customer.City;
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
                    db.Person.Add(customer);
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

        public  Person NewCustomer()
        {
            return new Person();
        }

        //public string UpdateCustomer(Person cus)
        //{
        //    if (cus != null)
        //    {
        //        using (var db = new CobraOnboardingEntities())
        //        {
        //            int cusId = Convert.ToInt32(cus.Id);
        //            Person _cus = db.People.Where(x => x.Id == cusId).FirstOrDefault();
        //            _cus.Name = cus.Name;
        //            _cus.Address1 = cus.Address1;
        //            _cus.Address2 = cus.Address2;
        //            _cus.Town_City = cus.Town_City;
        //            db.SaveChanges();
        //            return "Customer record updated successfully";
        //        }
        //    }
        //    else
        //    {
        //        return "Invalid customer record";
        //    }
        //}

        //public string AddCustomer(Person cus)
        //{
        //    if (cus != null)
        //    {
        //        using (var db = new CobraOnboardingEntities())
        //        {
        //            db.People.Add(cus);
        //            db.SaveChanges();
        //            return "Customer record added successfully";
        //        }
        //    }
        //    else
        //    {
        //        return "Invalid customer record";
        //    }
        //}
    }

}