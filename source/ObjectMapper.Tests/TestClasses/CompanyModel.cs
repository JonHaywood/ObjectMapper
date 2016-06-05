using System;

namespace ObjectMapper.Tests.TestClasses
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public Guid CompanyGuid { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public PersonModel Person { get; set; }
    }
}
