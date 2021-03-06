using Assessment.Health.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Assessment.Health.Persistence
{
    public interface IApplicationDbContext
    {
       
        DbSet<Patient> Patients { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<PatientDiagnosis> PatientDiagnoses { get; set; }
        Task<int> SaveChangesAsync();
    }
}
