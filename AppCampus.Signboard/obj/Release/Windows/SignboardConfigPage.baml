   M S B A M L   `   `   `  ���� O  KAppCampus.Signboard, Version=1.0.15.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:AppCampus.Signboard.ControlsAppCampus.Signboard.Controls  5    /AppCampus.Signboard.Windows.SignboardConfigPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     <local*clr-namespace:AppCampus.Signboard.Controls       N� Title$  SignboardConfigPage��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��auto ��� $��* �.+��� $��0��$��0��$��6q�$��Right=�$
��Bold"��� $�Device State:���� $��0��$��1�� �� Orientation$ 
Horizontal=�.+�3   -AppCampus.Signboard.Controls.AnimatedGifImage  -     Name$ RefreshGifImage��3   -AppCampus.Signboard.Controls.AnimatedGifImage   	MouseDown$ RefreshGifImageMouseDown��$��0,6,3,6q�$��17��$��17��$y�	Collapsed=��� -   $��DeviceStateTextBlock��$��0,6,6,6q�$
��Bold"��� -   $��DeviceStateDateTextBlock��$��0,6,6,6q��� $��1��$��0��$��6q�$��Right=�$
��Bold"��� $�MAC Address:���� $��1��$��1��$ 
Horizontal=�.+��� -   $��MacAddressTextBlock��$��0,6,6,6q��� -   $ ��../Resources/CopyImage.png��$��16��$��16��$��0,6,6,6q��� -   $��MacAddressValueTextBlock��$��0,6,6,6q�$y�	Collapsed=��� -    �Name$ ChangeMacAddressHyperlink��.�
(change)�� $��2��$��0��$��6q�$��Right=�$
��Bold"��� $
�Key:���� $��2��$��1��$ 
Horizontal=�.+��� -   $��KeyTextBlock��$��0,6,6,6q��� -	   $ ��../Resources/CopyImage.png��$��16��$��16��$��0,6,6,6q��� -
   $��KeyValueTextBlock��$��0,6,6,6q�$y�	Collapsed=��� -   $ ChangeKeyHyperlink��.�
(change)�� $��3��$��0��$��6q�$��Right=�$
��Bold"��� $�Comment:���� $��3��$��1��$ 
Horizontal=�.+��� -   $��CommentTextBlock��$��0,6,6,6q��� -   $��CommentValueTextBox��$��0,6,6,6q�$	��220��$��50��" �� VerticalScrollBarVisibility$
 Auto=�$ �� HorizontalScrollBarVisibility$ Disabled=�	 �� TextWrapping$
	 Wrap=�
 �� AcceptsReturn
 . .���� -   $��RetryButton��$��4��$��1��$��0,6,6,6q�$��80��$
��Left=�$y�	Collapsed=�.��Retry�� -   $��RequestButton��$��4��$��1��$��0,6,6,6q�$��80��$
��Left=�{�.  .��	Request