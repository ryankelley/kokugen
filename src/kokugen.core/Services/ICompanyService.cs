using System;
using System.Collections.Generic;
using System.Linq;
using Kokugen.Core.Domain;
using Kokugen.Core.Persistence.Repositories;

namespace Kokugen.Core.Services
{
    public interface ICompanyService
    {
        Company AddCompany(string name);
        IEnumerable<Company> ListAllCompanies();
        void DeleteCompany(Guid guid);
        Company Get(Guid id);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Company AddCompany(string name)
        {
            var company = new Company() {Name = name};

            _companyRepository.Save(company);

            return company;
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
    }
}