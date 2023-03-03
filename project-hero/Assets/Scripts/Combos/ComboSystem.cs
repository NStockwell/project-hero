using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class ComboSystem : Singleton<ComboSystem>
{
    [SerializeField] private int timeToReset = 1000;

    [SerializeField] private int initialMilestoneFrequency = 10;

    private int _milestoneFrequency = 0;

    public int currentHitCounter;
    private int _highestCombo = -1;
    private float _timeSinceLastHit = 0f;
    private int _leftToMilestone;

    public delegate void ComboEndedHandler(bool wasBreak, int total);

    public delegate void ComboMilestoneHandler(int total);
    public delegate void ComboHitHandler(int total);

    public event ComboEndedHandler OnComboFinished;
    public event ComboHitHandler OnComboHit;
    public event ComboMilestoneHandler OnComboMilestone;

    public void Start()
    {
        _milestoneFrequency = _leftToMilestone = initialMilestoneFrequency;
    }

    public override void Awake()
    {
        base.Awake();
        currentHitCounter = 0;
        _timeSinceLastHit = 0f;
    }

    void Update()
    {
        _timeSinceLastHit += Time.deltaTime;

        if (_timeSinceLastHit > timeToReset / 1000.0f)
        {
            FinishCombo();
        }
    }

    public void Hit()
    {
        currentHitCounter += 1;
        _leftToMilestone -= 1;
        Debug.Log("Frequency: " + _milestoneFrequency);
        OnComboHit?.Invoke(currentHitCounter);
        
        if (currentHitCounter > _highestCombo)
        {
            _highestCombo = currentHitCounter;
        }
        
        if (_milestoneFrequency > 0 && _leftToMilestone <= 0)
        {
            _leftToMilestone = _milestoneFrequency;

            OnComboMilestone?.Invoke(currentHitCounter);
        }
        
        _timeSinceLastHit = 0f;
        Debug.Log("taking hit " + currentHitCounter);
    }

    public void Break()
    {
        FinishCombo(true);
    }

    private void FinishCombo(bool brokenCombo = false)
    {
        if (currentHitCounter == 0) return;

        int tmpCounter = currentHitCounter;
        currentHitCounter = 0;
        _leftToMilestone = _milestoneFrequency;
        OnComboFinished?.Invoke(brokenCombo, tmpCounter);
    }

    public void SetMilestoneFrequency(int newFrequency)
    {
        _milestoneFrequency = newFrequency;
        _leftToMilestone = Math.Min(_leftToMilestone, _milestoneFrequency);
    }

    public int GetHighestCombo()
    {
        return _highestCombo;
    }

    public void Reset()
    {
        _highestCombo = 0;
        currentHitCounter = 0;
        _milestoneFrequency = initialMilestoneFrequency;
    }
}