   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   #    CouseWork.Views.OrganizerEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CouseWork.Views     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	     � 	Resources  5	         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle��%�  &	        &	     �� 5   
   "  6,   .�5   
   ��  � ��$ 14(�6%   �� 5   
    � ��
 �����6(   �� 5   
    �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � ��
 � ���63   �� 5       � ��
 �����63   5      5      5      �� 5      " ��6&   .�5   
   ��  � ��
 � ���6'   �� 5   
    � ��
 �����6'   �� 5   
    � ��$ 14(�6%   �� 5   
    � ��$
 Bold"�6'   �� 5   
    � ��$
 10,5q�6$   �� 5   
    � ��
 ���� 6(   �� 5   
    �@�Cursor$
 Hand��6#   �� 5   
    � ��$ 30��6#   �� 5    
    � ��$	 250��6"   �� 5!   
    � ��$ 5q�6#   �� 5"   
    � ��$
 Left=�60   5#      5$       � DataContext 5&      1   +CouseWork.ViewModels.OrganizerEditViewModel  5'   
   .��5*      � $��10q�6    � RowDefinitions 5+   
   �� 5,      $��1* �6   �� 5-      $��4* �6   .+�50   
     #��[  6      AutoGenerateColumns$ False��52      $��Stretch=�53      $��Stretch=�54      $��10q�55         
IsReadOnly$
 True��56      ��50   @   �� *
Organizers+ �� SelectedItem 51      �� *SelectedOrganizer+	 �� Mode$	 TwoWay=�
   Columns
 57      0  *System.Windows.Controls.DataGridTextColumn  58        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *OrganizerID+  59        item148# �  6%    6H   �� *CompanyName+  5:        item149# �  6%    6H   �� *	FirstName+  5;        item150# �  6%    6H   �� *
LastName+  5<        item151# �  6%    6H   �� *Email+  5=        item152# �  6%    6H   �� *Phone+5?      �� 5A   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5B      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5C      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5D       �� 
IsReadOnly$
 True��6\   $	��200��6n   ��6   �� *OrganizerID+ �� UpdateSourceTrigger$ PropertyChanged=�6n   �� 5E      
���  ��$��12(�6t   ��6   �� *OrganizerIDError+$ PropertyChanged=�6t   5F      �� 5H      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5I      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5J      $	��200��6\   ��6   �� *CompanyName+$ PropertyChanged=�6\   �� 5K      
���  ��$��12(�6t   ��6   �� *CompanyNameError+$ PropertyChanged=�6t   5L      �� 5N      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5O      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5P      $	��200��6Z   ��6   �� *	FirstName+$ PropertyChanged=�6Z   �� 5Q      
���  ��$��12(�6r   ��6   �� *FirstNameError+$ PropertyChanged=�6r   5R      �� 5T      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5U      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5V      $	��200��6[   ��6   �� *
SecondName+$ PropertyChanged=�6[   �� 5W      
���  ��$��12(�6s   ��6   �� *SecondNameError+$ PropertyChanged=�6s   5X      �� 5[      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5\      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5]      $	��200��6V   ��6   �� *Email+$ PropertyChanged=�6V   �� 5^      
���  ��$��12(�6n   ��6   �� *
EmailError+$ PropertyChanged=�6n   5_      �� 5a      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5b      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5c      $	��200��6V   ��6   �� *Phone+$ PropertyChanged=�6V   �� 5d      
���  ��$��12(�6n   ��6   �� *
PhoneError+$ PropertyChanged=�6n   5e      �� 5g        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   5h      5i      5j      