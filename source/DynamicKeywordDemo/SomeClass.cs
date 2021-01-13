namespace DynamicDispatchDemo
{
    public class SomeClass
    {
        public ISomeInterface Model { get; }

        public SomeClass(ISomeInterface model)
        {
            Model = model;
        }
    }
}