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
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UnitController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //GET All Unit
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var units = await unitOfWork.UnitRepository.GetAll();
                var unitDtos = mapper.Map<IEnumerable<UnitDTO>>(units);
                return Ok(unitDtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing products data");
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

        //GET Unit by id
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var unit = await unitOfWork.UnitRepository.GetById(id);                
                var unitDto = mapper.Map<UnitDTO>(unit);
                return Ok(unitDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing products data");
            }
            finally
            {
                unitOfWork.Dispose();
            }
        }

    }
}
