using System.ComponentModel.DataAnnotations;
using ASPNetCore.Localization.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNetCore.Localization.Pages.Contact
{
    
    public class IndexModel : BasePage
    {
        [Required, MaxLength(50), Display(Name = "Email"), BindProperty]
        public string Emaill { get; set; }


        [Required(), MaxLength(500), Display(Name = "Comment"), BindProperty]
        public string Comment { get; set; }
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                return Page();
            }

            return new RedirectToActionResult("Index", "Home", null);
        }
    }
}