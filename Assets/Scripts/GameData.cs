using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public enum Pizza { Margherita, Pepperoni, QuattroFormaggi, Diavola };
    public enum Ingredient { RedPepper, Mushroom, Cheese, BlueCheese, TomatoSauce, Dough, Pepperoni }; //mss niet dough
    public enum Level { Level1, Level2, Level3, Level4, Level5 };   

   
   
    static Ingredient[] margherita = { Ingredient.Dough, Ingredient.TomatoSauce, Ingredient.Cheese };
    static Ingredient[] pepperoni = { Ingredient.Dough, Ingredient.TomatoSauce, Ingredient.Cheese, Ingredient.Pepperoni };
    static Ingredient[] quattroFormaggi = { Ingredient.Dough, Ingredient.TomatoSauce, Ingredient.Cheese, Ingredient.BlueCheese };
    static Ingredient[] diavola = { Ingredient.Dough, Ingredient.TomatoSauce, Ingredient.Cheese, Ingredient.RedPepper };

    static Pizza[] level1 = { Pizza.Margherita };
    static Pizza[] level2 = { Pizza.Margherita, Pizza.Pepperoni };
    static Pizza[] level3 = { Pizza.Pepperoni, Pizza.Margherita, Pizza.Diavola };
    static Pizza[] level4 = { Pizza.Diavola, Pizza.QuattroFormaggi, Pizza.Diavola, Pizza.Pepperoni };
    static Pizza[] level5 = { Pizza.QuattroFormaggi, Pizza.Pepperoni, Pizza.Margherita, Pizza.QuattroFormaggi, Pizza.Diavola };

    public static readonly Dictionary<Pizza, Ingredient[]> ingredientsOfPizza = new Dictionary<Pizza, Ingredient[]> 
    {
        { Pizza.Margherita, margherita },
        { Pizza.Pepperoni, pepperoni },
        { Pizza.QuattroFormaggi, quattroFormaggi },
        { Pizza.Diavola, diavola }
    };
    public static Dictionary<Level, Pizza[]> pizzasPerLevel = new Dictionary<Level, Pizza[]>
    {
        { Level.Level1, level1 },
        { Level.Level2, level2 },
        { Level.Level3, level3 },
        { Level.Level4, level4 },
        { Level.Level5, level5 },
    };

    //
    public static int currentLevel = 0;
    public static Pizza currentPizza;
    public static Ingredient currentIngredient;

    private static int currentPizzaIndex = 0;

    public static void SetNextLevel()
    {
        if (currentLevel == pizzasPerLevel.Count)
        {
            currentLevel = 0;
            HUD.Victory();
        }
        else
        {
            currentLevel++;
            currentPizzaIndex = 0;
            SetNextPizza();
        }
    }
    public static void SetNextPizza()
    {
        if (currentLevel == 0)
        {
            return;
        }

        Level dummyCurrentLevel = (Level)(currentLevel - 1);
        Pizza[] dummyPizzasPerLevel = pizzasPerLevel[dummyCurrentLevel];
        //Console.WriteLine("pizzas this level = " + dummyPizzasPerLevel.Length);
        //Console.WriteLine("currentPizzaIndex = " + currentPizzaIndex);
        if (currentPizzaIndex >= dummyPizzasPerLevel.Length)
        {
            SetNextLevel();
            return;
        }
        currentPizza = dummyPizzasPerLevel[currentPizzaIndex];
        currentPizzaIndex++;
    }
}
