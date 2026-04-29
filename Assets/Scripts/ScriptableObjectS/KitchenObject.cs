using UnityEngine;

public class KitchenObject : MonoBehaviour {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

   
    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;

    }

    public void SetKitchenObjectParent(IKitchenObjectParent clearCounter) {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }
      
        this.kitchenObjectParent = clearCounter;
        clearCounter.SetKitchenObject(this);

        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKitchenObject() ;
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
   
}