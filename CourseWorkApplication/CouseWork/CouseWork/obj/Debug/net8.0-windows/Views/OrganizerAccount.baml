   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   &     CouseWork.Views.OrganizerAccount   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CouseWork.Views     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	     � 
ResizeMode$  NoResize=�5   5    � WindowStartupLocation$ CenterScreen=�5       � Title$ 
TicketWave��5	   	   $	��650��6   $
��1000��6)    � 	Resources 5
         SecondaryGradientBrush �� 
StartPoint �� EndPoint � Color � Offset  OutlineButtonStyle �� 
TargetType	 �� Property
 �� Value �� 
TargetType ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName��%C  &	        &	 �     �� 5   
   $	 0,0&�6=   $	 1,1&�6N   .��5      � $ #333333��6   $	 0.0Q�6+   � 5      $ #111111��6   $	 1.0Q�6+   5      �� 5   
   " ��6+   .�5      �� 	 � ��
 5      �� 5      " ��6&   .R�5      �� $ border��6!   $ 10��5   !   
���   �
������ $��2q�5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5       5      P� 5       �@J�IsMouseOver$
 True��6=   . �5   "   �� $ border��6)   	 � ��

 �   �6S   �� 5   "   	 � ��

 �����6?   5      5       5"      �� 5#      	 � ��

 �   �6+   �� 5$      	 � ��$
 8q�6'   �� 5%      	 � ��

 ���� 6+   �� 5&      	 � ��$

 10,5q�6(   �� 5'      	 � ��$
 14(�6)   �� 5(      	 � ��$
 SemiBold"�6+   5*      5+       � DataContext 5,      4   .CouseWork.ViewModels.OrganizerAccountViewModel  5-   
   .��5/      � #��[  6    � ColumnDefinitions 50   
   �� 51      $	��200 �6   �� 52      $��* �6   .+�54   
   �� $��0��6   
��� ���.+�55      �� #��[ 6     item138#���  6A   ��6e   �� *EventCommand+�� 56      #��[ 6     item139#���  6A   ��6e   �� *EventScheduleCommand+�� 57      #��[ 6     item145#���  6A   ��6e   �� *AskQuestionCommand+�� 58      -   5        #��[ 58        item140#���  6A   6e   5:      �� 5;   
   $��1��6   ��6)   �� *CurrentView+5=      5>      