namespace Defended.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class InjectableAttribute : Attribute
{
    public Type? ImplementationType { get; }
    public ServiceLifetime Lifetime { get; } = ServiceLifetime.Scoped;

    public InjectableAttribute() { }

    public InjectableAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : this()
    {
        Lifetime = lifetime;
    }

    public InjectableAttribute(Type implementationType, ServiceLifetime lifetime = ServiceLifetime.Scoped) : this(lifetime)
    {
        ImplementationType = implementationType;
    }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class InjectableAttribute<T> : InjectableAttribute
{
    public InjectableAttribute() : base(typeof(T)) { }

    public InjectableAttribute(ServiceLifetime lifetime) : base(typeof(T), lifetime) { }
}
