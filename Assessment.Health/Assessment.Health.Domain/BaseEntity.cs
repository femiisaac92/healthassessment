using System;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Health.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
