using App.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.models
{
    /// <summary>
    /// Defines the properties, structure / syntax and methods for a Knapsack
    /// </summary>
    public interface IKnapsack
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the maximum weight for the Knapsack
        /// </summary>
        int MaximumWeight { get; set; }
        /// <summary>
        /// Gets or Sets the potential items to be placed inside the Knapsack
        /// </summary>
        List<IKnapsackItem> KnapsackItems { get; set; }
        /// <summary>
        /// Gets or Sets a File IO wrapper
        /// </summary>
        IFileIOWrapper FileIOWrapper { get; set; }
        /// <summary>
        /// Gets or Sets the contents / items in the Knapsack
        /// </summary>
        List<IKnapsackItem> KnapsackContents { get; set; }
        /// <summary>
        /// Gets the current total weight of Knapsack contents / items
        /// </summary>
        int TotalWeight { get; }
        #endregion Internal Properties

        #region Internal Methods
        /// <summary>
        /// Adds items to the Knapsack, returns the list of added / selected Knapsack items
        /// </summary>
        /// <returns>List<IKnapsackItem></returns>
        List<IKnapsackItem> AddItems();
        /// <summary>
        /// Empties the Knapsack / Removes all items added to the Knapsack
        /// </summary>
        void EmptyKnapsack();
        #endregion Internal Methods
    }

    /// <summary>
    /// Implements the properties, structure / syntax and methods for a Knapsack
    /// </summary>
    public class Knapsack : IKnapsack
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the maximum weight for the Knapsack
        /// </summary>
        public int MaximumWeight { get; set; }
        /// <summary>
        /// Gets or Sets the potential items to be placed inside the Knapsack
        /// </summary>
        public List<IKnapsackItem> KnapsackItems { get; set; }
        /// <summary>
        /// Gets or Sets a File IO wrapper
        /// </summary>
        public IFileIOWrapper FileIOWrapper { get; set; }
        /// <summary>
        /// Gets or Sets the contents / items in the Knapsack
        /// </summary>
        public List<IKnapsackItem> KnapsackContents { get; set; }
        /// <summary>
        /// Gets the current total weight of Knapsack contents / items
        /// </summary>
        public int TotalWeight => KnapsackContents.Sum(knapsackItem => knapsackItem.Weight);
        #endregion Internal Properties

        #region Contructor(s)
        /// <summary>
        /// Instantiates a new Knaspack
        /// </summary>
        public Knapsack()
        {
            KnapsackItems = new List<IKnapsackItem>();
            KnapsackContents = new List<IKnapsackItem>();
        }
        /// <summary>
        /// Instantiates a new Knaspack with the specified maximum weight
        /// </summary>
        /// <param name="maximumWeight">the maximum weight of the Knapsack</param>
        public Knapsack(int maximumWeight) : this()
        {
            MaximumWeight = maximumWeight;
        }
        /// <summary>
        /// Instantiates a new Knaspack with the specified maximum weight and list of Knapsack items to use
        /// </summary>
        /// <param name="maximumWeight">the maximum weight of the Knapsack</param>
        /// <param name="knapsackItems">the list of Knapsack items to use</param>
        public Knapsack(int maximumWeight, List<IKnapsackItem> knapsackItems) : this(maximumWeight)
        {
            KnapsackItems = knapsackItems.IfEmpty(new List<IKnapsackItem>());
        }
        /// <summary>
        /// Instantiates a new Knaspack with the specified maximum weight and using the list of Knapsack items from the File IO wrapper
        /// </summary>
        /// <param name="maximumWeight">the maximum weight of the Knapsack</param>
        /// <param name="fileIOWrapper">the file IO wrapper</param>
        public Knapsack(int maximumWeight, IFileIOWrapper fileIOWrapper) : this(maximumWeight)
        {
            FileIOWrapper = fileIOWrapper;
            KnapsackItems = FileIOWrapper.ReadAllLines().Skip(1).Select(item => new KnapsackItem(item)).ToList<IKnapsackItem>();
        }
        #endregion Contructor(s)

        #region Internal Methods
        /// <summary>
        /// Adds items to the Knapsack, returns the list of added / selected Knapsack items
        /// </summary>
        /// <returns>List<IKnapsackItem></returns>
        public List<IKnapsackItem> AddItems()
        {
            try
            {
                foreach(IKnapsackItem selectedKnapsackItem in GetSeletedKnapsackItems(KnapsackItems.OrderByDescending(knapsackItem => knapsackItem.Value).ToList()))
                {
                    AddItem(selectedKnapsackItem);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally { };
            return KnapsackContents;
        }

        private IEnumerable<IKnapsackItem> GetSeletedKnapsackItems(List<IKnapsackItem> knapsackItems)
        {
            List<IKnapsackItem> selectedKnapsackItems = new List<IKnapsackItem>();
            try
            {
                foreach (IKnapsackItem knapsackItem in knapsackItems)
                {
                    var selectedKnapsackItem = GetSelectedKnapsackItem(selectedKnapsackItems, knapsackItem);
                    var selectedKnapsackItemIndex = selectedKnapsackItems.IndexOf(selectedKnapsackItem);
                    if (selectedKnapsackItems.IsEmpty() || selectedKnapsackItem.IsNotSet())
                    {
                        selectedKnapsackItems.Add(knapsackItem);
                    }
                    else if (selectedKnapsackItem.IsSet() && selectedKnapsackItemIndex > -1)
                    {
                        selectedKnapsackItems.Insert(selectedKnapsackItemIndex, knapsackItem);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally { };
            return selectedKnapsackItems;
        }

        static IKnapsackItem GetSelectedKnapsackItem(List<IKnapsackItem> selectedKnapsackItems, IKnapsackItem knapsackItem)
        {
            return selectedKnapsackItems.FirstOrDefault(ski => knapsackItem.DiffWeightValue < ski.DiffWeightValue);
        }

        private void AddItem(IKnapsackItem knapsackItem)
        {
            if(knapsackItem.IsSet() && !KnapsackContents.Contains(knapsackItem) && (TotalWeight + knapsackItem.Weight) <= MaximumWeight)
            {
                KnapsackContents.Add(knapsackItem);
            }
        }

        private IEnumerable<IKnapsackItem> GetSeletedKnapsackItems(IKnapsackItem knapsackItem)
        {
            List<IKnapsackItem> selectedItems = new List<IKnapsackItem>();
            if (selectedItems.IsEmpty())
            {
                selectedItems.Add(knapsackItem);
            }
            return selectedItems;
        }

        /// <summary>
        /// Empties the Knapsack / Removes all items added to the Knapsack
        /// </summary>
        /// <returns>returns true if Knapsack is empty, false if otherwise</returns>
        public void EmptyKnapsack()
        {
            KnapsackContents = new List<IKnapsackItem>();
        }
        #endregion Internal Methods
    }
}
