using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSpawner : MonoBehaviour
{
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
    [SerializeField]
    GameObject angryLogPrefab;

    Timer fallTimer;

    [SerializeField]
    float minTime = 6;
    [SerializeField]
    float maxTime = 8;

    float scaledMinTime;
    float scaledMaxTime;

    [SerializeField]
    float minX;
    
    [SerializeField]
    float maxX;
    
    
    [SerializeField]
    float spawnerRatio = 2;

    bool loadingStarted = false;

    bool startup = true;

    // Start is called before the first frame update
    void Start()
    {
        loadingStarted = false;
        if(startup)
        {
            fallTimer = gameObject.AddComponent<Timer>();
            fallTimer.Duration = NextFloat(minTime, maxTime);
            fallTimer.Run();
            float screenWidth = Screen.width;
            maxX = ScreenUtils.ScreenRight / spawnerRatio;
            minX = ScreenUtils.ScreenLeft / spawnerRatio;
            startup = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        scaledMinTime = minTime;
        scaledMaxTime = maxTime;
        scaledMinTime -= GameData.currentLevel / 2;
        scaledMaxTime -= GameData.currentLevel / 2;
        if (HUD.playing)
        {
            if (fallTimer.Finished)
            {
                //spawn een bal en begin opnieuw
                spawnBlock();
                //
                fallTimer.Duration = NextFloat(scaledMinTime, scaledMaxTime);
                fallTimer.Run();
            }
        }
        else
        {
            if (HUD.gameOverTimer.Finished && !loadingStarted)
            {
                //SceneManager.UnloadScene(SceneManager.GetActiveScene().buildIndex);
                SceneManager.LoadScene(0);
                loadingStarted = true;
            }
        }
    }
    static float NextFloat(float minimum, float maximum)
    {
        System.Random random = new System.Random();
        double value = random.NextDouble() * (maximum - minimum) + minimum;
        return (float)value;
    }

    void spawnBlock()
    {
        float RandX = NextFloat(minX, maxX);
        float RandY = ScreenUtils.ScreenTop;
        Vector3 blockPos = new Vector3(RandX, RandY, 0);
        float angryLogChance = NextFloat(0, 1);
        GameObject block;
        if (angryLogChance > (0.05*(GameData.currentLevel)))
        {
            block = InstantiateCurrentIngredient();
        }
        else
        {
            block = Instantiate(angryLogPrefab);
        }
        block.transform.position = blockPos;
    }

    private GameObject InstantiateCurrentIngredient()
    {
        System.Random random = new System.Random();
        float otherIngredient = NextFloat(0, 1);
        GameData.Ingredient dummyIngredient;
        if (otherIngredient < 0.05 * (GameData.currentLevel))
        {
            int randIngredientIndex = random.Next(0, GameData.currentIngredientList.Length);
            dummyIngredient = GameData.currentIngredientList[randIngredientIndex];
        }
        else dummyIngredient = GameData.currentIngredient;
        
        if (dummyIngredient == GameData.Ingredient.Dough) return Instantiate(doughPrefab); // ok
        else if (dummyIngredient == GameData.Ingredient.Cheese) return Instantiate(cheesePrefab); // ok
        else if (dummyIngredient == GameData.Ingredient.BlueCheese) return Instantiate(blueCheesePrefab); 
        else if (dummyIngredient == GameData.Ingredient.TomatoSauce) return Instantiate(tomatoSaucePrefab); 
        else if (dummyIngredient == GameData.Ingredient.RedPepper) return Instantiate(redPepperPrefab); // ok
        else if (dummyIngredient == GameData.Ingredient.Mushroom) return Instantiate(mushroomPrefab); // ok
        else if (dummyIngredient == GameData.Ingredient.Pepperoni) return Instantiate(pepperoniPrefab); 
        else return null;
    }
}
