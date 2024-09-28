List<int> numbers = [];
foreach (int current in Enumerable.Range(1, 100))
{
    numbers.Add(current);
}

List<string> outputList = [];
for (int i = 0; i < 100; i++)
{
    int current = numbers[i];
    string output = current.ToString();

    if (Fizz(current)) output = "Fizz";
    if (Buzz(current)) output = "Buzz";
    if (Fizz(current) && Buzz(current)) output = "FizzBuzz";
            
    outputList.Add(output);
}
Console.WriteLine(string.Join("\n", outputList));

bool Fizz(int i)
{
    int current = 0;
    while (true)
    {
        current += 3;
        if (current > i) return false;
        if (current == i) return true;
    }
}

bool Buzz(int i)
{
    string number = i.ToString();
    char last = number[number.Length - 1];
    return last == '5' || last == '0';
}
