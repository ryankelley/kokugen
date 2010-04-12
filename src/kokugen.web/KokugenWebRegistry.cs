using System.Web;
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

            ForRequestedType<HttpContextBase>().TheDefault.Is.ConstructedBy(ctx => new HttpContextWrapper(HttpContext.Current));
            ForRequestedType<HttpRequestBase>().TheDefault.Is.ConstructedBy(ctx => new HttpRequestWrapper(HttpContext.Current.Request));
        }
    }
}