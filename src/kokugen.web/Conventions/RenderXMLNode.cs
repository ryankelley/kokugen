using System;
using System.Xml;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Runtime;
using Kokugen.Web.Actions;

namespace Kokugen.Web.Conventions
{

    public class RenderXMLNode : OutputNode
    {
        private readonly Type _modelType;

        public RenderXMLNode(Type modelType)
            : base(typeof(RenderXMLBehavior<>).MakeGenericType(modelType))
        {
            _modelType = modelType;
        }

        public Type ModelType { get { return _modelType; } }
        public override string Description { get { return "Xml"; } }

        public bool Equals(RenderXMLNode other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._modelType, _modelType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(RenderXMLNode)) return false;
            return Equals((RenderXMLNode)obj);
        }

        public override int GetHashCode()
        {
            return (_modelType != null ? _modelType.GetHashCode() : 0);
        }
    }


    public class RenderXMLBehavior<T> : BasicBehavior where T : class, IXmlOutput
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
            var output = _request.Get<T>().XmlData;
            _writer.Write(output);
            return DoNext.Continue;
        }
    }
}