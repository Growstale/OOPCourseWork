   M S B A M L   `   `   `  ����D  @CouseWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:CouseWork.ViewsCouseWork.Views  5   	   ;"clr-namespace:CouseWork.ViewModelsCouseWork.ViewModels  5   	   9!clr-namespace:CouseWork.UtilitiesCouseWork.Utilities  5   	   &     CouseWork.Views.TicketRefundEdit   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:CouseWork.Views     5   	   9
viewModels"clr-namespace:CouseWork.ViewModels     5   	   7	utilities!clr-namespace:CouseWork.Utilities     5   	     � 	Resources  5
         CustomDataGridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader  OrangeButtonStyle��%�  &	        &	     �� 5   
   "  6,   .�5      ��  � ��$ 14(�6)   �� 5       � ��
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
 Left=�64   5#      5$       � DataContext 5&      0  *CouseWork.ViewModels.TicketRefundViewModel  5'   
   .��5)      �  � RowDefinitions 5*   
   �� 5+      $��1* �6   �� 5,      $��4* �6   .+�5/   
     #��[  6      AutoGenerateColumns$ False��51      $��Stretch=�52      $��Stretch=�53      $��10q�54         
IsReadOnly$
 True��55      ��5/   @   �� *TicketRefunds+ �� SelectedItem 50      �� *SelectedTicketRefund+	 �� Mode$	 TwoWay=�
   Columns
 56      0  *System.Windows.Controls.DataGridTextColumn  57        item163,  &System.Windows.Controls.DataGridColumn   Header# �  6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *TicketRefundID+  58        item164# �  6%    6H   �� *SaleID+  59        item165# �  6%    6H   �� *UserID+  5:        item154# �  6%    6H   �� *
RefundDate+5=      �� 5?   
   $��1��6    �� Orientation$ Vertical=�6"   $��10q�69   .+�5@      �� $ 
Horizontal=�6   $��0,0,0,10q�62   .+�5A      �� #���  6   
��� ���$��Center=�6Q   $	��100��6l   �� 5B       �� 
IsReadOnly$
 True��6_   $	��200��6q   ��6   �� *TicketRefundID+ �� UpdateSourceTrigger$ PropertyChanged=�6q   5C      �� 5F        item166#���  6   #��[ 6b   ��69   �� *ApproveRefundCommand+6b   �� 5G        item167#���  6   #��[ 6a   ��69   �� *RejectRefundCommand+6a   5H      5I      5J      