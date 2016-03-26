using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace Template.PropertyGridUtility
{
    public class ExpandableProperty : ExpandableObjectConverter
    {
        protected PropertyDescriptorCollection _propertyDescriptorCollection;
        protected PropertySpecCollection _propertyItemCollections;
        public ExpandableProperty()
        {
            _propertyItemCollections = new PropertySpecCollection();
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            if (_propertyDescriptorCollection == null)
            {
                PropertyDescriptor popDescp = (PropertyDescriptor)context.PropertyDescriptor;
                _propertyItemCollections = popDescp.PropertyItem.PropertyExpandableCollections;
                ArrayList properties = new ArrayList();

                foreach (PropertySpec propertyItem in _propertyItemCollections)
                {
                    ArrayList attributeList = new ArrayList();
                    propertyItem.Category = popDescp.PropertyItem.Category;
                    if (propertyItem.Category != null)
                        attributeList.Add(new CategoryAttribute(propertyItem.Category));

                    if (propertyItem.Description != null)
                        attributeList.Add(new DescriptionAttribute(propertyItem.Description));

                    if (propertyItem.EditorTypeName != null)
                        attributeList.Add(new EditorAttribute(propertyItem.EditorTypeName, typeof(UITypeEditor)));

                    if (propertyItem.TypeName != null)
                        attributeList.Add(new TypeConverterAttribute(propertyItem.TypeName));

                    // Additionally, append the custom attributes associated with the
                    // PropertySpec, if any.
                    Type propertyType = Type.GetType(propertyItem.TypeName);
                    string typeClass = propertyType.BaseType.Name;
                    if ((typeClass == typeof(ExpandableProperty).Name) || (typeClass == typeof(ExpandableObjectConverter).Name))
                    {
                        propertyItem.Category += "\\" + propertyItem.Name;
                    }
                    if (propertyItem.Attributes != null)
                        attributeList.AddRange(propertyItem.Attributes);
                    Attribute[] attributeArray = (Attribute[])attributeList.ToArray(typeof(Attribute));
                    properties.Add(new EQTPropertyDescriptor(popDescp.PropertyGridEx, propertyItem, attributeArray));
                }
                PropertyDescriptor[] props =
                    (PropertyDescriptor[])properties.ToArray(typeof(PropertyDescriptor));
                _propertyDescriptorCollection = new PropertyDescriptorCollection(props);
                return _propertyDescriptorCollection;
            }
            return _propertyDescriptorCollection;
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
