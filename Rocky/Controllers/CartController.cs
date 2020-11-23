
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using Rocky.Utility;
using Rocky.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        [BindProperty]
        ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard)!=null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCard);
            }
            List<int> prodInCart = shoppingCartsList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(p => prodInCart.Contains(p.Id));
            return View(prodList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            return RedirectToAction(nameof(Summary));
        }

        [HttpGet]
        public IActionResult Summary()
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCard);
            }
            List<int> prodInCart = shoppingCartsList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(p => prodInCart.Contains(p.Id));

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodList.ToList()
            };
       

            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "tamplates" + Path.DirectorySeparatorChar.ToString() +
                "Inquiry.html";

            var subject = "New Inquiry";
            string HtmlBody = "";
            using(StreamReader sr = System.IO.File.OpenText(PathToTemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }
            StringBuilder productList = new StringBuilder();
            foreach(var prod in ProductUserVM.ProductList)
            {
                productList.Append($" - Name: {prod.Name} <span style='font-size:14px;'> ID: {prod.Id}</span><br/>");
            }
            string messadgeBody = string.Format(HtmlBody,
                ProductUserVM.ApplicationUser.FullName,
                ProductUserVM.ApplicationUser.Email,
                ProductUserVM.ApplicationUser.PhoneNumber,
                productList.ToString()
                );

            await _emailSender.SendEmailAsync(WC.EmailAdmin, subject, messadgeBody);

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Payment));
        }

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentPost()
        {
            return View();
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCard);
            }

            shoppingCartsList.Remove(shoppingCartsList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WC.SessionCard, shoppingCartsList);
            
            return RedirectToAction(nameof(Index));
        }

    }
}
