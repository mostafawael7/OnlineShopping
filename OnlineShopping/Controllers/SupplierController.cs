using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Dtos;
using OnlineShopping.Models;
using OnlineShopping.Repositories.Interfaces;

namespace OnlineShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISupplierInterface _supplierService;
        public SupplierController(ISupplierInterface supplierService, IMapper mapper)
        {
            _mapper = mapper;
            _supplierService = supplierService;
        }

        [HttpGet("getAllSuppliers")]
        public IActionResult getAllSuppliers()
        {
            var suppliers = _mapper.Map<List<SupplierDto>>(_supplierService.getAllSuppliers());
            return Ok(suppliers);
        }

        [HttpGet("getSupplierById")]
        public IActionResult getSupplierById([FromQuery]int supplierId)
        {
            if (!_supplierService.isSupplierExists(supplierId))
                return NotFound();

            var supplier = _mapper.Map<SupplierDto>(_supplierService.getSupplierById(supplierId));
            return Ok(supplier);
        }

        [HttpGet("getSupplierByName")]
        public IActionResult getSupplierByName([FromQuery] string supplierName)
        {
            //if (!_supplierService.isSupplierExists(supplierId))
            //    return NotFound();

            var supplier = _mapper.Map<SupplierDto>(_supplierService.getSupplierByName(supplierName));
            return Ok(supplier);
        }

        [HttpPost("addSupplier")]
        public IActionResult addSupplier([FromForm]SupplierDto supplier)
        {
            if (supplier == null)
                return BadRequest(ModelState);

            var s = _supplierService.getSupplierByName(supplier.CompanyName.Trim().ToLower());

            if (s != null)
            {
                ModelState.AddModelError("", "Supplier Already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplierToAdd = _mapper.Map<Supplier>(supplier);
            if (!_supplierService.addSupplier(supplierToAdd))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("updateSupplier")]
        public IActionResult updateSupplier(int supplierId, [FromForm] SupplierDto updatedSupplier)
        {
            if (updatedSupplier == null)
                return BadRequest(ModelState);

            if (supplierId != updatedSupplier.Id)
                return BadRequest(ModelState);

            if (!_supplierService.isSupplierExists(supplierId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var updated = _mapper.Map<Supplier>(updatedSupplier);
            if (!_supplierService.updateSupplier(updated))
            {
                ModelState.AddModelError("", "Something went wrong updating supplier");
                return StatusCode(500, ModelState);
            }

            return Ok(updated);
        }

        [HttpDelete("removeSupplier")]
        public IActionResult removeSupplier(int supplierId)
        {
            if (!_supplierService.isSupplierExists(supplierId))
                return NotFound();

            var supplierToDelete = _supplierService.getSupplierById(supplierId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_supplierService.removeSupplier(supplierToDelete))
            {
                ModelState.AddModelError("", "Something went wrong removing supplier");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
