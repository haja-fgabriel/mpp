using System;
using System.Runtime.Serialization;

namespace ChatServices
{
    [Serializable]
    public class ChatException:ApplicationException
    {
        public ChatException()
        {
        }

        public ChatException(string message) : base(message)
        {
        }

        public ChatException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ChatException(SerializationInfo info, StreamingContext context) : base(info,context) { }
    }
}
