using FluentValidation;

namespace Notification.API.Hubs.DataTransferObjects.Validators;

public class GetNotificationDTOValidator : AbstractValidator<GetNotificationDTO>
{
    public GetNotificationDTOValidator()
    {
        CascadeMode = CascadeMode.Stop;
        RuleFor(dto => dto.EventId).NotEmpty()
                                   .WithMessage("{PropertyName} was empty or null!");
    }
}