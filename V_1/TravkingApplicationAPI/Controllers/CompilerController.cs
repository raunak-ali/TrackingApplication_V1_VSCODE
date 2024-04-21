using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace TravkingApplicationAPI.Controllers
{
    [Route("[controller]")]
    public class CompilerController : Controller
    {
        [HttpPost]
        [Route("compile")]
        public ActionResult<string> CompileCode([FromBody] string code)
        {
            // Compile the provided C# code
       // Compile the provided C# code
            var compilationResult = CompileCSharpCode(code);

            if (compilationResult.Success)
            {
                // Return a success message or the compiled code
                return Ok("Successfully compiled.");
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
                MetadataReference.CreateFromFile(typeof(System.ComponentModel.DataAnnotations.DataType).Assembly.Location), // System.ComponentModel.Annotations
                MetadataReference.CreateFromFile(typeof(System.Runtime.CompilerServices.AsyncStateMachineAttribute).Assembly.Location) // System.Runtime.Extensions
                // Add additional references as needed
            };

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
                    return new CompilationResult(true, null);
                }
                else
                {
                    var failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    return new CompilationResult(false, failures);
                }
            }
        }

        private class CompilationResult
        {
            public bool Success { get; }
            public IEnumerable<Diagnostic> Diagnostics { get; }

            public CompilationResult(bool success, IEnumerable<Diagnostic> diagnostics)
            {
                Success = success;
                Diagnostics = diagnostics ?? Enumerable.Empty<Diagnostic>();
            }
        }
    }
}