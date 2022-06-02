using App.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.models
{
    /// <summary>
    /// Defines the properties, structure / syntax and methods for a Knapsack item
    /// </summary>
    public interface IKnapsackItem
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the name of the Knapsack item
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or Sets the weight of the Knapsack item
        /// </summary>
        int Weight { get; set; }
        /// <summary>
        /// Gets or Sets the value of the Knapsack item
        /// </summary>
        int Value { get; set; }
        /// <summary>
        /// Gets the difference of the Weight from the Value i.e. what is the importance / signifance of this item
        /// </summary>
        int DiffWeightValue { get; }
        #endregion Internal Properties
    }

    /// <summary>
    /// Implements the properties, structure / syntax and methods for a Knapsack item
    /// </summary>
    public class KnapsackItem : IKnapsackItem
    {
        #region Internal Properties
        /// <summary>
        /// Gets or Sets the name of the Knapsack item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or Sets the weight of the Knapsack item
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        /// Gets or Sets the value of the Knapsack item
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// Gets the difference of the Weight from the Value i.e. what is the importance / signifance of this item
        /// </summary>
        public int DiffWeightValue => Value - Weight;
        #endregion Internal Properties

        #region Constructor(s)
        public KnapsackItem(string item)
        {
            Name = item.GetElementAt(0, ',').AsString();
            Weight = item.GetElementAt(1, ',').AsInt32();
            Value = item.GetElementAt(2, ',').AsInt32();
        }
        #endregion Constructor(s)
    }
}
