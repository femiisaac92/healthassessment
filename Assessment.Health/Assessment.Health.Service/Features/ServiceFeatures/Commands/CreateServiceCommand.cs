using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assessment.Health.Service.Features.ServiceFeatures.Commands
{
    public class CreateServiceCommand : IRequest<int>
    {
        public int PatientId { get; set; }

        //VITALS
        public int Temp { get; set; }
        public int Bpressure { get; set; }
        public int Prate { get; set; }
        public int Rrate { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        public int Symptoms { get; set; }
        public int[] DiagnosisIds { get; set; }
        public string[] DiagnosisName { get; set; }


        public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateServiceCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
            {
                var appointment = new Appointment();                                
                appointment.PatientId = request.PatientId;
                appointment.Bpressure = request.Bpressure;
                appointment.CreatedOn = DateTime.Now;
                appointment.Height = request.Height;
                appointment.Prate = request.Prate;
                appointment.Rrate = request.Rrate;
                appointment.Weight = request.Weight;
                appointment.Temp = request.Temp;
                appointment.Symptoms = request.Symptoms;
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                if (appointment.Id > 0 && request.DiagnosisIds!=null)
                {
                    var diagnoses = new List<PatientDiagnosis>();
                    for (int i = 0; i < request.DiagnosisIds.Length; i++)
                    {
                        var id = request.DiagnosisIds[i];
                        var name = request.DiagnosisName[i];
                        var diagnosis = new PatientDiagnosis();
                        diagnosis.AppointmentId = appointment.Id;
                        diagnosis.Id = id;
                        diagnosis.DiagnosisName = name;
                        diagnoses.Add(diagnosis);
                    }
                    //Commit all
                    if(diagnoses.Count > 0)
                    {
                        _context.PatientDiagnoses.AddRange(diagnoses);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                return appointment.Id;
            }
        }
    }
}
