using AutoMapper;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Classes.GetClasses
{
    public class GetClassesHandle : IRequestHandler<GetClassesQuery, PageResult<GetClassesResponse>>
    {
        private readonly IClassRepository classRepository;
        private readonly IMapper _mapper;
        public GetClassesHandle(IClassRepository classRepository, IMapper mapper)
        {
            this.classRepository = classRepository;
            _mapper = mapper;
        }
        async Task<PageResult<GetClassesResponse>> IRequestHandler<GetClassesQuery, PageResult<GetClassesResponse>>.Handle(GetClassesQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber <= 0 || request.PageSize <= 0)
            {
                throw new ArgumentException("Page Number and Page Size must be greater than 0");
            }
            var asOfId = request.AsOfId ?? await classRepository.GetMaxClassIdAsync(request.SchoolYearId);
           

            var (items, totalCount) = await classRepository.GetListAsync(asOfId, request.SchoolYearId, request.PageNumber, request.PageSize);

            return new PageResult<GetClassesResponse>
            {
                Items = _mapper.Map<List<GetClassesResponse>>(items),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                AsOfId = asOfId
            };
        }
    }
}
