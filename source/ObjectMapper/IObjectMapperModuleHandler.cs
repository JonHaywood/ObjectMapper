namespace ObjectMapper
{
    public interface IObjectMapperModuleHandler
    {
        void RegisterModule(ObjectMapperModule module);
        void RegisterModule<T>() where T : ObjectMapperModule, new();
    }
}