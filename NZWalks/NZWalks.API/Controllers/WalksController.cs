using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //Map DTO to Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            //Map Domain model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomainModel = await walkRepository.GetAllAsync();

            //Map Domain model to DTO
            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if(walkDomain == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var walkDTO = mapper.Map<WalkDTO>(walkDomain);

            return Ok(walkDTO);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Map  DTO to Domain model
            var walkDomain = mapper.Map<Walk>(updateWalkRequestDTO);

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null)
            {
                return NotFound();
            }

            //Map Domain to DTO

            var walkDTO = mapper.Map<WalkDTO>(walkDomain);

            return Ok(walkDTO);


        }
        

    }
}
