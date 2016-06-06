What is ObjectMapper?
--------------------------

ObjectMapper is just what it sounds like: a simple library for mapping one object onto another. It abides by the "law of least surprise" and just works<sup>TM</sup>.

How do you use it?
--------------------------

Like this:

```cs
var personDto = Mapper.Map<PersonDto>(personModel); 
```
or like this:

```cs
var personDto = Mapper.Map<PersonModel, PersonDto>(personModel); 
```