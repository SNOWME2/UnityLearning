using System;
using Unity.Hierarchy;
using UnityEngine;
using static StoveCounter;
using static UnityEngine.CullingGroup;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;

    [SerializeField] private KitchenObjectSO PlateKitchenObjecSO;
    private float spawnTimer;
    private const float SPAWN_TIMER_MAX = 4f;
    private int plateSpawnAmmount;
    private const int MAX_SPAWN_AMMOUNT = 4;
    private void Update() {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > SPAWN_TIMER_MAX) {
       
            spawnTimer = 0f;

            if (plateSpawnAmmount < MAX_SPAWN_AMMOUNT) {
                plateSpawnAmmount++;

                OnPlateSpawned?.Invoke(this, EventArgs.Empty);

            }
            else {
                spawnTimer = 0f;
            }

          

        }
    }
    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            if (plateSpawnAmmount > 0) {
                plateSpawnAmmount--;

                KitchenObject.SpawnKitchenObject(PlateKitchenObjecSO,player);
                OnPlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
