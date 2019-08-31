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
    public class UpdateModel : PageModel
    {
        private readonly IApiServiceClient _client;

        public UpdateModel(IApiServiceClient client)
        {
            _client = client;
            _client.ConfigureOptions(ops => { ops.TargetController = "treasures"; });
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Treasure Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id==0)
            {
                TempData.Warning("Treasure Id can't be zero!");
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
                var success = await _client.UpdateAsync<Treasure, int>(Id, Input);
                if (success)
                {
                    TempData.Success("Treasure updated successfully");
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData.Warning("Treasure not updated!");
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