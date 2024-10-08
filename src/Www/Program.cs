﻿using System.Text.Json;
using Www;

Console.WriteLine("Reading config ...");
using StreamReader reader = new("config.json");
var configString = await reader.ReadToEndAsync();
var config = JsonSerializer.Deserialize<Config>(configString);
Console.WriteLine("Starting server ...");
if(config == null) throw new NullReferenceException("config file not found or cannot read config file.");
var www = new Server(config);

await www.Start();