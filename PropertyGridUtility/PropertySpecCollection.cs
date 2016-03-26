using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Template.PropertyGridUtility
{
    [Serializable]
    public class PropertySpecCollection : IList
    {
        #region PropertySpecCollection definition 

        private ArrayList _propertySpecArray;
        public PropertySpecCollection()
        {
            _propertySpecArray = new ArrayList();
        }

        public int Count
        {
            get { return _propertySpecArray.Count; }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get { return null; }
        }

        public PropertySpec this[int nIndex]
        {
            get { return (PropertySpec)_propertySpecArray[nIndex]; }
            set { _propertySpecArray[nIndex] = value; }
        }

        public int Add( PropertySpec value )
        {
            int nIndex = _propertySpecArray.Add( value );

            return nIndex;
        }

        public void AddRange( PropertySpec[] array )
        {
            _propertySpecArray.AddRange(array);
        }

        public void Clear()
        {
            _propertySpecArray.Clear();
        }

        public bool Contains( PropertySpec item )
        {
            return _propertySpecArray.Contains(item);
        }

        public bool Contains( string name )
        {
            foreach (PropertySpec propSpec in _propertySpecArray)
            {
                if ( propSpec.Name == name )
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo( PropertySpec[] array )
        {
            _propertySpecArray.CopyTo( array );
        }

        public void CopyTo( PropertySpec[] array, int nIndex )
        {
            _propertySpecArray.CopyTo(array, nIndex);
        }

        public IEnumerator GetEnumerator()
        {
            return _propertySpecArray.GetEnumerator();
        }

        public int IndexOf( PropertySpec value )
        {
            return _propertySpecArray.IndexOf( value );
        }

        public int IndexOf( string name )
        {
            int nIndex = 0;
            foreach ( PropertySpec propSpec in _propertySpecArray )
            {
                if ( propSpec.Name == name )
                {
                    return nIndex;
                }
                nIndex++;
            }
            return -1;
        }

        public void Insert( int nIndex, PropertySpec value )
        {
            _propertySpecArray.Insert( nIndex, value );
        }

        public void Remove( PropertySpec obj )
        {
            _propertySpecArray.Remove( obj );
        }

        public void Remove(string name)
        {
            int nIndex = IndexOf( name );
            RemoveAt( nIndex );
        }

        public void RemoveAt(int nIndex)
        {
            _propertySpecArray.RemoveAt( nIndex );
        }

        public PropertySpec[] ToArray()
        {
            return ( PropertySpec[])_propertySpecArray.ToArray( typeof( PropertySpec ));
        }
        #endregion

        #region Explicit implementations for ICollection and IList
        void ICollection.CopyTo( Array array, int nIndex )
        {
            CopyTo(( PropertySpec[] )array, nIndex );
        }

        int IList.Add( object value )
        {
            return Add(( PropertySpec )value );
        }

        bool IList.Contains( object obj )
        {
            return Contains(( PropertySpec )obj );
        }

        object IList.this[int index]
        {
            get
            {
                return (( PropertySpecCollection )this)[ index ];
            }
            set
            {
                (( PropertySpecCollection ) this)[index] = ( PropertySpec )value;
            }
        }

        int IList.IndexOf( object obj )
        {
            return IndexOf(( PropertySpec )obj );
        }

        void IList.Insert( int nIndex, object value )
        {
            Insert( nIndex, (PropertySpec)value);
        }

        void IList.Remove( object value)
        {
            Remove(( PropertySpec )value);
        }
        #endregion
    }
}
