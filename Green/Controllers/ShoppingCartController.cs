using Green.Models;
using Green.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Green.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ApplicationDbContext storeDB = new ApplicationDbContext();
       
       
        // GET: /ShoppingCart/
        //[Authorize()]
        //[Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
               
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            //Success("Was Successfully Added.");
            // Return the view
            return View(viewModel);
        }
        //[Authorize(Roles = "Customer")]
        public void updateCart(string id, int qty)
        {
            var item = storeDB.Carts.Find(id);
            if (qty < 0)
                item.quantity = qty / -1;
            else if (qty == 0)
                RemoveFromCart(item.ProductID);
            else
                item.quantity = qty;
            storeDB.SaveChanges();
        }
        //
        // GET: /Store/AddToCart/5
        //[Authorize(Roles = "Customer")]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedAlbum = storeDB.Products
                .Single(album => album.ProductID == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);
            //Success("Was Successfully Added.");
            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
           // Success("Product Was Successfully Added To Your Cart.");
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        //[Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            //string albumName = storeDB.Carts.SingleOrDefault(item => item.RecordId == id).Item.ItemName;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
           
         //   Success("Product Successfully Removed from Cart");
            return RedirectToAction("Index");
            //return Json(results);

        }

        public int Plus_1(int id)
        {
            ShoppingCart s = new ShoppingCart();
           string ShoppingCartId = s.GetCartId(this.HttpContext);
            var cartItems = storeDB.Carts.SingleOrDefault(x => x.ProductID == id);
            int itemQuantity = 0;
            if (cartItems != null)
            {
                cartItems.quantity++;
                itemQuantity = cartItems.quantity;
               
                storeDB.SaveChanges();
            }
            return itemQuantity;
        }
       // [Authorize(Roles = "Customer")]
        public void RemoveItem(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.RemoveFromCart(id);

          
        }

        //
        // GET: /ShoppingCart/CartSummary
        //[Authorize(Roles = "Customer")]
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
        //[Authorize(Roles = "Customer")]
        public ActionResult ShoppingIndex()
        {
            var items = storeDB.Products.ToList();
            return View(items);
        }
        //[Authorize(Roles ="Customer")]
        public ActionResult Checkout(IList<Cart> vm)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            if (cart.GetCount() != 0)
            {
               // ViewBag.Err = "Opps... you should have atleat one cart item, please shop a few items";
                return RedirectToAction("DeliveryOption");
            }
            else
                return RedirectToAction("ShoppingIndex");


            //storeDB.Carts.ToList(;
            //return RedirectToAction("",obj);
        }
       // [Authorize(Roles = "Customer")]
        //public ActionResult PlaceOrder(string id)
        //{
            
        //    var customer = storeDB.Customers.ToList().Find(x => x.Email == HttpContext.User.Identity.Name);
        //    storeDB.Orders.Add(new Order()
        //    {
        //        Email = customer.Email,
        //        dateCraeted = DateTime.Now,
        //        shipped = false,
        //        status = "Awaiting Payment"
        //    });
        //    var email = "";
        //    if (User.Identity.IsAuthenticated)
        //    {
        //         email = Membership.GetUser().Email;
        //    }
        //    storeDB.SaveChanges();
        //    var order = storeDB.Orders.ToList()
        //        .FindAll(x => x.Email == email)
        //        .LastOrDefault();

        //    if (id == "deliver")
        //    {
        //        storeDB.OrderAddresses.Add(new OrderAddress()
        //        {
        //            OrderNo = order.OrderNo,
        //            street = Session["Street"].ToString(),
        //            city = Session["City"].ToString(),
        //            zipcode = Session["PostalCode"].ToString()
        //        });
        //        storeDB.SaveChanges();
        //    }
        //     var cart = ShoppingCart.GetCart(this.HttpContext);

        //    var items = cart.GetCartItems();

        //    foreach (var item in items)
        //    {
        //        var x = new OrderItem()
        //        {
        //            Order_ID = order.OrderNo,
        //            itemId = item.ItemNo,
        //            quantity = item.Count,
        //            price = (double)item.Item.ItemPrice
        //        };
        //        storeDB.OrderItems.Add(x);
        //        storeDB.SaveChanges();
        //    }
        //    cart.EmptyCart();
            //order tracking
            //storeDB.OrderTrackings.Add(new OrderTracking()
            //{
            //    orderNo = order.OrderNo,
            //    date = DateTime.Now,
            //    status = "Awaiting Payment",
            //    Recipient = ""
            //});
            //storeDB.SaveChanges();

            //Redirect to payment
        //    return RedirectToAction("Secure_Payment");
        //}
        public ActionResult DeliveryOption()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeliveryOption(string radio,string street, string City, string PostalCode)
        {
            if (!String.IsNullOrEmpty(radio))
            {
                if (radio.Equals("StandardDelivery"))
                {
                    Session["street"] = street;
                    Session["city"] = City;
                    Session["PostalCode"] = PostalCode;

                    return RedirectToAction("PlaceOrder", new { id = "deliver" });
                }
            }
            return View();
        }
        
        //public ActionResult Payment(int? id)
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    var order = storeDB.Orders.Find(id);
        //    ViewBag.Order = order;
        //    //ViewBag.Account = storeDB.Customers.Find(order.Email);
        //    ViewBag.Address = storeDB.OrderAddresses.ToList().Find(x => x.OrderNo == order.OrderNo);
        //    ViewBag.Items = storeDB.OrderItems.ToList().FindAll(x => x.Order_ID == order.OrderNo);
        //    ViewBag.Total = cart.GetTotal();


        //    try
        //    {
        //        string url = "<a href=" + "http://electronicshop.azurewebsites.net/ShoppingCart/Payment/" + id + " >  here" + "</a>";
        //        string table = "<br/>" +
        //                       "Items in this order<br/>" +
        //                       "<table>";
        //        table += "<tr>" +
        //                 "<th>Item</th>"
        //                 +
        //                 "<th>Quantity</th>"
        //                 +
        //                 "<th>Price</th>" +
        //                 "</tr>";
        //        foreach (var item in (List<OrderItem>)ViewBag.Items)
        //        {
        //            string itemsinoder = "<tr> " +
        //                                 "<td>" + item.Item.ItemName + " </td>" +
        //                                 "<td>" + item.quantity + " </td>" +
        //                                 "<td>R " + item.price +" </td>" +
        //                                 "<tr/>";
        //            table += itemsinoder;
        //        }

        //        table += "<tr>" +
        //                 "<th></th>"
        //                 +
        //                 "<th></th>"
        //                 +
        //                 "<th>" + cart.GetTotal().ToString("R0.00") + "</th>" +
        //                 "</tr>";
        //        table += "</table>";

                //var client = new SendGridClient("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");
                //var from = new EmailAddress("no-reply@shopifyhere.com", "Shopify Here");
                //var subject = "Order " + id + " | Awaiting Payment";
                //var to = new EmailAddress(((Customer)ViewBag.Account).Email, ((Customer)ViewBag.Account).FirstName + " " + ((Customer)ViewBag.Account).LastName);
                //var htmlContent = "Hi " + order.Customer.FirstName + ", Your order was placed, you can securely pay your order from " + url + table;
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                //var response = client.SendEmailAsync(msg);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}
        //public ActionResult DisplayOrder()
        //{
        //    return View();
        //}
        //public ActionResult Secure_Payment(int? id)
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);
        //    var order = storeDB.Orders.Find(id);
        //   // var order = new Order { OrderNo = 10,Email = User.Identity.GetUserName() };
        //    ViewBag.Order = order;
        //  //  ViewBag.Account = storeDB.Customers.Find(order.Email);
        //    ViewBag.Address = storeDB.OrderAddresses.ToList().Find(x => x.OrderNo == order.OrderNo);
        //    ViewBag.Items = storeDB.OrderItems.ToList().FindAll(x => x.Order_ID == order.OrderNo);

        //    ViewBag.Total = cart.GetTotal();


        //    string paymentMode = ConfigurationManager.AppSettings["PaymentMode"], site, merchantId, merchantKey, returnUrl;

        //    if (paymentMode == "test")
        //    {
        //        site = "https://sandbox.payfast.co.za/eng/process?";
        //        merchantId = "10010428";
        //        merchantKey = "v4fxn14sydypa";
        //    }
        //    else if (paymentMode == "live")
        //    {
        //        site = "https://www.payfast.co.za/eng/process?";
        //        merchantId = ConfigurationManager.AppSettings["PF_MerchantID"];
        //        merchantKey = ConfigurationManager.AppSettings["PF_MerchantKey"];
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Payment method unknown.");
        //    }
        //    StringBuilder str = new StringBuilder();
        //    str.Append("&merchant_id=" + HttpUtility.HtmlEncode(merchantId));
        //    str.Append("&merchant_key=" + HttpUtility.HtmlEncode(merchantKey));
        //    str.Append("&return_url=" + HttpUtility.HtmlEncode("http://electronicshop.azurewebsites.net/ShoppingCart/Index/"));
        //    str.Append("&cancel_url=" + HttpUtility.HtmlEncode("http://electronicshop-here.azurewebsites.net/ShoppingCart/Index/"));
        //    str.Append("&notify_url=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_NotifyURL"]));

        //    //string amt = totalCost;
        //    //amt = amt.Replace(",", ".");

        //    str.Append("&amount=" + HttpUtility.HtmlEncode("50000"));
        //    str.Append("&item_name=" + HttpUtility.HtmlEncode("Bomb"));
        //    str.Append("&email_confirmation=" + HttpUtility.HtmlEncode("1"));
        //    str.Append("&confirmation_address=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_ConfirmationAddress"]));

        //    return Redirect(site + str.ToString());

        //    //return Redirect(PaymentLink(cart.GetTotal()));
        //    //return Redirect(PaymentLink(cart.GetTotal().ToString(), "Order Payment | Order No: " + order.OrderNo, order.OrderNo));
        //}
        //public ActionResult Payment_Cancelled(int? id)
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    var order = storeDB.Orders.Find(id);
        //    ViewBag.Order = order;
        //    ViewBag.Account = storeDB.Customers.Find(order.Email);
        //    ViewBag.Address = storeDB.OrderAddresses.ToList().Find(x => x.OrderNo == order.OrderNo);
        //    ViewBag.Items = storeDB.OrderItems.ToList().FindAll(x => x.Order_ID == order.OrderNo);

        //    ViewBag.Total = cart.GetTotal();
        //    try
        //    {
        //        string url = "<a href=" + "http://electronicshop.azurewebsites.net/ShoppingCart/Index/" + id + " >  here" + "</a>";
        //        string table = "<br/>" +
        //                       "Items in this order<br/>" +
        //                       "<table>";
        //        table += "<tr>" +
        //                 "<th>Item</th>"
        //                 +
        //                 "<th>Quantity</th>"
        //                 +
        //                 "<th>Price</th>" +
        //                 "</tr>";
        //        foreach (var item in (List<OrderItem>)ViewBag.Items)
        //        {
        //            string items = "<tr> " +
        //                           "<td>" + item.Item.ItemName + " </td>" +
        //                           "<td>" + item.quantity + " </td>" +
        //                           "<td>R " + item.price + " </td>" +
        //                           "<tr/>";
        //            table += items;
        //        }

        //        table += "<tr>" +
        //                 "<th></th>"
        //                 +
        //                 "<th></th>"
        //                 +
        //                 "<th>" + cart.GetTotal().ToString("R0.00") + "</th>" +
        //                 "</tr>";
        //        table += "</table>";

                //        var client = new SendGridClient("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");
                //        var from = new EmailAddress("no-reply@shopifyhere.com", "Shopify Here");
                //        var subject = "Order " + id + " | Awaiting Payment";
                //        var to = new EmailAddress(order.Customer.Email, order.Customer.FirstName + " " + order.Customer.LastName);
                //        var htmlContent = "Hi " + order.Customer.FirstName + ", Your order payment process was cancelled, you can still securely pay your order from " + url + table;
                //        var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
                //        var response = client.SendEmailAsync(msg);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}
        // [Authorize(Roles ="Customer")]
        //public ActionResult Payment_Successfull(int? id)
        //{
        //    var cart = ShoppingCart.GetCart(this.HttpContext);

        //    var order = storeDB.Orders.Find(id);
        //    try
        //    {
        //        order.status = "At warehouse";

        //        //order tracking
        //        storeDB.OrderTrackings.Add(new OrderTracking()
        //        {
        //            orderNo = order.OrderNo,
        //            date = DateTime.Now,
        //            status = "Payment Recieved | Order still at warehouse",
        //            Recipient = ""
        //        });
        //        storeDB.SaveChanges();
        //        storeDB.Payments.Add(new Payment()
        //        {
        //            Date = DateTime.Now,
        //            Email = storeDB.Users.FirstOrDefault(p => p.Email == User.Identity.Name).Email,
        //            AmountPaid = (double)cart.GetTotal(),
        //            PaymentFor = "Order " + id + " Payment",
        //            PaymentMethod = "PayFast Online"
        //        });
        //        storeDB.SaveChanges();
        //        if (storeDB.OrderAddresses.Where(p => p.OrderNo == id) != null)
        //        {
        //            var expected_Date = DateTime.Now.AddDays(2);
        //            do
        //            {
        //                expected_Date = expected_Date.AddDays(1);
        //            } while (expected_Date.DayOfWeek.ToString().ToLower() == "sunday" ||
        //                expected_Date.DayOfWeek.ToString().ToLower() == "saturday");

        //            //Delivery
        //        }
        //        storeDB.SaveChanges();
        //        ViewBag.Items = storeDB.OrderItems.ToList().FindAll(x => x.Order_ID == order.OrderNo);

        //        update_Stock((int)id);

        //        string table = "<br/>" +
        //                       "Items in this order<br/>" +
        //                       "<table>";
        //        table += "<tr>" +
        //                 "<th>Item</th>"
        //                 +
        //                 "<th>Quantity</th>"
        //                 +
        //                 "<th>Price</th>" +
        //                 "</tr>";
        //        foreach (var item in (List<OrderItem>)ViewBag.Items)
        //        {
        //            string items = "<tr> " +
        //                           "<td>" + item.Item.ItemName + " </td>" +
        //                           "<td>" + item.quantity + " </td>" +
        //                           "<td>R " + item.price + " </td>" +
        //                           "<tr/>";
        //            table += items;
        //        }

        //        table += "<tr>" +
        //                 "<th></th>"
        //                 +
        //                 "<th></th>"
        //                 +
        //                 "<th>" + cart.GetTotal().ToString("R 0.00") + "</th>" +
        //                 "</tr>";
        //        table += "</table>";

               
        //         }
        //            catch (Exception ex) { }

        //    ViewBag.Order = order;
        //    ViewBag.Account = storeDB.Customers.Find(order.Email);
        //    ViewBag.Address = storeDB.OrderAddresses.ToList().Find(x => x.OrderNo == order.OrderNo);
        //    ViewBag.Total = cart.GetTotal();

        //    return View();
        //}
       // public string PaymentLink(string totalCost, string paymentSubjetc, int order_id)
       // {

       //     string paymentMode = ConfigurationManager.AppSettings["PaymentMode"], site, merchantId, merchantKey, returnUrl;

       //     if (paymentMode == "test")
       //     {
       //         site = "https://sandbox.payfast.co.za/eng/process?";
       //         merchantId = "10010428";
       //         merchantKey = "v4fxn14sydypa";
       //     }
       //     else if (paymentMode == "live")
       //     {
       //         site = "https://www.payfast.co.za/eng/process?";
       //         merchantId = ConfigurationManager.AppSettings["PF_MerchantID"];
       //         merchantKey = ConfigurationManager.AppSettings["PF_MerchantKey"];
       //     }
       //     else
       //     {
       //         throw new InvalidOperationException("Payment method unknown.");
       //     }
       //     var stringBuilder = new StringBuilder();
       //      stringBuilder.Append("&merchant_id=" + HttpUtility.HtmlEncode(merchantId));
       //     stringBuilder.Append("&merchant_key=" + HttpUtility.HtmlEncode(merchantKey));
       //     stringBuilder.Append("&return_url=" + HttpUtility.HtmlEncode("http://electronicshop.azurewebsites.net/ShoppingCart/Index/" + order_id));
       //     stringBuilder.Append("&cancel_url=" + HttpUtility.HtmlEncode("http://electronicshop.azurewebsites.net/ShoppingCart/Index/" + order_id));
       //     stringBuilder.Append("&notify_url=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_NotifyURL"]));

       //     string amt = totalCost;
       //     amt = amt.Replace(",", ".");

       //     stringBuilder.Append("&amount=" + HttpUtility.HtmlEncode(amt));
       //     stringBuilder.Append("&item_name=" + HttpUtility.HtmlEncode(paymentSubjetc));
       //     stringBuilder.Append("&email_confirmation=" + HttpUtility.HtmlEncode("1"));
       //     stringBuilder.Append("&confirmation_address=" + HttpUtility.HtmlEncode(ConfigurationManager.AppSettings["PF_ConfirmationAddress"]));

       //     return (site + stringBuilder);
       // }

       //// [Authorize(Roles ="Admin")]
       // public void update_Stock(int id)
       // {
       //     var order = storeDB.Orders.Find(id);
       //     List<OrderItem> items = storeDB.OrderItems.ToList().FindAll(x => x.Order_ID == id);
       //     foreach (var item in items)
       //     {
       //         var product = storeDB.Items.Find(item.itemId);
       //         if (product != null)
       //         {
       //             if ((product.QOH -= item.quantity) >= 0)
       //             {
       //                 product.QOH -= item.quantity;
       //             }
       //             else
       //             {
       //                 item.quantity = product.QOH;
       //                 product.QOH = 0;
       //             }
       //             try
       //             {
       //                 storeDB.SaveChanges();
       //             }
       //             catch (Exception ex) { }
       //         }
            }
        }
   