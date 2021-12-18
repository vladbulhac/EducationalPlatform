using FluentValidation;

namespace Notification.API.Hubs.DataTransferObjects.Validators;

public class SeenNotificationsDTOValidator : AbstractValidator<SeenNotificationsDTO>
{
    public SeenNotificationsDTOValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(dto => dto.EventsIds).NotEmpty()
                                     .WithMessage("{PropertyName} was empty or null!");

        RuleForEach(dto => dto.EventsIds).NotEmpty()
                                         .WithMessage("An id was empty or null!");
    }
}