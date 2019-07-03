using System;

namespace EstheticsPro.Core.Extensions
{
    public static class ExtensionMethods
    {
        public static int ToInt(this long value)
            => Convert.ToInt32(value);
    }
}