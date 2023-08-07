using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{   
    public static UIManager Instance {get; private set;}

    [Header("Global UI Counters/Text")]
    public List<TMP_Text> MedpackCount;
    public List<TMP_Text> ArmorPlateCount;
    public List<TMP_Text> CoinCount;
    public List<TMP_Text> CreditCount;
    public List<TMP_Text> ErrorMessage;

    [Header("Consumable Popup")]
    public GameObject ConsumablePopup;
    public TMP_Text MedpackPrice;
    public TMP_Text ArmorPlatePrice;
    
    public GameObject WalletPopup;

    [Header("Cursor")]
    public GameObject disappearingPointPrefab;
    public Texture2D cursorDefault;
    public Texture2D cursorPointer;
    private static Vector2 cursorDefaultHotSpot_ = new Vector2(0, 0);
    private static Vector2 cursorPointertHotSpot_ = new Vector2(15, 3);

    private void Awake()
    {
        Instance = this;
        GameModel.OperationComplete += HandleOperationComplete;
        GameModel.ModelChanged += RefreshUI;
    }

    private void Update()
    {
        GameModel.Update();
    }

    private void Start()
    {
        RefreshUI();
    }

    private void HandleOperationComplete(GameModel.OperationResult result)
    {
        if (!result.IsSuccess)
        {
            ErrorMessage.SetTextValue(result.ErrorDescription);
            foreach (TMP_Text text in ErrorMessage)
            {
                // StartCoroutine(Utils.FadeText(text));
                StartCoroutine(AnimationUtils.AnimateColorChange(text, new Color(text.color.r, text.color.g, text.color.b, 0f), 3.0f, () =>
                {
                    text.text = "";
                    text.color = text.color;
                }));
            }
        }
    }

    private void RefreshUI()
    {
        MedpackCount.SetTextValue(GameModel.GetConsumableCount(GameModel.ConsumableTypes.Medpack).ToString());
        ArmorPlateCount.SetTextValue(GameModel.GetConsumableCount(GameModel.ConsumableTypes.ArmorPlate).ToString());
        MedpackPrice.text = GameModel.ConsumablesPrice[GameModel.ConsumableTypes.Medpack].CoinPrice.ToString();
        ArmorPlatePrice.text = GameModel.ConsumablesPrice[GameModel.ConsumableTypes.ArmorPlate].CreditPrice.ToString();
        CreditCount.SetTextValue(GameModel.CreditCount.FormatWithSpaces());
        CoinCount.SetTextValue(GameModel.CoinCount.FormatWithSpaces());
    }

    public static void SetCursorPointer()
    {
        Cursor.SetCursor(Instance.cursorPointer, cursorPointertHotSpot_, CursorMode.Auto);
    }

    public static void SetCursorDefault()
    {
        Cursor.SetCursor(Instance.cursorDefault, cursorDefaultHotSpot_, CursorMode.Auto);
    }

    public void ShowDisappearingPoint(Vector2 position, Transform parent)
    {
        GameObject pointObject = Instantiate(disappearingPointPrefab, parent);
        Image pointImage = pointObject.GetComponent<Image>();
        RectTransform pointRectTransform = pointObject.GetComponent<RectTransform>();
        pointRectTransform.position = position;
        StartCoroutine(AnimationUtils.AnimateColorChange(pointImage, Color.clear, 0.3f, () => Destroy(pointImage.gameObject)));
    }

    public void OpenConsumablePopup()
    {
        ConsumablePopup.SetActive(true);
    }

    public void CloseConsumablePopup()
    {
        ConsumablePopup.SetActive(false);
    }

    public void BuyMedpack()
    {
        if (!GameModel.HasRunningOperations)
        {
            GameModel.BuyConsumableForGold(GameModel.ConsumableTypes.Medpack);
        }
    }

    public void BuyArmorPlate()
    {
        if (!GameModel.HasRunningOperations)
        {
            GameModel.BuyConsumableForSilver(GameModel.ConsumableTypes.ArmorPlate);
        }
    }
}
