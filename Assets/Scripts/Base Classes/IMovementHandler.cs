public interface IMovementHandler
{
    void Subscribe(IMovementValue value);
    void UnSubscribe(IMovementValue value);
}