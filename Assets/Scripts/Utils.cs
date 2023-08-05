using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Utils
{
    public static IEnumerator AnimatePointDisappearance(Image pointImage)
    {
        float time = 0f;
        float animationDuration = 0.3f;
        Color startColor = pointImage.color;
        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float t = time / animationDuration;
            pointImage.color = Color.Lerp(startColor, Color.clear, t);
            yield return null;
        }
        MonoBehaviour.Destroy(pointImage.gameObject);;
    }

    public static IEnumerator FadeText(TMP_Text text)
    {
        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        float duration = 3.0f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            text.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        text.text = "";
        text.color = startColor;
    }

    public static void SetObjectsTextValue(List<TMP_Text> list, string value)
    {
        foreach (TMP_Text item in list)
        {
            item.text = value;
        }
    }

}
