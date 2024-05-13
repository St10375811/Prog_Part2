using System;
using System.Collections.Generic;
using System.Linq;

// Define the Ingredient class to represent ingredients in a recipe
public class Ingredient
{
    // Name of the ingredient
    public string Name { get; set; }
    // Quantity of the ingredient
    public int Quantity { get; set; }
    // Original quantity of the ingredient
    public int OriginalQuantity { get; set; }
    // Unit of measurement for the ingredient
    public string UnitOfMeasurement { get; set; }
    // Number of calories for the ingredient
    public int Calories { get; set; }
    // Food group that the ingredient belongs to
    public string FoodGroup { get; set; }
}

// Define the Step class to represent steps in a recipe
public class Step
{
    // Description of the step
    public string Description { get; set; }
}

// Define the Recipe class to represent a recipe
public class Recipe
{
    // Name of the recipe
    public string RecipeName { get; set; }
    // List of ingredients in the recipe
    public List<Ingredient> Ingredients { get; set; }
    // List of steps in the recipe
    public List<Step> Steps { get; set; }

    // Constructor to initialize a new recipe with the given name
    public Recipe(string recipeName)
    {
        RecipeName = recipeName;
        Ingredients = new List<Ingredient>(); // Initialize the list of ingredients
        Steps = new List<Step>(); // Initialize the list of steps
    }

    // Method to add an ingredient to the recipe
    public void AddIngredient(Ingredient ingredient)
    {
        Ingredients.Add(ingredient); // Add the ingredient to the list of ingredients
    }

    // Method to add a step to the recipe
    public void AddStep(Step step)
    {
        // Add the step to the list of steps
        Steps.Add(step);
    }

    // Method to display the recipe
    public void DisplayRecipe()
    {
        Console.WriteLine("***********************");
        Console.WriteLine($"Recipe Name: {RecipeName}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"{ingredient.Name} - {ingredient.Quantity} {ingredient.UnitOfMeasurement}");
        }
        Console.WriteLine("Steps:");
        for (int i = 0; i < Steps.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Steps[i].Description}");
        }
        Console.WriteLine("***********************");
    }

    // Method to scale the recipe by a given factor
    public void ScaleRecipe(double factor)
    {
        foreach (var ingredient in Ingredients)
        {
            ingredient.Quantity = (int)(ingredient.OriginalQuantity * factor); // Scale the quantity of each ingredient
        }
    }

    // Method to reset the recipe to its original quantities
    public void ResetRecipe()
    {
        foreach (var ingredient in Ingredients)
        {
            ingredient.Quantity = ingredient.OriginalQuantity; // Reset the quantity of each ingredient
        }
    }

    // Method to calculate the total calories of the recipe
    public int CalculateTotalCalories()
    {
        int totalCalories = 0;
        foreach (var ingredient in Ingredients)
        {
            totalCalories += ingredient.Calories * ingredient.Quantity;
        }
        return totalCalories;
    }
}

// Main program class
public class Program
{
    // Delegate for notifying when a recipe exceeds 300 calories
    public delegate void RecipeCalorieNotification(string recipeName, int totalCalories);

