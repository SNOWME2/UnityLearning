using UnityEngine;

public class SelectedCounterVisuals : MonoBehaviour
{

    [SerializeField] private ClearCounter clearKitchenTable;
    [SerializeField] private GameObject visualGameObject;
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEvetArgs e) {
        if (e.selectedClearCounter == clearKitchenTable) {
            Show();

        }
        else {
            Hide();
        }
    }

    private void Show() {
        visualGameObject.SetActive(true);
    }
    private void Hide() {
        visualGameObject.SetActive(false);
    }
}
