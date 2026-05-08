using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player) {
        if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

            if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                GetKitchenObject().DestroySelf();
            }
        }

    }
}
