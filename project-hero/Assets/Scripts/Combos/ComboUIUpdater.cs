using System;
using UnityEngine;
using TMPro;

public class ComboUIUpdater : MonoBehaviour
{
    private ComboSystem _combo;
    [SerializeField]
    private TextMeshProUGUI counterText;
    [SerializeField]
    private TextMeshProUGUI statusText;
    [SerializeField]
    private TextMeshProUGUI milestoneText;

    private float timeSinceLastStatusUpdate = 0f;

    void Awake()
    {
        _combo = ComboSystem.Instance;
    }

    private void OnEnable()
    {
        _combo.OnComboFinished += OnComboEndHandler;
        _combo.OnComboMilestone += OnMilestoneReachedHandler;
    }

    private void OnDisable()
    {
        _combo.OnComboFinished -= OnComboEndHandler;
        _combo.OnComboMilestone -= OnMilestoneReachedHandler;
    }
    
    void OnDestroy()
    {
        _combo.OnComboFinished -= OnComboEndHandler;
        _combo.OnComboMilestone -= OnMilestoneReachedHandler;
    }

    void Update()
    {
        timeSinceLastStatusUpdate += Time.deltaTime;

        if (timeSinceLastStatusUpdate > 2.0f) { 
            statusText.SetText("");
        }

        if (counterText is not null) {
            counterText.SetText(""+_combo.currentHitCounter);
        }
    }

    void OnComboEndHandler(bool wasBreak, int total) 
    {
        if (statusText is null) return;

        if (wasBreak) {
            statusText.SetText("!C-c-c-c-c-combo breaker!");
        } else {
            statusText.SetText("Got " + total +" Hits!");
        }
        timeSinceLastStatusUpdate = 0;
    }

    void OnMilestoneReachedHandler(int total)
    {
        if (milestoneText is not null)
        {
            milestoneText.SetText("Reached combo " + total );
        }
    }
}
