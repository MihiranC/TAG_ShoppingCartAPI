﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.ShoppingCart.Service.Interfaces;

namespace TagTeam.ShoppingCart.API.Controllers
{
    [Route("api/TGShoppingCart/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITest_Interface _service;

        public TestController(ITest_Interface service)
        {
            _service = service;
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select()
        {
            var response = await _service.Select();
            return Ok(response);
        }
    }
}