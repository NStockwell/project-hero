using UnityEngine;

public class ComboSystem : MonoBehaviour
{

    [SerializeField]
    private int timeToReset = 1000;
    public int currentHitCounter;
    public int currentHitPoints;
    private float timeSinceLastHit = 0f;

    public delegate void ComboEndedHandler(bool wasBreak, int total, int points);
    public event ComboEndedHandler OnComboFinished;
    
    void Start()
    {
        currentHitCounter = 0;
        timeSinceLastHit = 0f;
    }

    void Update()
    {
        timeSinceLastHit += Time.deltaTime;
        
        if (timeSinceLastHit > timeToReset / 1000.0f){
            Reset();
        }
    }

    public void Hit(int points)
    {
        currentHitCounter += 1;
        currentHitPoints += points;
        timeSinceLastHit = 0f;
        Debug.Log("taking hit " + currentHitCounter);
    }

    public void Break()
    {
        Reset(true);
    }

    private void Reset(bool brokeCombo = false)
    {
        if (currentHitCounter == 0) return ;
        
        int tmpCounter = currentHitCounter;
        int tmpPoints = currentHitPoints;
        currentHitCounter = 0;
        currentHitPoints = 0;
        OnComboFinished(brokeCombo, tmpCounter, tmpPoints);
    }
}
