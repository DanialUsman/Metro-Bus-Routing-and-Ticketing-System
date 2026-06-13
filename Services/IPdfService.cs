using FirstWeb.Models;

namespace FirstWeb.Services
{
    public interface IPdfService
    {
        byte[] GenerateTicketPdf(Booking booking);
    }
}
