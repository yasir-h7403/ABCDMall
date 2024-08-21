using Microsoft.AspNetCore.Mvc;
using ABCDMall.Data;
using ABCDMall.Models;
using ABCDMall.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;



namespace ABCDMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Home Page - Display mall image, features, and navigation
        public IActionResult Index()
        {
            return View();
        }

        // About Page
        public IActionResult About()
        {
            return View();
        }

        // Contact Page
        public IActionResult Contact()
        {
            return View();
        }

        // Manage Shops
        public IActionResult Shops()
        {
            var shops = _context.Shops.ToList();
            return View(shops);
        }

        // View specific Shop
        public IActionResult ShopDetails(int id)
        {
            var shop = _context.Shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        // Manage Food Courts
        public IActionResult FoodCourts()
        {
            var foodCourts = _context.FoodCourts.ToList();
            return View(foodCourts);
        }

        // View specific Food Court
        public IActionResult FoodCourtDetails(int id)
        {
            var foodCourt = _context.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return NotFound();
            }
            return View(foodCourt);
        }

        // Manage Movies
        public IActionResult Movies()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // View specific Movie
        public IActionResult MovieDetails(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // Book a Movie Ticket
        [HttpGet]
        public IActionResult BookTicket(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            var ticket = new Ticket { MovieId = id, Movie = movie };
            return View(ticket);
        }

        // Example in BookTicket POST method
        [HttpPost]
        public async Task<IActionResult> BookTicket(Ticket ticket)
        {
            // Ensure the Id is not set by the application
            ticket.Id = 0; // or ensure ticket.Id is not being set manually

            // Check if the ticket parameter itself is null
            if (ticket == null)
            {
                return BadRequest("Ticket cannot be null");
            }

            // Check if the movie associated with the ticket is valid
            var movie = await _context.Movies.FindAsync(ticket.MovieId);
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            // Perform further validation or modifications to the ticket if needed

            // Check if the ModelState is valid before saving
            if (ModelState.IsValid)
            {
                _context.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BookingConfirmation));
            }

            // If ModelState is not valid, return the view with the current ticket object
            return View(ticket);
        }

        // Booking confirmation
        public IActionResult BookingConfirmation()
        {
            return View();
        }


        // Display Feedback List
        [HttpGet]
        public IActionResult FeedbackList()
        {
            var feedbacks = _context.Feedbacks.ToList();
            return View(feedbacks);  // Ensure the view expects IEnumerable<Feedback>
        }

        // Display Feedback Submission Form
        [HttpGet]
        public IActionResult SubmitFeedback()
        {
            return View(new Feedback());  // Ensure the view expects Feedback
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FeedbackConfirmation));
            }
            return View(feedback);  // Return the same model back to the form in case of errors
        }

        // Feedback Confirmation
        public IActionResult FeedbackConfirmation()
        {
            return View();
        }
    


        // Display Gallery
        public IActionResult Gallery()
                {
                    var galleries = _context.Galleries.ToList();
                    return View(galleries);
                }

        // Search Shops and Food Courts
        public IActionResult Search(string query)
        {
            // Handle null or empty query
            if (string.IsNullOrEmpty(query))
            {
                // Return an empty view model or handle the empty query scenario
                var emptySearchResults = new SearchViewModel
                {
                    Shops = new List<Shops>(), // Initialize with empty list
                    FoodCourts = new List<FoodCourt>() // Initialize with empty list
                };
                return View(emptySearchResults);
            }

            // Ensure query is not null or empty before using it in the search
            var shopResults = _context.Shops
                .Where(s => s.Name != null && s.Name.Contains(query))
                .ToList();
            var foodCourtResults = _context.FoodCourts
                .Where(f => f.Name != null && f.Name.Contains(query))
                .ToList();

            var searchResults = new SearchViewModel
            {
                Shops = shopResults,
                FoodCourts = foodCourtResults
            };

            return View(searchResults);
        }

    }
}
