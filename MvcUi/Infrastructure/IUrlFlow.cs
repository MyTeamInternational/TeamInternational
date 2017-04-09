namespace MvcUi.Infrastructure
{
    public interface IUrlFlow
    {
        bool CanGo(string action);
    }
}