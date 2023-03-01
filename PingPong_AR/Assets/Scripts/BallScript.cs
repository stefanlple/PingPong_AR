using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] AudioClip ballGround;
    bool GameMode = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.tag.Equals("Racket")))
        {
            
            AudioSource.PlayClipAtPoint(ballGround, transform.position, 1f);
           

        }
        if (collision.gameObject.tag.Equals("Floor"))
        {
            //FindObjectOfType<GameManager>().ContGame(); // Nicht die perfomantse Lösung , besser wäre eine Evnt aber da es keine anderen gibt vllt. nicht passend.
            StartCoroutine(WaitThenCont(0.5f));
        }
    }
    public IEnumerator WaitThenCont(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        FindObjectOfType<GameManager>().ContGame();
        if (GameMode)
        {
            Destroy(gameObject);
        }
       
    }
    public void GameOnMode()
    {
        GameMode = true;
        StartCoroutine(WaitThenCont(15));
    }
    
}
