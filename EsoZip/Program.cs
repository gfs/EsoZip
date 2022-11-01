// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using EsoZip.Lib;

var target = "hello world.";
var hash = SHA512.Create();
var encoded = EsoZipper.Compress(Encoding.UTF8.GetBytes(target));
var decoded = EsoZipper.Decompress(encoded, target.Length);
if (decoded != null)
{
    var reconstructed = Encoding.UTF8.GetString(decoded);
    Console.WriteLine($"Found {reconstructed}");
}