    static void Main(string[] args)
    {
        // Initialize the list of recipes
        List<Recipe> recipes = new List<Recipe>();

        while (true) // Loop for menu-driven interaction
        {
            // Display the menu options
            Console.WriteLine("*****************");
            Console.WriteLine("Welcome To Your Recipe Application");
            Console.WriteLine("***********************");
            Console.WriteLine("1. Enter new recipe");
            Console.WriteLine("2. Display all recipes");
            Console.WriteLine("3. Display recipe details");
            Console.WriteLine("4. Scale recipe");
            Console.WriteLine("5. Reset recipe");
            Console.WriteLine("6. Clear recipe data");
            Console.WriteLine("7. Exit Program");
            Console.WriteLine("*");

            // Get user input for menu choice
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("INVALID CHOICE. PLEASE ENTER A VALID NUMBER.");
                continue;
            }

            switch (choice) // Handle user's choice
            {
                case 1:
                    // Enter a new recipe
                    Console.WriteLine("Enter recipe name: ");
                    string recipeName = Console.ReadLine();
                    Recipe recipe = new Recipe(recipeName);

                    Console.WriteLine("Enter number of ingredients: ");
                    int numIngredients;
                    if (!int.TryParse(Console.ReadLine(), out numIngredients))
                    {
                        Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID NUMBER.");
                        continue;
                    }

                    // Loop to input ingredients
                    for (int i = 0; i < numIngredients; i++)
                    {
                        var ingredient = new Ingredient();

                        Console.WriteLine($"Enter the name of ingredient {i + 1}: ");
                        ingredient.Name = Console.ReadLine();

                        Console.WriteLine($"Enter the quantity of {ingredient.Name}: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal Quantity))
                        {
                            Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID NUMBER.");
                            continue;
                        }
                        // Assign parsed quantity to ingredient.Quantity
                        ingredient.Quantity = (int)Quantity;
                        ingredient.OriginalQuantity = ingredient.Quantity;

                        Console.WriteLine($"Enter the unit of measurement for {ingredient.Name}: ");
                        ingredient.UnitOfMeasurement = Console.ReadLine();

                        Console.WriteLine($"Enter the number of calories for {ingredient.Name}: ");
                        if (!int.TryParse(Console.ReadLine(), out int calories))
                        {
                            Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID NUMBER.");
                            continue;
                        }
                        ingredient.Calories = calories;

                        Console.WriteLine($"Enter the food group for {ingredient.Name}: ");
                        ingredient.FoodGroup = Console.ReadLine();

                        recipe.AddIngredient(ingredient);
                    }

                    // Input steps for the recipe
                    Console.WriteLine("Enter number of steps: ");
                    int numSteps;
                    if (!int.TryParse(Console.ReadLine(), out numSteps))
                    {
                        Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID NUMBER.");
                        continue;
                    }

                    // Loop to input steps
                    for (int i = 0; i < numSteps; i++)
                    {
                        var step = new Step();

                        Console.WriteLine($"Enter step {i + 1}: ");
                        step.Description = Console.ReadLine();

                        recipe.AddStep(step);
                    }

                    // Add the recipe to the list
                    recipes.Add(recipe);
                    break;

                case 2:
                    // Display all recipes
                    Console.WriteLine("List of Recipes:");
                    foreach (var r in recipes.OrderBy(r => r.RecipeName))
                    {
                        Console.WriteLine(r.RecipeName);
                    }
                    break;

                case 3:
                    // Display recipe details
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("NO RECIPES ENTERED YET.");
                    }
                    else
                    {
                        Console.WriteLine("Enter recipe name to display details: ");
                        string recipeToDisplay = Console.ReadLine();
                        Recipe selectedRecipe = recipes.FirstOrDefault(r => r.RecipeName == recipeToDisplay);
                        if (selectedRecipe != null)
                        {
                            selectedRecipe.DisplayRecipe();
                            int totalCalories = selectedRecipe.CalculateTotalCalories();
                            Console.WriteLine($"Total Calories: {totalCalories}");
                            if (totalCalories > 300)
                            {
                                Console.WriteLine("WARNING: Total calories exceed 300!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                    }
                    break;

                case 4:
                    // Scale the recipe
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("NO RECIPES ENTERED YET.");
                    }
                    else
                    {
                        Console.WriteLine("Enter recipe name to scale: ");
                        string recipeToScale = Console.ReadLine();
                        Recipe recipeToScaleObj = recipes.FirstOrDefault(r => r.RecipeName == recipeToScale);
                        if (recipeToScaleObj != null)
                        {
                            Console.WriteLine("Enter scaling factor (0.5, 2, or 3): ");
                            double factor;
                            if (!double.TryParse(Console.ReadLine(), out factor))
                            {
                                Console.WriteLine("INVALID INPUT. PLEASE ENTER A VALID NUMBER.");
                                continue;
                            }
                            recipeToScaleObj.ScaleRecipe(factor);
                            Console.WriteLine("Recipe scaled successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                    }
                    break;

                case 5:
                    // Reset the recipe
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("NO RECIPES ENTERED YET.");
                    }
                    else
                    {
                        Console.WriteLine("Enter recipe name to reset: ");
                        string recipeToReset = Console.ReadLine();
                        Recipe recipeToResetObj = recipes.FirstOrDefault(r => r.RecipeName == recipeToReset);
                        if (recipeToResetObj != null)
                        {
                            recipeToResetObj.ResetRecipe();
                            Console.WriteLine("Recipe reset to original values.");
                        }
                        else
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                    }
                    break;

                case 6:
                    // Clear the recipe data
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("NO RECIPES ENTERED YET.");
                    }
                    else
                    {
                        Console.WriteLine("Enter recipe name to clear: ");
                        string recipeToClear = Console.ReadLine();
                        Recipe recipeToClearObj = recipes.FirstOrDefault(r => r.RecipeName == recipeToClear);
                        if (recipeToClearObj != null)
                        {
                            recipes.Remove(recipeToClearObj);
                            Console.WriteLine("Recipe data cleared.");
                        }
                        else
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                    }
                    break;

                case 7:
                    // Exit the program
                    Console.WriteLine("THANK YOU FOR USING THIS APPLICATION");
                    return;

                default:
                    Console.WriteLine("INVALID CHOICE. PLEASE ENTER A VALID NUMBER.");
                    break;
            }
        }
    }

    // Method to notify when a recipe exceeds 300 calories
    public static void NotifyRecipeCalorieExceedance(string recipeName, int totalCalories)
    {
        Console.WriteLine($"WARNING: Recipe '{recipeName}' has exceeded 300 calories. Total calories: {totalCalories}");
    }
}

