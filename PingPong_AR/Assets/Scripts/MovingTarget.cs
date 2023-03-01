using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3[] far;
    public Vector3 moveAxis;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, far[0]) < 0.1f || Vector3.Distance(transform.position, far[1]) < 0.1f)//TODO wenn er in in einem Vertex gespawnt wird, bleibt er auf der Stelle weil er immer näher als 0.1 dran ist
        {
            moveAxis *= -1;
        }
        transform.Translate(moveAxis * Time.deltaTime); //TODO geschwindigkeit anpassen
    }
}
