using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ObjectMapper.MappingRules;
using ObjectMapper.TypeConverters;

namespace ObjectMapper
{
    public class ObjectMapperInstance : IObjectMapperInstance, IObjectMapperContext
    {
        private IMappingRule mappingRule;
        private readonly ConcurrentDictionary<Tuple<Type, Type>, ObjectMap> maps = new ConcurrentDictionary<Tuple<Type, Type>, ObjectMap>(); 
        private readonly List<ITypeConverter> typeConverters = new List<ITypeConverter>();
 
        public ObjectMapperInstance()
        {
            mappingRule = new MatchingNameAndType();
        }

        public IReadOnlyCollection<ITypeConverter> TypeConverters { get { return typeConverters; } }
        public IReadOnlyCollection<ObjectMap> Maps { get { return maps.Values.ToList(); } } 

        public void AddMap<TSource, TDestination>(Func<IObjectMapperContext, TSource, TDestination> mappingFunc)
        {
            var objectMap = new FuncObjectMap<TSource, TDestination>(mappingFunc);
            AddMap(objectMap);
        }

        public void AddMap<TObjectMap>() where TObjectMap : ObjectMap, new()
        {
            AddMap(new TObjectMap());
        }

        public void AddMap(ObjectMap objectMap)
        {
            if (objectMap == null)
                throw new ArgumentNullException("objectMap");
            var key = Tuple.Create(objectMap.SourceType, objectMap.DestinationType);
            maps.AddOrUpdate(key, objectMap, (k, oldValue) => objectMap);
        }        

        public void AddConverter(params ITypeConverter[] converters)
        {
            typeConverters.AddRange(converters);
        }

        public void WithRule(IMappingRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");
            mappingRule = rule;
        }

        public void RegisterModule(ObjectMapperModule module)
        {
            if (module == null)
                throw new ArgumentNullException("module");
            module.Configure(this);
        }

        public void RegisterModule<T>() where T : ObjectMapperModule, new()
        {
            RegisterModule(new T());
        }

        public TDestination Map<TDestination>(object source) where TDestination : new()
        {
            return (TDestination) MapInternal(source, new TDestination());
        }

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            return (TDestination) MapInternal(source, new TDestination());
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination existingInstance)
        {
            return (TDestination)MapInternal(source, existingInstance);
        }

        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) where TDestination : new()
        {
            return source.Select(Map<TSource, TDestination>);
        }

        TDestination IObjectMapperContext.Map<TSource, TDestination>(TSource source)
        {
            return ((IObjectMapperContext) this).Map(source, new TDestination());
        }

        TDestination IObjectMapperContext.Map<TSource, TDestination>(TSource source, TDestination existingInstance)
        {
            // skip objectmap since we're adding one for this source and destination
            // and go straight to mapping rule
            return (TDestination)mappingRule.Map(this, source, existingInstance);
        }

        private object MapInternal(object source, object destination)
        {
            if (source == null)
                return null;

            // first see if there's an objectmap set up for these types
            ObjectMap objectMap;
            maps.TryGetValue(Tuple.Create(source.GetType(), destination.GetType()), out objectMap);

            // use object map if it exists, otherwise use mapping rule
            return objectMap != null 
                ? objectMap.Map(this, source) 
                : mappingRule.Map(this, source, destination);
        }
    }
}
