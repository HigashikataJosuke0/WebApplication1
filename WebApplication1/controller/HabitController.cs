using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    //Валидация для DTO
    public class HabitsDtoValidator : AbstractValidator<HabitsDto>
    {
        public HabitsDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Discriminator).NotEmpty().WithMessage("Discriminator is required.");

            When(x => x.Discriminator == "GoodHabits",
                () => { RuleFor(x => x.Reward).NotEmpty().WithMessage("Reward is required for GoodHabits."); });

            When(x => x.Discriminator == "BadHabits",
                () => { RuleFor(x => x.Penalty).NotEmpty().WithMessage("Penalty is required for BadHabits."); });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabits([FromBody] HabitsDto habitsDto)
    {
        try
        {
            // Валидация на основе типа
            if (habitsDto.Discriminator == "GoodHabits" && string.IsNullOrEmpty(habitsDto.Reward))
            {
                return BadRequest(new { Message = "Reward is required for GoodHabits." });
            }

            if (habitsDto.Discriminator == "BadHabits" && string.IsNullOrEmpty(habitsDto.Penalty))
            {
                return BadRequest(new { Message = "Penalty is required for BadHabits." });
            }

            Habits habit;

            if (habitsDto.Discriminator == "GoodHabits")
            {
                habit = _mapper.Map<GoodHabits>(habitsDto);
            }
            else if (habitsDto.Discriminator == "BadHabits")
            {
                habit = _mapper.Map<BadHabits>(habitsDto);
            }
            else
            {
                return BadRequest("Invalid habit type.");
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