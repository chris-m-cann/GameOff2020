namespace Util
{
    public interface IProvider<T>
    {
        T Get();
    }
}