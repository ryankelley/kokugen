using System.IO;
using System.Xml;
using FubuCore.Binding;
using FubuMVC.Core;
using FubuMVC.Core.Runtime;
using HtmlTags;

namespace Kokugen.Web.Conventions
{
    public interface IXMLWriter
    {
        void Write(XmlDocument output);
    }

    public class XMLWriter : IXMLWriter
    {
        private readonly IOutputWriter _outputWriter;
        private readonly IRequestData _requestData;

        public XMLWriter(IOutputWriter outputWriter, IRequestData requestData)
        {
            _outputWriter = outputWriter;
            _requestData = requestData;
        }

        public void Write(XmlDocument output)
        {
            var rawXMLOutput = output.InnerXml;

            //if (_requestData.IsAjaxRequest())
            //{
                 _outputWriter.Write(MimeType.XML.ToString(), rawXMLOutput);
            //}
            //else
            //{
            //    // For proper jquery.form plugin support of file uploads
            //    // See the discussion on the File Uploads sample at http://malsup.com/jquery/form/#code-samples
            //    string html = "<html><body><textarea rows=\"10\" cols=\"80\">" + rawXMLOutput +
            //        "</textarea></body></html>";
            //    _outputWriter.Write(MimeType.Html.ToString(), html);
            //}
        }
    }
}