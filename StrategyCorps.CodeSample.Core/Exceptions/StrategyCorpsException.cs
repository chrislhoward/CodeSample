using System;
using System.Runtime.Serialization;

namespace StrategyCorps.CodeSample.Core.Exceptions
{
    [Serializable]
    public class StrategyCorpsException : Exception
    {
        public StrategyCorpsException() { }

        public StrategyCorpsException(string message)
            : base(message) { }

        public StrategyCorpsException(string message, Exception innerException)
            : base(message, innerException) { }

        public StrategyCorpsException(string message, ErrorCode errorCode, Exception innerException) : base(message, innerException)
        {
            StrategyCorpsErrorCode = errorCode;
        }

        protected StrategyCorpsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ErrorCode StrategyCorpsErrorCode { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("StrategyCorpsErrorCode", StrategyCorpsErrorCode);
        }
    }
}
