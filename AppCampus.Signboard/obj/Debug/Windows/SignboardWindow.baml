   M S B A M L   `   `   `  ����O  KAppCampus.Signboard, Version=1.0.15.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  5   	   1    +AppCampus.Signboard.Windows.SignboardWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /
Convertersclr-namespace:Converters     5   	     � Title$  SignboardWindow��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�6    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowState$ 	Maximized=�6.   
���   ���5   	   �� *&$SystemParameters.PrimaryScreenHeight+��5   	   �� *%#SystemParameters.PrimaryScreenWidth+ � 	Resources 5         Converters.CenterConverter   CenterConverter�� 5   
   %   &	          5      6)   5      .��5      � -   5        $��
ScreenGrid��5      
���   �$��0q�60    J� SnapsToDevicePixels .   � RowDefinitions 5   
   �� 5      $��8* �6   �� 5      -   5         �Name$ AnnouncementRow��5   '   $��* �6   6'   .+�5   
   � -   5        $��SlideshowGrid��5      $��0��6$   5      �� 5   
   -   5        $!��AnnouncementTextBlockBorder��5      $��1��6K   ���	#FF252161$��0q�6o   	 �� Padding$	 0q�6z   ��64   Z� ._�5      �� -   5        
 ��Name$
 AnnouncementCanvas��5      $��Center=�61   .+�5      �� -   5         ��Name$ AnnouncementTextBlock��5      $�� ��6;   
������� �� Padding$ 0q�6V   
 �� Top 5      `� 5       `� 	Converter# [  6    `� ConverterParameter$	 top��6H   .1�5      ��  �� ElementName$ AnnouncementCanvas��6    �� Path$ ActualWidth�6;   �� 5      $ AnnouncementCanvas��6   $ ActualHeight�6;   �� 5      $ AnnouncementTextBlock��6   $ ActualWidth�6>   �� 5       $ AnnouncementTextBlock��6   $ ActualHeight�6>   5!       �� Left 5#   
   `� 5$      # [  6   $
 left��6H   .1�5%      �� $ AnnouncementCanvas��6   $ ActualWidth�6;   �� 5&      $ AnnouncementCanvas��6   $ ActualHeight�6;   �� 5'      $ AnnouncementTextBlock��6   $ ActualWidth�6>   �� 5(      $ AnnouncementTextBlock��6   $ ActualHeight�6>   5)      5+      5,      5-      �� 5/   
   -   5        $��LoadingTextblock��5/      
�������$��
Loading...��6?   $��0��6Q   $��0��6a   $��38(�6n   $��0q�6|   $��Center=�6�   $��Center=�6�   50      51      