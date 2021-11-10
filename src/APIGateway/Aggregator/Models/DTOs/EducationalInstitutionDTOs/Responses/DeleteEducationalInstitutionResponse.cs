namespace Aggregator.Models.DTOs.EducationalInstitutionDTOs.Responses;

public record DeleteEducationalInstitutionResponse
{
    public DateTime DateForPermanentDeletion { get; init; }
}