What is ObjectMapper?
--------------------------

ObjectMapper is a simple .NET library that transforms a source object into a destination object of a different type. It uses a convention based approach to determine how one object maps to another. No extensive manual mapping needed!

Why should I map?
--------------------------
Often times we need to work with an object at an application boundary (like an ORM persistence layer) and map it to something else (say a domain model). Writing that mapping code sucks. ObjectMapper makes it easy!

Yet another mapper?
--------------------------
There's some excellent object-to-object mappers out there. However, depending on your project many of them might be overkill. ObjectMapper is a small, straightforward library that tries to do one thing and do it well. It's main goals are:
* A simple mapping API
* A simple way to extend functionality
* A simple way to inject it

How do you use it?
--------------------------
Like this:

```cs
var personDto = ObjectMapper.Map<PersonDto>(personModel);
```
or like this:

```cs
var personDto = ObjectMapper.Map<PersonModel, PersonDto>(personModel);
```

Properties of the same name and type are *automatically* mapped. You're done!

What if I don't want static calls?
--------------------------
Not a problem!

```cs
IObjectMapperInstance mapper = new ObjectMapperInstance();
var personDto = mapper.Map<PersonDto>(personModel);
```

What if I have mismatching types between objects?
--------------------------
Say you have the following two objects:
```cs
class Foo1
{
	public Guid Id { get; set; }
}

class Foo2
{
	public string Id { get; set; }
}
```
Those properties won't map because their types are different. If you always want a Guid to be able to map to a string, then one way to fix this is to use a TypeConverter.

```cs
mapper.AddConverter(new GuidToString());
var foo2 = mapper.Map<Foo2>(foo1); // The Id property is now set on foo2
```

And you can easily add your own TypeConverters. Also, if the conversion is simple you can even use lambdas:

```cs
class Address
{
	public string Address1 { get; set; }
	public string Address2 { get; set; }
}
class Foo1
{
	public Address Address{ get; set; }
}

class Foo2
{
	public string Address { get; set; }
}

// tell ObjectMapper how to convert Addresses to strings
mapper.AddConverter<Address, string>(source => source.Address1 + ", " + source.Address2);

var address = new Address { Address1 = "Test St", Address2 = "Unit 2" };
var foo1 = new Foo { Address = address };
var foo2 = mapper.Map<Foo2>(foo1); // foo2's Address property is now "Test St, Unit 2"
```

Can I map collections?
--------------------------
Of course!

```cs
List<Person> persons = GetPersons();
IEnumerable<PersonDto> personDtos = mapper.Map<Person, PersonDto>(persons);
```
Done.

But wait, there's more
--------------------------
ObjectMapper also supports:
* Mapping anonymous types
* Defining a manual mapping between types
* Flattening and unflattening
* Replacing the matching algorithm with your own
* For complex mapping, registering a "module" which contains all your mapping configuration in one place

ObjectMapper is Copyright &copy; 2016 [Jonathan Haywood](http://jonphaywood.com) and other contributors under the [MIT license](LICENSE.txt).