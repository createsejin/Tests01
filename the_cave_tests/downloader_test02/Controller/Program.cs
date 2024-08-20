// See https://aka.ms/new-console-template for more information
using controller;

Console.WriteLine("Controller Server start.");
var commander_messenger = new CommanderMessenger();
commander_messenger.TryConnect_commander_pipe();
Thread commander_thr = new(commander_messenger.Commander_pipe_loop);
commander_thr.Start();
commander_thr.Join();