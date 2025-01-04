using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.auto_mapper;
using WebApplication1.model;
using WebApplication1.model.dto;
using WebApplication1.repository;

namespace WebApplication1.controller;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HabitController : ControllerBase
{
    private readonly IHabitsRepository _habitRepository;
    private readonly IMapper _mapper;

    public HabitController(IHabitsRepository habitRepository, IMapper mapper)
    {
        _habitRepository = habitRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Habits>>> GetHabits()
    {
        var habits = await _habitRepository.GetAllHabits();
        return Ok(habits);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Habits>> GetHabit(Guid id)
    {
        var habits = await _habitRepository.GetHabits(id);
        if (habits == null)
        {
            return NotFound();
        }

        return Ok(habits);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHabits([FromBody] HabitsDto habitsDto)
    {
        try
        {
            var validator = new HabitsDtoValidator();
            var validationResult = validator.Validate(habitsDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Validation failed.",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }
            
            Habits habit = habitsDto.Discriminator switch
            {
                "GoodHabits" => _mapper.Map<GoodHabits>(habitsDto),
                "BadHabits" => _mapper.Map<BadHabits>(habitsDto),
                _ => null
            };

            if (habit == null)
            {
                return BadRequest(new { Message = "Invalid habit type." });
            }
            
            await _habitRepository.CreateHabits(habit);

            return Ok(new { Message = "Habit created successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Message = "An error occurred while creating the habit.",
                Details = ex.InnerException?.Message ?? ex.Message
            });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHabits(Guid id, Habits habits)
    {
        if (id != habits.Id)
        {
            return BadRequest();
        }

        await _habitRepository.UpdateHabits(habits);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHabits(Guid id)
    {
        var habits = await _habitRepository.GetHabits(id);
        if (habits == null)
        {
            return NotFound();
        }

        await _habitRepository.DeleteHabits(id);
        return NoContent();
    }
}