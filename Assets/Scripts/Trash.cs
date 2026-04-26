using UnityEngine;

public class Trash : BaseCounter
{
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no Kitchen Object Here
            if (player.HasKitchenObject()) {

               player.GetKitchenObject().DestroySelf();
            }
      
        }
       
    }
}
