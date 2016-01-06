using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomForestExplorer.DecisionTrees
{
    /// <summary>
    /// Defines extension methods for querying an ILinqTree
    /// </summary>
    public static class ITreeNodeExtensions
    {
        #region primary Linq methods

        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Descendants<T>(this ITreeNode<T> adapter)
        {
            foreach (var child in adapter.Children())
            {
                yield return child;

                foreach (var grandChild in child.Descendants())
                {
                    yield return grandChild;
                }
            }
        }

        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Ancestors<T>(this ITreeNode<T> adapter)
        {
            var parent = adapter.Parent;
            while (parent != null)
            {
                yield return parent;
                parent = parent.Parent;
            }
        }

        /// <summary>
        /// Returns a collection of child elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Elements<T>(this ITreeNode<T> adapter)
        {
            foreach (var child in adapter.Children())
            {
                yield return child;
            }
        }

        #endregion

        #region 'AndSelf' implementations

        /// <summary>
        /// Returns a collection containing this element and all child elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> ElementsAndSelf<T>(this ITreeNode<T> adapter)
        {
            yield return adapter;

            foreach (var child in adapter.Elements())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection of ancestor elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> AncestorsAndSelf<T>(this ITreeNode<T> adapter)
        {

            yield return adapter;

            foreach (var child in adapter.Ancestors())
            {
                yield return child;
            }
        }

        /// <summary>
        /// Returns a collection containing this element and all descendant elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> DescendantsAndSelf<T>(this ITreeNode<T> adapter)
        {
            yield return adapter;

            foreach (var child in adapter.Descendants())
            {
                yield return child;
            }
        }
        public static IEnumerable<ITreeNode<T>> ElementsAfterSelf<T>(this ITreeNode<T> adapter)
        {
            foreach (var child in adapter.Elements())
            {
                yield return child;
            }
        }
        #endregion

        #region Method which take type parameters

        /// <summary>
        /// Returns a collection of descendant elements.
        /// </summary>
        public static IEnumerable<ITreeNode<T>> Descendants<T, K>(this ITreeNode<T> adapter)            
        {
            return adapter.Descendants().Where(i => i.Item is K);
        }


        #endregion
    }







}
