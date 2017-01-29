using System;

namespace Angeronia.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            return string.Join(string.Empty, Array.ConvertAll(bytes, x => x.ToString("X2")));
        }
    }
}
