using System;
using System.Collections.Generic;

namespace Assessment.Health.Domain.Entities
{
    public class Patient : BaseEntity
    {
        //public Patient()
        //{
        //    Orders = new List<Order>();
        //}
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

        //public List<Order> Orders { get; set; }
    }
}
