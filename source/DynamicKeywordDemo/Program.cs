using System;

namespace DynamicDispatchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var modelA = new ModelA();
            var classA = new SomeClass(modelA);

            Console.WriteLine();

            classA.ConcealTheCallToControlWillPass();
            Console.WriteLine("When the dynamic keyword is used, the runtime directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.");
            Console.WriteLine();

            Console.WriteLine("Below is an exception caused by the methods inability to infer the type of Model.");
            classA.ConcealTheCallToControlWillFail();
        }
    }

    public interface IHaveSomeGeneralProperty
    {
        public int Id { get; }
    }

    public class ModelA : IHaveSomeGeneralProperty
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
            if (typeof(T) == typeof(ModelA))
            {
                Console.WriteLine($"Success: The type of T was found to be a specific type: {typeof(T)}");
            }
            else
            {
                throw new Exception($"Fail: The type of T wasn't specific enough. It should be ModelA, but was {typeof(T)}");
            }
        }
    }
}