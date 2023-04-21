using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_project.Pagemodels
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
        [TempData]
        public string Message { get; set; }
        
    }
}
