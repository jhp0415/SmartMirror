   M S B A M L   `   `   `  ����F  BSmartMirror, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SmartMirrorSmartMirror  5          SmartMirror.Alarm_list   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:SmartMirror     5        � Topmost  . $
��1500��5	      $
��1500��6    � AllowsTransparency .  � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�6    � WindowState$ 	Maximized=�6A    � 
ResizeMode$ NoResize=�5   	    � Title$ 
Alarm_list��5      ��5   	   Z� .��5      �� -   5        $��t_alarm_list��5       �� CornerRadius$ 10��6!   $	��300��63   $	��300��6?   ���	#33000000._�5   
   �  � RowDefinitions 5      �� 5      $��1* �6    �� 5      $��1 �6    �� 5      $��8* �6    .+�5      � $��0��6   	 � ColumnDefinitions	 5      �� 5      $��1* �6'   �� 5      $��6* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   .+�5      �� $��0��6   
������ 
������ $��5q�6\   .��5      �� -   5        $��image/button/cancel.png��5      6=   5       �� 5!      $��2��6   
������ 
������ $��3q�6\   .��5"      �� -   5        $��image\button\plus.png��5"      6;   5#      �� 5$      $��3��6   
������ 
������ $��3q�6\   .��5%      �� -   5        $��Image/Button/minus.png��5%      6<   5&      5'      � 5(      $��1��6   
���   �6    �� 5)      $��2��6   
������ ._�5*      �� -   5        
 ��Name$
 
alarm_list��5*      
�������$��20(�6A   
������ 
������ 6�   5+      5,      5-      5.      