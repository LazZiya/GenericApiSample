using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiServiceClient;
using Domain.Models;
using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Players
{
    [ValidateAntiForgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly GenericApiService _client;

        public DeleteModel(GenericApiService client)
        {
            _client = client;
            _client.ConfigureOptions(ops => { ops.TargetController = "players"; });
        }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public Player Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                TempData.Warning("Player Id can't be null!");
                return RedirectToPage("./Index");
            }

            try
            {
                Input = await _client.GetAsync<Player, string>(Id);
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
                var success = await _client.DeleteAsync<string>(Id);
                if (success)
                {
                    TempData.Success("Player deleted successfully");
                    return RedirectToPage("./Index");
                }
                else
                {
                    TempData.Warning("Player not deleted!");
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