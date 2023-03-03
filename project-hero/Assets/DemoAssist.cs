using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoAssist : MonoBehaviour
{

    [SerializeField] private TeamManager _teamManager;
    [SerializeField] private GameObject Assist1;
    [SerializeField] private GameObject Assist2;
    
    public void toggleAssist1()
    {
        if (Assist1 is null) return;
        if (_teamManager.IsSlotOccupied(1))
        {
            _teamManager.UnloadSlot(1);    
        }
        else
        {
            _teamManager.LoadSlot(1, Assist1);
        }
    }
    
    public void toggleAssist2()
    {
        if (Assist2 is null) return;
        if (_teamManager.IsSlotOccupied(2))
        {
            _teamManager.UnloadSlot(2);    
        }
        else
        {
            _teamManager.LoadSlot(2, Assist2);
        }
    }
}
