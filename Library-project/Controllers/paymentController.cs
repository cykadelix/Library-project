using Library_project.ViewModels.Checkout;
using Library_project.ViewModels.payment;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Library_project.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace Library_project.Controllers
{
    public class paymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> createPayment()
        {
            var mypay = new PaymentViewModel();
            await using NpgsqlConnection conn = new NpgsqlConnection("Host=127.0.0.1;Server=localhost;Port=5432;Database=library_server;UserID=postgres;Password=hatem0199;Pooling=true");
            var newCheckout2 = new CreateCheckoutViewModel();
            if (ModelState.IsValid)
            {

                await conn.OpenAsync();
                
                    await using var cmd = new NpgsqlCommand("UPDATE students SET overduefees = overduefees - overduefees WHERE library_card_number = 5", conn)
                    {
                        Parameters =
                        {
                            new("overduefees",mypay.overduefees),
                           
                        }
                    };
                    await using var reader = await cmd.ExecuteReaderAsync();
                
                    TempData["message"] = "payment is successful";
           
                
            }
            else
            {
                mypay.overduefees = -1;
            }
            return View(mypay);
        }

        

    }
}
