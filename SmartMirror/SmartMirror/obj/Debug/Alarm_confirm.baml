   M S B A M L   `   `   `  ����F  BSmartMirror, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SmartMirrorSmartMirror  5   	       SmartMirror.Alarm_confirm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SmartMirror     5   	     � Topmost  .  � Title$ Alarm_confirm��5	   	   $
��1024��6   $
��1280��6-    � WindowStyle$
 None=�6:    � 
ResizeMode$ NoResize=�6M    � WindowStartupLocation$ Manual=�6c    � WindowState$ 	Maximized=�6�    � AllowsTransparency . ��5   	   Z� .��5      � .+�5   
   �� -   5        $��t_alarm_confirm��5      $��519,376,461,348q�6(   $	��300��6A   $	��300��6N    �� CornerRadius$ 10��6Z   ���	#33000000._�5      �  � RowDefinitions 5      �� 5      $��9* �6$   �� 5      $��2* �6$   .+�5      �� -   5        $��alarm_content��5      $��0��6/   $��오후 5시 30분��6<   
������ $��Center=�6f   $��Center=�6�   $��40(�6�   $��
koverwatch)�6�   
�������
������ 6�   � 5      $��1��6   	 � ColumnDefinitions	 5      �� 5      $��1* �6+   �� 5      $��1* �6+   �� 5      $��1* �6+   .+�5      r� -   5        $��mp��5   #   
 r� LoadedBehavior$
 Manual=�6-    r� Stretch$
 Fill=�6E    r� UnloadedBehavior$
 Stop=�6T    r� Volume$
 0.30Q�6l   �� 5      -   5        $��1��5      $��확인��6-   $��
koverwatch)�6:   
�������
������ $��20(�6   
������ 6�   5      5      5      5      5!      