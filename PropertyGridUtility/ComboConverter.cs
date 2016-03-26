using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace EquipmentTemplateBuilder.PropertyGridUtility
{
    public class ComboConverter : StringConverter
    {
        public ComboConverter()
        {
        }
        
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            PropertyDescriptor temp = (PropertyDescriptor)context.PropertyDescriptor;
            return new StandardValuesCollection(temp.PropertyItem.ComboListItems);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
