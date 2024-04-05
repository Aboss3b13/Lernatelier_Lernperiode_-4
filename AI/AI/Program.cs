using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static double[] value = new double[3187];
    static double[] finval = new double[3187];
    static string[] result = new string[9999999];
    static string[] fields;
    static double[] numbers;
    static string[] strings;
    static double[] rearrangedNumbers;
    static string[] rearrangedStrings;
    static int count2 = 0;
    static int count3 = 0;
    static string[] words2;
    static int count = 0;

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Enter a sentence:");
        string sentence = Console.ReadLine();
        string[] words = sentence.Split(new char[8] { ' ', ',', '.', '?', '!', ';', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Do you mean:");

        foreach (string word in words)
        {
            result[count] = Identifier(word.ToLower());
            Console.Write(result[count] + " ");
            count++;
            ResetVariables();
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    static string Identifier(string input)
    {
        count2 = 0;
        count3 = 0;
        string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        string csvFilePath = Path.Combine(downloadsPath, "Library1V1.1.csv");
        string[] lines = File.ReadAllLines(csvFilePath);
        words2 = new string[lines.Length];
        for (int l = 0; l < lines.Length; l++)
        {
            fields = lines[l].Split(',');
            words2[l] = fields[0].ToLower();
        }
        for (int inputIndex = 0; inputIndex < input.Length; inputIndex++)
        {
            char c = input[inputIndex];
            for (int wordsIndex = 0; wordsIndex < words2.Length; wordsIndex++)
            {
                string word2 = words2[wordsIndex];
                count2 = 0;
                bool found = false;
                for (; count2 < word2.Length; count2++)
                {
                    if (char.ToLower(c) == word2[count2])
                    {
                        if (count2 == inputIndex)
                        {
                            value[wordsIndex] += 2;
                        }
                        else
                        {
                            value[wordsIndex] += 1;
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    value[wordsIndex] += 0.0;
                }
            }
        }
        count3 = 0;
        for (count2 = 0; count2 < words2.Length; count2++)
        {
            if (words2[count2].Length == input.Length)
            {
                finval[count2] = value[count2] / (double)input.Length;
            }
            if (words2[count2].Length > input.Length)
            {
                finval[count2] = value[count2] / (double)words2[count2].Length;
            }
            else
            {
                finval[count2] = value[count2] / (double)input.Length;
            }
            if (words2[count2] == input)
            {
                return words2[count2];
            }
        }
        numbers = finval;
        strings = words2;
        int closestIndex = 0;
        double minDifference = Math.Abs(numbers[0] - 2);
        for (int k = 1; k < numbers.Length; k++)
        {
            double difference = Math.Abs(numbers[k] - 2);
            if (difference < minDifference)
            {
                closestIndex = k;
                minDifference = difference;
            }
        }
        rearrangedNumbers = new double[numbers.Length];
        for (int j = 0; j < numbers.Length; j++)
        {
            rearrangedNumbers[j] = numbers[(closestIndex + j) % numbers.Length];
        }
        rearrangedStrings = new string[strings.Length];
        for (int i = 0; i < rearrangedNumbers.Length; i++)
        {
            int index = Array.IndexOf(numbers, rearrangedNumbers[i]);
            rearrangedStrings[i] = strings[index];
        }
        count2 = 0;
        count3 = 0;
        return rearrangedStrings[0];
    }

    static void ResetVariables()
    {
        Array.Clear(value, 0, value.Length);
        Array.Clear(finval, 0, finval.Length);
        Array.Clear(result, 0, result.Length);
    }
}
