using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using TravkingApplicationAPI.Services;
using System.Text.Json;
using Xceed.Document.NET;
using Xceed.Words.NET;
using System.IO;
using TravkingApplicationAPI.DTO;
using TravkingApplicationAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;


namespace TravkingApplicationAPI.Controllers
{
    public class TestCLASS
    {
        public string SampleInput { get; set; }
        public string ExpectedOutput { get; set; }
    }
    [Route("[controller]")]
    public class TryCompilerController : Controller
    {

        private readonly TaskServices taskService;
        private readonly TaskSubmissionService taskSubmissionService;

        public TryCompilerController(TaskServices taskService, TaskSubmissionService taskSubmissionService)
        {
            this.taskService = taskService;
            this.taskSubmissionService = taskSubmissionService;

        }
        [HttpPost]
        public async Task<ActionResult> CompileAndExecuteCode([FromBody] dynamic data)
        {
            CodeExecutionRequest request = new CodeExecutionRequest();
            // Compile the provided C# code
            request.Code = data.GetProperty("Code").ToString().Trim();
            request.SampleInput = data.GetProperty("SampleInput").ToString().Trim();
            var subtadkid = data.GetProperty("SubTaskId").GetInt32();

            var compilationResult = CompileCSharpCode(request.Code);
            if (request.SampleInput == null)
            {
                request.SampleInput = "";
            }

            if (compilationResult.Success)
            {
                // Execute the compiled code with the sample input
                //Simple idea here
                //If it compiles uscessfully
                //Find the testcases saved for this particular subtask 
                //parse those testcases and see if each of these execute properly
                //and return the response accordingly
                var responseList = new List<object>();

                var existingTestCases = await taskService.GetSubTasksTestCases(subtadkid);


                foreach (var testcaseString in existingTestCases)
                {

                    // Deserialize the JSON string into a TestCLASS object
                    TestCLASS testobj = JsonSerializer.Deserialize<TestCLASS>(testcaseString);

                    // Access SampleInput and ExpectedOutput properties
                    var SampleInput = testobj.SampleInput;
                    var ExpectedOutput = testobj.ExpectedOutput;
                    var executionResult = ExecuteCompiledCode(compilationResult.Assembly, SampleInput);
                    if (executionResult.ToString().Contains(ExpectedOutput))
                    {
                        Console.WriteLine("SUVESSCULLLLLLLLLLLLLLLLL");

                        //Something to do at Sucessfull testcases passed   
                    }
                    var responseObject = new
                    {
                        Input = SampleInput,
                        ExpectedOutput = ExpectedOutput,
                        ActualOutput = executionResult
                    };

                    // Add the response object to the list
                    responseList.Add(responseObject);

                }


                // Return the execution result
                return Ok(new { message = responseList });
            }
            else
            {
                // If compilation fails, return the compilation errors
                var errors = string.Join(Environment.NewLine, compilationResult.Diagnostics.Select(d => d.ToString()));
                return BadRequest($"Compilation failed:{Environment.NewLine}{errors}");
            }
        }
        [HttpPost("GenerateDocxFile")]
        public async Task<byte[]> GenerateDocxFile([FromBody] string code)
        {
            // Specify the file path
            string filePath = "CodeDocument.txt";

            try
            {
                // Create a new file and write the code content into it
                await System.IO.File.WriteAllTextAsync(filePath, code);

                // Read the file content into a byte array
                byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

                // Return the byte array
                return fileBytes;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Or handle the error accordingly
            }
        }
        // Read the file as bytes


