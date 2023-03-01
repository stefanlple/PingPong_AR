using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayScore : MonoBehaviour
{
   // public GameObject gameManagerObject;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] TextMeshProUGUI txt2;
    [SerializeField] int textSize = 100;
    // Start is called before the first frame update
    void Start()
    {
        txt.fontSize=textSize;
        FindObjectOfType<GameManager>().ApplyingScoreBaord(this);
    }

    // Update is called once per frame
    void Update()
    {
       // int scoreNumber=gameManagerObject.GetComponent<GameManager>().score;
       // txt.text=(scoreNumber).ToString();
    }
    public void UpdateScore(int scoreNumber)
    {
        txt.text = "Score:" +(scoreNumber).ToString();
        txt2.text = "Score:" +(scoreNumber).ToString();
    }
    public void UpdateScoreG(int scoreNumber,int max)
    {
        txt.text = "Score:" + (scoreNumber).ToString() + "/" + max.ToString();
        txt2.text = "Score:" + (scoreNumber).ToString() + "/" + max.ToString();
    }
}
