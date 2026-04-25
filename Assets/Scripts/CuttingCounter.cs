using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no Kitchen Object Here
            if (player.HasKitchenObject()) {
                //Player is Carrying Object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else {
                //Player does not carrying anything
            }
        }
        else {
            //There is Kitchen Object Here
            if (player.HasKitchenObject()) {
                //Player Is Carrying Object
            }
            else {
                //Player does not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject()) {

        
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, this);
        }
    }
}
