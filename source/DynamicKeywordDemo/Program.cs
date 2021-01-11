using System;

namespace DynamicDispatchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var modelA = new SpecificType();
            var classA = new SomeClass(modelA);

            Console.WriteLine();
            Console.WriteLine("Example 1: Casting to dynamic.");
            Console.WriteLine();
            Console.WriteLine("When the dynamic keyword is used, the runtime directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.");
            Console.WriteLine();
            classA.ConcealTheCallToControlWillPass();
            
            Console.WriteLine();
            Console.WriteLine("-----------");
            Console.WriteLine();
            
            Console.WriteLine("Example 2: Not Casting to dynamic.");
            Console.WriteLine();
            Console.WriteLine("If we don't cast the argument to (dynamic), the runtime does not use reflection. Below is an exception caused by the methods inability to infer the type of Model.");
            Console.WriteLine();
            classA.ConcealTheCallToControlWillFail();
        }
    }

    public interface IHaveSomeGeneralProperty
    {
        public int Id { get; }
    }

    public class SpecificType : IHaveSomeGeneralProperty
    {
        public int Id { get; set; }
    }

    public class SomeClass
    {
        public IHaveSomeGeneralProperty Model { get; set; }

        public SomeClass(IHaveSomeGeneralProperty model)
        {
            Model = model;
        }

        public void ConcealTheCallToControlWillFail()
        {
            // the compiler is happy, but the runtime has no idea what the specific type of
            // Model will be when the actual variable data is passed to this signature
            Dependency.MethodThatNeedsToKnowT(Model);
        }

        public void ConcealTheCallToControlWillPass()
        {
            // This call uses 'dynamic' to tell the runtime to use reflection to determine the specific 
            // type of Model (which the compiler knows as 'some type that implements IHaveSomeGeneralProperty'
            Dependency.MethodThatNeedsToKnowT((dynamic) Model);
        }
    }

    public static class Dependency
    {
        public static void MethodThatNeedsToKnowT<T>(T model) where T : IHaveSomeGeneralProperty
        {
            if (typeof(T) == typeof(SpecificType)) // We only pass in ModelA here
            {
                Console.WriteLine($"MethodThatNeedsToKnowT Result: Success! The type of T was found to be a specific type: {typeof(T)}");
            }
            else
            {
                throw new Exception($"MethodThatNeedsToKnowT Result: Fail D: The type of T wasn't specific enough. It should be ModelA, but was {typeof(T)}");
            }
        }
    }
}