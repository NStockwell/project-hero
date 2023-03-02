using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DummyPlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float radius = 15.0f;
    [SerializeField] private float angle = 0.0f;

    [SerializeField] private GameObject boss;
    
    void Start()
    {
        transform.LookAt(boss.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(angle) * radius;
        float z = Mathf.Cos(angle) * radius;
        Vector3 newPosition = new Vector3(x, transform.position.y, z);
        
        transform.position = newPosition;
        
        angle += speed * Time.deltaTime;
        
        if (angle >= 360.0f)
        {
            angle -= 360.0f;
        }
        
        transform.LookAt(boss.transform.position);
    }
}
