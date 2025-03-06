using System;
using System.Security.Cryptography;
using System.Text;

namespace mypass.PasswordTools
{
    internal static class Generator
    {
        private const string Lowers = "abcdefghijklmnopqrstuvwxyz";
        private const string Uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numbers = "0123456789";
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        private const int MinPasswordLength = 10;
        private const int MaxPasswordLength = 15;

        public static string GeneratePassword()
        {
            int length = GetRandomInt(MinPasswordLength, MaxPasswordLength + 1);

            int numerics = GetRandomInt(2, length / 3); 
            int uppercase = GetRandomInt(2, (length - numerics) / 2); 
            int lowercase = GetRandomInt(2, (length - numerics - uppercase)); 
            int special = length - (numerics + uppercase + lowercase); 

            return GenerateToken(lowercase, uppercase, numerics, special);
        }

        /// <summary>
        /// </summary>
        /// <param name="lowercase">Количество строчных букв</param>
        /// <param name="uppercase">Количество заглавных букв</param>
        /// <param name="numerics">Количество цифр</param>
        /// <param name="special">Количество специальных символов</param>
        /// <returns>Сгенерированный пароль</returns>
        private static string GenerateToken(int lowercase, int uppercase, int numerics, int special)
        {
            StringBuilder passwordBuilder = new StringBuilder();

            AddRandomCharacters(passwordBuilder, Lowers, lowercase);

            AddRandomCharacters(passwordBuilder, Uppers, uppercase);

            AddRandomCharacters(passwordBuilder, Numbers, numerics);

            AddRandomCharacters(passwordBuilder, SpecialChars, special);

            char[] passwordArray = passwordBuilder.ToString().ToCharArray();
            ShuffleArray(passwordArray);

            return new string(passwordArray);
        }

        /// <summary>
        /// </summary>
        /// <param name="builder">StringBuilder для добавления символов</param>
        /// <param name="charSet">Набор символов</param>
        /// <param name="count">Количество символов для добавления</param>
        private static void AddRandomCharacters(StringBuilder builder, string charSet, int count)
        {
            byte[] randomBytes = new byte[1];
            using (var rng = RandomNumberGenerator.Create())
            {
                for (int i = 0; i < count; i++)
                {
                    rng.GetBytes(randomBytes);
                    int index = randomBytes[0] % charSet.Length;
                    builder.Append(charSet[index]);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="array">Массив символов</param>
        private static void ShuffleArray(char[] array)
        {
            byte[] randomBytes = new byte[array.Length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);

                for (int i = 0; i < array.Length; i++)
                {
                    int swapIndex = randomBytes[i] % array.Length;
                    (array[i], array[swapIndex]) = (array[swapIndex], array[i]);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="minValue">Минимальное значение (включительно)</param>
        /// <param name="maxValue">Максимальное значение (исключительно)</param>
        /// <returns>Случайное целое число</returns>
        private static int GetRandomInt(int minValue, int maxValue)
        {
            if (minValue >= maxValue)
                throw new ArgumentException("minValue must be less than maxValue.");

            byte[] randomBytes = new byte[4]; // 4 байта для int32
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                int value = BitConverter.ToInt32(randomBytes, 0);
                return Math.Abs(value % (maxValue - minValue)) + minValue;
            }
        }
    }
}