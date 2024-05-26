using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SKU_PracticeApp.Models;
using SKU_PracticeApp.Repository;
using SKU_PracticeApp.ViewModels;

namespace SKU_PracticeApp.Controllers
{
    [Authorize]
    public class ShoppingController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly OrderRepository _orderRepository;
        private readonly UserManager<AppUser> _userManager;

        public ShoppingController(ProductRepository productRepository, OrderRepository orderRepository, UserManager<AppUser> userManager)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid || model.Items == null || !model.Items.Any())
            {
                return BadRequest(new { success = false, message = "Invalid order data." });
            }

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                OrderDetails = model.Items.Select(item => new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                }).ToList()
            };

            await _orderRepository.CreateOrderAsync(order);
            return Ok(new { success = true, orderId = order.Id });
        }
    }

}
