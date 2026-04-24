using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (kitchenObject == null) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            Debug.Log(kitchenObjectTransform.GetComponent<KitchenObject>());
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
        else {
            kitchenObject.SetKitchenObjectParent(player);
        }


    }

  
}
