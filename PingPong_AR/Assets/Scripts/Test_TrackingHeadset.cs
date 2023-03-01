using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TrackingHeadset : MonoBehaviour
{
    [SerializeField] GameObject objecttoFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objecttoFollow.transform.position;
    }
}
