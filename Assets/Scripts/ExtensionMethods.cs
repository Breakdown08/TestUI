using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public static class ExtensionMethods
{
    private static IEnumerator AnimateColorChangeCoroutine(Graphic graphic, Color endColor, float duration, Action onComplete)
    {
        Color startColor = graphic.color;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            graphic.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        graphic.color = endColor;
        onComplete?.Invoke();
    }

    public static void AnimateColorChange(this Graphic graphic, Color endColor, float duration, Action onComplete = null)
    {
        graphic.StartCoroutine(AnimateColorChangeCoroutine(graphic, endColor, duration, onComplete));
    }

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