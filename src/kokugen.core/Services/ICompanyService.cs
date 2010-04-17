using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Events;
using Kokugen.Core.Events.Messages;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface ICompanyService
    {
        IEnumerable<Company> ListAllCompanies();
        void DeleteCompany(Guid guid);
        Company Get(Guid id);
        Company AddCompany(string companyName, string addressStreetLine1, string addressStreetLine2, string addressCity, string addressState, string addressZipCode);
        void Save(Company company);
    }

    public class CompanyService : ICompanyService, IListener<ValueEntitySaved<Company>>, IListener<ValueEntityRemoved<Company>>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Company AddCompany(string companyName, string addressStreetLine1, string addressStreetLine2, string addressCity, string addressState, string addressZipCode)
        {
            var company = new Company() {Name = companyName};

            company.Address = new Address
                                  {
                                      StreetLine1 = addressStreetLine1,
                                      StreetLine2 = addressStreetLine2,
                                      City = addressCity,
                                      State = addressState,
                                      ZipCode = addressZipCode
                                  };

            _companyRepository.Save(company);
           
            return company;
        }

        public void Save(Company company)
        {
            _companyRepository.Save(company);
        }

        public IEnumerable<Company> ListAllCompanies()
        {
            return _companyRepository.Query().OrderBy(x => x.Name).ToList();
        }

        public void DeleteCompany(Guid guid)
        {
            var company = _companyRepository.Get(guid);
            
            _companyRepository.Delete(company);
        }

        public Company Get(Guid id)
        {
            return _companyRepository.Get(id);
        }

        public void Handle(ValueEntitySaved<Company> message)
        {
            ValueObjectRegistry.AddValueObject<Company>(new ValueObject(message.Entity.Id.ToString(), message.Entity.Name));
        }

        public void Handle(ValueEntityRemoved<Company> message)
        {
            ValueObjectRegistry.RemoveValueObject<Company>(new ValueObject(message.Entity.Id.ToString(), message.Entity.Name));
        }
    }

    
}