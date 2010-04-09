namespace Kokugen.Core.Membership.Abstractions.NHibernate
{
    public interface IPasswordHelperService
    {
        string CreatePasswordHash(string password);
        bool ComparePasswordToHash(string password, string passwordHash);
        string RandomPasswordNoHash(int length);
        string RandomPasswordHashed(int length);
    }
}