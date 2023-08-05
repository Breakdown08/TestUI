using UnityEngine;

public static class ExtensionMethods
{
    public static string FormatWithSpaces(this int number)
    {
        return string.Format("{0:n0}", number);
    }
}