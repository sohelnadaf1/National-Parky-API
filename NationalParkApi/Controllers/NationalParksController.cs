using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalParkApi.Models;
using NationalParkApi.Models.Dtos;
using NationalParkApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private INationalParkRepository _npRepo;

        private readonly IMapper _mapper;

        public NationalParksController(INationalParkRepository repo, IMapper mapper)
        {
            _npRepo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var data = _npRepo.GetAllNAtionalPark();

            var objDtor = new List<NationalParkDtos>();

            foreach (var item in data)
            {
                objDtor.Add(_mapper.Map<NationalParkDtos>(item));
            }
            return Ok(objDtor);
        }

        [HttpGet("{id}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int id)
        {
            var data = _npRepo.GetNationalParkById(id);
            if (data != null)
            {
                var ObjDto = _mapper.Map<NationalParkDtos>(data);
                return Ok(ObjDto);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null)
            {

                // ModelState Content all errror's
                return BadRequest(ModelState);
            }

            // if data is already exists with the same name
            if (_npRepo.NationalParkExists(nationalParkDtos.Name))
            {
                ModelState.AddModelError("", "NationalPark Exists");
                return StatusCode(404, ModelState);
            }

            // this code is not needed if you added 
            // required field inside model class

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDtos);

            if (!_npRepo.CreateNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"Somethign went wrong when saving the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            return CreatedAtRoute("GetNationalPark", new { id = nationalParkObj.Id }, nationalParkObj);



        }

        [HttpPatch("{id}",Name ="UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDtos nationalParkDtos)
        {
            if(nationalParkDtos == null || id != nationalParkDtos.Id)
            {
                return BadRequest(ModelState);
            }

            var nationalParkObj = _mapper.Map<NationalPark>(nationalParkDtos);

            if(!_npRepo.UpdateNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"somethig went wrong when updating value {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            // if everting is fine then 
            return NoContent();

        }

        [HttpDelete("{id}",Name ="DeleteNationalPark")]
        public IActionResult DeleteNationalPark(int id)
        {
            if(!_npRepo.NationalParkEXists(id))
            {
                return NotFound();
            }

            var nationalParkObj = _npRepo.GetNationalParkById(id);

            if(!_npRepo.DeleteNationlPark(nationalParkObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {nationalParkObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
