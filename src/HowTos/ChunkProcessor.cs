using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


int[] ids = Enumerable.Range(1, 100).ToArray();
const int chunkSize = 9;
CancellationToken ct = new();

var chunks = ids.Chunk(chunkSize).ToList();
Console.WriteLine($"Total items : {ids.Length}");
Console.WriteLine($"Chunk size  : {chunkSize}");
Console.WriteLine($"Chunks      : {chunks.Count}");
Console.WriteLine(new string('-', 40));

var tasks = chunks.Select((chunk, index) => CallApiAsync(chunk, index, ct)).ToList();

// Process completions in arrival order, not submission order.
await foreach (var finished in Task.WhenEach(tasks).WithCancellation(ct))
{
    try
    {
        var (chunkIndex, response) = await finished;
        Console.WriteLine($"[Chunk {chunkIndex,3}] ✓ Finished — status: {response.Status}, " +
                          $"ids processed: {response.Ids.Length}");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"[ERROR] Chunk failed: {ex.Message}");
    }
}

Console.WriteLine(new string('-', 40));
Console.WriteLine("All chunks processed.");

static async Task<(int ChunkIndex, ApiResponse Response)> CallApiAsync(
    int[] chunk, int index, CancellationToken ct)
{
    Console.WriteLine($"[Chunk {index,3}] Dispatched — {chunk.Length} ids");
    await Task.Delay(new Random().Next(500, 1500), ct); // Simulate network latency before API call
    var waitFor = new Random().Next(500, 2500);
    if (waitFor > 1200)
    {
        throw new Exception($"Simulated API failure for chunk {index}");
    }
    var response = await Task.Delay(waitFor, ct)
        .ContinueWith(_ => new ApiResponse("OK", chunk), ct);
    return (index, response);
}

record ApiResponse(string Status, int[] Ids);
