namespace TechnicalTest.Anagram;
public static class Anagram
{
    public static bool IsAnagram(string first, string second)
    {
        first = first.Replace(" ", "");
        second = second.Replace(" ", "");
        if (first.Length != second.Length) return false;
        
        Dictionary<char, int> firstFrequencies = GetCharFrequencies(first);
        Dictionary<char, int> secondFrequencies = GetCharFrequencies(second);

        // iterate through each char to see if they occur the same amount of times in the second word
        foreach((char firstCharacter, int firstFrequency) in firstFrequencies)
        {
            if (!secondFrequencies.TryGetValue(firstCharacter, out int secondFrequency)) return false;
            if (firstFrequency != secondFrequency) return false;
        }

        return true;
    }

    public static Dictionary<char, int> GetCharFrequencies(string word)
    {
        Dictionary<char, int> frequencies = [];

        foreach(char character in word.ToLower())
        {
            if (frequencies.ContainsKey(character)) frequencies[character]++;
            else frequencies.Add(character, 1);
        }

        return frequencies;
    }
}
