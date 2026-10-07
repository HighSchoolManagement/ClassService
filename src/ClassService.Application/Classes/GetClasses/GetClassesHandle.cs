using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Application.Models;
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
        async Task<PageResult<GetClassesResponse>> IRequestHandler<GetClassesQuery, PageResult<GetClassesResponse>>.Handle(GetClassesQuery request, CancellationToken cancellationToken)
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
            var (items, totalCount) = await classRepository.GetListAsync(request.SchoolYearId, request.SchoolId, request.PageNumber, request.PageSize, cancellationToken);

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
