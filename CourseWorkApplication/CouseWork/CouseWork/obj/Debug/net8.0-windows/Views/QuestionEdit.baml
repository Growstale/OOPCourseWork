   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   "    CouseWork.Views.QuestionEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CouseWork.Views     5   	   9
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
 Left=�64   5"      5#       � DataContext 5%      0   *CouseWork.ViewModels.QuestionEditViewModel  5&   
   .��5)      � $��10q�6    � RowDefinitions 5*   
   �� 5+      $��3* �6   �� 5,      $��4* �6   .+�5/   
     #��[  6      AutoGenerateColumns$ False��51      $��Stretch=�52      $��Stretch=�53      $��10q�54         
IsReadOnly$
 True��55      ��5/   @   �� *	Questions+ �� SelectedItem 50      �� *SelectedQuestion+	 �� Mode$	 TwoWay=�
   Columns
 56      0  *System.Windows.Controls.DataGridTextColumn  57        item119,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *
QuestionID+  58        item153# �  6%    6H   �� *QuestionText+  59        item154# �  6%    6H   �� *QuestionDate+  5:        item155# �  6%    6H   �� *
AnswerText+  5;        item156# �  6%    6H   �� *Status+  5<        item157# �  6%    6H   �� *QuestionType+5?      �� 5A   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5B      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5C      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5D       �� 
IsReadOnly$
 True��6[   $	��200��6m   ��6   �� *
QuestionID+ �� UpdateSourceTrigger$ PropertyChanged=�6m   �� 5E      
���  ��$��12(�6s   ��6   �� *QuestionIDError+$ PropertyChanged=�6s   5F      �� 5H      $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5I      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5J      $	��200��6[   ��6   �� *
AnswerText+$ PropertyChanged=�6[   �� 5K      
���  ��$��12(�6s   ��6   �� *AnswerTextError+$ PropertyChanged=�6s   5L      �� 5N        item158#���  6   #��[ 6[   ��69   �� *AnswerCommand+6[   �� 5O       	 item159#��� 	 6   #��[ 6Z   ��69   �� *CloseCommand+6Z   5Q      5R      5S      