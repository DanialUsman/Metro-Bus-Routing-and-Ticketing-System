using FirstWeb.Data;
using FirstWeb.Models;
using FirstWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.Controllers
{
    public class RoutesController : Controller
    {
        private readonly MetroBusContext _context;

        public RoutesController(MetroBusContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var routes = await _context.BusRoutes
                .Where(r => r.IsActive)
                .OrderBy(r => r.RouteName)
                .ToListAsync();

            return View(routes);
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var route = await _context.BusRoutes
                .FirstOrDefaultAsync(r => r.RouteId == id);

            if (route == null)
                return NotFound();

            var stops = await _context.BusStops
                .Where(s => s.RouteId == id)
                .OrderBy(s => s.StopOrder)
                .ToListAsync();

            var viewModel = new RouteDetailsViewModel
            {
                Route = route,
                Stops = stops
            };

            return View(viewModel);
        }

        // API endpoint to get stops for a route (used by booking form AJAX)
        [HttpGet]
        public async Task<IActionResult> GetStops(int routeId)
        {
            var stops = await _context.BusStops
                .Where(s => s.RouteId == routeId)
                .OrderBy(s => s.StopOrder)
                .Select(s => new { s.StopId, s.StopName, s.StopOrder, s.Area })
                .ToListAsync();

            return Json(stops);
        }

        // API endpoint to get fare for a route
        [HttpGet]
        public async Task<IActionResult> GetFare(int routeId)
        {
            var route = await _context.BusRoutes.FindAsync(routeId);
            if (route == null)
                return NotFound();

            return Json(new { fare = route.Fare });
        }
    }
}
