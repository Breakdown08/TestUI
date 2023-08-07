using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class AnimationUtils
{
    public static IEnumerator AnimateColorChange(Graphic graphic, Color endColor, float duration, Action onComplete = null)
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
}
