using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace tsol
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmptyOr(this string s, string value)
        {
            return string.IsNullOrEmpty(s) || s == value;
        }
    }
}
