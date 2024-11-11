using System.Reflection;
using PipelineWeaver.Ado;
using PipelineWeaver.Core.Transpiler.Yaml;

namespace PipelineWeaver.Transpiler;

public class TranspilerHook
{
    public void Run()
    {
        Console.WriteLine("Searching for Project Root");
        
        var root = FindProjectRoot();
        
        Console.WriteLine("Checking assemblies");
        var pipelineType = typeof(AdoPipeline);
        
        if(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) == null)
            throw new Exception("Unable to find the executing assembly");
        var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace("/lib","");
        foreach (var dllPath in Directory.GetFiles( assemblyFolder!, "*.dll"))
        {
            try
            {
                Console.WriteLine($"Checking assembly {dllPath}");
                // Load the assembly
                var assembly = Assembly.LoadFrom(dllPath);

                // Iterate through all types in the assembly
                foreach (var type in assembly.GetTypes())
                {
                    // Check if the type inherits from the specified base type
                    if (!type.IsClass || !pipelineType.IsAssignableFrom(type) || type == pipelineType) continue;

                    var pipelinePath = Path.Combine(root, "ADO", "Pipelines");
                    if(!Directory.Exists(pipelinePath))
                        Directory.CreateDirectory(pipelinePath);

                    var pipeline = Activator.CreateInstance(type) ?? throw new Exception($"Error instantiating {type.Name}");
                    var yamlDoc = new AdoYamlDocument();
                    yamlDoc.BuildPipeline(pipeline);
                    var fileName = $"{ToKebabCase(type.Name)}.yaml";
                    /*var ns = type.Namespace;
                    var pathParts = ns?.Split(".");
                    var outPath = (pathParts ?? []).Aggregate(root, Path.Combine);*/
                    var yamlPath = Path.Combine(pipelinePath, fileName);
                    yamlDoc.Save(yamlPath);

                    Console.WriteLine($"Class {type.FullName} in {dllPath} transpiled to {yamlPath}");
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
    
    public static string FindProjectRoot()
    {
        // Start from the directory where the DLL is running
        string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        // Traverse upwards until finding a directory that likely represents the project root
        while (currentDir != null && !Directory.GetFiles(currentDir, "*.csproj").Any())
        {
            currentDir = Directory.GetParent(currentDir)?.FullName;
        }

        return currentDir ?? throw new Exception("Project root not found.");
    }
    
    public static string ToKebabCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var builder = new System.Text.StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            // If it's uppercase and it's not the first character, add a hyphen before it
            if (char.IsUpper(c) && i > 0)
            {
                builder.Append('-');
            }

            builder.Append(char.ToLower(c));
        }

        return builder.ToString();
    }
    
    
}