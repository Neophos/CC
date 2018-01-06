using UnityEngine;
using System.Collections;

public class HelperFunctions
{
    public static byte ClearBit(byte value, int position)
    {
        return value &= (byte)(1 << position);
    }

    public static byte SetBit(byte value, byte position)
    {
        return value |= (byte)(1 << position);
    }

    public static bool ReadBit(byte value, int position)
    {
        return (value & (1 << position)) != 0;
    }

    public static string GetIntBinaryString(byte n)
    {
        char[] b = new char[8];
        int pos = 7;
        int i = 0;

        while (i < 8)
        {
            if ((n & (1 << i)) != 0)
                b[pos] = '1';
            else
                b[pos] = '0';
            pos--;
            i++;
        }
        return new string(b);
    }
}
