// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.GTA5.Cryptography;
using RageLib.Services;
using StructsDumper.GTA5;
using System.CommandLine;

var keysOptions = new Option<string>(new[] { "--keys", "-k" }) { IsRequired = true, Description = "The path to the folder containing the game keys" };
var gameOptions = new Option<string>(new[] { "--game", "-g" }) { IsRequired = true, Description = "The path to the folder where the game is installed" };
var stringsOptions = new Option<string?>(new[] { "--strings", "-s" }) { IsRequired = false, Description = "The path to the file containing the strings for the Joaat lookup" };

var command = new RootCommand();
command.AddGlobalOption(keysOptions);
command.AddGlobalOption(gameOptions);
command.AddGlobalOption(stringsOptions);

var psoCommand = new Command("pso", "Dump PSO");
psoCommand.SetHandler((keysPath, gamePath, stringsPath) =>
{
    Dump(keysPath, gamePath, stringsPath, true, false);
}, keysOptions, gameOptions, stringsOptions);

var metaCommand = new Command("meta", "Dump META");
metaCommand.SetHandler((keysPath, gamePath, stringsPath) =>
{
    Dump(keysPath, gamePath, stringsPath, false, true);
}, keysOptions, gameOptions, stringsOptions);

var allCommand = new Command("all", "Dump PSO and META");
allCommand.SetHandler((keysPath, gamePath, stringsPath) =>
{
    Dump(keysPath, gamePath, stringsPath, true, true);
}, keysOptions, gameOptions, stringsOptions);

command.AddCommand(psoCommand);
command.AddCommand(metaCommand);
command.AddCommand(allCommand);

await command.InvokeAsync(args);

static void Dump(string keysPath, string gamePath, string? stringsPath, bool dumpPso, bool dumpMeta)
{
    GTA5Constants.LoadFromPath(keysPath);

    var joaatDictionary = JenkinsDictionary.Shared;

    if(stringsPath is not null)
    {
        joaatDictionary.AddFromFile(stringsPath);
    }

    //joaatDictionary.AddFromFile("Lists\\MetaNames.txt");
    //joaatDictionary.AddFromFile("Lists\\PsoTypeNames.txt");
    //joaatDictionary.AddFromFile("Lists\\PsoFieldNames.txt");
    //joaatDictionary.AddFromFile("Lists\\PsoEnumValues.txt");
    //joaatDictionary.AddFromFile("Lists\\PsoCommon.txt");
    //joaatDictionary.AddFromFile("Lists\\FileNames.txt");
    //joaatDictionary.AddFromFile("UserDictionary.txt");

    if (dumpPso) new PsoDumper().Dump(gamePath);
    if (dumpMeta) new MetaDumper().Dump(gamePath);
}