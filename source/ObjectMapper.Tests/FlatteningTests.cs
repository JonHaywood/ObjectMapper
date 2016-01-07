using ObjectMapper.MappingRules;
using ObjectMapper.Tests.TestClasses;
using Xunit;

namespace ObjectMapper.Tests
{
    public class FlatteningTests
    {
        private const string Category = "Mapper Tests - Flattening Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapNormalProperty()
        {
            // arrange
            var source = new CompanyModel
            {
                CompanyName = "Test Company"
            };
            Mapper.WithRule(new Flattening());

            // act
            var result = Mapper.Map<CompanyDto>(source);

            // assert                
            Assert.Equal(source.CompanyName, result.CompanyName);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapOneLevelFlattedProperty()
        {
            // arrange
            var source = new CompanyModel
            {
                Person = new PersonModel
                {
                    Name = "Test Name",
                    Active = true
                }
            };
            Mapper.WithRule(new Flattening());

            // act
            var result = Mapper.Map<CompanyDto>(source);

            // assert                
            Assert.Equal(source.Person.Name, result.PersonName);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapMultipleLevelsFlattenedProperty()
        {
            // arrange
            var source = new CompanyModel
            {
                Person = new PersonModel
                {
                    Address = new PersonAddress
                    {
                        Address1 = "Test Address"
                    }
                }
            };
            Mapper.WithRule(new Flattening());

            // act
            var result = Mapper.Map<CompanyDto>(source);

            // assert                
            Assert.Equal(source.Person.Address.Address1, result.PersonAddressAddress1);
        }
    }
}
