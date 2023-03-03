using System;
using UnityEngine;

public class DummyPlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float radius = 12.0f;
    [SerializeField] private float angle = 0.0f;

    [SerializeField] private Boss boss;
   
    private bool shouldFixDodge;
    private Vector3 targetPosition;
    private float animationTime = 0.5f;
    public Vector3 startingPosition { get; set; }
    public float elapsedTime = 0;

    void Start()
    {
        angle = Vector3.Angle(boss.transform.position, transform.position);
        LookAtBoss();
        UpdatePosition();
    }

    private void LookAtBoss()
    {
        transform.LookAt(boss.transform.position);
    }

    public void Dodge(bool right)
    {
        angle -= 45 * (right ? 1 : -1);
        transform.Rotate(Vector3.up, 45 * (right?1:-1));
        startingPosition = transform.localPosition;

        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * 15;
        float z = Mathf.Cos(Mathf.Deg2Rad * angle) * 15;
        //Vector3 newPosition = new Vector3(x, transform.position.y, z);
        
        //targetPosition = startingPosition + transform.forward * 4;
        targetPosition = new Vector3(x, transform.position.y, z);
        boss.SetPlayerPositionsForRotation(transform.position, targetPosition);
        shouldFixDodge = true;
    }
    
    private void FixedUpdate()
    {
        if (!shouldFixDodge) return;
        
        elapsedTime += Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / animationTime);
        transform.localPosition = newPosition;

        if(elapsedTime >= animationTime)
        {
            LookAtBoss();
            elapsedTime = 0;
            shouldFixDodge = false;
        }  
    }

    private void UpdatePosition()
    {
        angle = Vector3.Angle(boss.transform.position, transform.position);
        float x = Mathf.Sin(angle) * -11;
        float z = Mathf.Cos(angle) * -11;
        Vector3 newPosition = new Vector3(x, transform.position.y, z);

        /*
        transform.position = newPosition;

        angle += speed * Time.deltaTime;

        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }
        */
    }
}
