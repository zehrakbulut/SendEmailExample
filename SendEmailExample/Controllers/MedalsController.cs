using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendEmailExample.Dtos;
using SendEmailExample.Models.Tables;
using SendEmailExample.Services;

namespace SendEmailExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MedalsController : ControllerBase
	{
		private readonly IBaseService<Medal> _medalService;
		private readonly IMapper _mapper;

		public MedalsController(IBaseService<Medal> medalService, IMapper mapper)
		{
			_medalService = medalService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllMedals()
		{
			var medals = await _medalService.GetAllAsync();
			var result = _mapper.Map<List<MedalDto>>(medals);
			return Ok(result);	
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetByIdMedal(Guid id)
		{
			var medal = await _medalService.GetByIdAsync(id);
			if (medal == null)
				return NotFound(); ;

			var result = _mapper.Map<MedalDto>(medal);
			return Ok(medal);
		}

		[HttpPost]
		public async Task<IActionResult> CreateMedal([FromBody] CreateMedalDto createMedalDto)
		{
			var medal = _mapper.Map<Medal>(createMedalDto);
			medal.Id = Guid.NewGuid();

			var created = await _medalService.CreateAsync(medal);
			var result = _mapper.Map<MedalDto>(created);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMedal(Guid id, [FromBody] CreateMedalDto updateMedalDto)
		{
			var medal = _mapper.Map<Medal>(updateMedalDto);
			medal.Id = id;

			var updated = await _medalService.UpdateAsync(id, medal);
			if (updated == null)
				return NotFound();

			var result = _mapper.Map<MedalDto>(updated);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteMedal(Guid id)
		{
			var result = await _medalService.DeleteAsync(id);
			if (!result)
				return NotFound();
			return Ok(result);
		}
	}
}
