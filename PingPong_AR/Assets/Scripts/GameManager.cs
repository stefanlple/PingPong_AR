using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public GameObject target;
    public GameObject targetPrefab;
    public Vector3[] site1; // Die zwei Ecken von einer Seite; m�ssen im Input �bergeben werden
    public Vector3[] site2;
    public GameObject player;
    public GameObject ballSpawner;
    BallSpawner BallSpawnerLogik;

    DisplayScore ScoreBoard;

    InputScript inputscript;

    bool GameOngoing = false;
    int maxrounds = 10;
    int rounds = 10;

    [SerializeField] AudioClip EndGameSound;
    [SerializeField] AudioClip StartGameSound;

    private void Start()
    {
        BallSpawnerLogik = ballSpawner.GetComponent<BallSpawner>();
        inputscript = FindObjectOfType<InputScript>();  
    }


    public void HitTarget()
    {
        Destroy(target);
        score++;
        if (ScoreBoard)
        {
            if (GameOngoing)
            {
                ScoreBoard.UpdateScoreG(score,maxrounds);
            }
            else
            {
                ScoreBoard.UpdateScore(score);
            }
            
        }
    }

    public void SpawnTargetSimple()
    {
        target = Instantiate(targetPrefab, new Vector3(-1,1,2), Quaternion.identity);
    }
    public void SetSite(Vector3 pointA,Vector3 pointB)
    {
        site1[0] = pointA;
        site1[1] = pointB;

    }
    public void SpawnTarget(Vector3[] far, Vector3[] near, bool moving) //TODO vllt Courutine dass man Zeit hat sich auf das Target einzustellen, dann muss aber beim ballspawner nachdem man drückt enabled auf false gesetzt werden damit man nicht mehrere Bälle auf einmal scheißen kann
    {

        if (target)
        {
            Destroy(target);
        }

        //target = Instantiate(targetPrefab, (farthestPoint + range), Quaternion.identity); // TODO rotation anpassen

        /*
        Vector3 targetPositionX = Near[0] + ((Near[1] - Near[0]) * Random.Range(0f, 1f)); //0: linke Seite | 1: rechte Seite
        Vector3 targetPositionY = new Vector3(0, 0, 0); //0: genau auf der Platte | >0: irgendwo �ber der Platte
        Vector3 targetPositionZ = (Near[0]-far[0]); //* Random.Range(0f, 0.45f); //0: Ende wo der Ball spawnt | 1: Ende wo der Ball nicht spawnt
        Vector3 targetPosition = targetPositionX + targetPositionY + targetPositionZ;
        target = Instantiate(targetPrefab, targetPosition, Quaternion.FromToRotation(Vector3.right, parallel)); // TODO rotation anpassen

        */

        Vector3 targetPosition = far[0] + ((far[1] - far[0]) * Random.Range(0f, 1f));
        
        targetPosition.y += Random.Range(0.05f, 0.15f);

        targetPosition += CaculauteDir(near[0], far[1]) * 0.5f; 


        target = Instantiate(targetPrefab, targetPosition, Quaternion.identity); // TODO rotation anpassen
        Vector3 lookPosition = near[0] + ((near[1] - near[0]) * 0.5f);

        target.transform.LookAt(lookPosition);
        if (moving) 
        {
            target.GetComponent<MovingTarget>().far = far;
            target.GetComponent<MovingTarget>().moveAxis = (far[1] - far[0]);
            target.GetComponent<MovingTarget>().enabled = true;
        }
        MoveBallSpawner(far, near);
    }


    public void MoveBallSpawner(Vector3[] far, Vector3[] near)
    {    
        Vector3 position = far[0] + ((far[1] - far[0]) * Random.Range(0.1f, 0.9f));

        position += CaculauteDir(near[0], far[1]) * 0.5f; 

        ballSpawner.transform.position = position;

        Vector3 lookPosition = (near[0] + ((near[1] - near[0]) * Random.Range(0.25f, 0.75f))); // - ((near[0] - far[1]) * 0.75f);
        lookPosition.y = ballSpawner.transform.position.y; //Er soll genau auf seiner Höhe gucken
        ballSpawner.transform.LookAt(lookPosition);
    }

    private Vector3 CaculauteDir(Vector3 near,Vector3 far)
    {
        Vector3 ReturnVector = (far - near).normalized;
        ReturnVector.y = 0;
        return ReturnVector; // (getNear()[0]- getFar()[1]);              
    }
    public void ApplyingScoreBaord(DisplayScore ds)
    {
        ScoreBoard = ds;
    }
    public void StartGame()
    {
       if (!GameOngoing)
       {
            AudioSource.PlayClipAtPoint(StartGameSound, Camera.main.transform.position);
            rounds = maxrounds;
            score = 0;
            Debug.Log("1");
            ScoreBoard.UpdateScoreG(score, maxrounds);
            GameOngoing = true;
            Debug.Log("ContGame");
            ContGame();
       }
        
    }
    public void ContGame()
    {
        if (GameOngoing)
        {
            rounds--;
            if (rounds <= 0)
            {
                Debug.Log("Gameoff");
                GameOngoing = false;
                AudioSource.PlayClipAtPoint(EndGameSound,Camera.main.transform.position);
            }
            else
            {
                Debug.Log("GameBallProt");
                BallSpawnerLogik.SpawnBallProtocol(true);
            }
           
        }
       
    }

}
