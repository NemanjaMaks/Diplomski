   M S B A M L   `   `   `  ����D  @Diplomski, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Diplomski.CustomComponentsDiplomski.CustomComponents  5      3    -Diplomski.CustomComponents.AdminDezurstvoPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:Diplomski.CustomComponents     5        N� Title$  AdminDezurstvoPage��5	       � 	Resources 5
       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius �� 
StartPoint	 �� EndPoint
 � Color � Offset ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5   
   " ��6   .�5      ��  �@��TextAlignment$ Center=�68   �� 5       � ��
 �����6;   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   $ 10��61   ���#ccc$��0,1,1,0q�5      ��5      �� 5   "   $ 0.5,0&�66   $	 0.5,1&�6I   .��5   &   � $
 #7c7c7c��63   $	 0.0Q�6C   6R   � 5   &   $
 #404040��63   $	 0.5Q�6C   6R   5   #   ._�5      �� $ contentPresenter��6/   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   #��z��5   '   5       5       P� 5!       �@J�IsMouseOver . . �5"   "   �� $ border��6?    � �� �#7c7c7c6S   5#      5%      5'      5(      5)      .��5*      �  � RowDefinitions 5+   
   �� 5,      $	��10* �6   6+   �� 5-      $��* �6   6)   .+�5/   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5/      $��0��6%      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . 51      �� 52   
   -   5         ��Name$ 
btn_export��52      $��Center=�6%   $��Center=�6@   $��1��6]   $	��100��6j   $��30��6v   .��6�   Export6�   53      54      