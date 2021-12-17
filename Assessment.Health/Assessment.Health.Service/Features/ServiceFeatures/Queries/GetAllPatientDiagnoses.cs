using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Assessment.Health.Service.Features.ServiceFeatures.Queries
{
    public class GetAllPatientDiagnoses : IRequest<IEnumerable<PatientDiagnosis>>
    {

        public class GetAllPatientDiagnosesHandler : IRequestHandler<GetAllPatientDiagnoses, IEnumerable<PatientDiagnosis>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPatientDiagnosesHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<PatientDiagnosis>> Handle(GetAllPatientDiagnoses request, CancellationToken cancellationToken)
            {
                var customerList = await _context.PatientDiagnoses
                                        .Include(e=>e.Appointment)
                                        .ThenInclude(f=>f.Patient)
                                        .ToListAsync();
                if (customerList == null)
                {
                    return null;
                }
                return customerList.AsReadOnly();
            }
        }
    }
}

