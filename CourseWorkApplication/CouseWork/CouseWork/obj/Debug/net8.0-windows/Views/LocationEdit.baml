   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   '    !CouseWork.ViewModels.LocationEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	   4local"clr-namespace:CouseWork.ViewModels     5   	     � 	Resources  5	         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle��%�  &	        &	     �� 5
   
   "  6,   .�5      ��  � ��$ 14(�6)   �� 5       � ��
 �����6,   �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��
 � ���67   �� 5       � ��
 �����67   5      5      5      �� 5   
   " ��6*   .�5      ��  � ��
 � ���6+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��
 ���� 6,   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 30��6'   �� 5        � ��$	 250��6&   �� 5!       � ��$ 5q�6'   �� 5"       � ��$
 Left=�64   5#      5%       � DataContext 5&      0   *CouseWork.ViewModels.LocationEditViewModel  5'   
   .��5)      � $��10q�6    � RowDefinitions 5*   
   �� 5+      $��3* �6   �� 5,      $��2* �6   .+�5/   
     #��[  6      AutoGenerateColumns$ False��51      $��Stretch=�52      $��Stretch=�53      $��10q�54         
IsReadOnly$
 True��55      ��5/   A   �� *	Locations+ �� SelectedItem 50      �� *SelectedLocation+	 �� Mode$	 TwoWay=�
   Columns
 56      0  *System.Windows.Controls.DataGridTextColumn  57        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 58      �� *
LocationID+  59        item104# �  6%    5:      �� *LocationName+  5;        item124# �  6%    5<      �� *NumberOfSectors+5>      �� 5B   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5C      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5D      �� #���  6   
��� ���$��Center=�6R   $	��100��6m   �� 5E       �� 
IsReadOnly$
 True��6S   $	��200��6e   ��6   �� *Id+ �� UpdateSourceTrigger$ PropertyChanged=�6e   �� 5F      
���  ��$��12(�6k   ��6   �� *	IdError+$ PropertyChanged=�6k   5G      �� 5H      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5I      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5J      $	��200��6U   ��6   �� *Name+$ PropertyChanged=�6U   �� 5K      
���  ��$��12(�6m   ��6   �� *	NameError+$ PropertyChanged=�6m   5L      �� 5M      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5N      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5O      $	��200��6U   ��6   �� *Rows+$ PropertyChanged=�6U   �� 5P      
���  ��$��12(�6n   ��6   �� *	RowsError+$ PropertyChanged=�6n   5Q      �� 5S        item105#���  6   #��[ 6X   ��69   �� *
AddCommand+6X   �� 5T        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   �� 5U        item107#���  6   #��[ 6[   ��69   �� *DeleteCommand+6[   5W      5X      5Y      