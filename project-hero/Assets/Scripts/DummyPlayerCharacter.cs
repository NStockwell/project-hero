using UnityEngine;

public class DummyPlayerCharacter : MonoBehaviour
{
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float radius = 12.0f;
    [SerializeField] private float angle = 0.0f;

    [SerializeField] private GameObject boss;
    
    void Start()
    {
        LookAtBoss();
        UpdatePosition();
    }

    private void LookAtBoss()
    {
        transform.LookAt(boss.transform.position);
    }

    private void UpdatePosition()
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
    }
}
