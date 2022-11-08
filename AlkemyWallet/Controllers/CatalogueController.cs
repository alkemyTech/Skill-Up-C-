using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Models;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AlkemyWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueService _catalogueService;
        private readonly IMapper _mapper;

        public CatalogueController(ICatalogueService catalogueService, IMapper mapper)
        {
            _catalogueService = catalogueService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogue()
        {
            var catalogues = await _catalogueService.GetCatalogues();

            var catalogueForShow = _mapper.Map<IEnumerable<CatalogueDTO>>(catalogues);
            return Ok(catalogueForShow);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatalogueById(int id)
        {
            var catalogue = await _catalogueService.GetCatalogueById(id);

            if (catalogue is null)
            {
                return NotFound("No existe ningún catalogo con el id especificado");
            }


            return Ok(catalogue);
        }
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult> PostCatalogue([FromForm] CatalogueForCreationDTO catalogueDTO)
        {

            await _catalogueService.InsertCatalogue(catalogueDTO);
            return Ok("Se ha creado el Catalogo exitosamente");

        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCatalogue(int id)
        {
            var result = await _catalogueService.DeleteCatalogue(id);

            if (!result)
                return BadRequest("no se encontro el catalogo");

            return Ok("el catalogo ha sido eliminada");
        }

       // [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCatalogue(int id, [FromForm] CatalogueForUpdateDTO catalogueDTO)
        {
            var result = await _catalogueService.UpdateCatalogues(id, catalogueDTO);
            if (!result) return NotFound("Catalogo No Encontrado");
            return Ok("Catalogo Modificado con exito");

        }
    }
}