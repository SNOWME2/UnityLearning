using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
        } else {
            //There is Kitchen Object Here
        
            if (player.HasKitchenObject()) {
             
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
              
                   if ( plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else {
                //Player does not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }

        }

    }

}