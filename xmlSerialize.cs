using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Adapters.Infrastructure.Serialization
{
    public class XmlSerializer<T> : IDeserializer<T>, ISerializer<T>
    {
        //We put private access modifier to preserve the integrity of the injected instances from being accessed outside of the scope of this class.
        //readonly is the keyword which ensures the instance can not be modified outside of the constructor.
        
        private readonly ILogger<XmlSerializer<T>> _logger;
        private readonly XmlSerializer _serializer;

        public XmlSerializer(ILogger<XmlSerializer<T>> logger)
        {
            _logger = logger;
            _serializer = new XmlSerializer(typeof(T));
        }

        /*
        reason why memory stream is the choice here is because we can instantly use it alongside XmlSerializer, 
        and also it is the type of stream dedicated for in-memory operations.
        */

        public byte[] Serialize(T serializable)
        {
            using var memoryStream = new MemoryStream();
            _serializer.Serialize(memoryStream, serializable);
            return memoryStream.ToArray();
        }

        public bool TryDeserialize(byte[] raw, [MaybeNullWhen(false)]out T deserialized)
        {
            try
            {
                using var memoryStream = new MemoryStream(raw);
                var result = (T)_serializer.Deserialize(memoryStream);
                deserialized = result;
                return true;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Deserialization of {MessageType} failed.", typeof(T).Name);
                deserialized = default!;
                return false;
            }
        }
    }
}
