using System;
using System.Collections.Generic;

namespace Facebook.Problems
{
    public class RotationString
    {
        public static string RotationalCipher(string input, int rotationFactor) {
            // Write your code here
            if (string.IsNullOrWhiteSpace(input)) return "";
            if (rotationFactor == 0) return input;
    
            var literaFactor = rotationFactor % 26;
            var digitFactor = rotationFactor % 10;
        
            var rotated = new char[input.Length];       //space: O(N)
            for (int i = 0; i < input.Length; i++)      //input.Length * {1, 3*const*const} => time O(N)
            {
                if (char.IsDigit(input[i]))
                    rotated[i] = RotatedDigit(input[i], digitFactor);
                else if (IsUpperCaseLitera(input[i]))
                    rotated[i] = RotateUpperCaseLitera(input[i], literaFactor);
                else if (IsLowerCaseLitera(input[i]))
                    rotated[i] = RotateLowerCaseLitera(input[i], literaFactor);
                else
                    rotated[i] = input[i];
            }
    
            return new string(rotated);
        }
  
        private static string _digits = "0123456789";
  
        private static char RotatedDigit(char digit, int digitFactor)
        {
            //1. char->int, +factor, %10, int->char
            //2. ANSII 0 : 49 -> char+factor % '9'...
            //3. lookup
            var rotatedDigitIndex = (_digits.IndexOf(digit) + digitFactor)%10;
            return _digits[rotatedDigitIndex];
        }
  
        private static string _upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static HashSet<char> _upperCaseLookup = new HashSet<char>(_upperCase);

        private static bool IsUpperCaseLitera(char litera)
            => _upperCaseLookup.Contains(litera);
  
        private static char RotateUpperCaseLitera(char litera, int literaFactor)
        {
            var rotatedLiteraIndex = (_upperCase.IndexOf(litera) + literaFactor)%26;
            return _upperCase[rotatedLiteraIndex];
        }
  
        private static string _lowerCase = "abcdefghijklmnopqrstuvwxyz";
        private static HashSet<char> _lowerCaseLookup = new HashSet<char>(_lowerCase);

        private static bool IsLowerCaseLitera(char litera)
            => _lowerCaseLookup.Contains(litera);
  
        private static char RotateLowerCaseLitera(char litera, int literaFactor)
        {
            var rotatedLiteraIndex = (_lowerCase.IndexOf(litera) + literaFactor)%26;
            return _lowerCase[rotatedLiteraIndex];
        }
    }
}