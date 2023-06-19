using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository) {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //GET: https://localhost:1234/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //map Domain models to DTOs
            var regionsDTO = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)     
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });   
            }

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
            var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            if(regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain model to Region DTO
            var regionDTO = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Return DTO
            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };

            //Use Domain Model to create region
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new {id = regionDTO.Id}, regionDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Check if region exists
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDTO.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert domain model to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //return deleted region back
            //Map domain model to dto
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDTO);
        }
    }
}
