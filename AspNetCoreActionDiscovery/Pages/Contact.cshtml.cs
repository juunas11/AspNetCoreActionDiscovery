using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreActionDiscovery.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        [BindProperty]
        public TestModel Test { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public void OnPost()
        {

        }
    }
}
