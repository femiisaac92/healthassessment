using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.ServiceFeatures.Queries
{
   public class GetAllPatientAppointments : IRequest<IEnumerable<Appointment>>
    {

        public class GetAllPatientAppointmentsHandler : IRequestHandler<GetAllPatientAppointments, IEnumerable<Appointment>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPatientAppointmentsHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Appointment>> Handle(GetAllPatientAppointments request, CancellationToken cancellationToken)
            {
                var list = await _context.Appointments
                                //.Include(e=>e.patientDiagnoses)
                                .ToListAsync();
                if (list == null)
                {
                    return null;
                }
                return list.AsReadOnly();
            }
        }
    }
}