        [HttpPost]
        [AllowAnonymous]
        [Route("SubmitCompiledCode")]
        public async Task<ActionResult> SubmitCompiledCode([FromBody] dynamic data)
        {
            //USED TO SAVE THE SUBMITTED CODE 

            CodeExecutionRequest request = new CodeExecutionRequest();

            AddTaskSubmission tasksubmission = new AddTaskSubmission();
            // Compile the provided C# code
            request.Code = data.GetProperty("Code").ToString().Trim();
            var userid = data.GetProperty("userid").GetInt32();
            //Create a docx file for the CODE

            request.SampleInput = data.GetProperty("SampleInput").ToString().Trim();
            var subtadkid = data.GetProperty("SubTaskId").GetInt32();

            var compilationResult = CompileCSharpCode(request.Code);
            if (request.SampleInput == null)
            {
                request.SampleInput = "";
            }

            if (compilationResult.Success)
            {
                // Execute the compiled code with the sample input
                //Simple idea here
                //If it compiles uscessfully
                //Find the testcases saved for this particular subtask 
                //parse those testcases and see if each of these execute properly
                //and return the response accordingly
                var responseList = new List<object>();

                var existingTestCases = await taskService.GetSubTasksTestCases(subtadkid);

                var testcases_passed = 0;
                var total_testcases=0;
                foreach (var testcaseString in existingTestCases)
                {

                    // Deserialize the JSON string into a TestCLASS object
                    TestCLASS testobj = JsonSerializer.Deserialize<TestCLASS>(testcaseString);

                    // Access SampleInput and ExpectedOutput properties
                    var SampleInput = testobj.SampleInput;
                    var ExpectedOutput = testobj.ExpectedOutput;
                    var executionResult = ExecuteCompiledCode(compilationResult.Assembly, SampleInput);
                    var normalizedExecutionResult = executionResult.Output.ToString().Trim().ToLower().Replace(" ", "");
                    var normalizedExpectedOutput = ExpectedOutput.Trim().ToLower().Replace(" ", "");

                    // Compare the normalized strings
                    bool result = normalizedExecutionResult.Equals(normalizedExpectedOutput);
                    total_testcases=total_testcases+1;

                    if (result)
                    {
                        testcases_passed = testcases_passed + 1;

                        //Something to do at Sucessfull testcases passed   
                    }
                    var responseObject = new
                    {
                        Input = SampleInput,
                        ExpectedOutput = ExpectedOutput,
                        ActualOutput = executionResult
                    };

                    // Add the response object to the list
                    responseList.Add(responseObject);

                }
                var filebytearray = await GenerateDocxFile(request.Code);
                tasksubmission.FileUploadSubmission = filebytearray;
                tasksubmission.UserId = userid;
                tasksubmission.SubTaskSubmitteddOn = DateTime.Now;
                tasksubmission.subtaskid = subtadkid;
                string jsonString = JsonSerializer.Serialize(responseList);
                tasksubmission.Test_cases_passed = testcases_passed;
                tasksubmission.Result = jsonString;
                tasksubmission.status = status.Complted;
                tasksubmission.submittedFileName = "CodeDocument.txt";

                var res = await taskSubmissionService.AddSubmission(tasksubmission);
                var submission = await taskSubmissionService.GetSubmOfaSubtaskbyUser(subtadkid, userid);
                string pattern = @"TaskSubmissionsId"":(\d+)";
                Match match = Regex.Match(submission, pattern);
                string numberStr = match.Groups[1].Value;

                // Convert the captured number from string to integer
                int number = int.Parse(numberStr);

                //var temp = JsonSerializer.Deserialize<dynamic>(submission);//Add the rating according to passed testcases as well

                //var taskSubmissions=JsonSerializer.Deserialize<TaskSubmissions>(temp);
                AddRating addrating = new AddRating();
                var temp=(int)Math.Round((double)testcases_passed / total_testcases * 100);
                addrating.RatingValue = temp;
                addrating.RatedTo = userid;
                addrating.RatedBy = userid;
                addrating.TaskSubmissionId = number;
                addrating.Comments = Comments.Average;
                try
                {
                    var rep = await taskSubmissionService.RateASubmittedTask(addrating);
                    Console.WriteLine(rep);
                }
                catch (Exception e) { }

                if (res != null)
                {
                    // Return the execution result
                    return Ok(new { message = res });
                }
                else
                {
                    return BadRequest();
                }
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