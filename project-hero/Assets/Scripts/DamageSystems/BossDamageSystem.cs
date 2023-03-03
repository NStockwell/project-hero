using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageSystem : MonoBehaviour
{

    public enum BossDamageType
    {
        SmallDamage,
        LargeDamage
    }
    
    [SerializeField] private GameObject boss;

    [SerializeField] private GameObject playerCharacter;

    [SerializeField] private GameObject damageCylinder;

    [SerializeField] private DamageAreaTransparencyController _transparencyController;

    private const float PlayerDistance = 15.0f;

    private const float SmallAttackDamageArea = 2.0f;
    private const float LargeAttackDamageArea = 4.0f;


    private float playCharacterFlashDuration = 0.5f;
    private bool isPlayerFlashing = false;
    private float flashingTimeElapsed;

    public bool isEnabled = false;
    private BossDamageType damageType;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        // Early exit if this is not to be calculated right now
        if (!isEnabled)
        {
            return;
        }

        var damageArea = damageType == BossDamageType.SmallDamage ? SmallAttackDamageArea : LargeAttackDamageArea;
        if (isPlayerInDamageArea(damageArea) && !isPlayerFlashing)
        {
            isPlayerFlashing = true;
            InvokeRepeating(nameof(FlashCharacter), 0.1f, 0.05f);
        }
    }

    public void PerformAttack(BossDamageType type)
    {
        damageType = type;
        CreateDamageArea(type);

        StartCoroutine(RemoveDamageAreaAfterTime(1.0f));
        StartCoroutine(SetPlayerDamageCheckAfterDelay(1.0f, true));

    }

    public void ShowDamageArea(BossDamageType type)
    {
        CreateDamageArea(type);
    }
    
    public void RemoveDamageArea()
    {
        damageCylinder.SetActive(false);
    }

    private bool isPlayerInDamageArea(float damageAreaRadius)
    {
        var damageAreaCenter = boss.transform.position + (boss.transform.forward * PlayerDistance);
        var playerCurrentPosition = playerCharacter.transform.position;

        var distanceToDamageCenter = Vector3.Distance(damageAreaCenter, playerCurrentPosition);

        return distanceToDamageCenter <= damageAreaRadius;
    }

    private void FlashCharacter()
    {
        playerCharacter.SetActive(!playerCharacter.activeSelf);
        flashingTimeElapsed += 0.05f;
        if (flashingTimeElapsed >= playCharacterFlashDuration)
        {
            CancelInvoke();
            isPlayerFlashing = false;
            flashingTimeElapsed = 0.0f;
            playerCharacter.SetActive(true);
            isEnabled = false;
        }
    }
    
    IEnumerator RemoveDamageAreaAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        
        RemoveDamageArea();
    }

    IEnumerator SetPlayerDamageCheckAfterDelay(float time, bool newValue)
    {
        yield return new WaitForSeconds(time);

        isEnabled = newValue;
        
        // This ensures that the module is only enabled for 0.2 seconds.
        // There is probably a better way to do this, but for now this if fine!
        if (newValue)
        {
            StartCoroutine(SetPlayerDamageCheckAfterDelay(0.2f, false));
        }
        
    }
    

    private void CreateDamageArea(BossDamageType type)
    {
        damageCylinder.transform.localScale = GetScale(type);
        var newPosition = boss.transform.position + (boss.transform.forward * PlayerDistance);
        damageCylinder.transform.position = new Vector3(newPosition.x, 0.1f, newPosition.z);
        damageCylinder.SetActive(true);
        _transparencyController.startAlphaCountDown();
    }

    private Vector3 GetScale(BossDamageType type)
    {
        switch (type)
        {
            case BossDamageType.SmallDamage: 
                return new Vector3(0.25f, 0.1f, 0.25f);
                break;
            case BossDamageType.LargeDamage:
                return new Vector3(0.5f, 0.1f, 0.5f);
            break;
        }
        
        return new Vector3(0.5f, 0.1f, 0.5f);
    }
}
