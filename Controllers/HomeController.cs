using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StripePaymentExample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult purchase(FormCollection form)
        {
            string token = form["stripeToken"]; // it is heart of the payment
            string email = form["stripeEmail"]; 
            StripeConfiguration.ApiKey = "Secret key"; //add your Secret key provided by stripe in API keys - for security purpose store api keys in appsettings.
            var customerOptions = new CustomerCreateOptions
            {
                // go through this  CustomerCreateOptions  for more fields
                Email = email,
                Source = token,
               
            };
            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOptions);

            var chargeOptions = new ChargeCreateOptions
            {
                // go through this  ChargeCreateOptions  for more fields
                Customer = customer.Id,
                Description = "Testing Payment",
                Amount = 200,
                Currency = "usd",
            };
            var chargeService = new ChargeService();
            Charge charge = chargeService.Create(chargeOptions);
          
            if(charge.Status=="succeeded")
            {
                // add your logic here after pyment done
                return RedirectToAction("Success");
            }
            else
            {
                // redirect payment page or add your logic
                return RedirectToAction("UnSuccess");
            }
           
        }

        public ActionResult Success()
        {
            return Content("Payment successfully done.");
        }
        public ActionResult UnSuccess()
        {
            return Content("Invalid data.");
        }
    }
}