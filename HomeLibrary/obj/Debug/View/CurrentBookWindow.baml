   M S B A M L   `   `   `  ����F  BHomeLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:HomeLibrary.ViewHomeLibrary.View  5   	   (    "HomeLibrary.View.CurrentBookWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:HomeLibrary.View     5   	     � Title$  CurrentBookWindow��5   	   $	��230��6#   $	��450��60    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       '�� G     �� 5
   
   " ��6   .�5      ��  � ��$ 8q�6'   5      �� 5   
   " ��6   .�5      ��  � ��$ 8q�6'   5      5      .��5      � ���#b5b3b0 � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��Title��6   $
��Bold"�6!   �� 5   
   $��Author��6   $��1��6"   $
��Bold"�6/   �� 5   
   $)��#Кількість сторінок��6   $��2��6.   $
��Bold"�6;   �� 5    
   $>��8Кількість прочитаних сторінок��6   $��3��69   $
��Bold"�6F   �� 5"   
   ���#e3e1de$��1��6>   ��6'   �� *Title+6>   �� 5#   
   ���#e3e1de$��1��6?   $��1��6O   ��6'   �� *Author+6O   �� 5$   
   ���#e3e1de$��1��6F   $��2��6V   ��6'   �� *NumberOfPages+6V   �� 5%   
   ���#e3e1de$��1��6J   $��3��6Z   ��6'   �� *NumberOfReadPages+6Z   �� 5'   
   $��Center=�6    �� Orientation$ 
Horizontal=�62   $��4��6K   $��1��6X   .+�5(      �� -   5        ���#424140
������� �� 	IsDefault . $��60��5(   c   $��5q�6q   .��6|   OK6�   �� 5)      ���#424140
�������	 �� IsCancel	 . $��60��6M   $��5q�6[   .��6f   Відміна6o   5*      5+      5,      