using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Queries
{
    public class GetAllCustomerQuery : IRequest<IEnumerable<Patient>>
    {

        public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<Patient>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllCustomerQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<Patient>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
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
