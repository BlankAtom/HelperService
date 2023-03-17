using System.Reflection;
using ETWHost.Core.Mission;

namespace ETWHost.Core.Helper;

public static class AssembliesHelper
{
    public static IEnumerable<Assembly> GetControllerAssembly(this IEnumerable<Assembly> assemblies)
    {
        var array = assemblies.ToArray();
        foreach (var assembly in assemblies)
        {
            foreach (var exportedType in assembly.GetExportedTypes())
            {
                if (exportedType.IsSubclassOf(typeof(IMission)))
                    yield return assembly;
            }
        }

        yield return null;
    }
}