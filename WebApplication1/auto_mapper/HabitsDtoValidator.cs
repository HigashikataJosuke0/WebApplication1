using FluentValidation;
using WebApplication1.model.dto;

namespace WebApplication1.auto_mapper;

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