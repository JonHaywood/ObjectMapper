using System;

namespace ObjectMapper.Tests.TestClasses
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public bool Active { get; set; }
        public Guid RowGuid { get; set; }
        public PersonData Data { get; set; }
        public int NonMatching { get; set; }
        public int[] List { get; set; }

        public string Address { get; set; }
        public string PersonGuid { get; set; }
        public decimal AmountPaid { get; set; }
        public string BirthDate { get; set; }        
    }
}