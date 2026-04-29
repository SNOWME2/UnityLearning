using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
   
{

    public event EventHandler <OnProgressEventChangedArgs> OnProgressChanged;
    public class OnProgressEventChangedArgs : EventArgs {
        public float progressNormalized;
    }

    public event EventHandler OnCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no Kitchen Object Here
            if (player.HasKitchenObject()) {
                //Player is Carrying Object

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new OnProgressEventChangedArgs {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCuttingProgress
                    });
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

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            cuttingProgress++;

            OnCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(GetKitchenObject().GetKitchenObjectSO() );

            OnProgressChanged?.Invoke(this, new OnProgressEventChangedArgs {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.maxCuttingProgress
            });
            if (cuttingProgress >= cuttingRecipeSO.maxCuttingProgress) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }


    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingSOWithInput(inputKitchenObjectSO);
     if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.Output;
        }
        else {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.Input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
