using System.Diagnostics;
using Art_Gallery.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Art_Gallery.BLL.Services.Contracts;
using Art_Gallery.DAL.Models;
using Art_Gallery.DAL;

namespace Art_Gallery.Controllers
{
    public class UserController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IProductToCategoryService _productToCategoryService;
        private readonly IUserService _userService;

        public UserController(ICategoryService categoryService, IProductService productService, IOrderService orderService, IProductToCategoryService productToCategoryService, IUserService userService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _productToCategoryService = productToCategoryService;
            _userService = userService;
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");//Page "Login" (as cshtml) from "Access" Controller
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowFilters()
        {
            List<Category> listC = await _categoryService.GetCategories();
            return View(listC);
        }

        public async Task<IActionResult> ShowProductsByCategory(int Id)
        {
            List<Product> listP = await _productService.GetProductsByCategory(Id);
            return View(listP);
        }

        public async Task<IActionResult> ShowAllProductsUser()
        {
            List<Product> listP = await _productService.GetProducts();// ALL
            return View(listP);
        }

        public async Task<IActionResult> OrderProduct(int Id)//Id of product
        {
            // Retrieve the email from TempData
            string email = (string)TempData["Email"];

            // Keep the "Email" key-value pair in TempData
            TempData.Keep("Email");

            User user = _userService.GetUserByEmail(email);
            Product product = await _productService.GetProductById(Id);

            //ONLY IF THE PRODUCT.OrderId == null
            // Add order
            if(product.OrderId == null)
            {
                Order order = new Order
                {
                    UserId = user.Id,
                    TotalPrice = product.Price
                
                };
                await _orderService.AddOrder(order);

                //Update the orderId field from this product
                product.OrderId = order.Id;
                await _productService.UpdateProduct(product);
                return RedirectToAction("ShowAllProductsUser", "User");
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.productIsSold });
            }
        }

        public async Task<IActionResult> ShowMyOrders()
        {
            // Retrieve the email from TempData
            string email = (string)TempData["Email"];

            // Keep the "Email" key-value pair in TempData
            TempData.Keep("Email");

            User user = _userService.GetUserByEmail(email);

            List<Order> listO = await _orderService.GetMyOrdersByUserId(user.Id);
            return View(listO);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
