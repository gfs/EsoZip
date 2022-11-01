using System.Text;
using EsoZip.Lib;

var target = "hello world.";

var encoded2 = HashZip2.Compress(Encoding.UTF8.GetBytes(target));
var decoded2 = HashZip2.Decompress(encoded2);
if (decoded2 != null)
{
    var reconstructed = Encoding.UTF8.GetString(decoded2);
    Console.WriteLine($"Found {reconstructed}");
}

var encoded = HashZip.Compress(Encoding.UTF8.GetBytes(target));
var decoded = HashZip.Decompress(encoded, target.Length);
if (decoded != null)
{
    var reconstructed = Encoding.UTF8.GetString(decoded);
    Console.WriteLine($"Found {reconstructed}");
}