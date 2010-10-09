using System;
using System.Collections;

namespace AlumnoEjemplos.Piguyis
{
    /// <summary>
    /// Modificacion del FibonacciHeap para que aplique a nuestra solucion.
    /// 
    /// 
    /// 
    /// This class implements a Fibonacci heap data structure. Much of the
    /// code in this class is based on the algorithms in Chapter 21 of the
    /// "Introduction to Algorithms" by Cormen, Leiserson, Rivest, and Stein.
    /// The amortized running time of most of these methods is O(1), making
    /// it a very fast data structure. Several have an actual running time
    /// of O(1). removeMin() and delete() have O(log n) amortized running
    /// times because they do the heap consolidation.
    /// <p><strong>Note that this implementation is not synchronized.</strong>
    /// If multiple threads access a set concurrently, and at least one of the
    /// threads modifies the set, it <em>must</em> be synchronized externally.
    /// This is typically accomplished by synchronizing on some object that
    /// naturally encapsulates the set.</p>
    /// @author  Nathan Fiedler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class FibonacciHeap<T>
    {
        /** Points to the minimum node in the heap. */
        private FibbonacciNode<T> min;

        internal FibbonacciNode<T> Min
        {
            get { return min; }
            set { min = value; }
        }
        /** Number of nodes in the heap. If the type is ever widened,
         * (e.g. changed to long) then recalcuate the maximum Degree
         * Value used in the ~Consolidate()() method. */
        private int n;

        /// <summary>
        /// Consolidates the trees in the heap by joining trees of equal
        /// Degree until there are no more trees of equal Degree in the
        /// root list.
        /// <p><em>Running time: O(log n) amortized</em></p>
        /// </summary>
        private void Consolidate()
        {
            // The magic 45 comes from log base phi of Integer.MAX_VALUE,
            // which is the most elements we will ever hold, and log base
            // phi represents the largest Degree of any root list node.
            FibbonacciNode<T>[] A = new FibbonacciNode<T>[45];

            // For each root list node look for others of the same Degree.
            FibbonacciNode<T> start = Min;
            FibbonacciNode<T> w = Min;
            do
            {
                FibbonacciNode<T> x = w;
                // Because x might be moved, save its sibling now.
                FibbonacciNode<T> nextW = w.Right;
                int d = x.Degree;
                while (A[d] != null)
                {
                    // Make one of the nodes a Child of the other.
                    FibbonacciNode<T> y = A[d];
                    if (x.Key > y.Key)
                    {
                        FibbonacciNode<T> temp = y;
                        y = x;
                        x = temp;
                    }
                    if (y == start)
                    {
                        // Because removeMin() arbitrarily assigned the Min
                        // reference, we have to ensure we do not miss the
                        // end of the root node list.
                        start = start.Right;
                    }
                    if (y == nextW)
                    {
                        // If we wrapped around we need to check for this case.
                        nextW = nextW.Right;
                    }
                    // FibbonacciNode<T>y disappears from root list.
                    y.link(x);
                    // We've handled this Degree, go to next one.
                    A[d] = null;
                    d++;
                }
                // Save this node for later when we might encounter another
                // of the same Degree.
                A[d] = x;
                // Move forward through list.
                w = nextW;
            } while (w != start);

            // The node considered to be Min may have been changed above.
            Min = start;
            // Find the Minimum Key again.
            foreach (FibbonacciNode<T> a in A)
            {
                if (a != null && a.Key < Min.Key)
                {
                    Min = a;
                }
            }
        }

        /// <summary>
        /// Decreases the Key Value for a heap node, given the new Value
        /// to take on. The structure of the heap may be changed, but will
        /// not be ~Consolidate()d.
        /// <p><em>Running time: O(1) amortized</em></p>
        /// @exception  IllegalArgumentException
        ///            if k is larger than x.Key Value.
        /// </summary>
        /// <param name="x">node to decrease the Key of</param>
        /// <param name="k">new Key Value for node x</param>
        public void DecreaseKey(FibbonacciNode<T> x, long k)
        {
            DecreaseKey(x, k, false);
        }

        /// <summary>
        /// Decrease the Key Value of a node, or simply bubble it up to the
        /// top of the heap in preparation for a delete operation.
        /// </summary>
        /// <param name="x">node to decrease the Key of.</param>
        /// <param name="k">new Key Value for node x.</param>
        /// <param name="delete">true if deleting node (in which case, k is ignored).</param>
        private void DecreaseKey(FibbonacciNode<T> x, long k, bool delete)
        {
            if (!delete && k > x.Key)
            {
                throw new ArgumentException("cannot increase Key Value");
            }
            x.Key = k;
            FibbonacciNode<T> y = x.Parent;
            if (y != null && (delete || k < y.Key))
            {
                y.cut(x, Min);
                y.cascadingCut(Min);
            }
            if (delete || k < Min.Key)
            {
                Min = x;
            }
        }

        /// <summary>
        /// Deletes a node from the heap given the reference to the node.
        /// The trees in the heap will be ~Consolidate()d, if necessary.
        /// <p><em>Running time: O(log n) amortized</em></p>
        /// </summary>
        /// <param name="x">node to remove from heap.</param>
        public void Delete(FibbonacciNode<T> x)
        {
            // make x as small as possible
            DecreaseKey(x, 0, true);
            // remove the smallest, which decreases n also
            RemoveMin();
        }

        /// <summary>
        /// Tests if the Fibonacci heap is empty or not. Returns true if
        /// the heap is empty, false otherwise.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <returns>true if the heap is empty, false otherwise.</returns>
        public bool isEmpty
        {
            get
            {
                return Min == null;
            }
        }

        /// <summary>
        /// Inserts a new data element into the heap. No heap consolidation
        /// is performed at this time, the new node is simply inserted into
        /// the root list of this heap.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <param name="x">data object to insert into heap.</param>
        /// <param name="Key">Key Value associated with data object.</param>
        /// <returns>newly created heap node.</returns>
        public FibbonacciNode<T> Insert(T x, long key)
        {
            FibbonacciNode<T> node = new FibbonacciNode<T>(x, key);
            // concatenate node into Min list
            if (Min != null)
            {
                node.Right = Min;
                node.Left = Min.Left;
                Min.Left = node;
                node.Left.Right = node;
                if (key < Min.Key)
                {
                    Min = node;
                }
            }
            else
            {
                Min = node;
            }
            n++;
            return node;
        }

        /// <summary>
        /// Returns the smallest element in the heap. This smallest element
        /// is the one with the Minimum Key Value.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <returns>heap node with the smallest Key, or null if empty.</returns>
        public FibbonacciNode<T> MinimumNode
        {
            get
            {
                return Min;
            }
        }

        /// <summary>
        /// Removes the smallest element from the heap. This will cause
        /// the trees in the heap to be ~Consolidate()d, if necessary.
        /// <p><em>Running time: O(log n) amortized</em></p>
        /// </summary>
        /// <returns>data object with the smallest Key.</returns>
        public FibbonacciNode<T> RemoveMin()
        {
            FibbonacciNode<T> z = Min;

            if (z == null)
            {
                return null;
            }

            if (z.Child != null)
            {
                z.Child.Parent = null;
                // for each Child of z do...
                for (FibbonacciNode<T> x = z.Child.Right; x != z.Child; x = x.Right)
                {
                    // set Parent[x] to null
                    x.Parent = null;
                }
                // merge the children into root list
                FibbonacciNode<T> Minleft = Min.Left;
                FibbonacciNode<T> zchildleft = z.Child.Left;
                Min.Left = zchildleft;
                zchildleft.Right = Min;
                z.Child.Left = Minleft;
                Minleft.Right = z.Child;
            }
            // remove z from root list of heap
            z.Left.Right = z.Right;
            z.Right.Left = z.Left;
            if (z == z.Right)
            {
                Min = null;
            }
            else
            {
                Min = z.Right;
                Consolidate();
            }
            // decrement size of heap
            n--;

            return z;
        }

        /// <summary>
        /// Joins two Fibonacci heaps into a new one. No heap consolidation is
        /// performed at this time. The two root lists are simply joined together.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <param name="H1">first heap</param>
        /// <param name="H2">second heap</param>
        /// <returns>new heap containing H1 and H2</returns>
        public static FibonacciHeap<T> Union(FibonacciHeap<T> H1, FibonacciHeap<T> H2)
        {
            FibonacciHeap<T> H = new FibonacciHeap<T>();

            if (H1 != null && H2 != null)
            {
                H.Min = H1.Min;
                if (H.Min != null)
                {
                    if (H2.Min != null)
                    {
                        H.Min.Right.Left = H2.Min.Left;
                        H2.Min.Left.Right = H.Min.Right;
                        H.Min.Right = H2.Min;
                        H2.Min.Left = H.Min;
                        if (H2.Min.Key < H1.Min.Key)
                        {
                            H.Min = H2.Min;
                        }
                    }
                }
                else
                {
                    H.Min = H2.Min;
                }
                H.n = H1.n + H2.n;
            }
            return H;
        }

        #region IOrderSerchStruct<T> Members

        private Hashtable internalHashtable = new Hashtable();
        /// <summary>
        /// Returns the size of the heap which is measured in the
        /// number of elements contained in the heap.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <return>number of elements in the heap.</return>
        public int Size
        {
            get
            {
                return n;
            }
        }

        public void Add(T item)
        {
            //TODO como evitar este casteo?
            //FibbonacciNode<T> fbNode = this.Insert(item, item);
            //internalHashtable.Add(item.GetHashCode(), fbNode);
#warning TODO THIS; 
        }

        /// <summary>
        /// Removes all elements from this heap.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        public void Clear()
        {
            internalHashtable.Clear();
            Min = null;
            n = 0;
        }

        public bool Contains(T item)
        {
            return internalHashtable.Contains(item.GetHashCode());
        }

        public T FindInStruct(T item)
        {
            return (internalHashtable[item.GetHashCode()] as FibbonacciNode<T>).Value;
        }

        public bool Remove(T item)
        {
            if (internalHashtable[item.GetHashCode()] != null)
            {
                this.Delete(internalHashtable[item.GetHashCode()] as FibbonacciNode<T>);
                internalHashtable.Remove(item.GetHashCode());
                return true;
            }
            return false;
        }

        public T getMinValue()
        {
            return this.MinimumNode.Value;
        }

        public T getMinimoAndRemove()
        {
            FibbonacciNode<T> minNode = this.RemoveMin();
            internalHashtable.Remove(minNode.Value.GetHashCode());
            return minNode.Value;
        }

        #endregion
    }
}
