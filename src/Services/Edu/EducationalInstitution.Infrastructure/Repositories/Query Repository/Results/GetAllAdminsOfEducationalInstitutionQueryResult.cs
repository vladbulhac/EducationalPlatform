﻿namespace EducationalInstitution.Infrastructure.Repositories.Query_Repository.Results;

public record GetAllAdminsOfEducationalInstitutionQueryResult
{
    public ICollection<string> AdminsIDs { get; init; }
}