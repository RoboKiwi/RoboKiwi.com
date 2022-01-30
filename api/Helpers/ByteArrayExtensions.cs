using System;
using System.Runtime.CompilerServices;

namespace RoboKiwi.Functions.Helpers;

static class ByteArrayExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char ToCharLower(int value)
    {
        value &= 0xF;
        value += '0';

        if (value > '9')
        {
            value += 'a' - ('9' + 1);
        }

        return (char)value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static char ToCharUpper(int value)
    {
        value &= 0xF;
        value += '0';

        if (value > '9')
        {
            value += 'A' - ('9' + 1);
        }

        return (char)value;
    }

    public static string ToHexStringLower(this byte[] value)
    {
        return ToHexString(value, 0, value.Length, false);
    }

    public static string ToHexStringUpper(this byte[] value)
    {
        return ToHexString(value, 0, value.Length, true);
    }

    public static string ToHexStringLower(this byte[] value, int startIndex, int length)
    {
        return ToHexString(value, startIndex, length, false);
    }

    public static string ToHexStringUpper(this byte[] value, int startIndex, int length)
    {
        return ToHexString(value, startIndex, length, true);
    }

    internal static string ToHexString(this byte[] value, int startIndex, int length, bool upperCase)
    {
        switch (length)
        {
            case 0:
                return string.Empty;
            case > int.MaxValue / 2:
                throw new ArgumentOutOfRangeException(nameof(length), length,
                    "The length of the resulting hex string would be greater than the max length of " + int.MaxValue / 2);
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(length));
        }
            
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (startIndex < 0 || startIndex >= value.Length && startIndex > 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
        if (startIndex > value.Length - length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "The startIndex is outside the bounds of the array");
        }
            
        return string.Create(length * 2, (value, startIndex, length, upperCase), static (dst, state) =>
        {
            var src = new ReadOnlySpan<byte>(state.value, state.startIndex, state.length);

            var i = 0;
            var j = 0;

            while (i < src.Length)
            {
                var b = src[i++];
                dst[j++] = state.upperCase ? ToCharUpper(b >> 4) : ToCharLower(b >> 4);
                dst[j++] = state.upperCase ? ToCharUpper(b) : ToCharLower(b);
            }
        });
    }

}