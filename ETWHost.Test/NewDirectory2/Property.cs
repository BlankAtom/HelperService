using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace ETWHost.Test.NewDirectory2;

public class Property
{
    private readonly PropertyGetter _getter;
    private readonly PropertySetter _setter;
    public string Name { get; private set; }
    public PropertyInfo Info { get; private set; }

    public Property(PropertyInfo info)
    {
        if (info == null)
            throw new ArgumentNullException(nameof(info));

        Name = info.Name;
        Info = info;

        if (Info.CanRead)
            _getter = new PropertyGetter(Info);
        if (Info.CanWrite)
            _setter = new PropertySetter(Info);
    }

    public object? GetValue(object instance) => _getter?.Invoke(instance);

    public void SetValue(object instance, object value) => _setter?.Invoke(instance, value);

    private static readonly ConcurrentDictionary<Type, Property[]> securityCache =
        new ConcurrentDictionary<Type, Property[]>();

    public static Property[] GetProperties(Type type) =>
        securityCache.GetOrAdd(type, t => t.GetProperties().Select(p => new Property(p)).ToArray());
}

class PropertySetter
{
    private readonly Action<object, object> _delegate;

    public PropertySetter(PropertyInfo property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));

        _delegate = CreateDelegate(property);
    }

    public void Invoke(object instance, object value)
    {
        _delegate?.Invoke(instance, value);
    }
    
    private static Action<object, object> CreateDelegate(PropertyInfo property)
    {
        var instance = Expression.Parameter(typeof(object));
        var value = Expression.Parameter(typeof(object));

        var instanceBody = Expression.Convert(instance, property.DeclaringType);
        var valueBody = Expression.Convert(value, property.PropertyType);
        var call = Expression.Call(instanceBody, property.GetSetMethod(), valueBody);

        return Expression.Lambda<Action<object, object>>(call, instance, value).Compile();
    }
}

internal class PropertyGetter
{
    private readonly Func<object, object> _delegate;

    public PropertyGetter(PropertyInfo info) : this(info?.DeclaringType, info?.Name)
    {
    }

    public PropertyGetter(Type declareType, string propertyName)
    {
        if (declareType == null)
            throw new ArgumentNullException(nameof(declareType));
        if (propertyName == null)
            throw new ArgumentNullException(nameof(declareType));

        _delegate = CreateDelegate(declareType, propertyName);
    }

    public object Invoke(object instance)
    {
        return _delegate?.Invoke(instance);
    }

    private static Func<object, object> CreateDelegate(Type declareType, string propertyName)
    {
        var instance = Expression.Parameter(typeof(object), "instance");
        var instanceCast = declareType.IsValueType
            ? Expression.TypeAs(instance, declareType)
            : Expression.Convert(instance, declareType);
        var typeProperty = Expression.Property(instanceCast, propertyName);
        var result = Expression.Convert(typeProperty, typeof(object));
        return Expression.Lambda<Func<object, object>>(result, instance).Compile();
    }
}