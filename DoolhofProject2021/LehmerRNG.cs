using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    /// <summary>
    /// Class that implements a much faster and better Random Number Generator.
    /// 
    /// </summary>
    public class LehmerRNG
    {
        private static ulong nLehmer = (ulong)DateTime.Now.Ticks;

        public static void initLehmer(ulong seed)
        {
            nLehmer = seed;
        }

        public static int Next(int max)
        {
            return (int)(Lehmer32() % max);
        }

        public static bool NextBool()
        {
            return (Lehmer32() & 1) == 1;
        }

        /// <summary>
        /// Code conveniently copied from: https://youtu.be/ZZY9YE7rZJw?t=978
        /// </summary>
        /// <returns>Choose a number with random bits.</returns>
        public static uint Lehmer32()
        {
            nLehmer += 0xe120fc15;
            ulong temp;
            temp = (ulong)nLehmer * 0x4a39b70d;
            uint m1 = (uint)QuickPow((temp >> 32), temp);
            temp = (ulong)m1 * 0x12fed5c9;
            uint m2 = (uint)QuickPow((temp >> 32), temp);
            return m2;
        }

        private static ulong QuickPow(ulong value, ulong power)
        {
            ulong result = 0;
            ulong pvalue = value;
            ulong remainder = power;
            while (remainder != 0)
            {
                if ((remainder & 1) == 1)
                {
                    result += pvalue;
                }
                remainder >>= 2;
            }
            return result;
        }
    }
}
