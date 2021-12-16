using Assessment.Health.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Commands
{
    public class DeletePatientByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCustomerByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
            {
                var customer = await _context.Patients.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
                if (customer == null) return default;
                _context.Patients.Remove(customer);
                await _context.SaveChangesAsync();
                return customer.Id;
            }
        }
    }
}
