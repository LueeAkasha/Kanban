   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Presentation.ViewPresentation.View  5   	   '    !Presentation.View.AddColumnWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Presentation.View     5   	     � Title$  AddColumnWindow��5   	   $��245.919��6!   $��303.945��62    � WindowStartupLocation$ CenterOwner=�6B    � WindowStyle$ 
ToolWindow=�6f   .��5	      � .+�5
   
   �� $��Column's name:��6   $
��Left=�6)   $��30��6D   $��	42,43,0,0q�6P   $	��Top=�6c   $	��105��6{   �� 5   
   $��Column's ordinal:��6   $
��Left=�6,   $��30��6G   $��	39,95,0,0q�6S   $	��Top=�6f   $	��105��6~   �� 5   
   -   5        $
��Name��5       �� 	MaxLength$ 15��6@   $
��Left=�6O   $��30��6j   $��
140,49,0,0q�6v    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6   ��  �� Path$ 
ColumnName�6�   �� 5   
   $
��Left=�66   $��30��6Q   $��
143,95,0,0q�6]   $
 Wrap=�6q   $	��Top=�6�   $��40��6�   ��6   �� $ ColumnOrdinal�6�   �� 5   
   -   5        $	��Add��5      $
��Add!��6   $
��Left=�6+   $��80��6F   $��30��6Q   $��
50,155,0,0q�6]   $	��Top=�6q   6�   �� 5   
   -   5        $��Cancel��5      $��Cancel��6   $
��Left=�60   $��30��6K   $��150,155,0,0q�6W   $	��Top=�6l   $��80��6�   6�   �� 5   
   -   5        $��
NameOutput��5      $��Center=�6&   $��
210,70,0,0q�6C   $
��Wrap=�6W   $	��Top=�6k   5      5      