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
    public class Ref_RetailWiseImagesController : ControllerBase
    {
        private readonly IRef_RetailWiseImages_interface _service;

        public Ref_RetailWiseImagesController(IRef_RetailWiseImages_interface service)
        {
            _service = service;
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(Ref_RetailWiseImagesModel data)
        {
            var response = await _service.Insert(data);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<ActionResult> Select(int imageId, int retailID)
        {
            var response = await _service.Select(imageId, retailID);
            return Ok(response);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(UpdateData data)
        {
            var response = await _service.Update(data);
            return Ok(response);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete(Ref_RetailWiseImagesModel data)
        {
            var response = await _service.Delete(data);
            return Ok(response);
        }
    }
}