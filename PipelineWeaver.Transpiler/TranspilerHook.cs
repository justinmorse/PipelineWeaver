using System.Reflection;
using PipelineWeaver.Ado;

namespace PipelineWeaver.Transpiler;

public class TranspilerHook
{
    public void Run()
    {
        Console.WriteLine("Checking assemblies");
        var pipelineType = typeof(AdoPipeline);

        var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace("/lib","");
        foreach (string dllPath in Directory.GetFiles( assemblyFolder, "*.dll"))
        {
            try
            {
                Console.WriteLine($"Checking assembly {dllPath}");
                // Load the assembly
                Assembly assembly = Assembly.LoadFrom(dllPath);

                // Iterate through all types in the assembly
                foreach (Type type in assembly.GetTypes())
                {
                    // Check if the type inherits from the specified base type
                    if (type.IsClass && pipelineType.IsAssignableFrom(type) && type != pipelineType)
                    {
                        Console.WriteLine($"Class {type.FullName} in {dllPath} inherits from {pipelineType.Name}");
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Handle exceptions for types that cannot be loaded
                Console.WriteLine($"Error loading types from {dllPath}: {ex.Message}");
                foreach (Exception loaderEx in ex.LoaderExceptions)
                {
                    Console.WriteLine($"Loader exception: {loaderEx.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading assembly {dllPath}: {ex.Message}");
            }
        }
        
    }
}