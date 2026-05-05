using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisuals : MonoBehaviour
{
    [Serializable]
    public struct kitchenObjectSO_GameObject {
        public GameObject gameObject;
        public KitchenObjectSO kitchenObjectSO;

    }
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<kitchenObjectSO_GameObject> plateKitchenObjectList;
 
    private void Start() {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (kitchenObjectSO_GameObject kitchenObjectSO_GameObject in plateKitchenObjectList) {
            kitchenObjectSO_GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
        foreach (kitchenObjectSO_GameObject kitchenObjectSO_GameObject in plateKitchenObjectList) {
            if (kitchenObjectSO_GameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSO_GameObject.gameObject.SetActive(true);
            }
        }
    }
}
