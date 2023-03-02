using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Action = InputSystem.Action;

public class TestActionSystem : MonoBehaviour
{
    [SerializeField] private ActionSystem actionSystem;

    private void OnEnable()
    {
        actionSystem.OnActionTaken += ActionTaken;
    }

    private void ActionTaken(Action action)
    {
        Debug.Log(action.ToString());
    }
}
