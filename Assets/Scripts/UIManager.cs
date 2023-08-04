using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager Instance {get; private set;} 
    public GameObject disappearingPointPrefab;
    public Texture2D cursorDefault;
    public Texture2D cursorPointer;
    public float disappearingRadius = 30f;
    public float animationDuration = 0.3f;
    
    public static Vector2 cursorDefaultHotSpot = new Vector2(0, 0);
    public static Vector2 cursorPointertHotSpot = new Vector2(15, 3);

    public static void SetCursorPointer()
    {
        Cursor.SetCursor(UIManager.Instance.cursorPointer, UIManager.cursorPointertHotSpot, CursorMode.Auto);
    }

    public static void SetCursorDefault()
    {
        Cursor.SetCursor(UIManager.Instance.cursorDefault, UIManager.cursorDefaultHotSpot, CursorMode.Auto);
    }


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
