using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float lifeSpan;
    
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    void SetUp(float time)
    {
        lifeSpan = time;
    }
}
