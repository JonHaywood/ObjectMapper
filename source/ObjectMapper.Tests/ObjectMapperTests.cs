using System;
using ObjectMapper.Tests.TestClasses;
using Xunit;

namespace ObjectMapper.Tests
{
    public class ObjectMapperTests
    {
        private const string Category = "Mapper Tests - Default Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapString()
        {
            // arrange
            var source = new PersonModel {Name = "Test"};

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.Name, result.Name);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapInt()
        {
            // arrange
            var source = new PersonModel { Id = 1 };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.Id, result.Id);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapDateTime()
        {
            // arrange
            var source = new PersonModel { CreatedOn = DateTime.Now };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.CreatedOn, result.CreatedOn);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapBool()
        {
            // arrange
            var source = new PersonModel { Active = true };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.Active, result.Active);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapNullableDateTime()
        {
            // arrange
            var source = new PersonModel { LastUpdatedOn = DateTime.Now };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.LastUpdatedOn, result.LastUpdatedOn);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapGuid()
        {
            // arrange
            var source = new PersonModel { RowGuid = Guid.NewGuid() };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.RowGuid, result.RowGuid);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapObject()
        {
            // arrange
            var source = new PersonModel
            {
                Data = new PersonData { Value = "Test" }
            };

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.Data.Value, result.Data.Value);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldNotMapNonMatchingProperties()
        {
            // arrange
            var source = new PersonModel {NonMatching = 1};

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.NotEqual(source.NonMatching, result.NonMatching);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapAnonymousType()
        {
            // arrange
            var source = new {Name = "Test"};

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.Name, result.Name);
        }   
    }
}
