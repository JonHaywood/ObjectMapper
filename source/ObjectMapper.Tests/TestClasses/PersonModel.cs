using System;
using System.Collections.Generic;

namespace ObjectMapper.Tests.TestClasses
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool Active { get; set; }
        public Guid RowGuid { get; set; }
        public PersonData Data { get; set; }
        public double NonMatching { get; set; }
        public List<int> List { get; set; } 

        public PersonAddress Address { get; set; }
        public Guid PersonGuid { get; set; }
        public decimal? AmountPaid { get; set; }
        public DateTime BirthDate { get; set; }
    }
}