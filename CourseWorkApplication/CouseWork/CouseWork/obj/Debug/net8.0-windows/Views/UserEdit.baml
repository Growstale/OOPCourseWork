   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	       CouseWork.Views.UserEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CouseWork.Views     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	     � 	Resources  5	         CustomDataGridStyle �� 
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
 Left=�64   5"      5#       � DataContext 5%      ,   &CouseWork.ViewModels.UserEditViewModel  5&   
   .��5)      � $��10q�6    � RowDefinitions 5*   
   �� 5+      $��1* �6   �� 5,      $��4* �6   .+�5/   
     #��[  6      AutoGenerateColumns$ False��51      $��Stretch=�52      $��Stretch=�53      $��10q�54         
IsReadOnly$
 True��55      ��5/   @   �� *Users+ �� SelectedItem 50      �� *SelectedUser+	 �� Mode$	 TwoWay=�
   Columns
 56      0  *System.Windows.Controls.DataGridTextColumn  57        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *UserID+  58        item168# �  6%    6H   �� *Login+  59        item149# �  6%    6H   �� *	FirstName+  5:        item150# �  6%    6H   �� *
LastName+  5;        item151# �  6%    6H   �� *Email+  5<        item152# �  6%    6H   �� *Phone+5>      �� 5@   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5A      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5B      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5C       �� 
IsReadOnly$
 True��6W   $	��200��6i   ��6   �� *UserID+ �� UpdateSourceTrigger$ PropertyChanged=�6i   �� 5D      
���  ��$��12(�6o   ��6   �� *UserIDError+$ PropertyChanged=�6o   5E      �� 5G      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5H      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5I      $	��200��6V   ��6   �� *Login+$ PropertyChanged=�6V   �� 5J      
���  ��$��12(�6n   ��6   �� *
LoginError+$ PropertyChanged=�6n   5K      �� 5M      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5N      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5O      $	��200��6Z   ��6   �� *	FirstName+$ PropertyChanged=�6Z   �� 5P      
���  ��$��12(�6r   ��6   �� *FirstNameError+$ PropertyChanged=�6r   5Q      �� 5S      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5T      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5U      $	��200��6[   ��6   �� *
SecondName+$ PropertyChanged=�6[   �� 5V      
���  ��$��12(�6s   ��6   �� *SecondNameError+$ PropertyChanged=�6s   5W      �� 5Z      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5[      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5\      $	��200��6V   ��6   �� *Email+$ PropertyChanged=�6V   �� 5]      
���  ��$��12(�6n   ��6   �� *
EmailError+$ PropertyChanged=�6n   5^      �� 5`      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5a      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5b      $	��200��6V   ��6   �� *Phone+$ PropertyChanged=�6V   �� 5c      
���  ��$��12(�6n   ��6   �� *
PhoneError+$ PropertyChanged=�6n   5d      �� 5f        item106#���  6   #��[ 6Y   ��69   �� *SaveCommand+6Y   5g      5h      5i      