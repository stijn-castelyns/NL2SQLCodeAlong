using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NL2SQL.Terminal.Infra;
internal static class Terminal
{
    internal static void PrintAssistantMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Assistant > {message}");
        Console.ForegroundColor = ConsoleColor.White;
    }

    internal static string GetUserInput()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("User > ");
        string userInput = Console.ReadLine()!;
        Console.ForegroundColor = ConsoleColor.White;
        return userInput;
    }
}
