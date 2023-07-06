using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,
            IMapper mapper) {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET: https://localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain models to DTOs
            var regionsDTO = mapper.Map<List<RegionDTO>>(regionsDomain); 

            //Return DTOs
            return Ok(regionsDTO);
        }

        //GET: https://localhost:1234/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //var region = dbContext.Regions.Find(id);

            //Get Region Domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);
            
            if(regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain model to Region DTO
            //var regionDTO = new RegionDTO()
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl
            //};

            var regionDTO = mapper.Map<RegionDTO>(regionDomain);    

            //Return DTO
            return Ok(regionDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
           
                //Map or convert DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);

                //Use Domain Model to create region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //Map Domain model back to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDTO.Id }, regionDTO);

                         
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
                        
                //Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);

                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

                //Check if region exists            
                if (regionDomainModel == null)
                {
                    return NotFound();
                }

                //Convert domain model to DTO
                var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

                return Ok(regionDTO);
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //return deleted region back
            //Map domain model to dto
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);

            return Ok(regionDTO);
        }
    }
}
