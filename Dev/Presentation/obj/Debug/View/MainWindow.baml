   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PresentationPresentation  5   	       Presentation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Presentation     5   	     � Title$  
MainWindow��5   	   $	��399��6   $	��317��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      � $��0,0,2,0q�6    � ColumnDefinitions 5
   
   �� 5      $��0* �6   �� 5      �� 5      $��0* �6   �� 5      $��0* �6   .+�5   
   �� $��	Username:��6   $
��Left=�6$   $��41��6?   $��	10,90,0,0q�6K   $	��Top=�6^   $��95��6v   $��16(�6�   $��1��6�   $
��Bold"�6�   �� 5   
   $��	Password:��6   $
��Left=�6$   $��41��6?   $��
10,140,0,0q�6K   $	��Top=�6_   $��95��6w   $��16(�6�   $��1��6�   $
��Bold"�6�   �� 5   
   -   5        $��emailbox��5      $
��Left=�6"   $��41��6=   $��
100,90,0,0q�6I    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��164��6�   $��1��6�   ��6�   ��  �� Path$ Email�6�   �� 5   
   $��A new user? ��6   $
��Left=�6'   $	��Top=�6B   $��100,260,0,0q�6Z   $	��169��6o   $��16(�6{   $
��Bold"�6�   $��31��6�   $��1��6�   �� 5   
   -   5        $��PasswordBox��5      $
��Left=�6\   $��41��6w   $��100,140,0,0q�6�   $	��Top=�6�   $	��164��6�   $��1��6�   ��6'   �� $ Password� �� Mode$ OneWayToSource=�6�   �� 5   
   -   5        $��Login��5      $��Login��6    $
��Left=�60   $��110,210,0,0q�6K   $	��Top=�6`   $��75��6x   $��20��6�   $��1��6�   �� 5   
   -   5        $��Register��5      $��Register��6!   $
��Left=�64   $��110,300,0,0q�6O   $	��Top=�6d   $��75��6|   $��20��6�   $��1��6�   �� 5   
   $��Center=�6   $��2��6-   $�� Kanban Board��6A   $��90,24,80,304q�6Z   $��Center=�6p   $��40��6�   $	��134��6�   $��18(�6�   $
��Bold"�6�   5      5      