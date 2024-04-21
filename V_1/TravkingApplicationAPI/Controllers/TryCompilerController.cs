using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class TryCompilerController : Controller
    {
         [HttpPost]
        public ActionResult<string> CompileAndExecuteCode( [FromBody]dynamic data)
        {
            CodeExecutionRequest request=new CodeExecutionRequest();
            // Compile the provided C# code
        request.Code=data.GetProperty("Code").ToString().Trim();
        request.SampleInput=data.GetProperty("SampleInput").ToString().Trim();

            var compilationResult = CompileCSharpCode(request.Code);
            if(request.SampleInput==null){
                request.SampleInput="";
            }

            if (compilationResult.Success)
            {
                // Execute the compiled code with the sample input
                var executionResult = ExecuteCompiledCode(compilationResult.Assembly, request.SampleInput);

                // Return the execution result
                return Ok(new {message=executionResult});
            }
            else
            {
                // If compilation fails, return the compilation errors
                var errors = string.Join(Environment.NewLine, compilationResult.Diagnostics.Select(d => d.ToString()));
                return BadRequest($"Compilation failed:{Environment.NewLine}{errors}");
            }
        }

        private CompilationResult CompileCSharpCode(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var assemblyName = Path.GetRandomFileName();

            // Add references to required assemblies
            var references = new List<MetadataReference>
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location), // mscorlib
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location), // System.Runtime
            };
            var runtimeAssembly = Assembly.Load("System.Runtime");
var runtimePath = runtimeAssembly.Location;
var runtimeReference = MetadataReference.CreateFromFile(runtimePath);
references.Add(runtimeReference);

            var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

            var compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: compilationOptions);

            using (var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);

                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    return new CompilationResult(true, null, ms.ToArray());
                }
                else
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    return new CompilationResult(false, failures, null);
                }
            }
        }

       private ExecutionResult ExecuteCompiledCode(byte[] assemblyBytes, string sampleInput)
{
    // Load the assembly into memory
    var assembly = System.Reflection.Assembly.Load(assemblyBytes);

    // Get all types in the assembly
    var types = assembly.GetTypes();

    // Find the type containing the Main method
    var mainType = types.FirstOrDefault(t => t.GetMethod("Main") != null);

    if (mainType == null)
    {
        return new ExecutionResult("Main method not found.");
    }

    // Get the Main method
    var method = mainType.GetMethod("Main");

    // Check if the Main method has the correct signature
    if (!method.IsStatic || method.ReturnType != typeof(void) || method.GetParameters().Length != 0)
    {
        return new ExecutionResult("Main method has incorrect signature.");
    }

    // Redirect standard input to read from sample input
    using (var consoleInput = new StringReader(sampleInput))
    {
        Console.SetIn(consoleInput);

        // Redirect standard output to capture the output
        using (var consoleOutput = new StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Invoke the Main method
            method.Invoke(null, null);

            // Get the output from StringWriter
            var output = consoleOutput.ToString();

            return new ExecutionResult(output);
        }
    }
}
    public class CodeExecutionRequest
    {
        public string Code { get; set; }
        public string SampleInput { get; set; }
    }

    public class CompilationResult
    {
        public bool Success { get; }
        public IEnumerable<Diagnostic> Diagnostics { get; }
        public byte[] Assembly { get; }

        public CompilationResult(bool success, IEnumerable<Diagnostic> diagnostics, byte[] assembly)
        {
            Success = success;
            Diagnostics = diagnostics ?? Enumerable.Empty<Diagnostic>();
            Assembly = assembly;
        }
    }

    public class ExecutionResult
    {
        public string Output { get; }

        public ExecutionResult(string output)
        {
            Output = output;
        }
    }
}
}