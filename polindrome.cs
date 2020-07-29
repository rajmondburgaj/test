using System;
using System.Diagnostics;
using System.Linq;

namespace Program
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var watcher = new Stopwatch();
            watcher.Start();
            var isPalindrome = IsPalindrome1("iTopiNonAvevanoNipoti");
            watcher.Stop();
            var elapsed = watcher.Elapsed;
            Console.WriteLine(
                $"Is 'iTopiNonAvevanoNipoti' polindrome using second approach: {isPalindrome}. Evaluated for: {elapsed}");

            watcher = new Stopwatch();
            watcher.Start();
            isPalindrome = IsPalindrome("iTopiNonAvevanoNipoti");
            watcher.Stop();
            elapsed = watcher.Elapsed;
            Console.WriteLine(
                $"Is 'iTopiNonAvevanoNipoti' polindrome using first approach: {isPalindrome}. Evaluated for: {elapsed}");
           
            Console.ReadLine();
        }

        private static bool IsPalindrome(string str, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(str))
                throw new Exception("Please provide a valid string to test");

            var length = str.Length;
            
            if (ignoreCase)
            {
                str = str.ToLower();
            }

            for (int i = 0, j = length - 1; i < length; i++, j--)
            {
                if (str[i] != str[j])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsPalindrome1(string str, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(str))
                throw new Exception("Please provide a valid string to test");

            var first = ignoreCase ? str.ToLower() : str;
            var second = first.Reverse().ToString();
            return first == second;
        }
    }
}
