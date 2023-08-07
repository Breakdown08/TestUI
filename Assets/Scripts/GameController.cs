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
}
