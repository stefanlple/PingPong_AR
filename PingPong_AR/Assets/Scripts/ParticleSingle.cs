using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSingle : MonoBehaviour
{
    [SerializeField] float DurationBDestroy = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.duration = DurationBDestroy;
        Destroy(gameObject, DurationBDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
