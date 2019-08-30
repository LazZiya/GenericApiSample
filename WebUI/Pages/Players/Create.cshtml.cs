using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.Http;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Players
{
    public class CreateModel : PageModel
    {
        private readonly IApiServiceClient _client;
        public CreateModel(IApiServiceClient client)
        {

        }

        [BindProperty]
        public Player Player { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                _client.AddAsync<Player>(Player);
            }
        }
    }
}