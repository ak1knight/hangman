using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            string word;

            if (args.Length < 1 || args[0] is null)
            {
                word = "Test";
            }
            else
            {
                word = args[0];
            }

            HashSet<string> wordLetters = new HashSet<string>(word.ToCharArray().Select(l => l.ToString().ToLower()));

            Console.WriteLine("Welcome to Hangman!");

            bool won = false;
            bool playing = true;
            HashSet<string> lettersGuessed = new HashSet<string>();
            int failures = 0;
            const int MAX_FAILURES = 6;
            int successes = 0;
            int maxSuccesses = wordLetters.Count;

            while (playing)
            {
                if (lettersGuessed.Any())
                {
                    Console.WriteLine("Letters guessed:");
                    Console.WriteLine(lettersGuessed.Aggregate((arg1, arg2) => arg1 + arg2));
                }

                Console.WriteLine("Current Word:");
                Console.WriteLine(word.ToArray().Select(l => l.ToString().ToLower()).Aggregate((left, next) =>
                    {
                        var total = left;

                        if (total.Length == 1 && !lettersGuessed.Contains(total))
                        {
                            total = "_";
                        }

                        if (lettersGuessed.Contains(next))
                        { 
                            return total + next;
                        }

                        return total + "_";
                    }
                ) + $"({MAX_FAILURES - failures} lives left)");

                Console.WriteLine("Guess a Letter");
                string input = Console.ReadLine().ToLower();

                if (input.Length != 1)
                {
                    Console.WriteLine("Input must be a single letter");
                    continue;
                }

                if (lettersGuessed.Contains(input))
                {
                    Console.WriteLine("Letter already guessed");
                    continue;
                }

                lettersGuessed.Add(input);

                if (wordLetters.Contains(input))
                {
                    Console.WriteLine("Correct!");
                    successes++;
                }
                else
                {
                    Console.WriteLine("Incorrect!");
                    failures++;
                }

                if (successes >= maxSuccesses)
                {
                    won = true;
                    playing = false;
                }
                else if (failures >= MAX_FAILURES)
                {
                    playing = false;
                }
            }

            if (won)
            {
                Console.WriteLine("Congratulations you guessed the word correctly!");
            }
            else
            {
                Console.WriteLine("Sorry, you failed to guess the word in time.");
            }
        }
    }
}
