   M S B A M L   `   `   `  ����F  BSmartMirror, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SmartMirrorSmartMirror  5          SmartMirror.weatherView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:SmartMirror     5        � 	Resources  5
         weatherData � Source � XPath"   SmartMirror.WeatherConverter  weatherConverter(   "SmartMirror.Weather_hour_Converter  hourConverter��%�   &	        &	 n     &	 �     � 5   
   $> 8http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4311374100?�6.   $ rss/channel/item��6p     5   
   6!     5   
   6'   5      .��5      � ��5   
   �� 5       �� Color$ Transparent��6    � RowDefinitions 5   
   �� 5      $��8* �6   �� 5      $��1* �6   �� 5      $��3* �6   .+�5   
   � $��0��6   .+�5      �� -   5         ��Name$ temperature��5      $
��Wrap=�6-   $��Right=�6�   $	��Top=�6�   $��
0, 2, 4, 0q�6�   $��20(�6�   
���������6A   ��  �� Source# [   �� XPath$# description/body/data[1]/temp��6�   �� 5      -   5        $ skyState��5      $
��Wrap=�6*   $��Right=�6�   $	��Top=�6�   $��	0,27,4,10q�6�   $��20(�6�   
���������6>   �� # [  $# description/body/data[1]/wfEn��6�   �� 5      -   5        $ location��5      $
��Wrap=�6*   $��Right=�6�   $��Bottom=�6�   $��0,0,4,10q�6�   $��20(�6�   
���������6>   �� # [  $ category��6�   5      � 5    
   $��1��6    � ColumnDefinitions 5!      �� 5"      $��1* �6#   �� 5#      $��1* �6#   �� 5$      $��1* �6#   �� 5%      $��1* �6#   .+�5'      �� $��0��6   $��Center=�6(   $��
koverwatch)�6E   
���������6]   �� # [  $# description/body/data[1]/hour��	 �� 	Converter#	 [ 6�   �� 5(      $��1��6   $��Center=�6(   $��
koverwatch)�6E   
���������6]   �� # [  $# description/body/data[2]/hour��#	 [ 6�   �� 5)      $��2��6   $��Center=�6(   $��
koverwatch)�6E   
���������6]   �� # [  $# description/body/data[3]/hour��#	 [ 6�   �� 5*      $��3��6   $��Center=�6(   $��
koverwatch)�6E   
���������6]   �� # [  $# description/body/data[4]/hour��#	 [ 6�   5+      � 5-   
   $��2��6    5.      �� 5/      $��1* �6#   �� 50      $��1* �6#   �� 51      $��1* �6#   �� 52      $��1* �6#   .+�54      �� $��30��6   $��30��6   $��0��6+   ��6;   �� # [  $# description/body/data[1]/wfEn��#	 [ �� 55      $��30��6   $��30��6   $��1��6+   ��6;   �� # [  $# description/body/data[2]/wfEn��#	 [ �� 56      $��30��6   $��30��6   $��2��6+   ��6;   �� # [  $# description/body/data[3]/wfEn��#	 [ �� 57      $��30��6   $��30��6   $��3��6+   ��6;   �� # [  $# description/body/data[4]/wfEn��#	 [ 58      59      5:      