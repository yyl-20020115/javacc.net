using System;
namespace javacc.net
{
    public static class Utils
    {
        public static string ToString(int n, int d) => d == 16 ? n.ToString("{X}") : n.ToString();
    }
}
