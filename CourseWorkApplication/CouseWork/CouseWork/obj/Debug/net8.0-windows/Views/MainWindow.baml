   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CouseWork	CouseWork  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	       CouseWork.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CouseWork     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	     � WindowStartupLocation$  CenterScreen=�5   /    � 
ResizeMode$ NoResize=�5       � Title$ 
TicketWave��5	   	   $	��450��6   $	��800��6)   ���	#FF9D3535���	#00000000 � 	Resources 5
         CustomTextBoxStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName  NormalGradientBrush �� 
StartPoint �� EndPoint � Color � Offset  HoverGradientBrush  CustomButtonStyle ��Name �� CornerRadius  OutlineButtonStyle��%U  &	        &	 �    &	 ,    &	 �    7  7  &	 �    �� 5   
   " ��6+   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� 
������ 
������ ._�5   "   �� $ PART_ContentHost��6/   5      �� 5      $	 
BottomLine��6(   
��� ���$��2��5   (   $��Bottom=�5   (   $��0,0,0,0q�5   (   5      
 5      P� 5       �@J�	IsFocused$
 True��6;   . �5   "   �� $ 
BottomLine��6)    � ��
 �  ��6Q   5      5       5"      �� 5#       � ��
 ���� 6+   �� 5$       � ��$ 0q�60   �� 5%       � ��$ 5q�6(   �� 5&       � ��
 �����6+   �� 5'       � ��$ Center=�62   5(      �� 5)   
   $	 0,0&�6:   $	 1,1&�6K   .��5*      � $ #FEA501��6   $	 0.0Q�6+   � 5+      $ #FE5501��6   $	 1.0Q�6+   5,      �� 5-   
   $	 0,0&�69   $	 1,1&�6J   .��5.      � $ #FEA501��6   $	 0.2Q�6+   � 5/      $ #FE5501��6   $	 1.0Q�6+   50      �� 53   
   " ��6*   .�54      ��  � �� 55      �� 56      " ��6&   .R�57      �� $ border��6!   $ 20��58   !   $��1q�59   !   8��  ._�5;      �� $��Center=�6/   $��Center=�6L   5<      
 5=      P� 5>       �@J�IsMouseOver$
 True��6=   . �5?   "   �� $ border��6)   8   � ��6S   5@      5B      5D      �� 5F       � ��
 �����6+   �� 5G       � ��
 ���� 6,   �� 5H       � ��$
 10,5q�6(   �� 5I       � ��$ 16(�6)   5J      �� 5M   
   " ��6+   .�5N      ��  � �� 5O      �� 5P      " ��6&   .R�5Q      �� $ border��6!   $ 10��5R   !   
��� ���
������ $��2q�5U   !   ._�5V      �� $��Center=�6/   $��Center=�6L   5W      
 5X      P� 5Y       �@J�IsMouseOver$
 True��6=   . �5Z   "   �� $ border��6)    � ��
 � ���6S   �� 5[   "    � ��
 �����6?   5\      5^      5`      �� 5a       � ��
 � ���6+   �� 5b       � ��
 ���� 6+   �� 5c       � ��$
 10,5q�6(   �� 5d       � ��$ 10(�6)   5e      5g       � DataContext 5h      1   +CouseWork.ViewModels.AuthorizationViewModel  5i   
   .��5l      � 
���   �.+�5m   
   �� $U��OD:\5sem_BSTU\CourseWork\CourseWorkApplication\Images\lineRegistrationScreen.png��6   $
��Fill=�6i   � 5o   
   $��	30,0,30,0q�6    � ColumnDefinitions 5p      �� 5q      $��* �6#   �� 5r      $��50 �6#   �� 5s      $	��350 �6#    � RowDefinitions 5u      �� 5v      $��90 �6    �� 5w      $
��Auto �6    �� 5x      $
��Auto �6    �� 5y      $
��Auto �6    �� 5z      $
��Auto �6    �� 5{      $
��Auto �6    �� 5|      .+�5~      �� -   5        $S��MD:\5sem_BSTU\CourseWork\CourseWorkApplication\Images\language-svgrepo-com.png��5~      $��28��5      $��28��6     � Cursor$
 Hand��6L   $	��Top=�5�      $��Right=�6,   $��0��5�      $��2��6!   $��0,8,7,0q�61   �� 5�      $O��ID:\5sem_BSTU\CourseWork\CourseWorkApplication\Images\user-svgrepo-com.png��6   $��28��5�      $��28��6    $��Center=�5�      $��Right=�6/   $��1��5�      $��1��6!   $��0,0,10,0q�61   �� 5�      $	��350��6U   $��40��6a   #��[  6m   $��1��5�      $��2��6#   $��Center=�63   ��5�      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5�   3   �� 5�      $��2��6   $��2��6%   
���  ����65   �� *UsernameError+$ PropertyChanged=�6y   �� 5�      $S��MD:\5sem_BSTU\CourseWork\CourseWorkApplication\Images\password-svgrepo-com.png��6   $��28��5�      $��28��6    $��Center=�5�      $��Right=�6/   $��3��5�      $��1��6!   $��0,0,10,0q�61   �� 5�      $	��350��6U   $��40��6a   #��[  6m   $��3��5�      $��2��6#   $��Center=�63   ��5�      �� *
Password+$ PropertyChanged=�5�   3   �� 5�      $��4��6   $��2��6%   
���  ����65   �� *PasswordError+$ PropertyChanged=�6y   �� 5�      $	��150��6   $��40��6!    	 item1#���  6-   #��[ 5�      $
 Hand��6A    �� 	IsDefault$
 True��6O   $��5��5�      $��2��6"   $��Center=�62   $	��Top=�6O   $��	0,20,0,20q�6g   ��5�   P   �� *LoginCommand+5�   g   �� 5�      -   5        $	��120��5�      $��30��6!    	 item2#���  6-   #��[ 5�      $
 Hand��6A   $��6��5�      $��2��6"   $��Center=�62   $	��Top=�6O   $��	0,00,0,10q�6g   5�      �� 5�   
   $J��DD:\5sem_BSTU\CourseWork\CourseWorkApplication\Images\cinemalogo1.jpg��6   $��0,41,433,122q�5�      $
��Fill=�6&   $��Right=�65   $	��327��6Q   5�      5�      