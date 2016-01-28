using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Gopen
{
    public class GopenContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves the key of the dictionary by resolving each part (seperator ".") with
        /// <see cref="M:Newtonsoft.Json.Serialization.DefaultContractResolver.ResolvePropertyName(System.String)" />.
        /// </summary>
        /// <param name="dictionaryKey">Key of the dictionary.</param>
        /// <returns>
        /// Resolved key of the dictionary.
        /// </returns>
        protected override string ResolveDictionaryKey(string dictionaryKey)
        {
            var parts = dictionaryKey.Split('.');

            return string.Join(".", parts.Select(ResolvePropertyName));
        }
    }
}
