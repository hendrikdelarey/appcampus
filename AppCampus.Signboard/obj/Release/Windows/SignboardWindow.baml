   M S B A M L   `   `   `  ���� O  KAppCampus.Signboard, Version=1.0.15.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  1    +AppCampus.Signboard.Windows.SignboardWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /
Convertersclr-namespace:Converters       � Title$  SignboardWindow�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�
���   ����� *&$SystemParameters.PrimaryScreenHeight+���� *%#SystemParameters.PrimaryScreenWidth+ � 	Resources    Converters.CenterConverter   CenterConverter�� %   &	          .��� -   $��
ScreenGrid��
���   �$��0q� J� SnapsToDevicePixels .   � RowDefinitions �� $��8* ��� -    �Name$ AnnouncementRow��$��* �.+�� -   $��SlideshowGrid��$��0���� -   $!��AnnouncementTextBlockBorder��$��1�����	#FF252161$��0q�	 �� Padding$	 0q���Z� ._��� -   
 ��Name$
 AnnouncementCanvas��$��Center=�.+��� -    ��Name$ AnnouncementTextBlock��$�� ��
������� �� Padding$ 0q�
 �� Top `�  `� 	Converter# [   `� ConverterParameter$	 top��.1���  �� ElementName$ AnnouncementCanvas�� �� Path$ ActualWidth��� $ AnnouncementCanvas��$ ActualHeight��� $ AnnouncementTextBlock��$ ActualWidth��� $ AnnouncementTextBlock��$ ActualHeight� �� Left `� # [  $
 left��.1��� $ AnnouncementCanvas��$ ActualWidth��� $ AnnouncementCanvas��$ ActualHeight��� $ AnnouncementTextBlock��$ ActualWidth��� $ AnnouncementTextBlock��$ ActualHeight��� -   $��LoadingTextblock��
�������$��
Loading...��$��0��$��0��$��38(�$��0q�$��Center=�$��Center=