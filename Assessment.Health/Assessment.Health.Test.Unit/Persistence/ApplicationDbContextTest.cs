using Assessment.Health.Domain.Entities;
using Assessment.Health.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Assessment.Health.Test.Unit.Persistence
{
    public class ApplicationDbContextTest
    {
        [Test]
        public void CanInsertCustomerIntoDatabasee()
        {

            using var context = new ApplicationDbContext();
            var customer = new Patient();
            context.Patients.Add(customer);
            Assert.AreEqual(EntityState.Added, context.Entry(customer).State);
        }
    }
}
