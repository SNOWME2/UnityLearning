using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{

    [SerializeField] protected Transform counterTopPoint;
    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.Log("Base Counter Interact");
    }

    public Transform GetKitchenObjectFollowTransform() {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject) {

        this.kitchenObject = kitchenObject;

    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }
}
