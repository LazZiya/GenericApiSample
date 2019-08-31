using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Http;
using Infrastructure.Models;
using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Treasures
{
    [ValidateAntiForgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly IApiServiceClient _client;

        public DeleteModel(IApiServiceClient client)
        {
            _client = client;
            _client.ConfigureOptions(ops => { ops.TargetController = "treasures"; });
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Treasure Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == 0)
            {
                TempData.Warning("Treasure Id can't be null!");
                return RedirectToPage("./Index");
            }

            try
            {
                Input = await _client.GetAsync<Treasure, int>(Id);
                return Page();
            }
            catch (Exception e)
            {
                TempData.Danger(e.Message);
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var success = await _client.DeleteAsync<int>(Id);
                if (success)
                {
                    TempData.Success("Treasure deleted successfully");
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData.Warning("Treasure not deleted!");
                }
            }
            catch (Exception e)
            {
                TempData.Danger(e.Message);
            }

            return Page();
        }
    }
}