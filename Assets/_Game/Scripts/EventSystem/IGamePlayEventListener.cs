namespace Partisan
{
    public interface IGamePlayEventListener<T> where T: IGamePlayEvent
    {
        void OnGamePlayEvent(T gamePlayEvent);
    }
}
