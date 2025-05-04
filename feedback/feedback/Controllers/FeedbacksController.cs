using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using feedback.Data;
using feedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace feedback.Controllers
{

    [Authorize]
    public class FeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public FeedbacksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            var user = await _userManager.GetUserAsync(User);
            feedback.UserId = user?.Id;
            feedback.AdminReply = null;
            if (ModelState.IsValid)
            {
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyFeedbacks));
            }
            return View(feedback);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyFeedbacks()
        {
            var user = await _userManager.GetUserAsync(User);
            var feedbacks = await _context.Feedbacks
                .Where(f => f.UserId == user.Id)
                .ToListAsync();

            return View(feedbacks);
        }

        // Админ бүх санал харах
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _context.Feedbacks.Include(f => f.User).ToListAsync();
            return View(feedbacks);
        }

        // Админ саналд хариу өгөх
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reply(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();

            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reply(int id, string adminReply)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null) return NotFound();

            feedback.AdminReply = adminReply;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
