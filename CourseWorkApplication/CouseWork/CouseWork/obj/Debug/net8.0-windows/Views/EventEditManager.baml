   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5      9!clr-namespace:CouseWork.UtilitiesCouseWork.Utilities  5      /clr-namespace:CouseWork.DataCouseWork.Data  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      &     CouseWork.Views.EventEditManager   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      9
viewModels"clr-namespace:CouseWork.ViewModels     5      7	utilities!clr-namespace:CouseWork.Utilities     5      -dataclr-namespace:CouseWork.Data     5      6sys&clr-namespace:System;assembly=mscorlib     5	        � 	Resources  5         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle3   -CouseWork.Utilities.ByteArrayToImageConverter  ByteArrayToImageConverter��%  &	        &	     &	 �    �� 5   
   "  6,   .�5      ��  � ��$ 14(�6)   �� 5       � ��
 �����6,   �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��
 � ���67   �� 5       � ��
 �����67   5      5      5      �� 5   
   " ��6*   .�5      ��  � ��
 � ���6+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$
 Bold"�6+   �� 5       � ��$
 10,5q�6(   �� 5       � ��
 ���� 6,   �� 5        �@�Cursor$
 Hand��6'   �� 5!       � ��$ 30��6'   �� 5"       � ��$	 250��6&   �� 5#       � ��$ 5q�6'   �� 5$       � ��$
 Left=�64   5%        5&   
   6.   5'       � DataContext 5)      4  .CouseWork.ViewModels.EventEditManagerViewModel  5*   
   .��5-      � $��10q�6    � RowDefinitions 5.   
   �� 5/      $��1* �6   �� 50      $��4* �6   .+�53   
     #��[  6      AutoGenerateColumns$ False��55      $��Stretch=�56      $��Stretch=�57      $��10q�58         
IsReadOnly$
 True��59      ��53   @   �� *Events+ �� SelectedItem 54      �� *SelectedEvent+	 �� Mode$	 TwoWay=�
   Columns
 5:      0  *System.Windows.Controls.DataGridTextColumn  5;        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *	EventID+  5<        item104# �  6%    6H   �� *	EventName+  5=        item112# �  6%    6H   �� *EventDuration+  5>        item113# �  6%    6H   �� *Category.CategoryName+  5?        item120# �  6%    6H   �� *Organizer.CompanyName+  5@        item114# �  6%    6H   �� *Description+  5A       	 item115# � 	 6%    6H   �� *	StartDate+  5B       
 item116# � 
 6%    6H   �� *Cost+4  .System.Windows.Controls.DataGridTemplateColumn  5C        item117# �  6)      CellTemplate 5D      �� 5E      .R�5F      �� $��50��5G   $   $��50��6/   ��5F   $   �� *Image+ �� 	Converter# [ 5G   /   5H      5J      5M      �� 5O   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5P      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5Q      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5R       �� 
IsReadOnly$
 True��6X   $	��200��6j   ��6   �� *	EventID+ �� UpdateSourceTrigger$ PropertyChanged=�6j   �� 5S      
���  ��$��12(�6p   ��6   �� *EventIDError+$ PropertyChanged=�6p   5T      �� 5V      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5W      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5X      $	��200��6Z   ��6   �� *	EventName+$ PropertyChanged=�6Z   �� 5Y      
���  ��$��12(�6r   ��6   �� *EventNameError+$ PropertyChanged=�6r   5Z      �� 5\      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5]      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5^      $	��200��6^   ��6   �� *EventDuration+$ PropertyChanged=�6^   �� 5_      
���  ��$��12(�6v   ��6   �� *EventDurationError+$ PropertyChanged=�6v   5`      �� 5b      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5c      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5d       �� DisplayMemberPath$ CategoryName��6h   $	��200��6�   ��6   �� *
Categories+ 6>   �� *SelectedCategory+6�   �� 5e      
���  ��$��12(�6y   ��6   �� *SelectedCategoryError+$ PropertyChanged=�6y   5f      �� 5h      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5i      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5j      $	��200��6\   ��6   �� *Description+$ PropertyChanged=�6\   �� 5k      
���  ��$��12(�6t   ��6   �� *DescriptionError+$ PropertyChanged=�6t   5l      �� 5n      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5o      �� #��� 	 6   
��� ���$��Center=�6Q   $	��100��6l   (	  "System.Windows.Controls.DatePicker	  5p      -   5         	 Name$ GRNDate��5p      $��0,5,0,0q�6.   $	��200��5t       	  SelectedDate 5q      �� *selectedGRN.grnDate+$	 TwoWay=� �� TargetNullValue$  �� 	  DisplayDateStart 5r      �� *	MinDate+ 	  DisplayDateEnd 5s      �� *	MaxDate+5t      �� 5u      
���  ��$��12(�6r   ��6   �� *StartDateError+$ PropertyChanged=�6r   5v      �� 5x      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5y      �� #��� 
 6   
��� ���$��Center=�6Q   $	��100��6l   �� 5z      $	��200��6U   ��6   �� *Cost+$ PropertyChanged=�6U   �� 5{      
���  ��$��12(�6m   ��6   �� *	CostError+$ PropertyChanged=�6m   5|      �� 5~      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5�      .+�5�      �� 
���  ��$��12(�6w   ��6    �� *EventImageError+$ PropertyChanged=�6w   �� 5�        item118#���  6   #��[ 6h   ��6A   �� *UploadImageCommand+6h   5�      �� 5�      $	��100��66   $	��100��6B   $��10,0,0,0q�6O   ��6   �� *
EventImage+6O   5�      �� 5�        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   �� 5�        item107#���  6   #��[ 6[   ��69   �� *DeleteCommand+6[   5�      5�      5�      