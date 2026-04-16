using System.ComponentModel;

var ints = Enumerable.Range(1, 1170);

var chunks = ints.Chunk(100);

Console.WriteLine($"There are {chunks.Count()} chunks");
