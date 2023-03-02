using UnityEngine;
using TMPro;

public class ComboUIUpdater : MonoBehaviour
{

    [SerializeField]
    private ComboSystem combo;
    [SerializeField]
    private TextMeshProUGUI counterText;
    [SerializeField]
    private TextMeshProUGUI statusText;

    private float timeSinceLastStatusUpdate = 0f;
    
    void Start()
    {
        combo.OnComboFinished += OnComboEndHandler;
    }

    void Update()
    {
        timeSinceLastStatusUpdate += Time.deltaTime;

        if (timeSinceLastStatusUpdate > 2.0f) { 
            statusText.SetText("");
        }

        if (counterText != null && combo != null) {
            counterText.SetText(""+combo.currentHitCounter);
        }
    }

    void OnComboEndHandler(bool wasBreak, int total, int points) 
    {
        if (statusText == null || combo == null) return;

        if (wasBreak) {
            statusText.SetText("!C-c-c-c-c-combo breaker!");
        } else {
            statusText.SetText("Got " + total +" Hits!");
        }
        timeSinceLastStatusUpdate = 0;
    }
}
