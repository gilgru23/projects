   M S B A M L   `   `   `  ����H  DKanbanProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:KanbanProject.PresentationLayerKanbanProject.PresentationLayer  5   	   6    0KanbanProject.PresentationLayer.CreateTaskWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:KanbanProject.PresentationLayer     5   	     � Title$  Create Task��5   	   $	��450��6   $��613.91��6*   .��5	      � .+�5
   
   �� -   5        $��Create Task��5
      $
��Left=�6'   $��390,340,0,0q�6B   $	��Top=�6W   $��75��6o   6z   �� 5   
   -   5         ��Name$ titleBox��5      $
��Left=�6P   $��23��6k   $��
109,86,0,0q�6w    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��120��6�   ��6$   �� *Title+ �� Mode$ OneWayToSource=�6�   �� 5   
   -   5        $ textBox��5      $
��Left=�6N   $	��148��6i   $��109,135,0,0q�6v   $
 Wrap=�6�   $	��Top=�6�   $	��400��6�   ��6#   �� *Text+$ OneWayToSource=�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpDate��5      $
��Left=�6%   $��109,299,0,0q�6o   $	��Top=�6�   $��21��6�   $	��120��6�      SelectedDate 6@   �� *
Due_date+$ TwoWay=�6�   �� 5   
   $��Title��6   $
��Left=�6    $��23��6;   $��	69,86,0,0q�6G   $	��Top=�6Z   $��35��6r   �� 5   
   $
��Text��6   $
��Left=�6   $��23��6:   $��
69,135,0,0q�6F   $	��Top=�6Z   $��35��6r   �� 5   
   $��Due Date��6   $
��Left=�6#   $��23��6>   $��
46,297,0,0q�6J   $	��Top=�6^   $��63��6v    J� RenderTransformOrigin$ 0.5,0.5&�6�   5      5      