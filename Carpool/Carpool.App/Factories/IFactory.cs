namespace Carpool.App.Factories
{
    //From example project "CookBook"
    public interface IFactory<out T>
    {
        T Create();
    }
}
