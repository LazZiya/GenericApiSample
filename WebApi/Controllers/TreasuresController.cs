﻿using WebApi.Data;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreasuresController : GenericBaseController<Treasure, int>
    {
        public TreasuresController(ApplicationDbContext context) : base(context)
        {

        }
    }
}