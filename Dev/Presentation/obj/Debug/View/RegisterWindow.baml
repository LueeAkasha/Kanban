   M S B A M L   `   `   `  ����G  CPresentation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Presentation.ViewPresentation.View  5   	   &     Presentation.View.RegisterWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Presentation.View     5   	     � Title$  RegisterWindow��5   	   $	��300��6    $	��400��6-    � WindowStartupLocation$ CenterOwner=�69   .��5	      � .+�5
   
   �� -   5        $��Register��5
      $��Register��6!   $
��Left=�64   $��30��6O   $��150,212,0,0q�6[   $	��Top=�6p   $	��100��6�   6�   �� 5   
   $��	Username:��6   $
��Left=�6$   $��30��6?   $��	30,50,0,0q�6K   $	��Top=�6^   $��95��6v   $��16(�6�   $
��Bold"�6�   �� 5   
   $��Email:��6   $
��Left=�6!   $��30��6<   $��	30,90,0,0q�6H   $	��Top=�6[   $��95��6s   $��16(�6~   $
��Bold"�6�   �� 5   
   $��	Password:��6   $
��Left=�6$   $��30��6?   $��
30,125,0,0q�6K   $	��Top=�6_   $��95��6w   $��16(�6�   $
��Bold"�6�   �� 5   
   $��Email host (optional):��6   $
��Left=�61   $��30��6L   $��
30,160,0,0q�6X   $	��Top=�6l   $	��162��6�   $��14(�6�   $
��Bold"�6�   �� 5   
   -   5        $��NickNameBox��5      $
��Left=�6%   $��30��6@   $��
120,50,0,0q�6L    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��180��6�   ��6�   ��  �� Path$ NickName��� 5   
   -   5        $��EmailBox��5      $
��Left=�6#   $��30��6>   $��
120,90,0,0q�6J   $
 Wrap=�6^   $	��Top=�6r   $	��180��6�   ��6�   �� $ Email��� 5   
   $
��Left=�6F   $��30��6a   $��120,125,0,0q�6m   $	��Top=�6�   $	��180��6�   ��6   �� $ Password� �� Mode$ OneWayToSource=�6�   �� 5   
   -   5        $��HostEmailBox��5      $
��Left=�6I   $��30��6d   $��180,160,0,0q�6p   $
 Wrap=�6�   $	��Top=�6�   $	��180��6�   $��16(�6�   ��6'   �� $ 	HostEmail�6�   �� 5   
   
���  ��$
��Left=�6D   $��104,347,0,0q�6_   $	��Top=�6t   $	��205��6�   $��30��6�   ��6   �� $ Message�6�   5      5      