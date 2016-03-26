# Customized-PropertyGrid(C#)
C# property grid is not flexible for loading data to property in runtime.
It is difficult to represent the property data values in combobox and change based the some other property's value. So customized the C sharp 
property grid to PropertyGridEx class which more flexible to handle data such as combo and edit box.
#Steps##
1. create  PropertyGridEx by passing  original PropertyGrid contorl 
2. create each property using PropertySpec and add those property spec in PropertyGridEx Collections
also you can set PropertySpec
. as ExpandableCollections for expand and collapse
. as combo list  
