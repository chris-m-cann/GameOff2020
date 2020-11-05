
namespace Util.Events
{
    public interface GameEventListener<T>
    {
        void OnEventRaised(T t);
    }
}