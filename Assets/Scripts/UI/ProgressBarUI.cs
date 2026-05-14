using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{

    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    private IHasProgress hasProgress;
    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        hasProgress.OnProgressChanged += HasProgress_ProgressChanged;

        barImage.fillAmount = 0f;
        Hide();
    }

    private void HasProgress_ProgressChanged(object sender, IHasProgress.OnProgressEventChangedArgs e) {
     
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
           Hide();
        }
        else{
Show();
        }

    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
