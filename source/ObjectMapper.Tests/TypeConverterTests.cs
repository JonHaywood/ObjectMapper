using System;
using System.Collections.Generic;
using ObjectMapper.MappingRules.TypeConverters;
using ObjectMapper.Tests.TestClasses;
using ObjectMapper.TypeConverters;
using Xunit;

namespace ObjectMapper.Tests
{
    public class TypeConverterTests
    {
        private const string Category = "TypeConverter Tests - Default Rule";
        private IObjectMapperInstance Mapper = new ObjectMapperInstance();

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapGuidToString()
        {
            // arrange
            var source = new PersonModel {PersonGuid = Guid.NewGuid()};
            Mapper.AddConverter(new GuidToString());

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.PersonGuid.ToString(), result.PersonGuid);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapStringToGuid()
        {
            // arrange
            var source = new PersonDto {PersonGuid = Guid.NewGuid().ToString()};
            Mapper.AddConverter(new StringToGuid());

            // act
            var result = Mapper.Map<PersonModel>(source);

            // assert
            Assert.Equal(source.PersonGuid, result.PersonGuid.ToString());
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapDateTimeToString()
        {
            // arrange
            var dateFormat = "mm/dd/YYYY";
            var source = new PersonModel {BirthDate = DateTime.Now};
            Mapper.AddConverter(new DateTimeToString(dateFormat));

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert            
            Assert.Equal(source.BirthDate.ToString(dateFormat), result.BirthDate);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapStringToDateTime()
        {
            // arrange
            var dateFormat = "mm/dd/YYYY";
            var source = new PersonDto {BirthDate = DateTime.Now.ToString(dateFormat)};
            Mapper.AddConverter(new StringToDateTime(dateFormat));

            // act
            var result = Mapper.Map<PersonModel>(source);

            // assert            
            Assert.Equal(source.BirthDate, result.BirthDate.ToString(dateFormat));
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapNullableToValue()
        {
            // arrange
            var source = new PersonModel {AmountPaid = 1m};
            Mapper.AddConverter(new NullableToValue());

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert            
            Assert.Equal(source.AmountPaid, result.AmountPaid);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapValueToNullable()
        {
            // arrange
            var source = new PersonDto { AmountPaid = 1m };
            Mapper.AddConverter(new ValueToNullable());

            // act
            var result = Mapper.Map<PersonModel>(source);

            // assert            
            Assert.Equal(source.AmountPaid, result.AmountPaid);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapListToArray()
        {
            // arrange
            var source = new PersonModel
            {
                List = new List<int> { 1, 2, 3, 4 }
            };
            Mapper.AddConverter(new ListToArray());

            // act
            var result = Mapper.Map<PersonDto>(source);

            // assert
            Assert.Equal(source.List.Count, result.List.Length);
            for (int i = 0; i < source.List.Count; i++)
                Assert.Equal(source.List[i], result.List[i]);
        }

        [Trait("Category", Category)]
        [Fact]
        public void ShouldMapArrayToList()
        {
            // arrange
            var source = new PersonDto
            {
                List = new[] { 1, 2, 3, 4 }
            };
            Mapper.AddConverter(new ArrayToList());

            // act
            var result = Mapper.Map<PersonModel>(source);

            // assert
            Assert.Equal(source.List.Length, result.List.Count);
            for (int i = 0; i < source.List.Length; i++)
                Assert.Equal(source.List[i], result.List[i]);
        }
    }
}
