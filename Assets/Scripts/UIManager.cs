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

    [Header("Wallet Popup")]
    public GameObject WalletPopup;
    public TMP_Text CoinToCreditRate;
    public TMP_Text CreditsAmount;
    public TMP_InputField calculationInput;

    [Header("Cursor")]
    public GameObject disappearingPointPrefab;
    public GameObject canvas;
    public Texture2D cursorDefault;
    public Texture2D cursorPointer;
    private static Vector2 cursorDefaultHotSpot_ = new Vector2(0, 0);
    private static Vector2 cursorPointertHotSpot_ = new Vector2(15, 3);

    private void Awake()
    {
        Instance = this;
        GameModel.OperationComplete += HandleOperationComplete;
        GameModel.ModelChanged += RefreshUI;
        calculationInput.onValueChanged.AddListener(delegate{UpdateCalculationResult();});
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
                if (text.gameObject.activeInHierarchy)
                {
                    Color startColor = text.color;
                    text.AnimateColorChange(new Color(text.color.r, text.color.g, text.color.b, 0f), 3.0f, () =>
                    {
                        text.text = "";
                        text.color = startColor;
                    });
                    continue;
                }
                text.text = "";
            }
        }
    }

    private static void UpdateCalculationResult()
    {
        int result = GameController.CalculateWalletExchange(GetCalculatorInputValue());
        Instance.CreditsAmount.text = result.ToString();
    }

    private void RefreshUI()
    {
        MedpackCount.SetTextValue(GameModel.GetConsumableCount(GameModel.ConsumableTypes.Medpack).ToString());
        ArmorPlateCount.SetTextValue(GameModel.GetConsumableCount(GameModel.ConsumableTypes.ArmorPlate).ToString());
        MedpackPrice.text = GameModel.ConsumablesPrice[GameModel.ConsumableTypes.Medpack].CoinPrice.ToString();
        ArmorPlatePrice.text = GameModel.ConsumablesPrice[GameModel.ConsumableTypes.ArmorPlate].CreditPrice.ToString();
        CreditCount.SetTextValue(GameModel.CreditCount.FormatWithSpaces());
        CoinCount.SetTextValue(GameModel.CoinCount.FormatWithSpaces());
        CoinToCreditRate.text = GameModel.CoinToCreditRate.ToString();
        UpdateCalculationResult();
    }

    public static void SetCursorPointer()
    {
        Cursor.SetCursor(Instance.cursorPointer, cursorPointertHotSpot_, CursorMode.Auto);
    }

    public static void SetCursorDefault()
    {
        Cursor.SetCursor(Instance.cursorDefault, cursorDefaultHotSpot_, CursorMode.Auto);
    }

    public static void OpenConsumablePopup()
    {
        Instance.ConsumablePopup.SetActive(true);
    }

    public static void CloseConsumablePopup()
    {
        Instance.ConsumablePopup.SetActive(false);
    }

    public static void OpenWalletPopup()
    {
        Instance.WalletPopup.SetActive(true);
    }

    public static void CloseWalletPopup()
    {
        Instance.WalletPopup.SetActive(false);
    }

    public static int GetCalculatorInputValue()
    {
        int result = 0;
        if (!string.IsNullOrEmpty(Instance.calculationInput.text))
        {
            result = (int.Parse(Instance.calculationInput.text));
        }
        return result;
    }
}