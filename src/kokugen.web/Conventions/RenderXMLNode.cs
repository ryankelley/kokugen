using System;
using System.Xml;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;

namespace Kokugen.Web.Conventions
{
    public class RenderXMLNode : OutputNode
    {
        public RenderXMLNode(Type behaviorType) : base(behaviorType)
        {
        }
    }

    public class RenderXMLBehavior<T> : BasicBehavior where T : XmlDocument
    {
        private readonly IFubuRequest _request;
        private readonly IXMLWriter _writer;

        public RenderXMLBehavior(IXMLWriter writer, IFubuRequest request)
            : base(PartialBehavior.Executes)
        {
            _writer = writer;
            _request = request;
        }

        protected override DoNext performInvoke()
        {
            var output = _request.Get<T>();
            _writer.Write(output);
            return DoNext.Continue;
        }
    }
}