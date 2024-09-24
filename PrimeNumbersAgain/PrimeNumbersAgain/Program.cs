using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace PrimeNumbersAgain
{
    class Program
    {
        static void Main(string[] args)
        {
            int n, prime;
            Stopwatch timer = new Stopwatch();

            PrintBanner();
            n = GetNumber();

            timer.Start();
            prime = FindNthPrime(n);
            timer.Stop();

            // outputs a different message if they put in a large number
            if (n > 2000000)
            {
                Console.WriteLine($"{n} is a bit bigger the 2 million");
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine();
                }
                Console.WriteLine($"But it is {prime}");
            }
            else
            {
                Console.WriteLine($"\nToo easy.. {prime} is the nth prime when n is {n}. I found that answer in {timer.Elapsed.Milliseconds} milliseconds.");
                Console.WriteLine($"                                                                 (That is {timer.Elapsed.Seconds} seconds)");

                EvaluatePassingTime(timer.Elapsed.Milliseconds);
            }
        }

        static int FindNthPrime(int n)
        {
            int limit = EstimateLimit(n);

            List<int> primes = SieveOfEratosthenes(limit);

            if (n <= primes.Count)
            {
                return primes[n - 1];
            }
            else
            {
                return -1;
            }
        }

        // Sieve of Eratosthenes to get the primes up to the limit found with the prime theorem
        private static List<int> SieveOfEratosthenes(int limit)
        {
            bool[] isPrime = new bool[limit + 1];
            List<int> primes = new List<int>();

            // set the full isPrime array to true
            for (int i = 2; i <= limit; i++) isPrime[i] = true;

            // for each number we take each of its multiples and set it to false
            // ex: 5 is prime so 5 * anything is not prime
            for (int i = 2; i * i <= limit; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= limit; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            for (int i = 2; i <= limit; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }

            return primes;
        }

        // estimate the limit with the prime theorem
        private static int EstimateLimit(int n)
        {
            if (n < 6)
            {
                return 15;
            }

            double estimatedLimit = n * (Math.Log(n) + Math.Log(Math.Log(n)));
            return (int)estimatedLimit + 1;
        }

        static int GetNumber()
        {
            int n = 0;
            while (true)
            {
                Console.Write("Which nth prime should I find?: ");
                
                string num = Console.ReadLine();
                if (Int32.TryParse(num, out n))
                {
                    return n;
                }

                Console.WriteLine($"{num} is not a valid number.  Please try again.\n");
            }
        }

        static void PrintBanner()
        {
            Console.WriteLine(".................................................");
            Console.WriteLine(".#####...#####...######..##...##..######...####..");
            Console.WriteLine(".##..##..##..##....##....###.###..##......##.....");
            Console.WriteLine(".#####...#####.....##....##.#.##..####.....####..");
            Console.WriteLine(".##......##..##....##....##...##..##..........##.");
            Console.WriteLine(".##......##..##..######..##...##..######...####..");
            Console.WriteLine(".................................................\n\n");
            Console.WriteLine("Nth Prime Solver O-Matic Online..\nGuaranteed to find primes up to 2 million in under 3 seconds!\n\n");
            
        }

        static void EvaluatePassingTime(int time)
        {
            Console.WriteLine("\n");
            Console.Write("Time Check: ");

            if (time <= 3 * 1000)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Pass");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Fail");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            
        }
    }
}
