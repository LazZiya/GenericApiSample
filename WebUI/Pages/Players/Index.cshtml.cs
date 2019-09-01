using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiServiceClient;
using Infrastructure.Models;
using LazZiya.TagHelpers.Alerts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly IHttpServiceClient _client;

        public IndexModel(IHttpServiceClient client)
        {
            _client = client;
            _client.ConfigureOptions(ops => { ops.TargetController = "players"; });
        }

        [BindProperty(SupportsGet = true)]
        public int P { get; set; } = 1; // page no

        [BindProperty(SupportsGet = true)]
        public int S { get; set; } = 5; // page size

        public int TotalRecords { get; set; } = 0;

        public IEnumerable<Player> Items { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                (Items, TotalRecords) = await _client.GetListAsync<Player>(P, S);
            }
            catch (HttpRequestException e)
            {
                TempData.Danger(e.Message);
            }
        }
    }
}