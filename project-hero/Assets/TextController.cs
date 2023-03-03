using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] private bool _rotateFlameOnEveryHit = false;
    [SerializeField] private TextMeshProUGUI counterText;
    private Animator _animator;
    [SerializeField] private Animator _animatorFlame;
    
    private float _timeSinceLastUpdate = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        ComboSystem.Instance.OnComboMilestone += OnReachedMilestone;
    }

    private void Update()
    {
        _timeSinceLastUpdate += Time.deltaTime;
        if (_timeSinceLastUpdate > 1.0f)
        {
            _animator.SetBool("isVisible", false);
            _animatorFlame.SetBool("isVisible", false);
        }
    }

    public void SetText(string text)
    {
        counterText.SetText(text);
        _animator.SetBool("isVisible", true);
        if (_rotateFlameOnEveryHit || !_animatorFlame.GetBool("isVisible")) _animatorFlame.SetTrigger("rotate");
        _animatorFlame.SetBool("isVisible", true);
        _timeSinceLastUpdate = 0.0f;
    }

    private void OnReachedMilestone(int total)
    {
        _animator.SetTrigger("reachedMilestone");
        _animatorFlame.SetTrigger("rotate");
    } 
    
}
