using ObjectMapper.MappingRules;
using ObjectMapper.Tests.TestClasses;
using ObjectMapper.TypeConverters;
using Xunit;

namespace ObjectMapper.Tests
{
    public class ModuleTests
    {
        private const string Category = "Module Tests - Default Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapUsingModule()
        {
            // arrange
            var model = new CompanyModel
            {
                Id = 1,
                Person = new PersonModel
                {
                    Name = "Test Name",
                    Active = true
                }
            }; 
            Mapper.RegisterModule<CompanyModule>();

            // act
            var result = Mapper.Map<CompanyDto>(model);

            // assert
            Assert.Equal(model.Id, result.Id);
            Assert.Equal("Test", result.CompanyName);
            Assert.Equal(model.CompanyGuid.ToString(), result.CompanyGuid);
            Assert.Equal(model.Person.Name, result.PersonName);
        }

        public class CompanyModule : ObjectMapperModule
        {
            protected override void Load(IObjectMapperConfiguration configuration)
            {
                configuration.AddConverter(new GuidToString());
                configuration.WithRule(new Flattening());
                configuration.AddMap<CompanyModel, CompanyDto>((ctx, source) =>
                {
                    var dto = ctx.Map<CompanyModel, CompanyDto>(source);
                    dto.CompanyName = "Test";
                    return dto;
                });
            }
        }
    }
}
