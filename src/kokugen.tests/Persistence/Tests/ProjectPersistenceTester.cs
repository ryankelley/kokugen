using System;
using Kokugen.Core.Domain;
using Kokugen.Core.Membership.Security;
using NUnit.Framework;
using FluentNHibernate.Testing;

namespace Kokugen.Tests.Persistence.Tests
{
    [TestFixture]
    public class ProjectPersistenceTester : PersistenceTesterContext<Project>
    {
        [Test]
        public void Can_save_Project()
        {
            var address = new Address() { City = "Amarillo", State = "TX", StreetLine1 = "8331 N Western", ZipCode = "52136" };
            var company = new Company {Name = "Test", Address = address};
            Specification
                .CheckProperty(x => x.Name, "Test Project Six Two")
                .CheckProperty(x => x.Description, "This is a test project used for persistence testing")
                .CheckProperty(x => x.StartDate, DateTime.Today)
                .CheckProperty(x => x.EndDate, DateTime.Today.AddDays(5))
                .CheckProperty(x => x.AverageTimeSpentPerSession, 12.8)
                .CheckProperty(x => x.TotalTime, 768.34)
                .CheckReference(x => x.Company, company)
                .VerifyTheMappings();

        }
    }


}