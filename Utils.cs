using System;
namespace javacc.net
{
    public static class Utils
    {
        public static string ToString(int n, int d) => d == 16 ? n.ToString("{X}") : n.ToString();
        public static string ToHexString(int n) => n.ToString("{X}");
        public static string ToHexString(long n) => n.ToString("{X}");
        //TODO:
        public static string ToOctString(int n) => n.ToString("{X}");
        //TODO:
        public static string ToOctString(long n) => n.ToString("{X}");
    }
}
