### Dynamic Dispatch using the (dynamic) keyword

Dynamic dispatch is useful in situations where we need to provide extra information about a type at runtime. Provides a working example.

Expected output:
```
Success: The type of T was found to be a specific type: DynamicDispatchDemo.ModelA
When the dynamic keyword is used, the runtime directed to use reflection at runtime to determine the specific type of Model from the variable metadata passed to the signature.

Below is an exception caused by the methods inability to infer the type of Model.
Unhandled exception. System.Exception: Fail: The type of T wasn't specific enough. It should be ModelA, but was DynamicDispatchDemo.IHaveSomeGeneralProperty

```