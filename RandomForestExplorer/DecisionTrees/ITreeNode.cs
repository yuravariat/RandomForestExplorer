using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomForestExplorer.DecisionTrees
{
    /// <summary>
    /// Defines an adapter that must be implemented in order to use the Tree
    /// extension methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITreeNode<T>
    {
        /// <summary>
        /// Obtains all the children of the Item.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITreeNode<T>> Children();

        /// <summary>
        /// The parent of the Item.
        /// </summary>
        ITreeNode<T> Parent { get; }

        /// <summary>
        /// The item being adapted.
        /// </summary>
        T Item { get; }
    }
}
