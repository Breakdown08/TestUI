public static class GameController
{

    private static bool CanPerformAction()
    {
        return !GameModel.HasRunningOperations;
    }

    private static void PerformAction(System.Action action)
    {
        if (CanPerformAction())
        {
            action.Invoke();
        }
    }

    public static void BuyMedpack()
    {
        PerformAction(() => {
            GameModel.BuyConsumableForGold(GameModel.ConsumableTypes.Medpack);
        });
    }

    public static void BuyArmorPlate()
    {
        PerformAction(() => {
            GameModel.BuyConsumableForSilver(GameModel.ConsumableTypes.ArmorPlate);
        });
    }

    public static void ConvertCoinToCredit()
    {
        PerformAction(() => {
            GameModel.ConvertCoinToCredit(UIManager.GetCalculatorInputValue());
        });
    }

    public static void OpenConsumablePopup()
    {
        UIManager.OpenConsumablePopup();
    }

    public static void CloseConsumablePopup()
    {
        UIManager.CloseConsumablePopup();
    }

    public static void OpenWalletPopup()
    {
        UIManager.OpenWalletPopup();
    }

    public static void CloseWalletPopup()
    {
        UIManager.CloseWalletPopup();
    }

    public static int CalculateWalletExchange(int value)
    {
        return value * GameModel.CoinToCreditRate;
    }
}
