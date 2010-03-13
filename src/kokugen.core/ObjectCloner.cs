#region Using Directives

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace Kokugen.Core
{
    public static class ObjectCloner
    {
        /// <summary>
        /// Performs a "Deep Copy" or Clone on an object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Clone(object obj)
        {
            using (var buffer = new MemoryStream())
            {
                ISerializationFormatter formatter =
                    SerializationFormatterFactory.GetFormatter();
                formatter.Serialize(buffer, obj);
                buffer.Position = 0;
                object temp = formatter.Deserialize(buffer);
                return temp;
            }
        }
    }

    /// <summary>
    /// Defines an object that can serialize and deserialize
    /// object graphs.
    /// </summary>
    public interface ISerializationFormatter
    {
        /// <summary>
        /// Converts a serialization stream into an
        /// object graph.
        /// </summary>
        /// <param name="serializationStream">
        /// Byte stream containing the serialized data.</param>
        /// <returns>A deserialized object graph.</returns>
        object Deserialize(Stream serializationStream);

        /// <summary>
        /// Converts an object graph into a byte stream.
        /// </summary>
        /// <param name="serializationStream">
        /// Stream that will contain the the serialized data.</param>
        /// <param name="graph">Object graph to be serialized.</param>
        void Serialize(Stream serializationStream, object graph);
    }

    public static class SerializationFormatterFactory
    {
        public static ISerializationFormatter GetFormatter()
        {
            //if (ApplicationContext.SerializationFormatter == ApplicationContext.SerializationFormatters.BinaryFormatter)
            return new BinaryFormatterWrapper();
            // else
            //return new NetDataContractSerializerWrapper();
        }
    }

    public class BinaryFormatterWrapper : ISerializationFormatter
    {
        private BinaryFormatter _formatter =
            new BinaryFormatter();

        #region ISerializtionFormatter

        /// <summary>
        /// Converts a serialization stream into an object graph
        /// </summary>
        /// <param name="serializationStream"></param>
        /// <returns></returns>
        public object Deserialize(Stream serializationStream)
        {
            return _formatter.Deserialize(serializationStream);
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            _formatter.Serialize(serializationStream, graph);
        }

        #endregion

        /// <summary>
        /// Gets a reference to the underlying
        /// <see cref="BinaryFormatter"/>
        /// object.
        /// </summary>
        public BinaryFormatter Formatter
        {
            get { return _formatter; }
        }
    }
}