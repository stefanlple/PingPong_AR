using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketScript : MonoBehaviour
{
    bool rightHandstate;

    Transform currentHandtoTrack;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform leftHand;
    
    GameObject otherHandObject;
    [SerializeField] GameObject rightHandGameobject;
    [SerializeField] GameObject leftHandGameobject;

    GameObject otherController;
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject leftController;

    Rigidbody racketRigi;
    public bool hitBall;
    [SerializeField] AudioClip ballhitSound1;
    // Start is called before the first frame update


    void Start()
    {
        rightHandstate=SceneLoader.rightHandState;

        racketRigi = GetComponent<Rigidbody>();
          
        currentHandtoTrack=rightHandstate?rightHand:leftHand;
        otherHandObject=rightHandstate?leftHandGameobject:rightHandGameobject;
        otherController=rightHandstate?leftController:rightController;

        
        //otherHandObject.GetComponent<ShpereCollider>();
        otherController.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        racketRigi.velocity = (currentHandtoTrack.position - transform.position) / Time.fixedDeltaTime;

        /*
        Quaternion roationdiff = currentHandtoTrack.rotation * Quaternion.Inverse(transform.rotation);
        roationdiff.ToAngleAxis(out float angleInDeg, out Vector3 rotationAxis);

        Vector3 rotationDifferneceInDegree = angleInDeg * rotationAxis;

        racketRigi.angularVelocity = (rotationDifferneceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
        */
        
      //  transform.rotation = currentHandtoTrack.rotation; // Nur zum Test -1
        
        racketRigi.MoveRotation( currentHandtoTrack.rotation); // Nur zum Test -1
    }

    //vibrationTime = Zeit in Sekunden, die der Controller vibrieren soll
    //controller = entweder OVRInput.Controller.RHand oder OVRInput.Controller.LHand f�r rechts oder links
    public IEnumerator Vibrate(float vibrationTime, OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(0.1f, 1f, controller);

        yield return new WaitForSeconds(vibrationTime);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            hitBall = true;
            AudioSource.PlayClipAtPoint(ballhitSound1, transform.position, 1f);
            if (rightHandstate)
            {
                StartCoroutine(Vibrate(0.05f, OVRInput.Controller.RHand));
            }
            else
            {
                StartCoroutine(Vibrate(0.05f, OVRInput.Controller.LHand));
            }

        }
    }

    public static implicit operator RacketScript(GameObject v)
    {
        throw new NotImplementedException();
    }
}
