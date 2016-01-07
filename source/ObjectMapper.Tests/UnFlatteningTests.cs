using ObjectMapper.MappingRules;
using ObjectMapper.Tests.TestClasses;
using Xunit;

namespace ObjectMapper.Tests
{
    public class UnFlatteningTests
    {
        private const string Category = "Mapper Tests - UnFlattening Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapNormalProperty()
        {
            // arrange
            var source = new CompanyDto
            {
                CompanyName = "Test Company",
                PersonName = "Test Person",
                PersonAddressAddress1 = "Test Address"
            };
            Mapper.WithRule(new UnFlattening());

            // act
            var result = Mapper.Map<CompanyModel>(source);

            // assert
            Assert.Equal(source.CompanyName, result.CompanyName);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapOneLevelUnflattedProperty()
        {
            // arrange
            var source = new CompanyDto
            {
                CompanyName = "Test Company",
                PersonName = "Test Person",
                PersonAddressAddress1 = "Test Address"
            };
            Mapper.WithRule(new UnFlattening());

            // act
            var result = Mapper.Map<CompanyModel>(source);

            // assert    
            Assert.NotNull(result.Person);
            Assert.Equal(source.PersonName, result.Person.Name);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapMultipleLevelsUnFlattenedProperty()
        {
            // arrange
            var source = new CompanyDto
            {
                CompanyName = "Test Company",
                PersonName = "Test Person",
                PersonAddressAddress1 = "Test Address"
            };
            Mapper.WithRule(new UnFlattening());

            // act
            var result = Mapper.Map<CompanyModel>(source);

            // assert            
            Assert.NotNull(result.Person);            
            Assert.NotNull(result.Person.Address);
            Assert.Equal(source.PersonAddressAddress1, result.Person.Address.Address1);  // multiple levels deep
        }
    }
}
