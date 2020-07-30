//This is a simple implementation with serialization/deserialization.
using System;
using System.Text.Json;

namespace Program
{
    public class JsonSerializer<TIn>
    {
        public byte[] Serialize(TIn serializable)
        {
            return JsonSerializer.SerializeToUtf8Bytes(serializable, typeof(TIn));
        }

        public bool TryDeserialize(byte[] raw, out TIn deserialized)
        {
            //4. Implement the rest of the function; .
            try
            {
                var result = JsonSerializer.Deserialize<TIn>(raw.AsSpan());
                deserialized = result;
                return true;
            }
            catch (Exception e) //5. Using the _logger object, an error message of "Deserialization of {Message} failed" must be thrown for deserialization exceptions
            {
                deserialized = default!;
                return false;
            }
        }
    }
}

//If a string should be used as input we also have other options as below:

using System.Text;
using System.Text.Json;

namespace Program
{
    public class JsonSerializer<TIn>
    {
        public byte[] Serialize(TIn serializable)
        {
            var json = JsonSerializer.Serialize(serializable);
            return Encoding.UTF8.GetBytes(json);
        }

        public bool TryDeserialize(byte[] raw, out TIn deserialized)
        {
            //4. Implement the rest of the function; .
            try
            {
                var str = Encoding.UTF8.GetString(raw);
                var result = JsonSerializer.Deserialize<TIn>(str);
                deserialized = result;
                return true;
            }
            catch (JsonException) //5. Using the _logger object, an error message of "Deserialization of {Message} failed" must be thrown for deserialization exceptions
            {
                deserialized = default!;
                return false;
            }
        }
    }
}

//Thou I still I am not clear what does the converter do here or how does it help without knowing the internal implementation of it.
//Overall I find this question a bit lacking on details what needs to be done.

//About 5): //5. Using the _logger object, an error message of "Deserialization of {Message} failed" must be thrown for deserialization exceptions. 
//To me this is unclear because logger is not used to throw anything, its used only to log an exception with a specific message.

//In any case we can pass a custom message and the exception we catch like below:

    catch (JsonException e) //5. Using the _logger object, an error message of "Deserialization of {Message} failed" must be thrown for deserialization exceptions
    {
        _logger.LogError(e, $"Deserialization of {typeof(TIn).Name} failed");
        deserialized = default!;
        return false;
    }

