using System;

namespace DynamicDispatchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://github.com/paulegradie/DynamicDispatchDemo
            var classA = new SomeClass(new SpecificType());

            Console.WriteLine();
            Console.WriteLine("Example 1: Casting to dynamic.");
            Console.WriteLine();
            Console.WriteLine("When the dynamic keyword is used, the runtime is directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.");
            Console.WriteLine();
            classA.MethodToConstrainTypesPassedToDependency_WillPass();

            Console.WriteLine();
            Console.WriteLine("-----------");
            Console.WriteLine();

            Console.WriteLine("Example 2: Not Casting to dynamic.");
            Console.WriteLine();
            Console.WriteLine("If we don't cast the argument to (dynamic), the runtime does not use reflection. Below is an exception caused by the methods inability to infer the type of T model.");
            Console.WriteLine();
            classA.MethodToConstrainTypesPassedToDependency_WillFail();
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


        public void MethodToConstrainTypesPassedToDependency_WillPass()
        {
            // This call uses 'dynamic' to tell the runtime to use reflection to determine the specific 
            // type of Model (which the compiler knows as 'some type that implements IHaveSomeGeneralProperty'
            Dependency.MethodThatNeedsToKnowT((dynamic) Model);
        }

        public void MethodToConstrainTypesPassedToDependency_WillFail()
        {
            // The compiler is happy, but the runtime has no idea what the specific type of
            // Model will be when the actual variable data is passed to this signature
            Dependency.MethodThatNeedsToKnowT(Model);
        }
    }

    public static class Dependency
    {
        public static void MethodThatNeedsToKnowT<T>(T model) where T : IHaveSomeGeneralProperty
        {
            if (typeof(T) == typeof(SpecificType))
            {
                Console.WriteLine($"MethodThatNeedsToKnowT Result: Success! The type of T was found to be a specific type: {typeof(T)}");
            }
            else
            {
                throw new Exception($"MethodThatNeedsToKnowT Result: Fail D: The type of T wasn't specific enough. It should be {typeof(SpecificType)}, but was {typeof(T)}");
            }
        }
    }
}