using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;

    [SerializeField] GameObject ballPrefab;

    public Vector3 shootDirection;

    public bool isEnabled;//Vllt removen wenn der nix anderes macht au�er sachen spawnen

    private float shootSpeed = 33f; //fester Wert, nicht �ndern
    public float speedMultiplier = 1f; //wird auf shootSpeed multipliziert, um sich der Tischl�nge anzupassen

    GameManager gameManager;
    [SerializeField] GameObject inputGuy;
    InputScript inputScript;
    RacketScript racketScript;
    public RacketScript racketScriptManuell;
    ParticleSystem partSystem;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        inputScript = inputGuy.GetComponent<InputScript>();
        racketScript = FindObjectOfType<RacketScript>();
        if(racketScript == null)
        {
            racketScript = racketScriptManuell;
        }
        shootDirection = Vector3.Normalize(new Vector3(0, 1, 1)); //schie�t immer 45� nach oben
        partSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (isEnabled && OVRInput.GetDown(OVRInput.Button.One))//TODO gucken welcher Knopf und ob der immer ausl�st oder unter bestimmten Bedingungen
        {
            Debug.Log("SpawnProt");
            SpawnBallProtocol(false);
        }
    }

    public float calculateSpeedMultiplier(float distance)
    {
        //Berechnen mit pq-Formel

        //Formel f�r Flugkurve: f(x) = 1.182*x**2 - 0.1823x
        //Zur Interpolation genutzte Werte: x:0 = y:0 | x:1 = y:1 | x:1.38f = y:2

        float p = -0.1823f / 1.182f; //durch 1.182 teilen, damit x**2 allein steht
        float q = (distance * -1f) / 1.182f; //mit -1 multiplizieren, damit die Funktion gleich 0 gesetzt wird, durch 1.182 teilen, damit x**2 allein steht

        float x1 = -(p / 2f) + Mathf.Sqrt(Mathf.Pow((p / 2f), 2f) - q);
        //float x2 = -(p / 2f) - Mathf.Sqrt(Mathf.Pow((p / 2f), 2f) - q); //idk ob wir das jemals brauchen, aber "ES GIBT IMMER EINEN POSITIVEN UND EINEN NEGATIVEN WERT" ~ jeder Mathelehrer

        return x1;
    }

    public void SpawnBallProtocol(bool waitCont)
    {
        Debug.Log("enterProt");
        racketScript.hitBall = false;
        Debug.Log("2");
        Vector3[] a = inputScript.getFar();
        Debug.Log("3");
        Vector3[] b = inputScript.vertices;
        Debug.Log("spawnTarget");
        gameManager.SpawnTarget(a, inputScript.getNear(), false);//TODO manchmal moving = true setzen, vllt bei jedem 5 ball oder so
        if (ball)
        {
            Destroy(ball);
        }
        Debug.Log("Targetspawned");
        speedMultiplier = calculateSpeedMultiplier(0.5f + (inputScript.GetLenght() * Random.Range(0.65f, 0.85f))); //0.5 = Entfernung vom Spawner zum Tisch | L�nge * Random = Ziel (zwischen 60% und 90% der Tischl�nge)
        Debug.Log("speedmult");
        ball = Instantiate(ballPrefab, transform);
        Debug.Log("ballinit");
        ball.GetComponent<Rigidbody>().AddRelativeForce(shootDirection * shootSpeed * speedMultiplier);
        if (waitCont)
        {
            Debug.Log("GamemodeON");
            ball.GetComponent<BallScript>().GameOnMode();
        }
        StartCoroutine(SpawnParticel(0.1f));
        
    }
    IEnumerator SpawnParticel(float Waitime)
    {
        partSystem.Play();
        yield return new WaitForSeconds(Waitime);
        partSystem.Stop();
    }
}
