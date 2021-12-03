using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject prefabHeart;

    static Text scoreText;
    static Text gameOverText;
    static float score = 0;
    
    private const int heartOffset = 30;

    const int maxLives = 5;
    static int lives = maxLives;

    public static bool playing = true;

    public static Timer gameOverTimer;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindWithTag("TextScore").GetComponent<Text>();
        scoreText.text = score.ToString();

        gameOverText = GameObject.FindWithTag("TextGameOver").GetComponent<Text>();

        resetHUD();
        //add score every 1 seconds
        //InvokeRepeating("addScoreText", 0, 1);
    }

    public static void addScoreText()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    public static void displayGameOverText()
    {
        gameOverText.text = "G A M E  O V E R";
    }

    public static void loseHeart()
    {
        if(lives > 1)
        {
            Debug.Log("LOSE LIFE");
            lives--;
            deleteHeart();
        }
        else
        {
            if(lives != 0)
            {
                deleteHeart();
            }
            displayGameOverText();
            playing = false;
            gameOverTimer.Run();
        }
    }

    public static void deleteHeart()
    {
        GameObject heart = GameObject.FindGameObjectWithTag("Heart");
        if (heart == null)
        {
            Debug.Log("HEART = NULLLLL");
        }
        else
        {
            //heart.GetComponent<Rigidbody2D>().AddForce(heart.transform.up * 2, ForceMode2D.Impulse);
            Destroy(heart);
        }
    }

    public void resetHUD()
    {
        lives = maxLives;
        playing = true;
        score = 0;
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(prefabHeart);
            heart.transform.parent = transform;
            heart.transform.localPosition = new Vector3(300 + heartOffset * i, 250, 0);
        }
        gameOverTimer = gameObject.AddComponent<Timer>();
        gameOverTimer.Duration = 3f;
    }
}