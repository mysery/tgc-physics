using System;
using System.Collections.Generic;
using System.Text;

namespace AlumnoEjemplos.Piguyis
{
    /// <summary>
    /// Implements a node of the Fibonacci heap. It holds the information
    /// necessary for maintaining the structure of the heap. It acts as
    /// an opaque handle for the data element, and serves as the Key to
    /// retrieving the data from the heap.
    /// @author  Nathan Fiedler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class FibbonacciNode<T>
    {
        /** Data object for this node, holds the Key Value. */
        private T value;

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /** Key Value for this node. */
        private long key;

        public long Key
        {
            get { return key; }
            set { key = value; }
        }
        /** Parent node. */
        private FibbonacciNode<T> parent;

        public FibbonacciNode<T> Parent
        {
            get { return parent; }
            set { parent = value; }
        }
        /** First Child node. */
        private FibbonacciNode<T> child;

        public FibbonacciNode<T> Child
        {
            get { return child; }
            set { child = value; }
        }
        /** Right sibling node. */
        private FibbonacciNode<T> right;

        public FibbonacciNode<T> Right
        {
            get { return right; }
            set { right = value; }
        }
        /** Left sibling node. */
        private FibbonacciNode<T> left;

        public FibbonacciNode<T> Left
        {
            get { return left; }
            set { left = value; }
        }
        /** Number of children of this node. */
        private int degree;

        public int Degree
        {
            get { return degree; }
            set { degree = value; }
        }
        /** True if this node has had a Child removed since this node was
         * added to its Parent. */
        private bool mark;

        public bool Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        /// <summary>
        /// Constructor which sets the data field to the
        /// passed arguments. It also initializes the Right and Left pointers,
        /// making this a circular doubly-linked list.
        /// </summary>
        /// <param name="Value">data object to associate with this node</param>
        public FibbonacciNode(T value, long key)
        {
            this.value = value;
            this.key = key;
            right = this;
            left = this;
        }

        /// <summary>
        /// Performs a cascading cut operation. Cuts this from its Parent
        /// and then does the same for its Parent, and so on up the tree.
        /// <p><em>Running time: O(log n)</em></p>
        /// </summary>
        /// <param name="min">the minimum heap node, to which nodes will be added.</param>
        public void cascadingCut(FibbonacciNode<T> min)
        {
            FibbonacciNode<T> z = parent;
            // if there's a Parent...
            if (z != null)
            {
                if (mark)
                {
                    // it's marked, cut it from Parent
                    z.cut(this, min);
                    // cut its Parent as well
                    z.cascadingCut(min);
                }
                else
                {
                    // if y is unmarked, set it marked
                    mark = true;
                }
            }
        }
        
        /// <summary>
        /// The reverse of the link operation: removes x from the Child
        /// list of this node.
        /// <p><em>Running time: O(1)</em></p>
        /// </summary>
        /// <param name="x">Child to be removed from this node's Child list</param>
        /// <param name="min">the minimum heap node, to which x is added.</param>
        public void cut(FibbonacciNode<T> x, FibbonacciNode<T> min)
        {
            // remove x from childlist and decrement Degree
            x.left.right = x.right;
            x.right.left = x.left;
            degree--;
            // reset Child if necessary
            if (degree == 0)
            {
                child = null;
            }
            else if (child == x)
            {
                child = x.right;
            }
            // add x to root list of heap
            x.right = min;
            x.left = min.left;
            min.left = x;
            x.left.right = x;
            // set Parent[x] to nil
            x.parent = null;
            // set Mark[x] to false
            x.mark = false;
        }

        /// <summary>
        /// Make this node a Child of the given Parent node. All linkages
        /// are updated, the Degree of the Parent is incremented, and
        /// Mark is set to false.
        /// </summary>
        /// <param name="Parent">the new Parent node.</param>
        public void link(FibbonacciNode<T> parent)
        {
            // Note: putting this code here in FibbonacciNode<T>makes it 7x faster
            // because it doesn't have to use generated accessor methods,
            // which add a lot of time when called millions of times.
            // remove this from its circular list
            left.right = right;
            right.left = left;
            // make this a Child of x
            this.parent = parent;
            if (parent.child == null)
            {
                parent.child = this;
                right = this;
                left = this;
            }
            else
            {
                left = parent.child;
                right = parent.child.right;
                parent.child.right = this;
                right.left = this;
            }
            // increase Degree[x]
            parent.degree++;
            // set Mark false
            mark = false;
        }
    }
}
