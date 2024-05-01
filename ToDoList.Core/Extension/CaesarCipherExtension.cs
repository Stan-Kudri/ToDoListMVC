namespace ToDoList.Core.Extension
{
    public static class CaesarCipherExtension
    {
        private static readonly int shiftValue = 3;

        public static string EncryptString(this string str)
            => Cipher(str, shiftValue);

        public static string DecryptString(this string str)
            => Cipher(str, -shiftValue);

        private static string Cipher(string text, int shift)
        {
            char[] buffer = text.ToCharArray();

            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];

                if (char.IsLetter(letter))
                {
                    char letterOffset = char.IsUpper(letter) ? 'A' : 'a';
                    letter = (char)((letter + shift - letterOffset) % 26 + letterOffset);
                }

                buffer[i] = letter;
            }

            return new string(buffer);
        }
    }
}
