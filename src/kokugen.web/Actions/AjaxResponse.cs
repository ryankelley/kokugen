    using System;
    using System.Xml;

namespace Kokugen.Web.Actions
{
    public class AjaxResponse
    {
        public bool Success { get; set; }
        public object Item { get; set; }
    }

    public class InPlaceAjaxResponse
    {
        public bool success { get; set; }
        public object NewValueToDisplay { get; set; }
    }

        public class XmlResponse : IXmlOutput
        {
            public XmlDocument XmlData
            {
                get; set;
            }
        }

    public interface IXmlOutput
    {
        XmlDocument XmlData { get; set; }
    }
}