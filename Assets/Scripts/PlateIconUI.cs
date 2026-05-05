using System;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform IconTemplate;

    private void Awake() {
        IconTemplate.gameObject.SetActive(false);
    }
   
    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }


    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        UpdateVisual();
        
    }

    private void UpdateVisual() {
        foreach( Transform child in transform) {
            if (child == IconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.kitchenObjecttSOList()) {
            Transform Icon=Instantiate(IconTemplate, transform);
            Icon.gameObject.SetActive(true);
            Icon.GetComponent<PlateSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
