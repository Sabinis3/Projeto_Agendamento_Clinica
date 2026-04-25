using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace ProjetoClinica.Extensions.Configuration
{
    internal partial class BackreferenceConfigurationProvider : ConfigurationProvider
    {
        readonly ConfigurationManager _configurationManager = new();

        readonly IList<IConfigurationSource> _sources;

        [GeneratedRegex(@"\${(.+)\}", RegexOptions.Compiled)]
        private static partial Regex BackreferencefRegex();

        public BackreferenceConfigurationProvider(IList<IConfigurationSource> sources) : base()
        {
            _sources = sources;
        }

        /// <summary>Loads (or reloads) the data for this provider.</summary>
        public override void Load()
        {
            foreach (var source in _sources)
            {
                if (source.GetType() != typeof(BackreferenceConfigurationSource))
                {
                    ((IConfigurationBuilder)_configurationManager).Add(source);
                }
            }
            base.Load();
        }

        /// <summary>Attempts to find a value with the given key, returns true if one is found, false otherwise.</summary>
        public override bool TryGet(string key, out string value)
        {
            value = GetValueOrDefault(key);

            if (TryGetBackreference(value, out string backreferenceValue))
            {
                value = backreferenceValue;
            }

            return value != default;
        }

        string GetValueOrDefault(string key)
        {
            string value = _configurationManager.GetValue<string>(key);
            return string.IsNullOrEmpty(value) ? default : value;
        }

        bool TryGetBackreference(string key, out string value)
        {
            value = default;

            if (key == default)
            {
                return false;
            }

            Match match = BackreferencefRegex().Match(key);
            if (match.Success)
            {
                string backreferenceKey = match.Groups[1].Value;
                value = GetValueOrDefault(backreferenceKey);
            }

            return value != default;
        }
    }
}
