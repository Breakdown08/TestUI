public static class GameController
{
    private static void BuyConsumable(GameModel.ConsumableTypes consumableType, System.Action purchaseAction)
    {
        if (!GameModel.HasRunningOperations)
        {
            purchaseAction.Invoke();
        }
    }

    public static void BuyMedpack()
    {
        BuyConsumable(GameModel.ConsumableTypes.Medpack, () => {
            GameModel.BuyConsumableForGold(GameModel.ConsumableTypes.Medpack);
        });
    }

    public static void BuyArmorPlate()
    {
        BuyConsumable(GameModel.ConsumableTypes.ArmorPlate, () => {
            GameModel.BuyConsumableForSilver(GameModel.ConsumableTypes.ArmorPlate);
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

    public static void ConvertCoinToCredit()
    {
        GameModel.ConvertCoinToCredit(UIManager.GetCalculatorInputValue());
    }
}
