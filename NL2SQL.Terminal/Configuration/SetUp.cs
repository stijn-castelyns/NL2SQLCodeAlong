using Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL2SQL.Terminal.Configuration;
public static class SetUp
{
    public static (AzureKeyCredential? azureKeyCredential, string? deploymentName, Uri? endpoint) LoadConfiguration()
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        AzureKeyCredential? azureKeyCredential = new(config["AzureOpenAI:AzureKeyCredential"]);
        string? deploymentName = config["AzureOpenAI:DeploymentName"];
        Uri? endpoint = new Uri(config["AzureOpenAI:Endpoint"]);

        return (azureKeyCredential, deploymentName, endpoint);
    }
}
