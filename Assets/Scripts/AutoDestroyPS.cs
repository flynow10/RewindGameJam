using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyPS : MonoBehaviour
{
    ParticleSystem ps;
    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (!ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}