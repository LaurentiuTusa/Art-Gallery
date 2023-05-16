using System.Diagnostics;
using Art_Gallery.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Art_Gallery.BLL.Services.Contracts;
using Art_Gallery.DAL.Models;
using Art_Gallery.BLL.Services;
using Microsoft.EntityFrameworkCore;
using Art_Gallery.DAL;
using Art_Gallery.DAL.ObserverDP;

namespace Art_Gallery.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductHistoryService _productHistoryService;
        private readonly IOrderService _orderService;
        private readonly IProductToCategoryService _productToCategoryService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ICategoryService categoryService, IProductService productService, IOrderService orderService, IProductToCategoryService productToCategoryService, IUserService userService, IWebHostEnvironment webHostEnvironment, IProductHistoryService productHistoryService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _productToCategoryService = productToCategoryService;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _productHistoryService = productHistoryService;
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");//Page "Login" (as cshtml) from "Access" Controller
        }

        public async Task<IActionResult> ShowCategories()
        {
            List<Category> listC = await _categoryService.GetCategories();
            return View(listC);
        }

        [HttpGet]
        public async Task<ActionResult> CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewCategory(VMCreateCategory vMCreateCategory)
        {
            try
            {
                Category cat = new Category
                {
                    ProductType = vMCreateCategory.ProductType
                };
                await _categoryService.CreateCategory(cat);

                return RedirectToAction("ShowCategories", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.completeAllFields });
            }
        }

        public async Task<IActionResult> ShowProductsByCategoryAdmin(int Id)
        {
            List<Product> listP = await _productService.GetProductsByCategory(Id);
            return View(listP);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> ShowAllProducts()
        {
            List<Product> listP = await _productService.GetProducts();// ALL
            return View(listP);
        }
        public async Task<IActionResult> AddProduct()
        {
            // Get the list of categories from your data source
            List<Category> categories = await _categoryService.GetCategories();

            // Pass the categories to the view using the ViewBag
            ViewBag.Categories = categories;

            return View();
        }


            [HttpPost]
        public async Task<IActionResult> AddProduct(VMAddProduct modelNewProduct)
        {
            try
            {
                string stringFileName = UploadFile(modelNewProduct);
                Product pr = new Product
                {
                    Title = modelNewProduct.Title,
                    Description = modelNewProduct.Description,
                    Price = modelNewProduct.Price,
                    ImgPath = stringFileName,
                };

                //Insert the product into the DB
                Product insertedprodut = await _productService.CreateProduct(pr);

                // Access the selected category IDs
                int[] selectedCategoryIds = modelNewProduct.Categories.ToArray();//Categories is te name of the list in the view

                // Iterate through the selected category IDs
                foreach (int categoryId in selectedCategoryIds)
                {
                    // Process each selected category
                    // insert into ProductToCategory table the new product id and the category id
                    ProductToCategory ptc = new ProductToCategory
                    {
                        ProductId = insertedprodut.Id,
                        TypeId = categoryId //TyeId instead of CategoryId
                    };
                    await _productToCategoryService.AddProductToCategory(ptc);
                }

                // Configure Observer pattern
                ConcreteSubject s = new ConcreteSubject();

                //Get all users
                List<User> users = await _userService.GetAllUsers();

                //Attach all observers
                foreach (User u in users)
                {
                    ConcreteObserver o = new ConcreteObserver(s, u.Email);
                    s.Attach(o);
                }
                ConcreteObserverLog obsLog = new ConcreteObserverLog(s, insertedprodut.Title);
                s.Attach(obsLog);
                
                // Change subject and notify observers
                s.SubjectState = ConstantStrings.newProductState;
                s.Notify();

                return RedirectToAction("ShowAllProducts", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.completeAllFields });
            }
        }

        private string UploadFile(VMAddProduct modelNewProduct)
        {
            string fileName = null;
            if (modelNewProduct.ImgPath != null)
            {
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, ConstantStrings.imagesPath);
                fileName = Guid.NewGuid().ToString() + "-" + modelNewProduct.ImgPath.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    modelNewProduct.ImgPath.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        //UPDATE PRODUCT
        //Push the initial product into the stack, to be albe to undo the changes

        [HttpGet]
        public async Task<ActionResult> UpdateProduct(int Id)
        {
            var product = await _productService.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }

            var vmUpdateProduct = new VMUpdateProduct
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImgPath = product.ImgPath
            };

            return View(vmUpdateProduct);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(int Id ,VMUpdateProduct vMUpdateProduct)
        {
            try
            {
                Product product = await _productService.GetProductById(Id);
                if (product == null)
                {
                    return NotFound();
                }

                //ADD it to the Stack
                Product previousState = new Product
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    ImgPath = product.ImgPath,
                    OrderId = product.OrderId
                };
                _productHistoryService.stackPush(previousState);

                //Continue to update the product
                product.Title = vMUpdateProduct.Title;
                product.Description = vMUpdateProduct.Description;
                product.Price = vMUpdateProduct.Price;
                product.ImgPath = vMUpdateProduct.ImgPath;

                await _productService.UpdateProduct(product);

                return RedirectToAction("ShowAllProducts", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.completeAllFields });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UndoUpdateProduct(/*int Id*/)
        {
            if (_productHistoryService.getCount() == 0)
            {
                // No previous state to undo
                return RedirectToAction("ShowAllProducts", "Admin");
            }

            Product previousState = _productHistoryService.stackPop();
            Product product = await _productService.GetProductById(previousState.Id);

            if (product == null)
            {
                // The product no longer exists in the database
                return NotFound();
            }

            // Detach the current product entity from the context
            _productService.DetachProduct(product);

            // Update the product with the previous state
            await _productService.UpdateProduct(previousState);

            return RedirectToAction("ShowAllProducts", "Admin");
        }

        public async Task<IActionResult> ShowOrders()
        {
            List<Order> listO = await _orderService.GetOrders();
            return View(listO);
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
