using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ListingDemo.API.Data;
using ListingDemo.API.Models.Country;
using AutoMapper;
using ListingDemo.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using ListingDemo.API.Exceptions;

namespace ListingDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository)
        {
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDTO>>> GetCountries()
        {
            return _mapper.Map<List<GetCountryDTO>>(await _countriesRepository.GetAllAsync());
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDTO>> GetCountry(int id)
        {
            var country = _mapper.Map<CountryDTO>(await _countriesRepository.GetDetails(id));

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO updateCountryDTO)
        {
            if (id != updateCountryDTO.Id)
            {
                throw new BadRequestException(nameof(PutCountry), id);
            }

            //Get the country detail
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            //Map the new data to the fetched data and change it's state to modified
            _mapper.Map(updateCountryDTO, country);

            try
            {
                //modified change will be saved
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    throw new NotFoundException(nameof(GetCountry), id);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDTO createCountry)
        {
            var country = _mapper.Map<Country>(createCountry);

            await _countriesRepository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countriesRepository.GetAsync(id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetCountry), id);
            }

            await _countriesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
        }
    }
}
