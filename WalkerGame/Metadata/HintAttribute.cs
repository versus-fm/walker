using System;
using WalkerGame.Metadata.Hinting;

namespace WalkerGame.Metadata
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HintAttribute : Attribute
    {
        public int IntValue { get; }
        public string StringValue { get; }
        public bool BoolValue { get; }

        public Type LiteralType { get; }
        public Type ServiceType { get; }
        
        public HintType HintType { get; }

        public HintAttribute(string value)
        {
            StringValue = value;
            LiteralType = typeof(string);
            HintType = HintType.Literal;
        }

        public HintAttribute(int value)
        {
            IntValue = value;
            LiteralType = typeof(int);
            HintType = HintType.Literal;
        }

        public HintAttribute(bool value)
        {
            BoolValue = value;
            LiteralType = typeof(bool);
            HintType = HintType.Literal;
        }

        public HintAttribute(Type serviceType)
        {
            ServiceType = serviceType;
            HintType = HintType.ServiceHint;
        }

        public object GetLiteral()
        {
            if (LiteralType == typeof(int))
                return IntValue;
            if (LiteralType == typeof(string))
                return StringValue;
            if (LiteralType == typeof(bool))
                return BoolValue;
            return null;
        }
    }
}