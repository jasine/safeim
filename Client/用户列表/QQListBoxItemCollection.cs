using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace Client
{
	/// <summary>
	/// Description of QQListBoxItemCollection.
	/// </summary>
[ListBindable(false)]
public class QQListBoxItemCollection : IList,ICollection,IEnumerable
{
        #region Fields

        private QQListBox _owner;

        #endregion

        public QQListBoxItemCollection(QQListBox owner)
        {
            _owner = owner;
        }

        internal QQListBox Owner
        {
            get{ return _owner; }
        }

        public QQListBoxItem this[int index]
        {
            get{ return Owner.OldItems[index] as QQListBoxItem; }
            set{ Owner.OldItems[index] = value; }
        }

        public int Count
        {
            get { return Owner.OldItems.Count; }
        }

        public bool IsReadOnly 
        {
            get{ return Owner.OldItems.IsReadOnly; }
        }

       public int Add(QQListBoxItem item)
       {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return Owner.OldItems.Add(item);
        }

       public void AddRange(QQListBoxItemCollection value)
       {
            foreach (QQListBoxItem item in value)
            {
                Add(item);
            }
        }

        public void AddRange(QQListBoxItem[] items)
        {
            Owner.OldItems.AddRange(items);
        }

        public void Clear()
        {
            Owner.OldItems.Clear();
        }

        public bool Contains(QQListBoxItem item)
        {
            return Owner.OldItems.Contains(item);
        }
        

        public void CopyTo(QQListBoxItem[] destination, int arrayIndex)
        {
            Owner.OldItems.CopyTo(destination, arrayIndex);
        }

        public int IndexOf(QQListBoxItem item)
        {
            return Owner.OldItems.IndexOf(item);
        }

        public void Insert(int index, QQListBoxItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            Owner.OldItems.Insert(index, item);
        }

        public void Remove(QQListBoxItem item)
        {
            Owner.OldItems.Remove(item);
        }

        public void RemoveAt(int index)
        {
            Owner.OldItems.RemoveAt(index);
        }

        public IEnumerator GetEnumerator()
        {
            return Owner.OldItems.GetEnumerator();
        }



        int IList.Add(object value)
        {
            if (!(value is QQListBoxItem))
           {
                throw new ArgumentException();
            }
            return Add(value as QQListBoxItem);
        }

        void IList.Clear()
       {
            Clear();
        }

        bool IList.Contains(object value)
       {
            return Contains(value as QQListBoxItem);
        }

        int IList.IndexOf(object value)
       {
            return IndexOf(value as QQListBoxItem);
        }

        void IList.Insert(int index, object value)
       {
            if (!(value is QQListBoxItem))
           {
                throw new ArgumentException();
            }
            Insert(index, value as QQListBoxItem);
        }

        bool IList.IsFixedSize
       {
            get{ return false; }
        }

        bool IList.IsReadOnly
       {
            get{ return IsReadOnly; }
        }

        void IList.Remove(object value)
       {
            Remove(value as QQListBoxItem);
        }

       void IList.RemoveAt(int index)
       {
           RemoveAt(index);
        }

       object IList.this[int index]
       {
            get
           {
                return this[index];
            }
            set
           {
                if (!(value is QQListBoxItem))
               {
                    throw new ArgumentException();
                }
                this[index] = value as QQListBoxItem;
            }
        }



        void ICollection.CopyTo(Array array, int index)
       {
            CopyTo((QQListBoxItem[])array, index);
        }

        int ICollection.Count
       {
            get{ return Count; }
        }

        bool ICollection.IsSynchronized
       {
            get{ return false; }
        }

        object ICollection.SyncRoot
       {
            get{ return this; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
}

}
