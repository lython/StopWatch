using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Primitives;

namespace LiStopWatch
{
    public partial class LoopPage : PhoneApplicationPage
    {
        List<int> one = new List<int>();
        List<int> two = new List<int>();
        List<int> three = new List<int>();

        // 构造函数
        public LoopPage()
        {
            InitializeComponent();
            storyboard_1.Begin();
            for (int i = 0; i < 24; i++) one.Add(i);
            for (int j = 0; j < 60; j++)
            {
                two.Add(j);
                three.Add(j);
            }
            this.selectorOne.DataSource = new ListLoopingDataSource<int>() { Items = one, SelectedItem = Config.HourNum };
            this.selectorTwo.DataSource = new ListLoopingDataSource<int>() { Items = two, SelectedItem = Config.MinuteNum };
            this.selectorThree.DataSource = new ListLoopingDataSource<int>() { Items = three, SelectedItem = Config.SecondNum };
        }

        private void appbar_ok_Click(object sender, EventArgs e)
        {
            int aa, bb, cc;
            aa = Convert.ToInt16(this.selectorOne.DataSource.SelectedItem);
            bb = Convert.ToInt16(this.selectorTwo.DataSource.SelectedItem);
            cc = Convert.ToInt16(this.selectorThree.DataSource.SelectedItem);
            Config.HourNum = aa;
            Config.MinuteNum = bb;
            Config.SecondNum = cc;
            storyboard_2.Begin();
            this.NavigationService.GoBack();
        }

        private void appbar_cancel_Click(object sender, EventArgs e)
        {
            storyboard_2.Begin();
            this.NavigationService.GoBack();
        }

        private void selectorOne_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.selectorTwo.IsExpanded = false;
            this.selectorThree.IsExpanded = false;
        }

        private void selectorTwo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.selectorOne.IsExpanded = false;
            this.selectorThree.IsExpanded = false;
        }

        private void selectorThree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.selectorOne.IsExpanded = false;
            this.selectorTwo.IsExpanded = false;
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            storyboard_2.Begin();
        }
    }

    public class ListLoopingDataSource<T> : LoopingDataSourceBase
    {
        private LinkedList<T> linkedList;
        private List<LinkedListNode<T>> sortedList;
        private IComparer<T> comparer;
        private NodeComparer nodeComparer;

        public ListLoopingDataSource()
        {
        }

        public IEnumerable<T> Items
        {
            get
            {
                return this.linkedList;
            }
            set
            {
                this.SetItemCollection(value);
            }
        }

        private void SetItemCollection(IEnumerable<T> collection)
        {
            this.linkedList = new LinkedList<T>(collection);
            this.sortedList = new List<LinkedListNode<T>>(this.linkedList.Count);
            // initialize the linked list with items from the collections
            LinkedListNode<T> currentNode = this.linkedList.First;
            while (currentNode != null)
            {
                this.sortedList.Add(currentNode);
                currentNode = currentNode.Next;
            }

            IComparer<T> comparer = this.comparer;
            if (comparer == null)
            {
                // if no comparer is set use the default one if available
                if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
                {
                    comparer = Comparer<T>.Default;
                }
                else
                {
                    throw new InvalidOperationException("There is no default comparer for this type of item. You must set one.");
                }
            }
            this.nodeComparer = new NodeComparer(comparer);
            this.sortedList.Sort(this.nodeComparer);
        }

        public IComparer<T> Comparer
        {
            get
            {
                return this.comparer;
            }
            set
            {
                this.comparer = value;
            }
        }

        public override object GetNext(object relativeTo)
        {
            // find the index of the node using binary search in the sorted list
            int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), this.nodeComparer);
            if (index < 0)
            {
                return default(T);
            }
            // get the actual node from the linked list using the index

            LinkedListNode<T> node = this.sortedList[index].Next;
            if (node == null)
            {
                // if there is no next node get the first one
                node = this.linkedList.First;
            }
            return node.Value;
        }
        public override object GetPrevious(object relativeTo)
        {
            int index = this.sortedList.BinarySearch(new LinkedListNode<T>((T)relativeTo), this.nodeComparer);
            if (index < 0)
            {
                return default(T);
            }
            LinkedListNode<T> node = this.sortedList[index].Previous;
            if (node == null)
            {
                // if there is no previous node get the last one
                node = this.linkedList.Last;
            }
            return node.Value;
        }

        private class NodeComparer : IComparer<LinkedListNode<T>>
        {
            private IComparer<T> comparer;
            public NodeComparer(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            #region IComparer<LinkedListNode<T>> Members
            public int Compare(LinkedListNode<T> x, LinkedListNode<T> y)
            {
                return this.comparer.Compare(x.Value, y.Value);
            }
            #endregion
        }
    }

    public abstract class LoopingDataSourceBase : ILoopingSelectorDataSource
    {
        private object selectedItem;
        #region ILoopingSelectorDataSource Members
        public abstract object GetNext(object relativeTo);
        public abstract object GetPrevious(object relativeTo);
        public object SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                // this will use the Equals method if it is overridden for the data source item class
                if (!object.Equals(this.selectedItem, value))
                {
                    // save the previously selected item so that we can use it
                    // to construct the event arguments for the SelectionChanged event
                    object previousSelectedItem = this.selectedItem;
                    this.selectedItem = value;
                    // fire the SelectionChanged event
                    this.OnSelectionChanged(previousSelectedItem, this.selectedItem);
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;
        protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
        {
            EventHandler<SelectionChangedEventArgs> handler = this.SelectionChanged;
            if (handler != null)
            {
                handler(this, new SelectionChangedEventArgs(new object[] { oldSelectedItem }, new object[] { newSelectedItem }));
            }
        }
        #endregion
    }
}