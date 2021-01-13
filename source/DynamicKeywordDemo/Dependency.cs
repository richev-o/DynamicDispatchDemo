using System;

namespace DynamicDispatchDemo
{
    public static class Dependency
    {
        public static void MethodThatNeedsToKnowT<T>(T model, Type expectedType) where T : ISomeInterface
        {
            if (typeof(T) != expectedType)
                throw new InvalidOperationException($"The type of {nameof(T)} wasn't specific enough. It should be {expectedType.Name}, but was {typeof(T)}.");
            // rest of the method would be here...
        }
    }
}