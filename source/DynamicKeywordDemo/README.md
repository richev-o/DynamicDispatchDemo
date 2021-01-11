## Dynamic Dispatch using the (dynamic) keyword

Dynamic dispatch is useful in situations where we need to provide extra information about a type at runtime.

This project provides a working example of how a unspecific type can make its way to the signature of a polymorphic function that requires knowledge of the specific type of the variable (specified generically as `T`) passed via its signature.

`SomeClass` contains a property that is typed as an unspecific interface. This satisifies the compiler. Later this property is passed to a polymorphic method (`MethodThatNeedsToKnowT`) that requires that the specific type at runtime be made availe at some point up the call stack. Since we place a constraint on the generic `T` of the polymorphic function `MethodThatNeedsToKnowT`, the compiler is satisfied. (The compiler would indeed be satisfied without this constraint, however we as engineers would not be satisfied!)

The compiler is blissfully unaware that, in our setup, this method will be unable to correctly resolve the type of `T` at runtime, since the specific type of `T` is never concretely provided. Therefore, at runtime, this code with throw.

To direct the runtime to retrieve the specific type of information, we make use of **Dynamic Dispatch**, which is the process of dynamically providing type information at runtime. Dynamic dispatch is a concept, not an implementation detail, therefore there are multiple ways to achieve this. For example, we could consider using a function pointer patter where we map a type to a method. This approach may not be suitable in all circumstances.

Here, we use the `dynamic` keyword and mark the property as dynamic as we pass it to the polymorphic method. This directs the runtime to use reflection to retrieve the specific type of the variable (provided as parameter metadata) and assign it to the signature parameter type `T`.

## Expected output

This small program simply writes a few lines to stdout and throws an exception with a message describing the result of the experiment.

Expected output:
```
Example 1: Casting to dynamic.
When the dynamic keyword is used, the runtime directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.
MethodThatNeedsToKnowT Result: Success! The type of T was found to be a specific type: DynamicDispatchDemo.SpecificType

Example 2: Not Casting to dynamic.
If we don't cast the argument to (dynamic), the runtime does not use reflection. Below is an exception caused by the methods inability to infer the type of Model.

Unhandled exception. System.Exception: MethodThatNeedsToKnowT Result: Fail D: The type of T wasn't specific enough. It should be ModelA, but was DynamicDispatchDemo.IHaveSomeGeneralProperty
```

