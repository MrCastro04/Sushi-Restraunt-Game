using System;
using System.Collections.Generic;

namespace Modules.Core.Extensions
{
    public static class ExtensionKeyGenerator
    {
        public static string GenerateUniqueKey<TValue>(this Dictionary<string, TValue> dictionary, string baseID)
        {
            if (dictionary.ContainsKey(baseID) == false)
                return baseID;

            int numberStartIndex = baseID.Length - 1;

            while (numberStartIndex >= 0 && char.IsDigit(baseID[numberStartIndex]))
                numberStartIndex--;

            numberStartIndex++;

            string lettersPart = baseID.Substring(0, numberStartIndex);
            string numberPart = baseID.Substring(numberStartIndex);

            if (int.TryParse(numberPart, out int number) == false)
                number = 1;

            string newKey;
            do
            {
                number++;
                newKey = lettersPart + number;
            } while (dictionary.ContainsKey(newKey));

            return newKey;
        }
    }
}