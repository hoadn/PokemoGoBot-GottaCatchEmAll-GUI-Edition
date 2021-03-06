﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace RocketAPI.Helpers
{
    public class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static long GetLongRandom(long min, long max)
        {
            var buf = new byte[8];
            Random.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return Math.Abs(longRand % (max - min)) + min;
        }

        public static double RandomRoundDouble(double minimum, double maximum, int roundCases)
        {
            Random rand = new Random();
            var next = rand.NextDouble();
            // even the cut should be random to look like more real stuff, too big number would be double size
            var nextFixed = Math.Round(next, RandomNumber(1, roundCases), MidpointRounding.AwayFromZero);
            return minimum + (nextFixed * (maximum - minimum));
        }

        public static int GetNumberOfDigits(double d)
        {
            double abs = Math.Abs(d);
            return abs < 1 ? 0 : (int)(Math.Log10(abs) + 1);
        }

        public static async Task RandomDelay(int delay)
        {
            var randomFactor = 0.3f;
            var randomMin = (int)(delay * (1 - randomFactor));
            var randomMax = (int)(delay * (1 + randomFactor));
            var randomizedDelay = Random.Next(randomMin, randomMax);

            await Task.Delay(randomizedDelay);
        }

        public static async Task RandomDelay(int min, int max)
        {
            await Task.Delay(Random.Next(min, max));
        }

        public static void RandomSleep(int min, int max)
        {
            Thread.Sleep(Random.Next(min, max));
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static double RandomWalkSpeed(double delay)
        {
            var randomFactor = 0.3f;
            var randomMin = (int)(delay * (1 - randomFactor));
            var randomMax = (int)(delay * (1 + randomFactor));

            return Random.Next(randomMin, randomMax);
        }

        public static string RandomString(int length, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            var outOfRange = Byte.MaxValue + 1 - (Byte.MaxValue + 1) % alphabet.Length;

            return string.Concat(
                Enumerable
                    .Repeat(0, Int32.MaxValue)
                    .Select(e => RandomByte())
                    .Where(randomByte => randomByte < outOfRange)
                    .Take(length)
                    .Select(randomByte => alphabet[randomByte % alphabet.Length])
            );
        }

        public static byte RandomByte()
        {
            using (var randomizationProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[1];
                randomizationProvider.GetBytes(randomBytes);
                return randomBytes.Single();
            }
        }
    }
}