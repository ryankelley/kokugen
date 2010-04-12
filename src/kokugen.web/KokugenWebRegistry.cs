using Kokugen.Core;
using StructureMap.Configuration.DSL;

namespace Kokugen.Web
{
    public class KokugenWebRegistry : Registry
    {
        public KokugenWebRegistry()
        {
            Scan(x =>
                     {
                         x.TheCallingAssembly();
                         x.WithDefaultConventions();

                         x.AddAllTypesOf<IStartable>();
                     }
                );
        }
    }
}