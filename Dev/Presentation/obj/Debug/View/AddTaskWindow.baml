   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Presentation.ViewPresentation.View  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   %    Presentation.View.AddTaskWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Presentation.View     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     � Title$  AddTaskWindow��5	   	   $	��450��6   $��561.919��6,    � WindowStartupLocation$ CenterOwner=�6<    � ShowInTaskbar .   � WindowStyle$ 
ToolWindow=�6v   .��5
      � .+�5   
   � $
��Left=�6   $	��419��6*   $��	-4,-1,0,0q�67   $	��Top=�6J   $	��557��6b    � ColumnDefinitions 5      �� 5      $
��156* �6#   �� 5      $
��401* �6#   .+�5      �� $��Title: ��6   $
��Left=�6'   $��	60,70,0,0q�6B   $	��Top=�6U   $��40��6m   $
��Bold"�6x   $��26��6�   �� 5      $��Description:��6   $
��Left=�6+   $��
60,150,0,0q�6F   $	��Top=�6Z   $��77��6r   $
��Bold"�6}   $��26��6�   �� 5      $��DueDate:��6   $
��Left=�6'   $��
60,280,0,0q�6B   $	��Top=�6V   $��62��6n   $��26��6y   $
��Bold"�6�   �� 5      -   5        $��TitleBox��5      $
��Left=�6'   $��50��6B   $	��170��6N    �� 	MaxLength$ 50��6[   $��
100,70,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��2��6�   ��6j   ��  �� Path$ Title�6�   �� 5      -   5        $��DescriptionBox��5      $
��Left=�6N   $	 300��6i   $	��120��6y   $��140,150,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��400��6�   $��2��6�   ��6,   �� $ Description�6�   (  "System.Windows.Controls.DatePicker  5      $
��Left=�6    �� Today	   DisplayDateStart#	 Z 64   $��130,280,0,0q�6i   $	��Top=�6~   $��24��6�   $	��102��6�   $��2��6�   
   SelectedDate
 6�   �� $ DueDate�6�   �� 5      -   5        $	��Add��5      $
��Left=�6#   $��100,340,0,0q�6>   $	��Top=�6S   $��75��6k   $��20��6�   $��2��6�   �� 5      -   5        $��Cancel��5      $��Cancel��6#   $
��Left=�64   $��
54,340,0,0q�6O   $	��Top=�6c   $��75��6{   $��20��6�   $��1��6�   �� 5      -   5        $��
descOutput��5      $
��Left=�6*   $��1��6E   $��332,273,0,0q�6V   $
��Wrap=�6k   $	��Top=�6   �� 5      -   5        $��titleOutput��5      $��1��6-   $
��Left=�6=   $��
63,121,0,0q�6X   $
��Wrap=�6l   $	��Top=�6�   5      5       5!      