using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace ETWHost.Test.NewDirectory2;

public class Data<T> where T :class
{
    // 存储值的对象
    public object? Value;
    
    // 存储属性名的集合
    public IList<string> PropertyNames;
    
    // 存储类型的属性
    public readonly Type TargetType = typeof(T);

    public ConcurrentDictionary<string, Property> Properties = new ConcurrentDictionary<string, Property>();

    public Data()
    {
        if (TargetType.IsGenericType)
            throw new ArgumentException("Can not use generic type");
    }

    public void AddProperty(string key)
    {
        var info = TargetType.GetProperty(key);
        if (info == null)
            throw new NullReferenceException(key);

        Properties.GetOrAdd(key, new Property(info));
    }

    public void DeleteProperty(string key)
    {
        var containsKey = Properties.ContainsKey(key);
        if (!containsKey) return;
        
        containsKey &= Properties.TryRemove(key, out var _);
        if (!containsKey)
            throw new Exception("Remove property failed! About of: " + key);
    }

    public object GetPropertyValue(string key)
    {
        var success = Properties.TryGetValue(key, out var value);
        if (!success)
            throw new ArgumentNullException(key);

        if (value == null)
            throw new NullReferenceException("Current object is null.");
        return value.GetValue(Value);
    }
    
}