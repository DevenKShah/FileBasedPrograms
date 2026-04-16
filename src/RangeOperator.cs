int[] numbers = [0, 10, 20, 30, 40, 50];
Display(nameof(numbers), numbers); // output: 0 10 20 30 40 50

int[] subset = numbers[1..4]; //LHS is inclusive, RHS is exclusive
Display(nameof(subset), subset);  // output: 10 20 30

int[] lastTwo = numbers[^2..]; // from the second last to the end
Display(nameof(lastTwo), lastTwo); // output: 40 50

int[] allButLastTwo = numbers[..^2]; // from the start to the second last
Display(nameof(allButLastTwo), allButLastTwo); // output: 0 10 20 30

int last = numbers[^1]; // the last element
Console.WriteLine($"Last element: {last}"); // output: Last element: 50

void Display(string desc, IEnumerable<int> xs) => Console.WriteLine($"{desc}:{string.Join(" ", xs)}");
