using Kokugen.Core.Domain;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace Kokugen.Tests.Persistence.Tests
{
    [TestFixture]
    public class CompanyPersistenceTester : PersistenceTesterContext<Company>
    {
        [Test]
        public void Test_The_Mappings()
        {
            Specification.CheckProperty(x => x.Name, "Company XYZ")
                .VerifyTheMappings();
            
        }

        
    }

}