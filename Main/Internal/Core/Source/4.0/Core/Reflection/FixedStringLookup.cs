namespace EyeSoft.Reflection
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    internal static class FixedStringLookup
	{

		// Returns whether the match is found in the lookup table
		internal static bool Contains(string[][] lookupTable, string value, bool ignoreCase)
		{
			var length = value.Length;
			if (length <= 0 || length - 1 >= lookupTable.Length)
			{
				return false;
			}

			var subArray = lookupTable[length - 1];
			return subArray != null && Contains(subArray, value, ignoreCase);
        }

#if DEBUG

		internal static void VerifyLookupTable(string[][] lookupTable, bool ignoreCase)
		{
			for (var i = 0; i < lookupTable.Length; i++)
			{
				var subArray = lookupTable[i];
                
                if (subArray == null)
                {
					continue;

                }
                string lastValue = null;
                
                foreach (var value in subArray)
                {
                    // Must all be the length of the hashed position
                    Debug.Assert(value.Length == i + 1, "Lookup table contains an item in the wrong sub table. Item name: " + value);
                    if (lastValue != null)
                    {
                        // Must be sorted within the sub array;
                        Debug.Assert(string.Compare(lastValue, value, ((ignoreCase) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)) < 0,
                            string.Format(CultureInfo.InvariantCulture, "Lookup table is out of order.  Items {0} and {1} are reversed", lastValue, value));
                    }
                    lastValue = value;
                }
            }
		}

#endif

		// This routine finds a hit within a single sorted array, with the assumption that the
		// value and all the strings are of the same length.
		private static bool Contains(string[] array, string value, bool ignoreCase)
		{
			var min = 0;
			var max = array.Length;
			var pos = 0;
			char searchChar;
			while (pos < value.Length)
			{
				if (ignoreCase)
				{
					searchChar = char.ToLower(value[pos], CultureInfo.InvariantCulture);
				}
				else
				{
					searchChar = value[pos];
				}
				if ((max - min) <= 1)
				{
					// we are down to a single item, so we can stay on this row until the end.
					if (searchChar != array[min][pos])
					{
						return false;
					}
					pos++;
					continue;
				}

				// There are multiple items to search, use binary search to find one of the hits
				if (!FindCharacter(array, searchChar, pos, ref min, ref max))
				{
					return false;
				}
				// and move to next char
				pos++;
			}
			return true;
		}

		// Do a binary search on the character array at the specific position and constrict the ranges appropriately.
		private static bool FindCharacter(string[] array, char value, int pos, ref int min, ref int max)
		{
			var index = min;
			while (min < max)
			{
				index = (min + max) / 2;
				var comp = array[index][pos];
				if (value == comp)
				{
					// We have a match. Now adjust to any adjacent matches
					var newMin = index;
					while (newMin > min && array[newMin - 1][pos] == value)
					{
						newMin--;
					}
					min = newMin;

					var newMax = index + 1;
					while (newMax < max && array[newMax][pos] == value)
					{
						newMax++;
					}
					max = newMax;
					return true;
				}
				if (value < comp)
				{
					max = index;
				}
				else
				{
					min = index + 1;
				}
			}
			return false;
		}
	}
}