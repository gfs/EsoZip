using System.Security.Cryptography;
using MoreLinq.Extensions;

namespace EsoZip.Lib;

public static class HashZip2
{
    public record HashZip2Archive(byte[] Digest, Dictionary<byte, int> ByteCounts);

    public static byte[]? Decompress(HashZip2Archive archive)
    {
        var hasher = SHA512.Create();
        byte[]? result = null;
        var byteList = new List<byte>();
        foreach (var byt in archive.ByteCounts)
        {
            var toWrite = Enumerable.Repeat(byt.Key, byt.Value).ToArray();
            byteList.AddRange(toWrite);
        }

        var candidate = byteList.ToArray();
        
        foreach (var permutation in byteList.Permutations())
        {
            permutation.CopyTo(candidate,0);
            var hash = hasher.ComputeHash(candidate);
            if (hash.SequenceEqual(archive.Digest))
            {
                return candidate;
            }
        }

        return result;
    }

    public static HashZip2Archive Compress(byte[] content)
    {
        var hasher = SHA512.Create();
        var hash = hasher.ComputeHash(content);
        var dict = new Dictionary<byte, int>();
        foreach (var byt in content)
        {
            if (!dict.ContainsKey(byt))
            {
                dict.Add(byt, 0);
            }
            dict[byt]++;
        }

        return new HashZip2Archive(hash, dict);
    }
}