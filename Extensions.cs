using System;
using System.Collections.Generic;

namespace BierPongTurnier
{
    public class Extensions
    {
        public static void Shuffle(IList<string> list, Random random)
        {
            int n = list.Count;
            int k;
            string value;
            while (n > 1)
            {
                n--;
                k = random.Next(n + 1);
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}