using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TagTeam.ShoppingCart.Domain;
using TagTeam.ShoppingCart.Domain.CustomModels;
using TagTeam.ShoppingCart.Service.Interfaces;

namespace TagTeam.ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ref_RetailController : ControllerBase
    {

        private readonly IRef_Retail_interface _service;

        public Ref_RetailController(IRef_Retail_interface service)
        {
            _service = service;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(Ref_RetailModel data)
        {
            var response = await _service.Insert(data);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int retailID)
        {
            var response = await _service.Select(retailID);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateData data)
        {
            var response = await _service.Update(data);
            return Ok(response);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Ref_RetailModel data)
        {
            var response = await _service.Delete(data);
            return Ok(response);
        }
    }
}