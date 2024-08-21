using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ABCDMall.Data;
using ABCDMall.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ABCDMall.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // Manage Shops
        public IActionResult ManageShops()
        {
            var shops = _context.Shops.ToList();
            return View(shops);
        }

        [HttpGet]
        public IActionResult AddShop()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddShop(Shops shop)
        {
            if (ModelState.IsValid)
            {
                _context.Shops.Add(shop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageShops));
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditShop(int id)
        {
            var shop = _context.Shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        [HttpPost]
        public async Task<IActionResult> EditShop(int id, Shops shop)
        {
            if (ModelState.IsValid)
            {
                var existingShop = await _context.Shops.FindAsync(id);
                if (existingShop != null)
                {
                    existingShop.Name = shop.Name;
                    existingShop.Description = shop.Description;
                    existingShop.ImageUrl = shop.ImageUrl;
                    existingShop.UpdatedAt = DateTime.Now; // Update the timestamp

                    _context.Update(existingShop);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(ManageShops));
                }
            }
            return View(shop);
        }


        [HttpGet]
        public IActionResult DeleteShop(int id)
        {
            var shop = _context.Shops.Find(id);
            if (shop == null)
            {
                return NotFound();
            }
            return View(shop);
        }

        [HttpPost, ActionName("DeleteShop")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageShops));
        }

        // Manage Food Courts
        public IActionResult ManageFoodCourts()
        {
            var foodCourts = _context.FoodCourts.ToList();
            return View(foodCourts);
        }

        // GET: AddFoodCourt
        [HttpGet]
        public IActionResult AddFoodCourt()
        {
            return View();
        }

        // POST: AddFoodCourt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoodCourt(FoodCourt foodCourt)
        {
            if (ModelState.IsValid)
            {
                _context.FoodCourts.Add(foodCourt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageFoodCourts));
            }
            return View(foodCourt);
        }

        // GET: EditFoodCourt
        [HttpGet]
        public IActionResult EditFoodCourt(int id)
        {
            var foodCourt = _context.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return NotFound();
            }
            return View(foodCourt);
        }

        // POST: EditFoodCourt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFoodCourt(int id, FoodCourt foodCourt)
        {
            if (id != foodCourt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingFoodCourt = await _context.FoodCourts.FindAsync(id);
                if (existingFoodCourt == null)
                {
                    return NotFound();
                }

                existingFoodCourt.Name = foodCourt.Name;
                existingFoodCourt.Description = foodCourt.Description;
                existingFoodCourt.ImageUrl = foodCourt.ImageUrl;
                existingFoodCourt.MenuImageUrl = foodCourt.MenuImageUrl;
                existingFoodCourt.UpdatedAt = DateTime.Now;

                _context.FoodCourts.Update(existingFoodCourt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageFoodCourts));
            }
            return View(foodCourt);
        }

        // GET: DeleteFoodCourt
        [HttpGet]
        public IActionResult DeleteFoodCourt(int id)
        {
            var foodCourt = _context.FoodCourts.Find(id);
            if (foodCourt == null)
            {
                return NotFound();
            }
            return View(foodCourt);
        }

        // POST: DeleteFoodCourt
        [HttpPost, ActionName("DeleteFoodCourt")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFoodConfirmed(int id)
        {
            var foodCourt = await _context.FoodCourts.FindAsync(id);
            if (foodCourt == null)
            {
                return NotFound();
            }

            _context.FoodCourts.Remove(foodCourt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageFoodCourts));
        }


        // GET: ManageMovies
        public IActionResult ManageMovies()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // GET: AddMovie
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        // POST: AddMovie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageMovies));
            }
            return View(movie);
        }

        // GET: EditMovie
        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: EditMovie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageMovies));
            }
            return View(movie);
        }

        // GET: DeleteMovie
        [HttpGet]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: DeleteMovie
        [HttpPost]
        [ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovieConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ManageMovies));
        }

        // Manage Gallery
        public IActionResult ManageGallery()
        {
            var galleries = _context.Galleries.ToList();
            return View(galleries);
        }

        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _context.Galleries.Add(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageGallery));
            }
            return View(gallery);
        }

        // GET: EditImage
        [HttpGet]
        public IActionResult EditImage(int id)
        {
            var gallery = _context.Galleries.Find(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        // POST: EditImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditImage(Gallery gallery)
        {
            if (ModelState.IsValid)
            {
                _context.Galleries.Update(gallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ManageGallery));
            }
            return View(gallery);
        }


        [HttpGet]
        public IActionResult DeleteImage(int id)
        {
            var gallery = _context.Galleries.Find(id);
            if (gallery == null)
            {
                return NotFound();
            }
            return View(gallery);
        }

        [HttpPost, ActionName("DeleteImage")]
        public async Task<IActionResult> DeleteImageConfirmed(int id)
        {
            var gallery = _context.Galleries.Find(id);
            if (gallery != null)
            {
                _context.Galleries.Remove(gallery);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ManageGallery));
        }

        // View Feedback
        public IActionResult ViewFeedback()
        {
            var feedback = _context.Feedbacks.ToList();
            return View(feedback);
        }

        // View Booked Tickets
        public IActionResult ViewBookedTickets()
        {
            var tickets = _context.Tickets.ToList();
            return View(tickets);
        }

        // Admin Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Users model)
        {
            if (ModelState.IsValid)
            {
                // Simple authentication logic with hardcoded credentials
                if (model.Username == "admin" && model.Password == "password")
                {
                    // Create the identity and sign in
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // Keep the user signed in
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction(nameof(Index)); // Redirect to the admin dashboard
                }

                // Invalid login attempt
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        //[Authorize]
        //public IActionResult Index()
        //{
        //    return View("Dashboard");
        //}

    }
}
