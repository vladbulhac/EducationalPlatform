using FluentValidation;

namespace Notification.API.Hubs.DataTransferObjects.Validators;

public class GetNotificationsOfRecipientDTOValidator : AbstractValidator<GetNotificationsOfRecipientDTO>
{
    public GetNotificationsOfRecipientDTOValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(dto => dto.Offset).GreaterThanOrEqualTo(0)
                                  .WithMessage("{PropertyName} was an incorrect value! It must be a value greater or equal to 0!");

        RuleFor(dto => dto.ResultsCount).GreaterThanOrEqualTo(1)
                                      .WithMessage("{PropertyName} was an incorrect value! It must be a value greater or equal to 1!");
    }
}