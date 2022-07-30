using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListingDemo.API.Data;
using AutoMapper;
using ListingDemo.API.Contracts;
using ListingDemo.API.Models.Hotel;
using Microsoft.AspNetCore.Authorization;
using ListingDemo.API.Exceptions;
using ListingDemo.API.Models;

namespace ListingDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHotelsRepository _hotelsRepository;

        public HotelsController(IMapper mapper, IHotelsRepository hotelsRepository)
        {
            this._mapper = mapper;
            this._hotelsRepository = hotelsRepository;
        }

        // GET: api/Hotels/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetHotelDTO>>> GetHotels()
        {
            return _mapper.Map<List<GetHotelDTO>>(await _hotelsRepository.GetAllAsync());
        }

        // GET: api/Hotels?StartIndex=0&PageSize=5&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetHotelDTO>>> GetPagedHotels([FromQuery] QueryParameters queryParameters)
        {
            return await _hotelsRepository.GetAllAsync<GetHotelDTO>(queryParameters);
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            var hotel = _mapper.Map<HotelDTO>(await _hotelsRepository.GetAsync(id));

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5    
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO updateHotelDTO)
        {
            if (id != updateHotelDTO.Id)
            {
                throw new BadRequestException(nameof(PutHotel), id);
            }

            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            //Map the new data to the fetched data and change it's state to modified
            _mapper.Map(updateHotelDTO, hotel);

            try
            {
                await _hotelsRepository.UpdateAsync(hotel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelDTO>> PostHotel(CreateHotelDTO createHotelDTO)
        {
            var hotel = _mapper.Map<Hotel>(createHotelDTO);

            await _hotelsRepository.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, _mapper.Map<HotelDTO>(hotel));
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelsRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelsRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelsRepository.Exists(id);
        }
    }
}
