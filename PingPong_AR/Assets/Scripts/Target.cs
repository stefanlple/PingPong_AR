using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    GameManager gameManager;
    RacketScript racketScript;
    [SerializeField] AudioClip HitSound;
    [SerializeField] ParticleSingle ps;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Es gibt nur einen GameManger also liber diesen Befehl müssen und nicht String Refernece basirend
        racketScript = FindObjectOfType<RacketScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Ball") && racketScript.hitBall) //hitBall verhindert, dass der Ball das Target trifft, ohne vorher den Schläger berührt zu haben
        {
            gameManager.HitTarget();
            AudioSource.PlayClipAtPoint(HitSound, transform.position, 1f);
            //Debug.Log("Target destroyed. Cause: Hit Target");
            Instantiate(ps, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
