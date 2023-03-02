using UnityEngine;

public class CharacterController : MonoBehaviour, ICharacterController
{
    public void Attack()
    {
    }

    public void StrifeLeft()
    {
    }

    public void StrifeRight()
    {
    }

    public void EnterArena()
    {
        Debug.Log("entering arena");
    }

    public void LeaveArena()
    {
        Debug.Log("KBye Thx");
        Invoke("DestroyCharacter", 1.0f);
    }

    private void DestroyCharacter()
    {
        Destroy(gameObject);
    }
}