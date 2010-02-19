using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Kokugen.Core.Config
{
    public interface IPayPalConfiguration
    {
        string Credentials { get; }
        string ApiVersion { get; }
        string Server { get; }
        X509Certificate2 Cert { get; }
    }

    public class CAPayPalConfiguration : IPayPalConfiguration
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _signature;
        private readonly string _server;
        private readonly string _apiVersion;
        private readonly string _certPassword;
        private X509Certificate2 _cert;
        private readonly string _certPath;

        public CAPayPalConfiguration()
        {
            _username = ConfigurationManager.AppSettings["PayPalUserName"];
            _password = ConfigurationManager.AppSettings["PayPalPassword"];
            _signature = ConfigurationManager.AppSettings["PayPalSignature"];
            _server = ConfigurationManager.AppSettings["PayPalServer"];
            _apiVersion = ConfigurationManager.AppSettings["PayPalVersion"];
            _certPassword = ConfigurationManager.AppSettings["PayPalCertPassword"];
            _certPath = ConfigurationManager.AppSettings["PayPalCertPath"];

            if (!string.IsNullOrEmpty(_certPassword) && !string.IsNullOrEmpty(_certPath)) LoadCertificate();
        }

        public string Credentials
        {
            get
            {
                return "USER=" + _username + "&PWD=" + _password + "&SIGNATURE=" + _signature;
                ;
            }
        }

        public string ApiVersion
        {
            get { return _apiVersion; }
        }

        public string Server
        {
            get { return _server; }
        }

        public X509Certificate2 Cert
        {
            get { return _cert; }
        }

        private void LoadCertificate()
        {
            var fs = File.Open(_certPath, FileMode.Open, FileAccess.Read);
            var buffer = new byte[fs.Length];
            var count = fs.Read(buffer, 0, buffer.Length);
            fs.Close();
            _cert = new X509Certificate2(buffer, _certPassword);
        }
    }
}