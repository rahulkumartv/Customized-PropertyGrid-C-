using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Template.PropertyGridUtility
{
    public class PropertySpecEventArgs : EventArgs
    {
        #region members
        private PropertySpec _property;
        private object _value;
        #endregion members

        #region constructor
        public PropertySpecEventArgs( PropertySpec property, object value )
        {
            this._property = property;
            this._value = value;
        }
        #endregion constructor

        #region properties
        public PropertySpec Property
        {
            get { return _property; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        #endregion properties
    }
}
