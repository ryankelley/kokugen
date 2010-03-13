#region Using Directives

using FluentNHibernate;

#endregion

namespace Kokugen.Core.Persistence.FluentMappings
{
    /// <summary>
    /// This class is used for me to scan for Fluent Mappings, if I want to later.
    /// </summary>
    public class KokugenPersistenceModel : PersistenceModel
    {
        public KokugenPersistenceModel()
        {
            AddMappingsFromThisAssembly();
        }
    }
}