using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LazZiya.TagHelpers.Alerts;
using ApiServiceClient;

namespace WebUI.Pages.Treasures
{
    [ValidateAntiForgeryToken]
    public class CreateModel : PageModel
    {
        private readonly IHttpServiceClient _client;
        public CreateModel(IHttpServiceClient client)
        {
            _client = client;
            _client.ConfigureOptions(ops => { ops.TargetController = "treasures"; });
        }

        [BindProperty]
        public Treasure Input { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData.Danger("Please recheck input fields!");
                return Page();
            }

            try
            {
                var success = await _client.AddAsync<Treasure>(Input);
                if (success)
                {
                    TempData.Success("New treasure saved!");
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData.Danger("New treasure not saved!");
                }
            }
            catch (HttpRequestException e)
            {
                TempData.Danger(e.Message);
            }

            return Page();
        }
    }
}