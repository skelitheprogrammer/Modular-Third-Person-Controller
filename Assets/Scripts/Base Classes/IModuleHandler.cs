public interface IModuleHandler
{
    void Subscribe(IModule module);
    void UnSubscribe(IModule module);
}
