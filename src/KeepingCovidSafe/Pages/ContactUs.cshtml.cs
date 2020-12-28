using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KeepingCovidSafe.Pages
{
    public class ContactUsModel : PageModel
    {
        private readonly IEmailSender emailSender;

        public ContactUsModel(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [BindProperty]
        public ContactUsDto ContactUsDto { get; set; }


        public IActionResult OnGet()
        {
            ContactUsDto = new ContactUsDto { EmailAddress = User.Identity.Name };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await emailSender.SendEmailAsync("tysonbrown@live.com.au", "KeepingCovidSafe Enquiry", ContactUsDto.EmailAddress + " | " + ContactUsDto.Message);

            return RedirectToPage("/Index");
        }
    }
}
