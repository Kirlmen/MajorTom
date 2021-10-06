using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject, 0.2f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
