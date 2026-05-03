using System;
using UnityEngine;


public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressEventChangedArgs> OnProgressChanged;
    public event EventHandler<OnStateChangeEventArgs> OnStateChanged;
    public class OnStateChangeEventArgs : EventArgs {
        public State state;
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    public enum State {
        Idle,
        Frying,
        Fried,
        Burnt
    }
    private State state;

    private void Start() {
        state = State.Idle;
    }
    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
            case State.Idle:
                    break;
            case State.Frying:

                fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimingMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimingMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.Output, this);
                        state = State.Fried;
                        burningTimer = 0f;

                        burningRecipeSO = GetBurningSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs() {
                            state = state
                        });
                     

              }
                break;
            case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.Output, this);
                        state = State.Burnt;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs() {
                            state = state

                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                            progressNormalized = 0f
                        });

                    }
                    break;
             case State.Burnt:

                break;
        }
            Debug.Log(state);

        }
    }
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            //There is no Kitchen Object Here
            if (player.HasKitchenObject()) {
                //Player is Carrying Object

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;

                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangeEventArgs() {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimingMax
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangeEventArgs() {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                            progressNormalized = 0f
                        });
                    }
                }

       
      

            }
            else {
                //Player does not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangeEventArgs() {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressEventChangedArgs() {
                    progressNormalized = 0f
                });
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

    private BurningRecipeSO GetBurningSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.Input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
