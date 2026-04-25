using Microsoft.Extensions.Configuration;

namespace ProjetoClinica.Extensions.Configuration
{
    internal class BackreferenceConfigurationSource : IConfigurationSource
    {
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new BackreferenceConfigurationProvider(builder.Sources);
        }
    }
}
