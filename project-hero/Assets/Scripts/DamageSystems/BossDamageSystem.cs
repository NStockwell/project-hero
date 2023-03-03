using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject boss;

    [SerializeField] private GameObject playerCharacter;

    private const float PlayerDistance = 7.0f;

    private const float SmallAttackDamageArea = 2.0f;
    private const float LargeAttackDamageArea = 4.0f;


    private float playCharacterFlashDuration = 1.0f;
    private bool isPlayerFlashing = false;
    private float flashingTimeElapsed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (isPlayerInDamageArea(SmallAttackDamageArea) && !isPlayerFlashing)
        {
            isPlayerFlashing = true;
            InvokeRepeating(nameof(FlashCharacter), 0.1f, 0.05f);
        }
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
        }
    }

    private IEnumerator FlashCoroutine()
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < playCharacterFlashDuration)
        {
            playerCharacter.SetActive(!playerCharacter.activeSelf);
            timeElapsed += 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        playerCharacter.SetActive(true);
        isPlayerFlashing = false;
    }
}
