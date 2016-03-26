using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Template.PropertyGridUtility
{
    public class PropertySpec
    {
        #region members
        private Attribute[] _attributes;
        private string _category;
        private object _defaultValue;
        private string _description;
        private string _editor;
        private string _name;
        private string _type;
        private string _typeConverter;
        public ArrayList ComboListItems { get; set; }

        public PropertySpecCollection PropertyExpandableCollections { get; set; }
        #endregion members
        
        #region constructor
        public PropertySpec( string name, string type ) : this( name, type, null, null, null ) { }

        public PropertySpec( string name, Type type ) : this( name, type.AssemblyQualifiedName, null, null, null ) { }

        public PropertySpec( string name, string type, string category ) : this( name, type, category, null, null ) { }

        public PropertySpec( string name, Type type, string category ) : this( name, 
                                                                            type.AssemblyQualifiedName, 
                                                                            category, null, null) { }

        public PropertySpec( string name, string type, string category, string description ) : this( name, 
                                                                                                   type, category, 
                                                                                                   description, null) { }
        public PropertySpec( string name, Type type, string category, string description ) : this( name, 
                                                                                                   type.AssemblyQualifiedName,
                                                                                                   category,
                                                                                                   description, null) { }

        public PropertySpec( string name, string type, string category, object defaultValue, string description )
        {
            _name = name;
            _type = type;
            _category = category;
            _description = description;
            _defaultValue = defaultValue;
            _attributes = null;
            PropertyExpandableCollections = new PropertySpecCollection();
        }
        
        public PropertySpec( string name, Type type, string category, object defaultValue, string description ) : this( name,
                                                                                                                        type.AssemblyQualifiedName,
                                                                                                                        category,
                                                                                                                        defaultValue,
                                                                                                                        description
                                                                                                                        ) { }

        public PropertySpec( string name, string type, string category,object defaultValue, string description,
                             string editor, string typeConverter ) : this( name, type, category,defaultValue, description )
        {
            _editor = editor;
            _typeConverter = typeConverter;
        }

        public PropertySpec(string name, Type type, string category, object defaultValue, string description,
                             string editor, string typeConverter ) : this( name, type.AssemblyQualifiedName, category, 
                                                                           defaultValue, description,
                                                                           editor, typeConverter) { }

        public PropertySpec(string name, string type, string category, object defaultValue, string description, 
                             Type editor, string typeConverter ) : this( name, type, category, defaultValue,description,
                                                                         editor.AssemblyQualifiedName, typeConverter) { }

        public PropertySpec(string name, Type type, string category, object defaultValue, string description,
                             Type editor, string typeConverter ) : this( name, type.AssemblyQualifiedName,
                                                                         category, defaultValue, description,
                                                                         editor.AssemblyQualifiedName, 
                                                                         typeConverter) { }

        public PropertySpec( string name, string type, string category, object defaultValue,string description,
                             string editor, Type typeConverter ) : this( name, type, category,defaultValue,
                                                                         description,editor,
                                                                         typeConverter.AssemblyQualifiedName) { }

        public PropertySpec(string name, Type type, string category, object defaultValue, string description,
                             string editor, Type typeConverter ) : this( name, type.AssemblyQualifiedName,
                                                                         category, defaultValue, description,
                                                                         editor,
                                                                         typeConverter.AssemblyQualifiedName) { }

        public PropertySpec(string name, string type, string category, object defaultValue, string description,
                             Type editor, Type typeConverter ) : this( name, type, category,defaultValue,
                                                                       description,editor.AssemblyQualifiedName,
                                                                       typeConverter.AssemblyQualifiedName) { }

        public PropertySpec(string name, Type type, string category, object defaultValue, string description,
                             Type editor, Type typeConverter ) : this( name, type.AssemblyQualifiedName,
                                                                       category, defaultValue,description,
                                                                       editor.AssemblyQualifiedName,
                                                                       typeConverter.AssemblyQualifiedName) { }
        #endregion constructor

        #region properties
        public Attribute[] Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }


        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

       
        public string ConverterTypeName
        {
            get { return _typeConverter; }
            set { _typeConverter = value; }
        }

        public object DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string EditorTypeName
        {
            get { return _editor; }
            set { _editor = value; }
        }

      
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        
        public string TypeName
        {
            get { return _type; }
            set { _type = value; }
        }

        #endregion properties

    }
}
