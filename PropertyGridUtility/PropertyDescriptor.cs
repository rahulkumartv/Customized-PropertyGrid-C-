using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Template.PropertyGridUtility
{
    internal class PropertyDescriptor : PropertyDescriptor
    {
        private PropertySpec _propertyItem;
        public PropertyGridEx PropertyGridEx { set; get; }

        internal PropertyDescriptor(PropertyGridEx propertyGridEx, PropertySpec propertyItem, Attribute[] attributes)
            : base(propertyItem.Name, attributes)
        {
            _propertyItem = propertyItem;
            PropertyGridEx = propertyGridEx;
        }

        public PropertySpec PropertyItem
        {
            get { return _propertyItem; }
        }

        public override Type ComponentType
        {
            get { return _propertyItem.GetType(); }
        }

        public override bool IsReadOnly
        {
            get { return ( Attributes.Matches( ReadOnlyAttribute.Yes )); }
        }

        public override Type PropertyType
        {
            get { return Type.GetType( _propertyItem.TypeName ); }
        }

        public override bool CanResetValue( object component )
        {
            if ( _propertyItem.DefaultValue == null )
            {
                return false;
            }
            else
            {
                return !this.GetValue( component ).Equals( _propertyItem.DefaultValue );
            }
        }

        public override object GetValue( object component )
        {
            PropertySpecEventArgs eventArgs = new PropertySpecEventArgs(_propertyItem, _propertyItem.DefaultValue);
            PropertyGridEx.OnGetValue(eventArgs);
            return eventArgs.Value;
        }

        public override void ResetValue( object component )
        {
            SetValue( component, _propertyItem.DefaultValue );
        }

        public override void SetValue(object component, object value)
        {
            _propertyItem.DefaultValue = value;
            PropertySpecEventArgs eventArgs = new PropertySpecEventArgs( _propertyItem, value );
            PropertyGridEx.OnSetValue(eventArgs);
        }

        public override bool ShouldSerializeValue( object component )
        {
            return true;
        }
    }
}
