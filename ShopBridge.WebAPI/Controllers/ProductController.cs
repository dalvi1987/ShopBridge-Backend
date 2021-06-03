using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Core;
using ShopBridge.Core.DTO;
using ShopBridge.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitofWork;
        private readonly IMapper mapper;
        public ProductController(IUnitOfWork unitofWork, IMapper mapper)
        {
            this.unitofWork = unitofWork;
            this.mapper = mapper;
        }

        //GET All Product
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var products = await unitofWork.ProductRepository.GetAll();
                if (products == null)
                {
                    return NotFound("Product data not found");
                }
                var productDtos = mapper.Map<IEnumerable<ProductDTO>>(products);
                return Ok(productDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing products data");
            }
            finally
            {
                unitofWork.Dispose();
            }
        }

        //GET Product by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var product = await unitofWork.ProductRepository.GetById(id);
                if (product == null)
                {
                    return NotFound($"Product with id {id} not found");
                }
                var productDto = mapper.Map<ProductDTO>(product);
                return Ok(productDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing products data");
            }
            finally
            {
                unitofWork.Dispose();
            }
        }

        //Add Product
        [HttpPost]
        public async Task<IActionResult> Post(ProductDTO productdto)
        {
            try
            {
                if (productdto == null || !ModelState.IsValid)
                    return BadRequest();
                var product = new Product();
                product = mapper.Map<Product>(productdto);                
                product.CreatedBy = 100;
                product.CreatedDate = DateTime.Now;
                var createdProduct = await unitofWork.ProductRepository.Create(product);
                await unitofWork.SaveAsync();
                return CreatedAtAction("Get", new { id = createdProduct.Id });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating product");
            }
            finally
            {
                unitofWork.Dispose();
            }
        }

        //Delete Product
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await unitofWork.ProductRepository.GetById(id);
                if (product == null)
                {
                    return NotFound($"Product with id = {id} not found");
                }
                await unitofWork.ProductRepository.Delete(id);
                await unitofWork.SaveAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting product");
            }
            finally
            {
                unitofWork.Dispose();
            }
        }
        //Delete Product
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProductDTO productdto)
        {
            try
            {
                if (id != productdto.Id || !ModelState.IsValid)
                {
                    return BadRequest("Product ID mismatch or validation errors");
                }

                var productToUpdate = await unitofWork.ProductRepository.GetById(id);
                if (productToUpdate == null)
                {
                    return NotFound($"Product with id {id} not found");
                }
                
                productToUpdate.UpdatedBy = 100;
                productToUpdate.UpdatedDate = DateTime.Now;
                mapper.Map(productdto, productToUpdate);                
                unitofWork.ProductRepository.Update(productToUpdate);
                await unitofWork.SaveAsync();
                
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating product");
            }
            finally
            {
                unitofWork.Dispose();
            }
        }       
    }
}
