using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{

    [SerializeField] PlatesCounter plateCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platePreFab;
    private List<GameObject> plateVisualGameObjectLists;
    public void Awake() {
        plateVisualGameObjectLists = new List<GameObject>();
    }
    private void Start() {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateTaken += PlateCounter_OnPlateTaken;
    }

    private void PlateCounter_OnPlateTaken(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjectLists[plateVisualGameObjectLists.Count - 1];
        plateVisualGameObjectLists.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e) {
      Transform plateVisualTransform=  Instantiate(platePreFab, counterTopPoint);
        float plateVisualOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateVisualOffsetY * plateVisualGameObjectLists.Count, 0);
        plateVisualGameObjectLists.Add(plateVisualTransform.gameObject);
    }
}
