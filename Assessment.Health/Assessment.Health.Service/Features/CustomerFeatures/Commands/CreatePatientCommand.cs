using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.CustomerFeatures.Commands
{
    public class CreatePatientCommand : IRequest<int>
    {
        public string PatientName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// Male = 1, Female = 2
        /// </summary>
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreatePatientCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateCustomerCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
            {
                var patient = new Patient();
                patient.PatientName = request.PatientName;
                patient.ContactName = request.ContactName;
                patient.Address = request.Address;
                patient.ContactTitle = request.ContactTitle;
                patient.CreatedOn = DateTime.Now;
                patient.Gender = request.Gender;
                patient.DateOfBirth = request.DateOfBirth;
                patient.Country = request.Country;
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return patient.Id;
            }
        }
    }
}
