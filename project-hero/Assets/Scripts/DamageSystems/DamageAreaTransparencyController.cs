using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAreaTransparencyController : MonoBehaviour
{
    // Start is called before the first frame update

    private float currentAlpha = 0.0f;

    private Material meshMaterial;
    
    void Start()
    {
        // Get the mesh renderer component
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Get the material
        meshMaterial = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAlphaCountDown()
    {
        currentAlpha = 0.0f;
        InvokeRepeating(nameof(updateAlpha), 0.0f,0.02f);
    }

    private void updateAlpha()
    {
        if (currentAlpha <= 1.0)
        {
            // Set the alpha value to 0.5
            Color color = meshMaterial.color;
            color.a = currentAlpha;
            meshMaterial.color = color;
            
            // Update Alpha
            currentAlpha += 0.02f;
        }
        else
        {
            CancelInvoke();
        }
    }
}
