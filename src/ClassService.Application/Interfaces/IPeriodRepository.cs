using ClassService.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassService.Application.Interfaces
{
    public interface IPeriodRepository
    {
        Task<List<PeriodReadModel>> GetListPeriodAsync(int schoolYearId, CancellationToken cancellationToken);
    }
}
