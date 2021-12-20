using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Shop.Data;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IEmailSender _emailSender;

        private readonly IConfiguration _configuration;


        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }

        public CartController(ApplicationDbContext db, 
                              IWebHostEnvironment webHostEnvironment,
                              IEmailSender emailSender,
                              IConfiguration configuration)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var shoppingCartList = GetCartFromSession();

            var prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            var productList = _db.Product.Where(u => prodInCart.Contains(u.Id)).ToList();

            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            // вариант
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingCartList = GetCartFromSession();

            var prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();

            ProductUserVM = new ProductUserVM()
            {
                ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId),
                ProductList = _db.Product.Where(u => prodInCart.Contains(u.Id)).ToList(),
            };

            return View(ProductUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var PathTotemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                + "templates" + Path.DirectorySeparatorChar.ToString() + "inquiry.html";

            var subject = "New Inquiry";

            var HtmlBody = string.Empty;

            using (var sr = System.IO.File.OpenText(PathTotemplate))
            {
                HtmlBody = sr.ReadToEnd();
            }

            var stringBuilder = new StringBuilder();
            foreach(var prod in ProductUserVM.ProductList)
            {
                stringBuilder.Append($" - Name: {prod.Name} <span style=\"font-size:14px;\">  [ID : {prod.Id}] </span> \n </br>");
            }

            var messageBody = string.Format(HtmlBody, ProductUserVM.ApplicationUser.FullName,
                                                      ProductUserVM.ApplicationUser.Email,
                                                      ProductUserVM.ApplicationUser.PhoneNumber,
                                                      stringBuilder.ToString());

            var MailSettings = _configuration.GetSection("Credentials").GetSection("Email").Get<MailSettings>();
            try
            {
                await _emailSender.SendEmailAsync(ProductUserVM.ApplicationUser.Email, subject, messageBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RedirectToAction(nameof(Index));
            }
            await _emailSender.SendEmailAsync(MailSettings.Login, subject, messageBody);
            HttpContext.Session.Clear();
            Thread.Sleep(1500);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var shoppingCartList = GetCartFromSession();

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u=>u.ProductId == id));

            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);

            return RedirectToAction(nameof(Index));
        }

        private List<ShoppingCart> GetCartFromSession()
        {
            var shoppingCartList = new List<ShoppingCart>();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WC.SessionCart);
            }

            return shoppingCartList;
        }
    }
}
