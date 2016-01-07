using ObjectMapper.Tests.TestClasses;
using Xunit;

namespace ObjectMapper.Tests
{
    public class ObjectMapTests
    {
        private const string Category = "ObjectMap Tests - Default Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapUsingFunction()
        {
            // arrange
            var model = new PersonModel {Id = 1};
            Mapper.AddMap<PersonModel, PersonDto>((ctx, source) =>
            {
                var dto = ctx.Map<PersonModel, PersonDto>(source);
                dto.Name = "Test";
                return dto;
            });

            // act
            var result = Mapper.Map<PersonDto>(model);

            // assert
            Assert.Equal(model.Id, result.Id);
            Assert.Equal("Test", result.Name);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapUsingFunctionOnProperty()
        {
            // arrange
            var model = new PersonModel
            {
                Address = new PersonAddress { Address1 = "Address1", Address2 = "Address2"}
            };
            Mapper.AddMap<PersonAddress, string>((ctx, source) =>
            {
                return source.Address1 + "," + source.Address2;
            });

            // act
            var result = Mapper.Map<PersonDto>(model);

            // assert
            Assert.Equal(model.Address.Address1 + "," + model.Address.Address2, result.Address);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapUsingObjectMap()
        {
            // arrange
            var model = new PersonModel {Id = 1};
            Mapper.AddMap<TestObjectMap>();

            // act
            var result = Mapper.Map<PersonDto>(model);

            // assert
            Assert.Equal(model.Id, result.Id);
            Assert.Equal(TestObjectMap.Name, result.Name);
        }

        public class TestObjectMap : ObjectMap<PersonModel, PersonDto>
        {
            public const string Name = "TestObjectMap";

            protected override PersonDto MapObject(IObjectMapperContext mapperContext, PersonModel source)
            {
                var dto = mapperContext.Map<PersonModel, PersonDto>(source);
                dto.Name = Name;
                return dto;
            }
        }
    }
}
