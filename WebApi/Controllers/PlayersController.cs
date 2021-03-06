﻿using WebApi.Data;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : GenericBaseController<Player, string>
    {
        public PlayersController(ApplicationDbContext context) : base(context)
        {

        }
    }
}