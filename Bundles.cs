using System.Collections.Generic;
using System.Configuration;
using System.Web.Optimization;

namespace Camelonta.Utilities
{
    public class Bundles
    {
        /// <summary>
        /// To disable bundling/optimization, add <add key="disableBundles" value="true" /> to appSettings in web.config
        /// </summary>
        public static void DisableBundles()
        {
            var disableBundles = ConfigurationManager.AppSettings["disableBundles"] ?? "false";
            if (bool.Parse(disableBundles))
            {
                foreach (var bundle in BundleTable.Bundles)
                {
                    bundle.Transforms.Clear();
                }
                BundleTable.EnableOptimizations = false;
            }
            else
            {
                BundleTable.EnableOptimizations = true;
            }
        }

        public AsIsBundleOrderer AsIsBundleOrderer
        {
            get
            {
                return new AsIsBundleOrderer();
            }
        }
    }

    public class AsIsBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}