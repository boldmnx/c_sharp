using feedback.Data;
using feedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace feedback.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.CreatedAt = DateTime.Now;
                feedback.Status = FeedbackStatus.New;
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        public IActionResult Index()
        {
            var feedbacks = _context.Feedbacks.ToList();
            Console.WriteLine($"## {feedbacks}");
            return View(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Respond(int id, string response)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                feedback.Response = response ?? string.Empty;  // Null шалгах
                feedback.Status = FeedbackStatus.Resolved;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        

    }

}
