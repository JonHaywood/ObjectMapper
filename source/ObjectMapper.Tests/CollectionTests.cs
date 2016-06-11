using System.Collections;
using System.Linq;
using ObjectMapper.Tests.TestClasses;
using Xunit;

namespace ObjectMapper.Tests
{
    public class CollectionTests
    {
        private const string Category = "Mapper Tests - Collections";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapList()
        {
            // arrange
            var source = Enumerable.Range(1, 10).Select(i => new PersonModel { Name = "Test" + i }).ToList();

            // act
            var result = Mapper.Map<PersonModel, PersonDto>(source);

            // assert
            for (int i = 0; i < source.Count; i++)
                Assert.Equal(source[i].Name, result.ElementAt(i).Name);                 
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapArray()
        {
            // arrange
            var source = Enumerable.Range(1, 10).Select(i => new PersonModel { Name = "Test" + i }).ToArray();

            // act
            var result = Mapper.Map<PersonModel, PersonDto>(source);

            // assert
            for (int i = 0; i < source.Length; i++)
                Assert.Equal(source[i].Name, result.ElementAt(i).Name);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapArrayList()
        {
            // arrange
            var source = new ArrayList(Enumerable.Range(1, 10).Select(i => new PersonModel {Name = "Test" + i}).ToList());

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            for (int i = 0; i < source.Count; i++)
            {
                var element = (PersonModel)source[i];
                Assert.Equal(element.Name, result.ElementAt(i).Name);
            }
        }
    }
}
