using System.Collections.Generic;
using System.Linq;

namespace Hydra.Core.Definitions
{
    public class ApplicationManifestManager
    {
        List<AssemblyManifest> _assemblyManifests = new List<AssemblyManifest>();

        public ApplicationManifestManager() { }

        public void AddManifest(AssemblyManifest manifest)
        {
            _assemblyManifests.Add(manifest);
        }

        public void AddManifest(IEnumerable<AssemblyManifest> manifests)
        {
            _assemblyManifests.AddRange(manifests);
        }

        public bool HasProcessor<TInput,TOutput>(string name)
        {
            return _assemblyManifests.Exists(x=>x.HasProcessor<TInput,TOutput>(name));
        }

        public IWorkProcessor<TInput,TOutput> GetProcessor<TInput, TOutput>(string name)
        {
            return _assemblyManifests.Select(x=>x.GetProcessor<TInput,TOutput>(name)).FirstOrDefault(x=>x!=null);           
        }
    }
}
