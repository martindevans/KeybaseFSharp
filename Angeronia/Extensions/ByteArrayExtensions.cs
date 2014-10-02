using System;

namespace Angeronia.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToHexString(this byte[] bytes)
        {
            return String.Join(String.Empty, Array.ConvertAll(bytes, x => x.ToString("X2")));
        }
    }
}
