using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Http;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LazZiya.TagHelpers.Alerts;

namespace WebUI.Pages.Treasures
{
    [ValidateAntiForgeryToken]
    public class CreateModel : PageModel
    {
        private readonly IApiServiceClient _client;
        public CreateModel(IApiServiceClient client)
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