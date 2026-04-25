namespace ProjetoClinica.Extensions.DependencyInjection
{
    /// <summary>
    /// Define o tempo de vida de injeção para uma implementação como Singleton.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonLifetimeAttribute : Attribute
    {
    }

}
