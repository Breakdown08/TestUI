using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIClickableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Color defaultColor;
    public Color hoverColor;
    public Color pressedColor;
    private bool isPressed_;


    public void SetImageColor(Color color)
    {
        GetComponent<Image>().color = color;
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
        UIManager.Instance.ShowDisappearingPoint(eventData.position, transform);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsPointerOverGameObject())
        {
            SetImageColor(hoverColor);
        }
        else
        {
            SetImageColor(defaultColor);
            UIManager.SetCursorDefault();
        }
        isPressed_ = false;
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
}
