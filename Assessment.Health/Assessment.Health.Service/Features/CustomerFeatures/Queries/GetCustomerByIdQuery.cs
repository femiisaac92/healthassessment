using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Queries
{
    public class GetPatientByIdQuery : IRequest<Patient>
    {
        public int Id { get; set; }
        public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient>
        {
            private readonly IApplicationDbContext _context;
            public GetPatientByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Patient> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
            {
                var customer = _context.Patients.Where(a => a.Id == request.Id).FirstOrDefault();
                if (customer == null) return null;
                return customer;
            }
        }
    }
}
