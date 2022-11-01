# EsoZip

This Repository contains esoteric archive/compression formats.

## HashZip
### Compression Ratio
HashZip boasts an incredible infinite compression ratio - all inputs are compressed to only 64 bytes!
### Performance 
Compression performance is very fast. Decompression may take a while for inputs larger than a few bytes, so make sure you have an inheritance plan to keep the process running to completion. 
### Data Integrity
The SHA-512 and length of the decompressed data will always match the SHA-512 and length of the original data.

### Compression
A hash is taken of the content to be compressed.
```csharp
public static byte[] Compress(byte[] content)
{
    var hasher = SHA512.Create();
    return hasher.ComputeHash(content);
}
```

### Decompression
The hash is reversed.
```csharp
public static byte[]? Decompress(byte[] digest, int fileLength)
{
    var cts = new CancellationTokenSource();
    byte[]? result = null;
    Parallel.For((long)0, byte.MaxValue,
        new ParallelOptions()
            { CancellationToken = cts.Token, MaxDegreeOfParallelism = Environment.ProcessorCount * 2 },
        i =>
        {
            var sha512 = SHA512.Create();
            var currentTest = Enumerable.Repeat((byte)0, fileLength).ToArray();
            currentTest[0] = (byte)i;
            var found = false;
            while (!found)
            {
                if (sha512.ComputeHash(currentTest).SequenceEqual(digest))
                {
                    found = true;
                }
                else
                {
                    IncrementByteArray(currentTest);
                }
            }

            if (found)
            {
                result = currentTest;
                cts.Cancel();
            }
        });
    return result;
}
```

# License
MIT.
