// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var filePath1 = @"E:\CloudMusic\KSHMR,Mark Sixma - Gladiator (Remix).pcm";
var filePath2 = @"E:\CloudMusic\西木康智 - バトル1.pcm";

using var file1 = File.OpenRead(filePath1);
using var file2 = File.OpenRead(filePath2);
using var output = File.Create("output.pcm");

using var reader1 = new BinaryReader(file1);
using var reader2 = new BinaryReader(file2);
using var outputWriter = new BinaryWriter(output);

try
{
    while (true)
    {
        var value1 = reader1.ReadSingle();
        var value2 = reader2.ReadSingle();
        var outputValue = value1 / 2 + value2 / 2;

        outputWriter.Write(outputValue);
    }
}
catch { }