using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomForestExplorer.DecisionTrees
{
    public static class ITreeNodeEnumerableExtensions
    {
        /// <summary>
        /// Applies the given function to each of the items in the supplied
        /// IEnumerable.
        /// </summary>
        private static IEnumerable<ITreeNode<T>> DrillDown<T>(this IEnumerable<ITreeNode<T>> items,
            Func<ITreeNode<T>, IEnumerable<ITreeNode<T>>> function)
        {
            foreach (var item in items)
            {
                foreach (ITreeNode<T> itemChild in function(item))
                {
                    yield return itemChild;
                }
            }
        }

        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Descendants<T>(this IEnumerable<ITreeNode<T>> items)
        {
            
            return items.DrillDown(i => i.Descendants());
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantsAndSelf<T>(this IEnumerable<ITreeNode<T>> items)
        {
            return items.DrillDown(i => i.DescendantsAndSelf());
        }

        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Ancestors<T>(this IEnumerable<ITreeNode<T>> items)
        {
            return items.DrillDown(i => i.Ancestors());
        }

        /// <summary>
        /// Returns a collection containing this element and all ancestor elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> AncestorsAndSelf<T>(this IEnumerable<ITreeNode<T>> items)
        {
            return items.DrillDown(i => i.AncestorsAndSelf());
        }

        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Elements<T>(this IEnumerable<ITreeNode<T>> items)
        {
            return items.DrillDown(i => i.Elements());
        }

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> ElementsAndSelf<T>(this IEnumerable<ITreeNode<T>> items)
        {
            return items.DrillDown(i => i.ElementsAndSelf());
        }
           
    }
}
