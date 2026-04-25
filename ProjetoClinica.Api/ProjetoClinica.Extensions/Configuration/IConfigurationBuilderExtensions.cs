using Microsoft.Extensions.Configuration;

namespace ProjetoClinica.Extensions.Configuration
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder UseBackreference(this IConfigurationBuilder configurationBuilder)
        {
            return configurationBuilder.Add(new BackreferenceConfigurationSource());
        }
    }
}
