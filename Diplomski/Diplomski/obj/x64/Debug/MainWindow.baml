   M S B A M L   `   `   `  ����D  @Diplomski, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Diplomski	Diplomski  5   	       Diplomski.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Diplomski     5   	   &shellSystem.Windows.Shell     5   	     � Title$  	Diplomski��5	   	   $	��800��6   $
��1200��6(    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$ ThreeDBorderWindow=�6.    � Icon$ /Resources/logo.png��5   	    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
StartPoint	 �� EndPoint
 � Color � Offset �� Triggers P� Property P� Value��%  'm�       'o� �    �� 5   
   " m�6   .�5      ��  � ��
 �   �6,   �� 5       � ��
 �����6;   �� 5       � �� 5      �� 5      $ 0.5,0&�6*   $	 0.5,1&�6=   .��5      � $
 #7c7c7c��6'   $	 0.0Q�67   6F   � 5      $
 #404040��6'   $	 0.5Q�67   6F   5      5      �� 5       � ��$ 1q�60   �� 5       � ��$ 1q�6'   63    5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �#7c7c7c63   5      5      �� 5    
   " o�6   .�5!      ��  � �� 5"      �� 5#      $ 0.5,0&�6*   $	 0.5,1&�6=   .��5$      � $
 #9a0101��6'   $	 0.0Q�67   6F   � 5%      $
 #b13a3a��6'   $
 0.75Q�67   6G   5&      5(      5*      5+      .��5,      �  � ColumnDefinitions 5-   
   �� 5.      -   5         ��Name$ collapsingColumn��5.      ��5/      �� 50      " ��6   .�51      ��  � ��$ 0 �62   6>    52      �� 53       �� Binding 6*   �� *ExpandColumn+ �� Value True6K   .a�54   "   ��  � ��$ * �6:   6F   55      57      59      �� 5:      $��4* �6   6,   .+�5<   
   o� -   5         o�Name$ 
basic_menu��5<      $��0��6#   ��5=      �� 5>      .R�5?      ]� $��Stretch=�6    ��.  6<   5@      .@�5B      m� -   5        $��	dezurstva��5B      $��Moja dežurstva��6(   $	��Top=�6@   6n   m� 5C      -   5        $��sva_dezurstva��5C      $��Sva dežurstva��6,   $	��Top=�6C   6q   m� 5D      -   5        $��	prference��5D      $��
Preference��6(   $	��Top=�6<   6j   m� 5E      -   5        $��poslati_zahtevi��5E      $��Moji zahtevi��6.   $	��Top=�6D   6r   m� 5F      -   5        $��primljeni_zahtevi��5F      $��Pristigli zahtevi��60   $	��Top=�6K   6y   m� 5G      -   5         m�Name$ logout��5G      $��Logout��6'   $��Bottom=�68   $	��1,2q�6g    � UseLayoutRounding .  6t   5H      o� 5I   
   -	   5        $ 
admin_menu��5I      $��0��6#   $y�Hidden=�63   ��5J      �� 5K      .R�5L      ]� $��Stretch=�6    ��.  6<   5M      .@�5O      m� -
   5        $��dezurstva_admin��5O      $��
Dežurstva��6.   $	��Top=�6A   6o   m� 5P      -   5        $��dezurstva_import��5P      $��Dodavanje dežurstva��6/   $	��Top=�6L   6z   m� 5Q      -   5        $��	korisnici��5Q      $��	Korisnici��6(   $	��Top=�6;   6i   m� 5R      -   5        $��
statistika��5R      $��
Statistika��6)   $	��Top=�6=   6k   m� 5S      -   5        $ logout_admin��5S      $��Logout��6-   $��Bottom=�6>   $	��1,2q�6m    .  6z   5T       � 5U   
   -   5          �Name$ frame��5U        � NavigationUIVisibility$ Hidden=�6   $��1��6?   5V      5W      