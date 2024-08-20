// See https://aka.ms/new-console-template for more information
using commander;

Console.WriteLine("Commander start.");
var commander = new Commander();
commander.TryConnect();
commander.CommandLoop();