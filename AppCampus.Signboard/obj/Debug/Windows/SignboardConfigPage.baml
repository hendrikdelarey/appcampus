   M S B A M L   `   `   `  ����O  KAppCampus.Signboard, Version=1.0.15.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AppCampus.Signboard.ControlsAppCampus.Signboard.Controls  5      5    /AppCampus.Signboard.Windows.SignboardConfigPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:AppCampus.Signboard.Controls     5        N� Title$  SignboardConfigPage��5	      .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5      �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��6q�61   $��Right=�6<   $
��Bold"�6X   �� 6k   $�Device State:��6o   6�   �� 5   
   $��0��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5      3   -AppCampus.Signboard.Controls.AnimatedGifImage  -   5          Name$ RefreshGifImage��5   6   3   -AppCampus.Signboard.Controls.AnimatedGifImage   	MouseDown$ RefreshGifImageMouseDown��6O   $��0,6,3,6q�6%   $��17��6t   $��17��6   $y�	Collapsed=�6�   �� 5      -   5        $��DeviceStateTextBlock��5   )   $��0,6,6,6q�6   $
��Bold"�6E   6Y   �� 5      -   5        $��DeviceStateDateTextBlock��5   )   $��0,6,6,6q�6   6K   5      �� 5    
   $��1��6   $��0��6!   $��6q�61   $��Right=�6<   $
��Bold"�6X   �� 6k   $�MAC Address:��6o   6�   �� 5!      $��1��6   $��1��6   $ 
Horizontal=�6,   .+�5"      �� -   5        $��MacAddressTextBlock��5"   )   $��0,6,6,6q�6   6F   �� 5#      -   5        $ ��../Resources/CopyImage.png��5#      $��16��68   $��16��6C   $��0,6,6,6q�6s   6�   �� 5$      -   5        $��MacAddressValueTextBlock��5$      $��0,6,6,6q�68   $y�	Collapsed=�6I   �� 5%      -   5         �Name$ ChangeMacAddressHyperlink��5%      .�6X   
(change)5'      5(      5)      �� 5+   
   $��2��6   $��0��6!   $��6q�61   $��Right=�6<   $
��Bold"�6X   �� 6k   $
�Key:��6o   6~   �� 5,      $��2��6   $��1��6   $ 
Horizontal=�6,   .+�5-      �� -   5        $��KeyTextBlock��5-   )   $��0,6,6,6q�6   6?   �� 5.      -	   5        $ ��../Resources/CopyImage.png��5.      $��16��68   $��16��6C   $��0,6,6,6q�6l   6�   �� 5/      -
   5        $��KeyValueTextBlock��5/      $��0,6,6,6q�61   $y�	Collapsed=�6C   �� 50      -   5        $ ChangeKeyHyperlink��50      .�6J   
(change)52      53      54      �� 56   
   $��3��6   $��0��6!   $��6q�61   $��Right=�6<   $
��Bold"�6X   �� 6k   $�Comment:��6o   6�   �� 57   
   $��3��6   $��1��6"   $ 
Horizontal=�62   .+�58      �� -   5        $��CommentTextBlock��58   )   $��0,6,6,6q�6   6C   �� 59      -   5        $��CommentValueTextBox��59      $��0,6,6,6q�61   $	��220��6B   $��50��6N   " �� VerticalScrollBarVisibility$
 Auto=�6Z   $ �� HorizontalScrollBarVisibility$ Disabled=�6}   	 �� TextWrapping$
	 Wrap=�6�   
 �� AcceptsReturn
 . .��6�   5:      5;      �� 5=   
   -   5        $��RetryButton��5=   x   $��4��6   $��1��6   $��0,6,6,6q�6.   $��80��6?   $
��Left=�6J   $y�	Collapsed=�6�   .��6�   Retry6�   �� 5>   
   -   5        $��RequestButton��5>   z   $��4��6   $��1��6   $��0,6,6,6q�6.   $��80��6?   $
��Left=�6J   {�.  .��6�   	Request6�   5?      5@      