// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using FluentValidationDemo;

Console.WriteLine("Show me...");

var result = new PersonValidator().Validate(new Person());

Console.WriteLine(
    JsonSerializer.Serialize(result, new JsonSerializerOptions{ WriteIndented = true }));