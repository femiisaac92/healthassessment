using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Queries
{
    public class GetAllPatientQuery : IRequest<IEnumerable<Patient>>
    {

        public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<Patient>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPatientQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Patient>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
            {
                var customerList = await _context.Patients.ToListAsync();
                if (customerList == null)
                {
                    return null;
                }
                return customerList.AsReadOnly();
            }
        }
    }
}
