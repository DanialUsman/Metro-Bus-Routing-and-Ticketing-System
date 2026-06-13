using FirstWeb.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QRCoder;

namespace FirstWeb.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateTicketPdf(Booking booking)
        {
            try 
            {
                // Set License for Community version
                QuestPDF.Settings.License = LicenseType.Community;

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1, Unit.Inch);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(11).FontColor(Colors.Grey.Darken4));

                        // Header
                        page.Header().Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text("METRO BUS").FontSize(28).SemiBold().FontColor(Colors.Blue.Medium);
                                column.Item().Text("TWIN CITIES").FontSize(14).Medium().FontColor(Colors.Orange.Medium);
                            });

                            row.RelativeItem().AlignRight().Column(column =>
                            {
                                column.Item().Text("Booking Reference").FontSize(10).FontColor(Colors.Grey.Medium);
                                column.Item().Text(booking.BookingReference).FontSize(16).SemiBold();
                                column.Item().Text($"Status: {booking.Status}").FontSize(10).FontColor(Colors.Green.Medium);
                            });
                        });

                        // Content
                        page.Content().PaddingVertical(20).Column(column =>
                        {
                            column.Item().LineHorizontal(2).LineColor(Colors.Blue.Medium);

                            column.Item().PaddingTop(20).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("PASSENGER DETAILS").FontSize(12).SemiBold();
                                    c.Item().PaddingTop(5).Text(booking.PassengerName);
                                    c.Item().Text(booking.PassengerEmail);
                                    c.Item().Text(booking.PassengerPhone);
                                });

                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("JOURNEY INFO").FontSize(12).SemiBold();
                                    c.Item().PaddingTop(5).Text($"Route: {booking.Route?.RouteName ?? "N/A"}");
                                    c.Item().Text($"Date: {booking.TravelDate:MMM dd, yyyy}");
                                    c.Item().Text($"From: {booking.BoardingStop}");
                                    c.Item().Text($"To: {booking.DestinationStop}");
                                });
                            });

                            column.Item().PaddingTop(30).Background(Colors.Grey.Lighten4).Padding(15).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("TICKET SUMMARY").FontSize(12).SemiBold();
                                    c.Item().PaddingTop(5).Text($"{booking.NumberOfPassengers} Passengers");
                                });

                                row.RelativeItem().AlignRight().Column(c =>
                                {
                                    c.Item().Text("TOTAL FARE").FontSize(12).SemiBold();
                                    c.Item().PaddingTop(5).Text($"Rs. {booking.TotalFare}").FontSize(22).SemiBold().FontColor(Colors.Orange.Medium);
                                });
                            });

                            // QR Code and Instructions
                            column.Item().PaddingTop(40).Row(row =>
                            {
                                row.RelativeItem().Column(c =>
                                {
                                    c.Item().Text("INSTRUCTIONS").FontSize(12).SemiBold();
                                    c.Item().PaddingTop(5).Text("• Please present this ticket at the counter to get your token.");
                                    c.Item().Text("• This ticket is non-transferable.");
                                    c.Item().Text("• Keep this ticket with you during the entire journey.");
                                    c.Item().Text("• Cancellation is only possible 1 hour before travel.");
                                });

                                row.ConstantItem(100).AlignRight().Column(c =>
                                {
                                    var qrCodeData = GenerateQrCode(booking.BookingReference);
                                    c.Item().Image(qrCodeData);
                                    c.Item().AlignCenter().Text("SCAN TO VALIDATE").FontSize(7).FontColor(Colors.Grey.Medium);
                                });
                            });
                        });

                        // Footer
                        page.Footer().AlignCenter().Text(x =>
                        {
                            x.Span("Generated on: ").FontSize(9).FontColor(Colors.Grey.Medium);
                            x.Span(DateTime.Now.ToString("f")).FontSize(9);
                        });
                    });
                });

                return document.GeneratePdf();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PDF GENERATION ERROR: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        private byte[] GenerateQrCode(string reference)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeInfo = qrGenerator.CreateQrCode($"https://metrotwincities.com/verify/{reference}", QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeInfo);
            return qrCode.GetGraphic(20);
        }
    }
}
