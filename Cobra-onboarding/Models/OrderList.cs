using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cobra_onboarding.Models
{
    public class OrderList
    {
        public int Id { get; set; }   
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public double ProdPrice { get; set; }

    }

}