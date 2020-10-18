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
    [Route("api/ShoppingCart/[controller]")]
    [ApiController]
    public class Ref_StockWiseImagesController : ControllerBase
    {
        private readonly IRef_StockWiseImages_interface _service;

        public Ref_StockWiseImagesController(IRef_StockWiseImages_interface service)
        {
            _service = service;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(Ref_StockWiseImagesModel data)
        {
            var response = await _service.Insert(data);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int imageId, int stockID)
        {
            var response = await _service.Select(imageId, stockID);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateData data)
        {
            var response = await _service.Update(data);
            return Ok(response);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Ref_StockWiseImagesModel data)
        {
            var response = await _service.Delete(data);
            return Ok(response);
        }
    }
}