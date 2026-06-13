using FirstWeb.Data;
using FirstWeb.Models;
using FirstWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using FirstWeb.Services;

namespace FirstWeb.Controllers
{
    public class BookingController : Controller
    {
        private readonly MetroBusContext _context;
        private readonly IPdfService _pdfService;

        public BookingController(MetroBusContext context, IPdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // GET: Booking/DownloadTicket/5
        [Authorize]
        public async Task<IActionResult> DownloadTicket(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = await _context.Bookings
                .Include(b => b.Route)
                .FirstOrDefaultAsync(b => b.BookingId == id && b.UserId == userId);

            if (booking == null)
            {
                return NotFound();
            }

            try
            {
                var pdfBytes = _pdfService.GenerateTicketPdf(booking);
                string fileName = $"Ticket_{booking.BookingReference}.pdf";
                return File(pdfBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Could not generate ticket PDF. Please try again later.";
                return RedirectToAction(nameof(MyBookings));
            }
        }

        // GET: Booking/Create
        [Authorize]
        public async Task<IActionResult> Create(int? routeId)
        {
            var routes = await _context.BusRoutes
                .Where(r => r.IsActive)
                .ToListAsync();

            var viewModel = new BookingViewModel
            {
                AvailableRoutes = routes,
                TravelDate = DateTime.Today.AddDays(1)
            };

            if (routeId.HasValue)
            {
                viewModel.RouteId = routeId.Value;
                var route = routes.FirstOrDefault(r => r.RouteId == routeId.Value);
                if (route != null)
                    viewModel.FarePerPerson = route.Fare;

                viewModel.AvailableStops = await _context.BusStops
                    .Where(s => s.RouteId == routeId.Value)
                    .OrderBy(s => s.StopOrder)
                    .ToListAsync();
            }

            return View(viewModel);
        }

        // POST: Booking/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (model.TravelDate < DateTime.Today)
            {
                ModelState.AddModelError("TravelDate", "Travel date cannot be in the past.");
            }

            if (model.BoardingStop == model.DestinationStop)
            {
                ModelState.AddModelError("DestinationStop", "Destination must be different from boarding stop.");
            }

            if (ModelState.IsValid)
            {
                var route = await _context.BusRoutes.FindAsync(model.RouteId);
                if (route == null)
                    return NotFound();

                var booking = new Booking
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
                    PassengerName = model.PassengerName,
                    PassengerEmail = model.PassengerEmail,
                    PassengerPhone = model.PassengerPhone,
                    RouteId = model.RouteId,
                    BoardingStop = model.BoardingStop,
                    DestinationStop = model.DestinationStop,
                    TravelDate = model.TravelDate,
                    NumberOfPassengers = model.NumberOfPassengers,
                    TotalFare = route.Fare * model.NumberOfPassengers,
                    Status = "Confirmed",
                    BookingDate = DateTime.Now,
                    BookingReference = GenerateBookingReference()
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Confirmation), new { id = booking.BookingId });
            }

            // Reload dropdown data on validation failure
            model.AvailableRoutes = await _context.BusRoutes.Where(r => r.IsActive).ToListAsync();
            if (model.RouteId > 0)
            {
                model.AvailableStops = await _context.BusStops
                    .Where(s => s.RouteId == model.RouteId)
                    .OrderBy(s => s.StopOrder)
                    .ToListAsync();

                var route = model.AvailableRoutes.FirstOrDefault(r => r.RouteId == model.RouteId);
                if (route != null) model.FarePerPerson = route.Fare;
            }

            return View(model);
        }

        // GET: Booking/Confirmation/5
        [Authorize]
        public async Task<IActionResult> Confirmation(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = await _context.Bookings
                .Include(b => b.Route)
                .FirstOrDefaultAsync(b => b.BookingId == id && b.UserId == userId);

            if (booking == null)
                return NotFound();

            var viewModel = new BookingConfirmationViewModel
            {
                Booking = booking,
                Route = booking.Route!
            };

            return View(viewModel);
        }

        // GET: Booking/MyBookings
        [Authorize]
        public async Task<IActionResult> MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _context.Bookings
                .Include(b => b.Route)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

            return View(bookings);
        }

        // POST: Booking/Cancel/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.BookingId == id && b.UserId == userId);

            if (booking == null)
                return NotFound();

            if (booking.TravelDate <= DateTime.Today)
            {
                TempData["Error"] = "Cannot cancel a booking for today or a past date.";
                return RedirectToAction(nameof(MyBookings));
            }

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Booking #{booking.BookingReference} has been cancelled successfully.";
            return RedirectToAction(nameof(MyBookings));
        }

        private string GenerateBookingReference()
        {
            return "MTC-" + DateTime.Now.ToString("yyyyMMdd") + "-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
        }
    }
}
