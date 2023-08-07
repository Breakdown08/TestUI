using System.Collections.Generic;
using TMPro;

public static class ExtensionMethods
{
    public static string FormatWithSpaces(this int number)
    {
        return string.Format("{0:n0}", number);
    }

    public static void SetTextValue(this List<TMP_Text> textList, string value)
    {
        foreach (TMP_Text textItem in textList)
        {
            textItem.text = value;
        }
    }
}