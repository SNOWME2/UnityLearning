using System;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no Kitchen Object Here
            if (player.HasKitchenObject()) {
                //Player is Carrying Object

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                }
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

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.Output;
        }
        else {
            return null;
        }
    }

    private FryingRecipeSO GetFryingSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.Input == inputKitchenObjectSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

}
