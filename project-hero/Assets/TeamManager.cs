using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour, ICharacterController
{

    [SerializeField] private GameObject _mainCharacterPrefab;
    
    [SerializeField] private Transform[] Slots;
    private ICharacterController[] slotsObjects;

    void Start()
    {
        slotsObjects = new ICharacterController[Slots.Length];
        
        LoadSlot(0, _mainCharacterPrefab);
    }

    
    void Update()
    {
        for (int i = 0; i < slotsObjects.Length; i++)
        {
            if (slotsObjects[i] is null) continue;
        }
    }

    public void LoadSlot(int SlotIndex, GameObject prefab)
    {
        if (SlotIndex >= Slots.Length) return;
        UnloadSlot(SlotIndex);

        Transform slot = Slots[SlotIndex];
        GameObject newCharacter = Instantiate(prefab, gameObject.transform);
        newCharacter.transform.position = slot.transform.position;
        ICharacterController controller = newCharacter.GetComponent<ICharacterController>();
        slotsObjects[SlotIndex] = controller;
        controller.EnterArena();
    }

    public void UnloadSlot(int SlotIndex)
    {
        if (SlotIndex >= Slots.Length) return;
        if (slotsObjects[SlotIndex] is not null)
        {
            slotsObjects[SlotIndex].LeaveArena();
            
            // someone should destroy this thing...
            slotsObjects[SlotIndex] = null;
        }
    }

    public bool IsSlotOccupied(int SlotIndex)
    {
        if (SlotIndex >= Slots.Length) return false;
        return slotsObjects[SlotIndex] is not null;
    }
    
    
    public void Attack()
    {
        for (int i = 0; i < slotsObjects.Length; i++)
        {
            if (slotsObjects[i] is null) continue;
                slotsObjects[i].Attack();
        }
    }

    public void StrifeLeft()
    {
        for (int i = 0; i < slotsObjects.Length; i++)
        {
            if (slotsObjects[i] is null) continue;
            slotsObjects[i].StrifeLeft();
        }
    }

    public void StrifeRight()
    {
        for (int i = 0; i < slotsObjects.Length; i++)
        {
            if (slotsObjects[i] is null) continue;
            slotsObjects[i].StrifeRight();
        }
    }

    public void EnterArena()
    {
        // do nothing
    }

    public void LeaveArena()
    {
        // Debug.Log("KBye Thx");
    }
}
