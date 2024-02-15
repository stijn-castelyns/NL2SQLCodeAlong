using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using NL2SQL.Terminal.Configuration;
using NL2SQL.Terminal.Infra;
using System.Data;
using System.Reflection;
using System.Text.Json;
using System.Xml;

(AzureKeyCredential? azureKeyCredential, string? deploymentName, Uri? endpoint) = SetUp.LoadConfiguration();
OpenAIClient openAIClient = new(endpoint, azureKeyCredential);

IKernelBuilder builder = Kernel.CreateBuilder();
builder.Services.AddAzureOpenAIChatCompletion(deploymentName, openAIClient);

Kernel kernel = builder.Build();

// Load prompt from YAML
var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("NL2SQL.Terminal.Prompts.nl2tsqlTranslator.prompt.yaml")!;
using StreamReader reader = new(stream);
KernelFunction nl2TsqlTranslator = kernel.CreateFunctionFromPromptYaml(
    reader.ReadToEnd(),
    promptTemplateFactory: new HandlebarsPromptTemplateFactory()
);

Terminal.PrintAssistantMessage("Hello! I am an assistant that let's you use natural language to query the Northwind Sql Server database. How can I be of use to you?");
string request = Terminal.GetUserInput();

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("++++++++ The Generated T-SQL Query ++++++++\n");
var result = kernel.InvokeAsync(nl2TsqlTranslator, new() {
            { "request", request } });
string generatedQuery = result.Result.ToString();
Console.WriteLine(generatedQuery);
Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++");

Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("+++++++++ The Results of the Query ++++++++\n");

using (IDbConnection db = new SqlConnection("Data Source=(localdb)\\tsql;Initial Catalog=Northwind;Integrated Security=True;Encrypt=True"))
{
    var queryResult = db.Query(generatedQuery);
    var options = new JsonSerializerOptions
    {
        WriteIndented = true
    };
    var json = JsonSerializer.Serialize(queryResult,options);
    Console.WriteLine(json);
}
Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++");
Console.ForegroundColor = ConsoleColor.White;
