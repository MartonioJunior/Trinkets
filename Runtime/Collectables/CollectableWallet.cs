using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables {
  /**
  <summary>Class used to store in-game collectables.</summary>
  */
  [CreateAssetMenu(fileName = "NewWallet",
                   menuName = "Trinkets/Collectable/Wallet")]
  public class CollectableWallet : EngineScrob, ICollectableWallet {
#region Variables
    /**
    <summary>List of collectables in the wallet which does not belong
    to a category.</summary>
    */
    List<ICollectable> nullCategoryCollectables = new List<ICollectable>();
    /**
    <summary>Dictionary table of the collectables in the wallet, organized by
    the category that they belong to.</summary>
    */
    Dictionary<ICollectableCategory, List<ICollectable>> contents =
        new Dictionary<ICollectableCategory, List<ICollectable>>();
#endregion
#region EngineScrob Implementation
    /**
    <inheritdoc />
    */
    public override void Setup() {}
    /**
    <inheritdoc />
    */
    public override void TearDown() {}
    /**
    <inheritdoc />
    */
    public override void Validate() {}
#endregion
#region ICollectableWallet Implementation
    /**
    <summary>Adds a collectable to the wallet.</summary>
    <param name="collectable">The collectable to be added.</param>
    <inheritdoc />
    */
    public bool Add(ICollectable collectable) {
      ICollectableCategory category = collectable.Category;

      if (category == null) {
        if (nullCategoryCollectables.Contains(collectable)) {
          return false;
        } else {
          nullCategoryCollectables.Add(collectable);
          return true;
        }
      }

      if (!contents.ContainsKey(category)) {
        contents[category] = new List<ICollectable>();
      } else if (contents[category].Contains(collectable)) {
        return false;
      }

      contents[category].Add(collectable);
      return true;
    }
    /**
    <summary>Adds a collectable category to the wallet.</summary>
    <param name="category">The category to be added.</param>
    <remarks>This will also add all of the active collectables belonging to
    the category.</remarks>
    <inheritdoc />
    */
    public bool Add(ICollectableCategory category) {
      bool wasSuccessful = false;
      foreach (var item in category?.Search(null)) {
        if (Add(item))
          wasSuccessful = true;
      }
      return wasSuccessful;
    }
    /**
    <inheritdoc />
    */
    public void Add(ICollectableCategory category, int amount) {
      if (amount <= 0)
        return;

      foreach (var item in category?.Search(null)) {
        if (Add(item))
          amount--;
        if (amount <= 0)
          break;
      }
    }
    /**
    <summary>Checks the amount of a collectable inside a wallet.</summary>
    <param name="collectable">The collectable to be checked.</param>
    <returns><c>0</c> when there's no collectable in the wallet.<br/>
    <c>1</c> when there's a collectable in the wallet.</returns>
    <inheritdoc />
    */
    public int AmountOf(ICollectable collectable) {
      ICollectableCategory category = collectable.Category;

      bool foundInDictionary = category != null &&
                               contents.ContainsKey(category) &&
                               contents[category].Contains(collectable);

      if (foundInDictionary || nullCategoryCollectables.Contains(collectable)) {
        return 1;
      } else {
        return 0;
      }
    }
    /**
    <summary>Gives the number of collectables on a wallet that belong to a
    category.</summary>
    <returns>The amount of collectables from a category.</returns>
    <param name="category">The category to be checked.</param>
    <inheritdoc />
    */
    public int AmountOf(ICollectableCategory category) {
      if (category == null) {
        return nullCategoryCollectables.Count;
      } else if (!contents.ContainsKey(category)) {
        return 0;
      } else {
        return contents[category].Count;
      }
    }
    /**
    <inheritdoc />
    */
    public void Clear() {
      contents.Clear();
      nullCategoryCollectables.Clear();
    }
    /**
    <summary>Searches for collectables inside of a wallet.</summary>
    <inheritdoc />
    */
    public ICollectable[] Search(Predicate<ICollectable> predicate) {
      var resultList = new List<ICollectable>();

      resultList.AddRange(ListSearch(predicate, nullCategoryCollectables));
      foreach (var list in contents.Values) {
        resultList.AddRange(ListSearch(predicate, list));
      }

      return resultList.ToArray();
    }
    /**
    <summary>Searches all the collectable categories inside a collectable
    wallet.</summary> <returns>An array of collectable categories.</returns>
    <inheritdoc />
    */
    public ICollectableCategory[] Search(
        Predicate<ICollectableCategory> predicate) {
      var resultList = new List<ICollectableCategory>();

      if (predicate == null) {
        resultList.AddRange(contents.Keys);
      } else
        foreach (var category in contents.Keys) {
          if (predicate(category))
            resultList.Add(category);
        }

      return resultList.ToArray();
    }
    /**
    <summary>Removes a collectable from the wallet.</summary>
    <param name="collectable">The collectable to be removed.</param>
    <inheritdoc />
    */
    public bool Remove(ICollectable collectable) {
      ICollectableCategory category = collectable.Category;

      if (category == null) {
        return nullCategoryCollectables.Remove(collectable);
      } else if (!contents.ContainsKey(category)) {
        return false;
      }

      return contents[category].Remove(collectable);
    }
    /**
    <summary>Removes a collectable category from the wallet.</summary>
    <remarks>This will also remove all of the active collectables belonging
    to the category.</remarks>
    <param name="category">The category to be removed.</param>
    <inheritdoc />
    */
    public bool Remove(ICollectableCategory category) {
      return contents.Remove(category);
    }
    /**
    <inheritdoc />
    */
    public void Remove(ICollectableCategory category, int amount) {
      if (!contents.ContainsKey(category))
        return;

      var list = contents[category];

      if (amount >= list.Count) {
        list.Clear();
        return;
      }

      for (int i = 0; i < amount; i++) {
        if (list.Count == 0)
          break;
        list.RemoveAt(0);
      }
    }
#endregion
#region Methods
    /**
    <summary>Checks whether a collectable is part of a wallet.</summary>
    <param name="collectable">The collectable to be checked.</param>
    <returns><c>true</c> when the collectable is found.<br/>
    <c>false</c> when the collectable is not found.</returns>
    */
    public bool Contains(ICollectable collectable) {
      ICollectableCategory category = collectable?.Category;

      if (category == null) {
        return nullCategoryCollectables.Contains(collectable);
      } else if (!contents.ContainsKey(category)) {
        return false;
      }

      return contents[category].Contains(collectable);
    }
    /**
    <summary>Describes the list of collectables that are part of the
    wallet.</summary>
    <returns>A string describing the contents of the wallet by
    category.</returns>
    */
    public string DescribeContents() {
      StringBuilder sb = new StringBuilder();
      sb.Append(name);
      sb.Append("\n");

      sb.Append(GetDescription("No Category", nullCategoryCollectables));
      foreach (var pair in contents) {
        sb.Append(
            GetDescription((pair.Key as IRepresentable).Name, pair.Value));
      }
      return sb.ToString();
    }
    /**
    <summary>Provides a formatted string for a category and the respective
    collectables.</summary>
    <param name="categoryName">The name of the category.</param>
    <param name="collectables">The list of collectables to display.</param>
    <returns>A string with the name of the category and it's
    collectables.</returns>
    */
    private string GetDescription(string categoryName,
                                  List<ICollectable> collectables) {
      if (collectables.Count == 0)
        return "";

      StringBuilder sb = new StringBuilder();
      sb.Append(categoryName);
      sb.Append(": ");

      foreach (var collectable in collectables) {
        sb.Append(collectable.ToString());
        sb.Append(" | ");
      }
      sb.Append("\n");
      return sb.ToString();
    }
    /**
    <summary>Allows to search for collectables inside a specified
    list.</summary> <param name="predicate">The function used as a
    filter.</param> <param name="list">The list of collectables.</param>
    <returns>An array of collectables.</returns>
    */
    private ICollectable[] ListSearch(Predicate<ICollectable> predicate,
                                      List<ICollectable> list) {
      if (predicate == null)
        return list.ToArray();

      var resultList = new List<ICollectable>();

      foreach (var item in list) {
        if (predicate(item))
          resultList.Add(item);
      }

      return resultList.ToArray();
    }
#endregion
  }
}