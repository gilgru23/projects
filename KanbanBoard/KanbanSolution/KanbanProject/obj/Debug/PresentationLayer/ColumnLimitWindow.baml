   M S B A M L   `   `   `  ����H  DKanbanProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KanbanProject.PresentationLayerKanbanProject.PresentationLayer  5   	   7    1KanbanProject.PresentationLayer.ColumnLimitWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:KanbanProject.PresentationLayer     5   	     � Title$  Column Task-Limit��5   	   $��165.789��6#   $��267.105��64   .��5	      � .+�5
   
   �� -   5         ��Name$ 
limitCheck��5
      $��Limit the selected column��6'   $
��Left=�6x   $��	38,23,0,0q�6�   $	��Top=�6�    d� 	IsChecked 6K   �� *	IsLimited+ �� Mode$ TwoWay=�6�   �� 5   
   $
��Left=�6�   $��23��6�   $��	39,60,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6   �� *Limit+$ TwoWay=� �� StringFormat$ {0:D}��{�6L   ��  �� ElementName$ 
limitCheck�� �� Path$ 	IsChecked�6�   �� 5   
   -   5        $��OK��5      $
��Left=�6   $��
187,93,0,0q�69   $	��Top=�6M   $��48��6e   6p   5      5      