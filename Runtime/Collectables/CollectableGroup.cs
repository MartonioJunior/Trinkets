using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MartonioJunior.Trinkets.Collectables
{
    /**
    <summary>Class used to group in-game collectables.</summary>
    */
    public class CollectableGroup: IResourceGroup, ICollectableOperator
    {
        #region Variables
        /**
        <summary>List of collectables in the group which does not belong
        to a category.</summary>
        */
        List<ICollectable> nullCategoryCollectables = new List<ICollectable>();
        /**
        <summary>Dictionary table of the collectables in the group, organized by
        the category that they belong to.</summary>
        */
        Dictionary<ICollectableCategory, List<ICollectable>> contents = new Dictionary<ICollectableCategory, List<ICollectable>>();
        #endregion
        #region IResourceGroup Implementation
        /**
        <summary>Adds a collectable to the group.</summary>
        <param name="data">The collectable to be added.</param>
        <inheritdoc />
        */
        public bool Add(IResourceData data)
        {
            if (!(data.Resource is ICollectable collectable)) return false;

            ICollectableCategory category = collectable.Category;
            if (category == null) {
                nullCategoryCollectables.Add(collectable);
                return true;
            }

            List<ICollectable> list;
            if (!contents.TryGetValue(category, out list)) {
                list = new List<ICollectable>();
                contents[category] = list;
            }
            
            if (list.Contains(collectable)) {
                return false;
            } else {
                list.Add(collectable);
                return true;
            }

        }
        /**
        <summary>Checks the amount of collectables inside a group.</summary>
        <param name="resource">The collectable to be checked.</param>
        <returns><c>0</c> when there's no collectable.<br/>
        <c>1</c> when there's a collectable.</returns>
        <remarks>When a CollectableCategory is supplied, it returns
        the number of collectables that belongs to the category.</remarks>
        <inheritdoc />
        */
        public int AmountOf(IResource resource)
        {
            if (resource is ICollectable collectable) {
                ICollectableCategory category = collectable.Category;
                if (category == null) {
                    return nullCategoryCollectables.Contains(collectable) ? 1 : 0;
                } else if (contents.TryGetValue(category, out var list)) {
                    return list.Contains(collectable) ? 1 : 0;
                }
            } else if (resource is ICollectableCategory category) {
                if (contents.TryGetValue(category, out var list)) {
                    return list.Count;
                }
            }
            
            return 0;
        }
        /**
        <inheritdoc />
        */
        public void Clear()
        {
            nullCategoryCollectables.Clear();
            contents.Clear();
        }
        /**
        <summary>Removes a collectable from the group.</summary>
        <param name="data">The collectable to be removed.</param>
        <remarks>When a Collectable Category is supplied with an amount,
        removes collectables belonging to the category in ascending order
        of addition.</remarks>
        <inheritdoc />
        */
        public bool Remove(IResourceData data)
        {
            if (data.Resource is ICollectable collectable) {
                ICollectableCategory category = collectable.Category;
                if (category == null) {
                    return nullCategoryCollectables.Remove(collectable);
                }

                if (contents.ContainsKey(category)) {
                    return contents[category].Remove(collectable);
                }
            } else if (data.Resource is ICollectableCategory category) {
                if (contents.TryGetValue(category, out var list)) {
                    list.RemoveRange(0, data.Amount);
                }
            }

            return false;
        }
        /**
        <summary>Searches for collectables that fulfill the specified
        predicate.</summary>
        <returns>All collectables which fulfill the predicate.</returns>
        <inheritdoc />
        */
        public ICollection<IResourceData> Search(Predicate<IResourceData> predicate)
        {
            var resultList = new List<IResourceData>();

            resultList.AddRange(ListSearch(predicate, nullCategoryCollectables));
            foreach(var list in contents.Values) {
                resultList.AddRange(ListSearch(predicate, list));
            }

            return resultList.ToArray();
        }
        #endregion
        #region ICollectableOperator Implementation
        /**
        <inheritdoc />
        */
        public int AddFrom(CollectableGroup group, int amount)
        {
            int count = 0;
            var unique = group.Unique(this);

            foreach(var item in unique.Search(null)) {
                if (amount-- <= 0) return count;
                count += Add(item) ? 1 : 0;
            }

            return count;
        }
        /**
        <inheritdoc />
        */
        public int RemoveFrom(CollectableGroup group, int amount)
        {
            int count = 0;
            var overlap = group.Overlap(this);

            foreach(var item in overlap.Search(null)) {
                if (amount-- <= 0) return count;
                count += Remove(item) ? 1: 0;
            }

            return count;
        }
        #endregion
        #region Methods
        /**
        <summary>Allows to search for collectables inside a specified list.</summary>
        <param name="predicate">The function used as a filter.</param>
        <param name="list">The list of collectables.</param>
        <returns>An array of collectables.</returns>
        */
        private IResourceData[] ListSearch(Predicate<IResourceData> predicate, List<ICollectable> list)
        {
            var resultList = new List<IResourceData>();

            foreach(var item in list) {
                IResourceData data = new ResourceData(item);
                if (predicate?.Invoke(data) ?? true) {
                    resultList.Add(data);
                }
            }

            return resultList.ToArray();
        }
        /**
        <summary>Describes the list of collectables that are part of the
        group.</summary>
        <returns>A string describing the contents of the group by
        category.</returns>
        */
        [Obsolete]
        public string DescribeContents()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetDescription("No Category", nullCategoryCollectables));
            foreach(var pair in contents) {
                sb.Append(GetDescription((pair.Key as IRepresentable).Name, pair.Value));
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
        [Obsolete]
        private string GetDescription(string categoryName, List<ICollectable> collectables)
        {
            if (collectables.Count == 0) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(categoryName);
            sb.Append(": ");

            foreach(var collectable in collectables) {
                sb.Append(collectable.ToString());
                sb.Append(" | ");
            }
            sb.Append("\n");
            return sb.ToString();
        }
        #endregion
    }
}