using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonColorRandomizer : MonoBehaviour
{
    public Material[] balloonMaterials;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && balloonMaterials.Length > 0)
        {
            int randomMaterialIndex = Random.Range(0, balloonMaterials.Length);
            renderer.material = balloonMaterials[randomMaterialIndex];
        }
    }
}
