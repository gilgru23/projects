   M S B A M L   `   `   `  ����H  DKanbanProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KanbanProject.PresentationLayerKanbanProject.PresentationLayer  5   	   :    4KanbanProject.PresentationLayer.BoardSelectionWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:KanbanProject.PresentationLayer     5   	     � Title$  BoardSelectionWindow��5   	   $	��250��6&   $	��461��63   .��5	      � $��0,0,12,5q�6   .+�5
   
   �� -   5         ��Name$ AddBoard��5
      $��	Add Board��6$   $
��Left=�68   $	��Top=�6S   $��75��6k   $��23��6v   $��121,172,0,0q�6�   6�   �� 5   
   -   5        $ RemoveBoard��5      $��Remove Board��6&   $
��Left=�6=   $	��Top=�6X   $��94��6p   $��23��6{   $��215,172,0,0q�6�   6�   �� 5   
   -   5        $ ChooseBoard��5      $��Choose Board��6&   $
��Left=�6=   $	��Top=�6X   $��94��6p   $��23��6{   $��328,172,0,0q�6�   6�   �� 5   
   -   5        $
��Left=�5   j   $	��142��6�   $	��Top=�6�   $	��310��6�   $��	70,10,0,0q�6�   ��6   �� *
BoardNames+ �� Mode$ OneWay=� �� SelectedItem 6C   �� *SelectedBoard+ �� View 5      �� 5      .P�5      �� 5      5      �� 5   
   -   5         ��Name$ 
AddtextBox��5      $
��Left=�6A   $��23��6\    �� TextWrapping$
 Wrap=�6h   $	��Top=�6|   $	��105��6�   $��
17,172,0,0q�6�    J� RenderTransformOrigin$ 	0.5,2.478&�6�   ��6'   �� *
NameText+6�   5      5      