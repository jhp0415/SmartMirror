   M S B A M L   `   `   `  ����F  BSmartMirror, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SmartMirrorSmartMirror  5   	       SmartMirror.NewsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SmartMirror     5   	     � Topmost  .  � AllowsTransparency .  � Title$ 
NewsWindow��5   	   $
��1024��6   $
��1280��6*    � WindowState$ 	Maximized=�67    � 
ResizeMode$ NoResize=�6O    � WindowStyle$
 None=�6e   ��5
   	   Z� .��5      �� -   5        $��t_news��5      $	��600��6   $	��300��6'   
������  �� CornerRadius$ 10��6N   ���	#33000000._�5   
   �  � RowDefinitions 5      �� 5      $��1* �6    �� 5      $��9* �6    .+�5      � $��0��6    � ColumnDefinitions 5      �� 5      $��10 �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��1* �6'   �� 5      $��10 �6'   .+�5      �� -   5        $��속보��5      $��1��6&   
������ 
������ 
�������6}   �� 5      -   5        $��정치��5      $��2��6&   
������ 
������ 
�������6�   �� 5       -   5        $��경제��5       $��3��6&   
������ 
������ 
�������6   �� 5!      -   5        $��사회��5!      $��4��6&   
������ 
������ 
�������6   �� 5"      -   5        $��국제��5"      $��5��6&   
������ 
������ 
�������6�   �� 5#      -   5        $��문화��5#      $��6��6&   
������ 
������ 
�������6   5$      � 5&      -   5        	 �Name$	 
news_items��5&   %   $��1��6   6%   5'      5(      5)      