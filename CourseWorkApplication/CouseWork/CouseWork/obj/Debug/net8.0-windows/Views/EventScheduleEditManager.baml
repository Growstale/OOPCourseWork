   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   .    (CouseWork.Views.EventScheduleEditManager   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	   $	��500��5      $	��800��6'     � 	Resources  5         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle��%�  &	        &	     �� 5	   
   "  6,   .�5
      ��  � ��$ 14(�6)   �� 5       � ��
 �����6,   �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��
 � ���67   �� 5       � ��
 �����67   5      5      5      �� 5   
   " ��6*   .�5      ��  � ��
 � ���6+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��
 ���� 6,   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 30��6'   �� 5       � ��$	 250��6&   �� 5       � ��$ 5q�6'   �� 5        � ��$
 Left=�64   5!      5"       � DataContext 5$      <  6CouseWork.ViewModels.EventScheduleEditManagerViewModel  5%   
   .��5(      � $��5q�6    � RowDefinitions 5)   
   �� 5*      $��3* �6   �� 5+      $��5* �6   .+�5.   
     #��[  6      AutoGenerateColumns$ False��50      $��Stretch=�51      $��Stretch=�52      $��10q�53         
IsReadOnly$
 True��54      ��5.   @   �� *EventSchedules+ �� SelectedItem 5/      �� *SelectedEventSchedule+	 �� Mode$	 TwoWay=�
   Columns
 55      0  *System.Windows.Controls.DataGridTextColumn  56        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *EventScheduleID+  57        item121# �  6%    6H   �� *	EventDate+  58        item115# �  6%    6H   �� *Event.StartDate+  59        item104# �  6%    6H   �� *Event.EventName+  5:        item122# �  6%    6H   �� *Location.LocationName+5<      �� 5>   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5?      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5@      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5A       �� 
IsReadOnly$
 True��6`   $	��200��6r   ��6   �� *EventScheduleID+ �� UpdateSourceTrigger$ PropertyChanged=�6r   �� 5B      
���  ��$��12(�6x   ��6   �� *EventScheduleIDError+$ PropertyChanged=�6x   5C      �� 5E      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5F      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   (  "System.Windows.Controls.DatePicker  5G      -   5          Name$ GRNDate��5G      $��0,5,0,0q�6.   $	��200��5K         SelectedDate 5H      �� *selectedGRN.grnDate+$	 TwoWay=� �� TargetNullValue$  ��   DisplayDateStart 5I      �� *	MinDate+   DisplayDateEnd 5J      �� *	MaxDate+5K      �� 5L      
���  ��$��12(�6r   ��6   �� *EventDateError+$ PropertyChanged=�6r   5M      �� 5O      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5P      ��   item123#���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5Q      $	��200��6Z   ��6   �� *	EventTime+$ PropertyChanged=�6Z   �� 5R      
���  ��$��12(�6r   ��6   �� *EventTimeError+$ PropertyChanged=�6r   5S      �� 5U      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5V      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5W       �� DisplayMemberPath$ 	EventName��6a   $	��200��6   ��6   �� *Events+ 6:   �� *SelectedEvent+6   �� 5X      
���  ��$��12(�6v   ��6   �� *SelectedEventError+$ PropertyChanged=�6v   5Y      �� 5[      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5\      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5]      $ LocationName��6g   $	��200��6�   ��6   �� *	Locations+ 6=   �� *SelectedLocation+6�   �� 5^      
���  ��$��12(�6y   ��6   �� *SelectedLocationError+$ PropertyChanged=�6y   5_      �� 5b        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   �� 5c       	 item107#��� 	 6   #��[ 6[   ��69   �� *DeleteCommand+6[   5d      5e      5f      