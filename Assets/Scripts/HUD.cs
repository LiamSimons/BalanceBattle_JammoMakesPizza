using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject prefabHeart;

    [SerializeField]
    GameObject redPepperPrefab;
    [SerializeField]
    GameObject doughPrefab;
    [SerializeField]
    GameObject cheesePrefab;
    [SerializeField]
    GameObject mushroomPrefab;
    [SerializeField]
    GameObject blueCheesePrefab;
    [SerializeField]
    GameObject pepperoniPrefab;
    [SerializeField]
    GameObject tomatoSaucePrefab;

    static Text scoreText;
    static Text gameOverText;
    static Text tvLevelText;
    static Text tvPizzaText;
    static float score = 0;
    
    private const int heartOffset = 30;

    const int maxLives = 5;
    static int lives = maxLives;

    public static bool playing = false;

    public static Timer gameOverTimer;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindWithTag("TextScore").GetComponent<Text>();
        scoreText.text = score.ToString();

        gameOverText = GameObject.FindWithTag("TextGameOver").GetComponent<Text>();
        tvLevelText = GameObject.FindWithTag("TVLevel").GetComponent<Text>();
        tvPizzaText = GameObject.FindWithTag("TVPizza").GetComponent<Text>();

        resetHUD();
        //add score every 1 seconds
        //InvokeRepeating("addScoreText", 0, 1);
    }
    private void Update()
    {
        DisplayCurrentIngredients();
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

    public static void displayVictoryText()
    {
        gameOverText.text = "Y O U  W O N";
    }

    public static void Victory()
    {
        displayVictoryText();
        playing = false;
        gameOverTimer.Run();
    }

    public static void loseHeart()
    {
        if(lives > 1)
        {
            //Debug.Log("LOSE LIFE");
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
        //playing = true;
        score = 0;
        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(prefabHeart);
            heart.transform.parent = transform;
            heart.transform.localPosition = new Vector3(100 + heartOffset * i, 200, 0);
        }
        gameOverTimer = gameObject.AddComponent<Timer>();
        gameOverTimer.Duration = 3f;
        UpdateTVLevelText();
        UpdateTVPizzaText();
    }

    public static void UpdateTVLevelText()
    {
        tvLevelText.text = "Level " + GameData.currentLevel;
    }

    public static void UpdateTVPizzaText()
    {
        if (GameData.currentLevel != 0)
        {
            tvPizzaText.text = GameData.currentPizza.ToString();
        }
        else
        {
            tvPizzaText.text = "Start the game";
        }
    }

    public void DisplayCurrentIngredients()
    {
        if (GameData.newPizzaStart)
        {
            //Debug.Log("DISPLAY INGREDIENTS BEFORE FOR");
            /*int i = 0;
            foreach (var ingredient in GameData.currentIngredientList)
            {
                //Debug.Log("DISPLAY INGREDEIENT " + i);
                // instantiate all the ingredients
                GameObject ingredientPrefab = InstantiateIngredient(ingredient);
                ingredientPrefab.transform.parent = transform;
                ingredientPrefab.transform.localPosition = new Vector3(300, 80 - 40 * i, 0);
                i++;
            }*/
            GameData.Ingredient ingredient = GameData.currentIngredient;
            GameObject ingredientPrefab = InstantiateIngredient(ingredient);
            ingredientPrefab.transform.localScale *= 2;
            ingredientPrefab.transform.parent = transform;
            ingredientPrefab.transform.localPosition = new Vector3(300, 80, 0);
            GameData.newPizzaStart = false;
            UpdateTVLevelText();
            UpdateTVPizzaText();
        }
    }

    public static void PopIngredient()
    {
        GameObject ingredientUI = GameObject.FindGameObjectWithTag("IngredientUI");
        if (ingredientUI == null)
        {
            Debug.Log("INGREDIENT = NULLLLL");
        }
        else
        {
            //heart.GetComponent<Rigidbody2D>().AddForce(heart.transform.up * 2, ForceMode2D.Impulse);
            Destroy(ingredientUI);
        }
    }

    public static void StartGame()
    {
        playing = true;
        GameData.SetNextLevel();
        UpdateTVLevelText();
        UpdateTVPizzaText();
    }
    private GameObject InstantiateIngredient(GameData.Ingredient ingredient)
    {
        //GameData.Ingredient dummyIngredient = GameData.currentIngredient;
        if (ingredient == GameData.Ingredient.Dough) return Instantiate(doughPrefab); // ok
        else if (ingredient == GameData.Ingredient.Cheese) return Instantiate(cheesePrefab); // ok
        else if (ingredient == GameData.Ingredient.BlueCheese) return Instantiate(blueCheesePrefab);
        else if (ingredient == GameData.Ingredient.TomatoSauce) return Instantiate(tomatoSaucePrefab);
        else if (ingredient == GameData.Ingredient.RedPepper) return Instantiate(redPepperPrefab); // ok
        else if (ingredient == GameData.Ingredient.Mushroom) return Instantiate(mushroomPrefab); // ok
        else if (ingredient == GameData.Ingredient.Pepperoni) return Instantiate(pepperoniPrefab);
        else return null;
    }
}