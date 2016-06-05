using System;

namespace ObjectMapper
{
    public abstract class ObjectMapperModule
    {
        public void Configure(IObjectMapperConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");
            Load(configuration);
        }

        protected abstract void Load(IObjectMapperConfiguration configuration);
    }
}
