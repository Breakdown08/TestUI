using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIClickableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public string onClickFunctionName;
    public Color defaultColor;
    public Color hoverColor;
    public Color pressedColor;
    public GameObject onClickEffectPrefab;
    private bool isPressed_;


    private void SetImageColor(Color color)
    {
        GetComponent<Image>().color = color;
    }

    private bool IsPointerOverGameObject()
    {
        Vector3 mousePosition = Input.mousePosition;
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            GameObject hitObject = result.gameObject;
            if (hitObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void ShowDisappearingPoint(Vector2 position)
    {
        GameObject pointObject = Instantiate(onClickEffectPrefab, UIManager.Instance.canvas.transform);
        Image pointImage = pointObject.GetComponent<Image>();
        RectTransform pointRectTransform = pointObject.GetComponent<RectTransform>();
        pointRectTransform.position = position;
        pointImage.AnimateColorChange(Color.clear, 0.3f, () => Destroy(pointImage.gameObject));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetImageColor(hoverColor);
        UIManager.SetCursorPointer();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPressed_)
        {
            SetImageColor(hoverColor);
            UIManager.SetCursorPointer();
        }
        else
        {
            SetImageColor(defaultColor);
            UIManager.SetCursorDefault();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetImageColor(pressedColor);
        isPressed_ = true;
        ShowDisappearingPoint(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsPointerOverGameObject())
        {
            InitClick();
        }
        UIManager.SetCursorDefault();
        SetImageColor(defaultColor);
        isPressed_ = false;
    }

    public void InitClick()
    {
        if (!string.IsNullOrEmpty(onClickFunctionName))
        {
            System.Type type = typeof(GameController);
            System.Reflection.MethodInfo methodInfo = type.GetMethod(onClickFunctionName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (methodInfo != null)
            {
                methodInfo.Invoke(null, null);
            }
        }
    }
}