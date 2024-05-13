using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]

namespace UnitTest_
{
    [TestClass]
    public class RecipeTests
    {
        [Test]
        public void CalculateTotalCalories_Returns_CorrectTotalCalories()
        {
            // Arrange
            Recipe recipe = new Recipe("Test Recipe");
            List<Ingredient> ingredients = new List<Ingredient>
        {
            new Ingredient { Name = "Ingredient 1", Quantity = 100, Calories = 10 },
            new Ingredient { Name = "Ingredient 2", Quantity = 200, Calories = 20 },
            new Ingredient { Name = "Ingredient 3", Quantity = 300, Calories = 30 }
        };
            recipe.Ingredients = ingredients;

            // Act
            int totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.AreEqual(1000, totalCalories); // 100 * 10 + 200 * 20 + 300 * 30 = 1000
        }
    }

}

