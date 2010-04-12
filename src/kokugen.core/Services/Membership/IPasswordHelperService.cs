namespace Kokugen.Core.Services
{
    public interface IPasswordHelperService
    {
        string CreatePasswordHash(string password);
        bool ComparePasswordToHash(string password, string passwordHash);
        string RandomPasswordNoHash(int length);
        string RandomPasswordNoHash(int length, int nonAlphaNumberic);
        string RandomPasswordHashed(int length);
    }
}