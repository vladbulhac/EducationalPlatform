using Aggregator.Models.DTOs;
using EducationaInstitutionAPI.Utils;
using System;
using System.Threading.Tasks;

namespace Aggregator.Services
{
    public interface IEducationalInstitutionService
    {
        public Task<Response<Guid>> CreateEducationalInstiutionAsync(DTOCreateEducationalInstitution request);
    }
}