using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager Instance {get; private set;} 
    public GameObject disappearingPointPrefab;
    public float disappearingRadius = 30f;
    public float animationDuration = 0.3f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        GameModel.Update();
    }

    public void ShowDisappearingPoint(Vector2 position, Transform parent)
    {
        GameObject pointObject = Instantiate(disappearingPointPrefab, parent);
        Image pointImage = pointObject.GetComponent<Image>();
        RectTransform pointRectTransform = pointObject.GetComponent<RectTransform>();
        pointRectTransform.position = position;
        StartCoroutine(AnimatePointDisappearance(pointImage));
    }

    private IEnumerator AnimatePointDisappearance(Image pointImage)
    {
        float time = 0f;
        Color startColor = pointImage.color;
        while (time < animationDuration)
        {
            time += Time.deltaTime;
            float t = time / animationDuration;
            pointImage.color = Color.Lerp(startColor, Color.clear, t);
            yield return null;
        }
        Destroy(pointImage.gameObject);
    }
}
