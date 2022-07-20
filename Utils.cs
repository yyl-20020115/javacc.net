using System;

namespace JavaCC.NET;

public static class Utils
{
    public static string ToString(int n, int d) => Convert.ToString(n, d);
    public static string ToString(long n, int d) => Convert.ToString(n, d);
    public static string ToHexString(int n) => ToString(n, 16);
    public static string ToHexString(long n) => ToString(n, 16);
    public static string ToOctString(int n) => ToString(n, 8);
    public static string ToOctString(long n) => ToString(n, 8);
}
