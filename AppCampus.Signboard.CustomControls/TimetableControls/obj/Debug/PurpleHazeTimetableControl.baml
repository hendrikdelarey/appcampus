   M S B A M L   `   `   `  ����  TimetableControls2    ,TimetableControls.PurpleHazeTimetableControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          >�Name$  PurpleHazeTimetable��5      .��5	      (�  (� Stretch$
 Fill=�6   .��5
      � -   5         �Name$ 
LayoutRoot��5
   	   $	��836��6   $
��1920��6*   .+�5      �� -   5         ��Name$% PurpleHazeTimetableControl_Copy��5      $��Stretch=�65   $	��Top=�6S   }�. $	��836��6   $
��Auto��6�    � UseLayoutRounding .  .+�5      F� -   5         F�Name$ TopPurpleLine��5      �����`� `� �`� `� @"�r`� @"�r`� �@"�r`� @"�r���@"�r����@"�r���`� ���`� ����`� ���`� `� `� `� ���	#FF252161$	��251��6�    �� Left$	 -14��6�   $
��1935��6�    �� Stretch$
 Fill=�6�   �� 5      -   5         ��Name$ StopName��5      $��CharacterEllipsis=�66   
�������$	��100(�6j   $)��#/Timetable;component/Fonts/#Calibri)�6y   $��NoWrap=�6�   $	��113��6�   	 �� IsHyphenationEnabled	 . 
 �� LineStackingStrategy$
 BlockLineHeight=�6�   $ 37��6   �� 
LineHeight$	 100��6"   �� TextAlignment$
 Left=�63  
 �� Top$ 34��6I  $	��915��6^  $��Center=�6l  $��Center=�6�  ��6�  �� *
StopName+�� 5      -   5        $ OperatorName��5      $��CharacterEllipsis=�66   
�������$	��100(�6j   $)��#/Timetable;component/Fonts/#Calibri)�6y   $��NoWrap=�6�   $	��113��6�   	 . $
 BlockLineHeight=�6�    �� Right$ 37��6  $	 100��6"  $ 31.835��63  $��901.5��6H  $��Right=�6V  $��Center=�6r  ��6�  �� *OperatorName+�� 5      -   5        $ CurrentTime��5      $��CharacterEllipsis=�66   
�������$��65(�6j   $)��#/Timetable;component/Fonts/#Calibri)�6x   $��NoWrap=�6�   $��66��6�   	 . $
 BlockLineHeight=�6�   $ 37��6  $ 65��6!  $ Right=�61  $ 151.25��6I  $	��288��6^  $��Center=�6l  $��Center=�6�  ��6�  �� *TimeDisplayString+�� 5      -   5        $ WalkingDistance��5      $��CharacterEllipsis=�66   
�������$��48(�6j   $)��#/Timetable;component/Fonts/#Calibri)�6x   $��NoWrap=�6�   $��66��6�   	 . $
 BlockLineHeight=�6�   $ 37��6  $ 65��6!  $
 Left=�61  $ 151.25��6I  $��817.5��6^  $��Center=�6l  $��Center=�6�  ��6�  �� *WalkingTime+&   System.Windows.Controls.DataGrid  5      $ -1��6   $	 248��6,   $	��712��6=   $
��1927��6J      AutoGenerateColumns .     VerticalGridLinesBrush �	#00000000$)��#/Timetable;component/Fonts/#Calibri)�6�   $��64(�6     HeadersVisibility$
 None=�6#     AlternatingRowBackground �	#FFE6E6E6���	#FF252161 � DataContext 6W   �� ��6�   �� *TimetableEntryModelCollection+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6      Header$ ArrivalTime��6T      CanUserSort .     CanUserReorder .     CanUserResize .     
Foreground �	#FF252161   FontSize$ 64Q�6�      
FontFamily$) #/Timetable;component/Fonts/#Calibri)�6�      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *DepartureTimeDisplayString+   ElementStyle 5   	   �� 5   
     �� 
TargetType"  ��6   .�5      �� ! �� Property! � ��" �� Value$" 92,0,0,0q�6$   5      5   	     5      $ * 6   #   	FontStyle$# Italic$�6%   $ DestinationStop��6X    68   �� *Destination+ 5   	   �� 5   
   "  ��6   .�5      �� ! � ��$" 0,0,92,0q�6$   �� 5       ! � ��$" Right=�61   5!      5#   	   5%      5&      5'      5(      5)      