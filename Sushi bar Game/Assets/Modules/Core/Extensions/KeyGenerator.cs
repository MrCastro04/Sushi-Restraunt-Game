using System.Collections.Generic;

namespace Modules.Core.Extensions
{
    public static class KeyGenerator
    {
        public static string GenerateUniqueKey<TValue>(this Dictionary<string, TValue> dictionary, string baseID)
        {
            if (dictionary.ContainsKey(baseID) == false)
                return baseID;

            // Ключ занят - генерируем новый
            // Формат: буквы + цифра(ы), например "IT1", "FG12", "MC2"

            // Находим индекс где начинается число (идём с конца)
            
            int numberStartIndex = baseID.Length - 1;

            while (numberStartIndex >= 0 && char.IsDigit(baseID[numberStartIndex]))
                numberStartIndex--;

            numberStartIndex++; // Переходим на первую цифру

            // Разделяем на буквенную и числовую части
            string lettersPart = baseID.Substring(0, numberStartIndex);
            string numberPart = baseID.Substring(numberStartIndex);

            // Парсим число или начинаем с 1 если числа нет
            if (int.TryParse(numberPart, out int number) == false)
                number = 1;

            // Ищем свободный ключ, увеличивая число
            string newKey;
            do
            {
                number++;
                newKey = lettersPart + number;
            }
            while (dictionary.ContainsKey(newKey));

            return newKey;
        }
    }
}