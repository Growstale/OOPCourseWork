   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   !    CouseWork.Views.AskQuestion   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	   /localclr-namespace:CouseWork.Views     5   	     � DataContext  5	      /   )CouseWork.ViewModels.AskQuestionViewModel  5
   
    � 	Resources 5         CustomTextBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName �� Triggers :� RoutedEvent	 �� To g� Duration �� TargetProperty��%@  &	        '�� �    �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� 
������ 
������ ._�5   "   �� $ PART_ContentHost��6/   5      �� 5      $ 
BottomLine��6(   
��� ���$��2��5   !   $��Bottom=�5   !   $��0,0,0,0q�5   !   5       5      P� 5      	 �@J�	IsFocused$

 True��6;   . �5   "   �� $ 
BottomLine��6)    � ��
 �  ��6Q   5       5"      5$      �� 5%       � ��
 ���� 6+   �� 5&       � ��$ 0q�60   �� 5'       � ��$ 5q�6(   �� 5(       � ��
 �����6+   �� 5)       � ��$ Center=�62   5*      �� 5+   
   " ��6   .�5,      ��  � ��
 � ���6+   �� 5-       � ��
 �����6+   �� 5.       � ��$ 16(�6)   �� 5/       � ��$ 10q�6'   �� 50       �@�Cursor$
 Hand��6'    51      :� 52      $ Button.MouseEnter��6   .[�53      �� .��54      �� .��55      �� 
 Yellow6-   $ 0:0:0.2D�69   $1 +(Button.Background).(SolidColorBrush.Color)�6L   56      57      58      :� 59      $ Button.MouseLeave��6   .[�5:      �� .��5;      �� .��5<      �� 
 Orange6-   $ 0:0:0.2D�69   $1 +(Button.Background).(SolidColorBrush.Color)�6L   5=      5>      5?      5A      5B      .��5C      � .+�5D   
   �� .+�5E      ��   item101#���  6   
��� ���$��24(�6M   $
��Bold"�6[   $��Center=�6m   $
��0,20q�6�   �� 5F      #��[  6U   $	��300��6�   $��30��6�   $	��0,2q�6�   ��6   �� *
Question+ �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5G        item102#���  6   
�������
��� ���$	��150��6   $��35��6�   $
��0,20q�6�   $��Center=�6�   ��69   �� *
AskCommand+6�   �� 5I        item103#���  6   
��� ���$��24(�6M   $
��Bold"�6[   $��Center=�6m   $
��0,20q�6�   �� 5K      $
��Auto=�6   $
��Auto=�6@   $	��600��6c   $	��300��6o   .��5M      �� 
���   �$
��0,20q�6W   ��6.   �� *OrganizerAnswers+��5N      �� 5O      .R�5P      � $��0,0,0,10q�6#   ���#333333 � ColumnDefinitions 5Q   "   �� 5R   &   $��3* �67   �� 5S   &   $��1* �67   �� 5T   &   $��1* �67   �� 5U   &   $��1* �67   �� 5V   &   $��1* �67   .+�5Y   "   �� 
�������$��16(�6]   $��0��6k   $��Center=�6{    �� Padding$ 10q�6�   ��6,   �� *QuestionText+6�   �� 5Z   "   
�������$��16(�6}   $��1��6�   $��Center=�6�   $ 10q�6�   ��6,   �� *QuestionDate+ �� StringFormat$ {0:dd MMM yyyy}��6�   �� 5[   "   
�������$��16(�6[   $��2��6i   $��Center=�6y   $ 10q�6�   ��6,   �� *
AnswerText+6�   �� 5\   "   
�������$��16(�6W   $��3��6e   $��Center=�6u   $ 10q�6�   ��6,   �� *Status+6�   �� 5]   "   
�������$��16(�6v   $��4��6�   $��Center=�6�   $ 10q�6�   ��6,   �� *	ManagerID+$ Менеджер: {0}��6�   5^      5_      5a      5b      5c      5d      5e      