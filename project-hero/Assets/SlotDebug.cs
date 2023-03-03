using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotDebug : MonoBehaviour
{

    [SerializeField] private Color color = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, .5f);
    }
}
