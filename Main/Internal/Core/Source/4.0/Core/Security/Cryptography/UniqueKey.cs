//// The original article
//// http://www.codeproject.com/Articles/14403/Generating-Unique-Keys-in-Net

namespace EyeSoft.Security.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class UniqueKey
    {
        private static readonly IDictionary<UniqueKeyMethod, Func<string>> uniqueKeysDictionary =
            new Dictionary<UniqueKeyMethod, Func<string>>
                {
                    { UniqueKeyMethod.Guid, FromGuid },
                    { UniqueKeyMethod.Ticks, FromTicks },
                    { UniqueKeyMethod.DateTime, FromDateTime },
                    { UniqueKeyMethod.RngCharacterMask, FromRngMask },
                };

        public static string From(UniqueKeyMethod uniqueKeyMethod)
        {
            return uniqueKeysDictionary[uniqueKeyMethod]();
        }

        public static string FromGuid()
        {
            var result = Guid.NewGuid().ToString().GetHashCode().ToString("x");
            return result;
        }

        public static string FromTicks()
        {
            var val = DateTime.Now.Ticks.ToString("x");
            return val;
        }

        public static string FromDateTime()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture).GetHashCode().ToString("x");
        }

        public static string FromRngMask()
        {
            return FromRng(CharSet.LowerCase, CharSet.UpperCase, CharSet.UpperCase);
        }

        public static string FromRng(params IEnumerable<char>[] charSet)
        {
            return FromRng(8, charSet);
        }

        public static string FromRandomNumberGeneratedComplex(int size = 12)
        {
            return FromRng(size);
        }

        public static string FromRng(int size = 8, params IEnumerable<char>[] charSet)
        {
            if (charSet == null | !charSet.Any())
            {
                charSet = CharSet.Complex.ToArray();
            }

            if (size < charSet.Length)
            {
                throw new ArgumentException("The lenght of the generated text must be equal or greather than the charset set.");
            }

            var generated = new StringBuilder();

            var charsetList = charSet.Select(chars => chars.ToList()).ToList();

            while (generated.Length < size)
            {
                foreach (var charList in charsetList)
                {
                    generated.Append(FromRng(1, charList));

                    if (generated.Length == size)
                    {
                        break;
                    }
                }
            }

            var fromRng = generated.ToString();

            var shuffled = fromRng.ToCharArray().Shuffle();

            fromRng = new string(shuffled);

            return fromRng;
        }

        private static string FromRng(int size, IList<char> chars)
        {
            var crypto = new RNGCryptoServiceProvider();

            var data = new byte[size];
            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(size);

            foreach (var b in data)
            {
                result.Append(chars[b % chars.Count]);
            }

            var fromRng = result.ToString();

            return fromRng;
        }
    }
}