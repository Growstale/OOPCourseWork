   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5      ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5      (    "CouseWork.ViewModels.SectorRowEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9
viewModels"clr-namespace:CouseWork.ViewModels     5      4local"clr-namespace:CouseWork.ViewModels     5        � 	Resources  5	         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle��%�  &	        &	     �� 5
   
   "  6,   .�5      ��  � ��$ 14(�6)   �� 5       � ��
 �����6,   �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��
 � ���67   �� 5       � ��
 �����67   5      5      5      �� 5   
   " ��6*   .�5      ��  � ��
 � ���6+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��
 ���� 6,   �� 5       �@�Cursor$
 Hand��6'   �� 5       � ��$ 30��6'   �� 5       � ��$	 250��6&   �� 5        � ��$ 5q�6'   �� 5!       � ��$
 Left=�64   5"      5#       � DataContext 5$      1   +CouseWork.ViewModels.SectorRowEditViewModel  5%   
   .��5'      � $��10q�6    � RowDefinitions 5(   
   �� 5)      $��2* �6   �� 5*      $��3* �6   .+�5-   
     #��[  6      AutoGenerateColumns$ False��5/      $��Stretch=�50      $��Stretch=�51      $��10q�52         
IsReadOnly$
 True��53      ��5-   @   �� *
SectorRows+ �� SelectedItem 5.      �� *SelectedSectorRow+	 �� Mode$	 TwoWay=�
   Columns
 55      0  *System.Windows.Controls.DataGridTextColumn  56        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *SectorRowID+  57        item160# �  6%    6H   �� *	SectorRow+  58        item161# �  6%    6H   �� *NumberOfSeats+  59        item122# �  6%    6H   �� *Location.LocationName+  5:        item162# �  6%    6H   �� *
CostFactor+5<      �� 5>   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5?      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5@      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5A       �� 
IsReadOnly$
 True��6S   $	��200��6e   ��6   �� *Id+ �� UpdateSourceTrigger$ PropertyChanged=�6e   �� 5B      
���  ��$��12(�6k   ��6   �� *	IdError+$ PropertyChanged=�6k   5C      �� 5D      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5E      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5F      $
 True��6Z   $	��200��6l   ��6   �� *	SectorRow+$ PropertyChanged=�6l   �� 5G      
���  ��$��12(�6r   ��6   �� *SectorRowError+$ PropertyChanged=�6r   5H      �� 5I      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5J      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5K      $	��200��6^   ��6   �� *NumberOfSeats+$ PropertyChanged=�6^   �� 5L      
���  ��$��12(�6v   ��6   �� *NumberOfSeatsError+$ PropertyChanged=�6v   5M      �� 5N      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5O      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5P       �� DisplayMemberPath$ LocationName��6=   $	��200��6�   ��6   �� *	Locations+ 6^   �� *SelectedLocation+6�   �� 5Q      
���  ��$��12(�6q   ��6   �� *LocationError+$ PropertyChanged=�6q   5R      �� 5S      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5T      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5U      $	��200��6[   ��6   �� *
CostFactor+$ PropertyChanged=�6[   �� 5V      
���  ��$��12(�6s   ��6   �� *CostFactorError+$ PropertyChanged=�6s   5W      �� 5Y        item105#���  6   #��[ 6X   ��69   �� *
AddCommand+6X   �� 5Z        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   �� 5[       	 item107#��� 	 6   #��[ 6[   ��69   �� *DeleteCommand+6[   5\      5]      5^      