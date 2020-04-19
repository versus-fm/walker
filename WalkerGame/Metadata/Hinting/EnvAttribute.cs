using System;

namespace WalkerGame.Metadata.Hinting
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class EnvAttribute : HintAttribute
    {
        public EnvAttribute(string environment) : base(Environment.GetEnvironmentVariable(environment))
        {
            
        }
    }
}