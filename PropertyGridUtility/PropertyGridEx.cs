using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing.Design;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;

namespace Template.PropertyGridUtility
{
    public delegate void PropertySpecEventHandler( object sender, PropertySpecEventArgs eventArgs );
    public class PropertyGridEx : ICustomTypeDescriptor
    {
        #region PropertyGridEx implementation
        private string _defaultProperty;
        private PropertySpecCollection _propertyItemCollections;
        private PropertyDescriptorCollection _propertyDescriptorCollection;
        public event PropertySpecEventHandler GetValue;
        public event PropertySpecEventHandler SetValue;
        private PropertyGrid _parentPropertyGrid;

        public PropertyGridEx( PropertyGrid control)
        {
            _defaultProperty = null;
            _propertyItemCollections = new PropertySpecCollection();
            _parentPropertyGrid = control;
        }

        public void RemoveItem(string category, string itemName)
        {
            RemoveItemfromCollection(_propertyItemCollections, category, itemName);
        }
        
        private void RemoveItemfromCollection(PropertySpecCollection itemCollections, string category, string itemName)
        {
            foreach (PropertySpec specItem in itemCollections)
            {
                Type propertyType = Type.GetType(specItem.TypeName);
                string typeClass = propertyType.BaseType.Name;
                if ((typeClass == typeof(ExpandableProperty).Name) ||
                    (typeClass == typeof(ExpandableObjectConverter).Name))
                {
                    if (string.IsNullOrEmpty(itemName) && (specItem.Category == category))
                    {
                        itemCollections.Remove(specItem);
                        break;
                    }
                    else
                    {
                        RemoveItemfromCollection(specItem.PropertyExpandableCollections, category, itemName);
                    }
                }
                else if ((specItem.Category == category) && specItem.Name == itemName)
                {
                    itemCollections.Remove(specItem);
                    break;
                }
            }

        }

        public void Refresh()
        {
            _propertyDescriptorCollection = null;
            _parentPropertyGrid.Refresh();
        }

        public string DefaultProperty
        {
            get { return _defaultProperty; }
            set { _defaultProperty = value; }
        }

        public PropertySpecCollection PropertyGridCollections
        {
            get { return _propertyItemCollections; }
        }

        internal virtual void OnGetValue(PropertySpecEventArgs eventArgs)
        {
            if ( GetValue != null )
            {
                GetValue( this, eventArgs );
            }
        }

        internal virtual void OnSetValue( PropertySpecEventArgs eventArgs )
        {
            if ( SetValue != null )
            {
                SetValue( this, eventArgs );
            }
        }
        #endregion

        #region ICustomTypeDescriptor interface definitions
        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            return TypeDescriptor.GetClassName( this, true );
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName( this, true );
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter( this, true );
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent( this, true );
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            PropertySpec propertySpec = null;
            if ( _defaultProperty != null )
            {
                int nIndex = _propertyItemCollections.IndexOf( _defaultProperty );
                propertySpec = _propertyItemCollections[nIndex];
            }

            if (propertySpec != null)
            {
                return new PropertyDescriptor(this, propertySpec, null);
            }
            else
            {
                return null;
            }
        }

        object ICustomTypeDescriptor.GetEditor( Type editorBaseType )
        {
            return TypeDescriptor.GetEditor( this, editorBaseType, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents( this, true );
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents( Attribute[] attributes )
        {
            return TypeDescriptor.GetEvents( this, attributes, true );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return (( ICustomTypeDescriptor ) this ).GetProperties( new Attribute[0] );
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties( Attribute[] attributes )
        {
            ArrayList properties = new ArrayList();

            if ( _propertyDescriptorCollection == null )
            {
                foreach (PropertySpec propertyItem in _propertyItemCollections)
                {
                    ArrayList attributeList = new ArrayList();
                    if (propertyItem.Category != null)
                    {
                        IEnumerable<string> splitPath = propertyItem.Category.Split('\\').Where(x => x.Length > 0).Select(x => x.Trim());
                        propertyItem.Category = splitPath.ElementAt(0);
                        attributeList.Add(new CategoryAttribute(propertyItem.Category));
                    }
                    if (propertyItem.Description != null)
                        attributeList.Add(new DescriptionAttribute(propertyItem.Description));

                    if (propertyItem.EditorTypeName != null)
                        attributeList.Add(new EditorAttribute(propertyItem.EditorTypeName, typeof (UITypeEditor)));

                    if (propertyItem.TypeName != null)
                        attributeList.Add(new TypeConverterAttribute(propertyItem.TypeName));

                    // Additionally, append the custom attributes associated with the
                    // PropertySpec, if any.
                    Type propertyType = Type.GetType(propertyItem.TypeName);
                    string typeClass = propertyType.BaseType.Name;
                    if ((typeClass == typeof (ExpandableProperty).Name) ||
                        (typeClass == typeof (ExpandableObjectConverter).Name))
                    {
                        propertyItem.Category += "\\" + propertyItem.Name;
                    }
                    if (propertyItem.Attributes != null)
                        attributeList.AddRange(propertyItem.Attributes);
                    Attribute[] attributeArray = (Attribute[]) attributeList.ToArray(typeof (Attribute));
                    properties.Add(new PropertyDescriptor(this, propertyItem, attributeArray));
                }
                PropertyDescriptor[] propDespArray = (PropertyDescriptor[]) properties.ToArray(
                    typeof (PropertyDescriptor));
                _propertyDescriptorCollection = new PropertyDescriptorCollection(propDespArray);
            }
            return _propertyDescriptorCollection;
        }

        object ICustomTypeDescriptor.GetPropertyOwner( PropertyDescriptor pd )
        {
            return this;
        }
        #endregion
    }
}
