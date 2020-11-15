<<<<<<< HEAD
<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
=======
﻿using Microsoft.AspNetCore.Http;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;
using Rocky.Utility;
<<<<<<< HEAD
<<<<<<< HEAD
using Rocky.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
=======
using System;
using System.Collections.Generic;
using System.Linq;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
=======
using System;
using System.Collections.Generic;
using System.Linq;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
using System.Threading.Tasks;

namespace Rocky.Controllers
{
<<<<<<< HEAD
<<<<<<< HEAD
    [Authorize]
    public class CartController : Controller
    {
        ApplicationDbContext _db;
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
=======
    public class CartController : Controller
    {
        ApplicationDbContext _db;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
=======
    public class CartController : Controller
    {
        ApplicationDbContext _db;
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        [HttpGet]
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard) != null &&
=======
=======
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartsList = new List<ShoppingCart>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard)!=null &&
<<<<<<< HEAD
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
=======
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCard).Count() > 0)
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCard);
            }
            List<int> prodInCart = shoppingCartsList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _db.Product.Where(p => prodInCart.Contains(p.Id));
            return View(prodList);
        }

<<<<<<< HEAD
<<<<<<< HEAD
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {

            return RedirectToAction(nameof(Summary));
        }

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
                ProductList = prodList
            };
       

            return View(ProductUserVM);
        }

=======
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
=======
>>>>>>> 7810362b0766bbbe13bb0c543ceba226821bb7f3
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
