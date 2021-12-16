using Assessment.Health.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Commands
{
    public class UpdatePatientCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public class UpdateCustomerCommandHandler : IRequestHandler<UpdatePatientCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public UpdateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
            {
                var cust = _context.Patients.Where(a => a.Id == request.Id).FirstOrDefault();

                if (cust == null)
                {
                    return default;
                }
                else
                {
                    cust.PatientName = request.PatientName;
                    cust.ContactName = request.ContactName;
                    _context.Patients.Update(cust);
                    await _context.SaveChangesAsync();
                    return cust.Id;
                }
            }
        }
    }
}
