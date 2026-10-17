using AutoMapper;
using ClassService.Application.Classes.Specifications;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Common.Specification;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
using ClassService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClasses
{
    public class GetClassesHandle : IRequestHandler<GetClassesQuery, PageResult<GetClassesResponse>>
    {
        private readonly IClassRepository classRepository;
        private readonly IMapper _mapper;
        private const int MaxPageSize = 20;
        private const int MaxPageNumber = 500;
        public GetClassesHandle(IClassRepository classRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            _mapper = mapper;
        }

        public async Task<PageResult<GetClassesResponse>> Handle(GetClassesQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 0 || request.PageSize < 0)
            {
                throw new ArgumentException("PageNumber or PageSize is negative number ");
            }
            if (request.PageNumber == 0 || request.PageSize == 0)
            {
                request.PageNumber = request.PageNumber > 0? request.PageNumber: 1;
                request.PageSize = request.PageSize  > 0 ? request.PageSize : MaxPageSize;
            }

            if (request.PageNumber > MaxPageNumber)
            {
                throw new ArgumentException($"Page Number must be smaller or equal than {MaxPageNumber}");
            }
            if (request.PageSize > MaxPageSize)
            {
                throw new ArgumentException($"Page Size must be smaller or equal than {MaxPageSize}");
            }

            var specs = new List<ISpecification<Class>>
            {
                new ClassActiveBySchoolYearIdAndSchoolIdSpec(request.SchoolYearId, request.SchoolId)
            };
            if (!string.IsNullOrWhiteSpace(request.ClassName))
            {
                specs.Add(new ClassNameContainsSpec(request.ClassName));
            }
            var (items, totalCount) = await classRepository.GetListAsync(specs, request.PageNumber, request.PageSize, cancellationToken);

            return new PageResult<GetClassesResponse>
            {
                Items = _mapper.Map<List<GetClassesResponse>>(items),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
            };
        }
    }
}
