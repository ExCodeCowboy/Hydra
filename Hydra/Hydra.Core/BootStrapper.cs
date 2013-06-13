using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Hydra.Core.Definitions;
using Hydra.Core.Definitions.Builders;

namespace Hydra.Core
{
    public static class BootStrapper
    {
        public static ApplicationManifestManager GenerateApplicationManifest(String appDirectory)
        {
            //Build Manager
            ApplicationManifestManager manager = new ApplicationManifestManager();
            //Build Manifests
            IEnumerable<AssemblyManifest> manifests =
                GetManifests("*.dll", appDirectory)
                .Union(GetManifests("*.exe", appDirectory)); 
            
            manager.AddManifest(manifests);
            
            //Return manager
            return manager;        
        }   
     
        private static Assembly LoadAssembly(string filename)
        {
            Assembly result = null;
            try
            {
                result = Assembly.LoadFile(filename);
            }
            catch 
            {
            }

            return result;
        }

        private static IEnumerable<AssemblyManifest> GetManifests(string type, string location)
        {
            return Directory.EnumerateFiles(location, type)
                      .Select(LoadAssembly)
                      .Where(x => x != null)
                      .Select(ManifestBuilder.BuildManifest);
        }
    }
}
