public interface IEventBus
{
    public EventBus EventBus { get; }
    public void SetupEventBus(EventBus eventBus);
}
