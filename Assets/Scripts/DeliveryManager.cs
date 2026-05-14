using System;
using UnityEngine;


using System.Collections.Generic;


public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager Instance { get; private set; }
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer ;
    private float spawnRecipeTimerMax = 4f;

    private int maxSpawnCount = 6;
   
    private void Awake() {
      Instance= this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
   
        if (spawnRecipeTimer <=0f) {
            
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < maxSpawnCount) {
           RecipeSO waitingRecipeSO= recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
               
            waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for(int i=0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSO.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {

                bool plateContentsMatch = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSO) {

                    bool ingredientFound= false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {

                        if(plateKitchenObjectSO  == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {

                        plateContentsMatch = false;
                    }
                }
                if (plateContentsMatch) {

                    

                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
}
