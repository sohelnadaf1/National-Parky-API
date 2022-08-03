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
    [Route("api/trails")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private ITrailRepository _trRepo;
        private readonly IMapper mapper;

        public TrailsController(ITrailRepository trailRepository,IMapper mapp)
        {
            _trRepo = trailRepository;
            mapper = mapp;
        }

        [HttpGet]
        public IActionResult GetAllTrials()
        {
            var data = _trRepo.GetTrials();

            var trialObj = new List<TrialDtos>();

            foreach (var item in data)
            {
                trialObj.Add(mapper.Map<TrialDtos>(item));
            }

            return Ok(trialObj);
        }

       [HttpGet("{id}")]
       public IActionResult GetTrilas(int id)
        {
            var data = _trRepo.GetTrailById(id);
           
            if(data != null)
            {
                var dataObj = mapper.Map<TrialDtos>(data);
                return Ok(dataObj);
            }
            return NotFound();

        }


        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] TrailsCreateDtos trialDtos)
        {
            if (trialDtos == null)
            {

                // ModelState Content all errror's
                return BadRequest(ModelState);
            }

            // if data is already exists with the same name
            if (_trRepo.TrailExists(trialDtos.Name))
            {
                ModelState.AddModelError("", "Trails already Exists");
                return StatusCode(404, ModelState);
            }

            // this code is not needed if you added 
            // required field inside model class

            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var TrialObj = mapper.Map<Trial>(trialDtos);

            if (!_trRepo.CreateTrail(TrialObj))
            {
                ModelState.AddModelError("", $"Somethign went wrong when saving the record {TrialObj.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            return CreatedAtRoute("GetNationalPark", new { id = TrialObj.Id }, TrialObj);



        }

        [HttpPatch("{id}", Name = "UpdateTrials")]
        public IActionResult UpdateTrials(int id, [FromBody] TrailsUpdateDtos trialUpdateDtos)
        {
            if (trialUpdateDtos == null || id != trialUpdateDtos.Id)
            {
                return BadRequest(ModelState);
            }

            var TrialsObj = mapper.Map<Trial>(trialUpdateDtos);

            if (!_trRepo.UpdateTrailPark(TrialsObj))
            {
                ModelState.AddModelError("", $"somethig went wrong when updating value {TrialsObj.Name}");
                return StatusCode(500, ModelState);
            }

            // if everting is fine then 
            return NoContent();

        }

        [HttpDelete("{id}", Name = "DeleteTrails")]
        public IActionResult DeleteNationalPark(int id)
        {
            if (!_trRepo.TrailExists(id))
            {
                return NotFound();
            }

            var TrialsObj = _trRepo.GetTrailById(id);

            if (!_trRepo.DeleteTrail(TrialsObj))
            {
                ModelState.AddModelError("", $"something went wrong when deleting the record {TrialsObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }
    }
}
