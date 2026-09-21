using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.SchoolYears.CreateSchoolYear
{
    public class CreateSchoolYearHandle : IRequestHandler<CreateSchoolYearCommand, CreateSchoolYearResponse>
    {
        private readonly ISchoolYearRepository _schoolYearRepository;
        public CreateSchoolYearHandle(ISchoolYearRepository schoolYearRepository)
        {
            _schoolYearRepository = schoolYearRepository;
        }
        public async Task<CreateSchoolYearResponse> Handle(CreateSchoolYearCommand request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.CreateSchoolYearRequest.Name))
            {
                throw new ArgumentException("School year name is null");
            }
            var existingSchoolYear = _schoolYearRepository.GetSchoolYearReadModelByNameAsync(request.CreateSchoolYearRequest.Name);
            if (existingSchoolYear != null)
            {
                throw new DuplicateSchoolYearNameException();
            }
            var schoolYearModel = new SchoolYearCreateModel
            {
                Name = request.CreateSchoolYearRequest.Name,
                StartDate = request.CreateSchoolYearRequest.StartDate,
                EndDate = request.CreateSchoolYearRequest.EndDate,
                IsActive = true
            };
            var createdSchoolYear = await _schoolYearRepository.AddAsync(schoolYearModel);
            return new CreateSchoolYearResponse
            {
                Name = createdSchoolYear.Name,
                StartDate = createdSchoolYear.StartDate,
                EndDate = createdSchoolYear.EndDate,
                IsActive = createdSchoolYear.IsActive,
            };
        }
    }
}
