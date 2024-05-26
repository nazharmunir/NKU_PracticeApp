using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SKU_PracticeApp.Models;
using SKU_PracticeApp.Repository;

namespace SKU_PracticeApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(ProductRepository productRepository, OrderRepository orderRepository, UserManager<AppUser> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var orders = await _orderRepository.GetOrdersByUserEmailAsync(user.Email);
            return View(orders);
        }
    }
}
