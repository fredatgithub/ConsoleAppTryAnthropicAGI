// See https://aka.ms/new-console-template for more information
using System;
using Anthropic;
using Anthropic.Models.Messages;

Console.WriteLine("Hello, Anthropic!");

AnthropicClient client = new() { ApiKey = "my-anthropic-api-key", MaxRetries = 3 };

MessageCreateParams parameters = new()
{
  MaxTokens = 1024,
  Messages =
    [
        new()
        {
            Role = Role.User,
            Content = "Hello, Claude",
        },
    ],
  Model = "claude-opus-4-6",
};

//var message = await client.Messages.Create(parameters);
var message = await client
    .WithOptions(options =>
        options with
        {
          MaxRetries = 3,
          BaseUrl = "https://api.anthropic.com",
          Timeout = TimeSpan.FromSeconds(42),
        }
    )
    .Messages.Create(parameters);

Console.WriteLine(message);
Console.WriteLine("Press Enter to exit...");
Console.ReadLine();