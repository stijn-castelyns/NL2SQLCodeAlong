using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using NL2SQL.Infra;
using System.Reflection;
using System.Text.Json;

(AzureKeyCredential? azureKeyCredential, string? deploymentName, Uri? endpoint) = LoadConfiguration();
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

//Console.ForegroundColor = ConsoleColor.Green;
//Console.WriteLine("Assistant > Hello! I am an assistant that let's you use natural language to query the Northwind Sql Server database. How can I be of use to you?");
//Console.ForegroundColor = ConsoleColor.Cyan;
//Console.Write("User > ");
//var request = Console.ReadLine()!;

//Console.ForegroundColor = ConsoleColor.Yellow;
//Console.WriteLine("++++++++ The Generated T-SQL Query ++++++++\n");
//var result = kernel.InvokeAsync(nl2TsqlTranslator, new() {
//            { "request", request } });
//Console.WriteLine(result.Result);
//Console.WriteLine("\n+++++++++++++++++++++++++++++++++++++++++++");



(AzureKeyCredential? azureKeyCredential, string? deploymentName, Uri? endpoint) LoadConfiguration()
{
    IConfigurationRoot config = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    AzureKeyCredential? azureKeyCredential = new(config["AzureOpenAI:AzureKeyCredential"]);
    string? deploymentName = config["AzureOpenAI:DeploymentName"];
    Uri? endpoint = new Uri(config["AzureOpenAI:Endpoint"]);

    return (azureKeyCredential, deploymentName, endpoint);
}