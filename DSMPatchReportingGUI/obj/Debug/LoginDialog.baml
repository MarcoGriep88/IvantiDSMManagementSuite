   M S B A M L   `   `   `  ����O  KDSMPatchReportingGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:DSMPatchReportingGUIDSMPatchReportingGUI  5   	   &     DSMPatchReportingGUI.LoginDialog   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:DSMPatchReportingGUI     5   	     � Title$  Anmelden��5   	   $	��320��6   $	��600��6'    � 
ResizeMode$ NoResize=�63    � WindowStyle$ 
ToolWindow=�6I    � WindowStartupLocation$ CenterScreen=�6b    � 	Resources 5	         GlassButton �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName	 �� TargetProperty
 O� KeyTime O� Value �� CornerRadius ��Name � RowDefinitions J� Opacity �� RelativeTransform �� ScaleX �� ScaleY �� AngleX �� AngleY �� Angle T� X T� Y � Color � Offset �� EndPoint �� 
StartPoint �� Triggers P� Property P� Value �� 
TargetName  �� Property! �� Value" N� EnterActions  	Timeline1# N� ExitActions$ ��Name  	Timeline2��%�  &	        7  7  �� 5
   
   " ��6.    5      ���� 5      	Timeline1 .��5      U� $ 00:00:00X�64   $
 glow��6I   $	 (UIElement.Opacity)�6f   .\�5      �� $
 00:00:00.3000000��6/   $ 1Q�6J   5      5      �� 5      	Timeline2 .��5      U� $ 00:00:00X�64   $
 glow��6I   $	 (UIElement.Opacity)�6f   .\�5      �� $
 00:00:00.3000000��6/   $ 0Q�6J   5      5      5      .R�5      �� ���	#FFFFFFFF$��1,1,1,1q�6-   $ 4,4,4,4��6G   ._�5      �� $ border��6   ���	#7F000000���	#FF000000$��1,1,1,1q�6X   $ 4,4,4,4��6r   ._�5      �  5      �� 5      $��0.507* �6,   �� 5      $��0.493* �6,   .+�5      �� $
 glow��6K   $ 0Q�6!   $��Stretch=�6-   $
��Auto��6Y   $��2��6f   $ 4,4,4,4��6w   ��5      
� 5    "    5!   &   V� 5"   *   .~�5#   .   �� $ 1.702Q�6=   $ 2.243Q�6L   �� 5$   .   $ 0Q�6<   $ 0Q�6G   �� 5%   .   $ 0Q�6>   T� 5&   .   $ -0.368Q�6A   $ -0.152Q�6L   5'   +   .��5)   &   � $ 	#B28DBDFF��63   $ 0Q�6E   � 5*   &   $ 	#008DBDFF��63   $ 1Q�6E   5+   #   5-      �� 5.      $��Center=�6+   $��Center=�6H   $
��Auto��6c   $��2��6p   �� 5/      $ shine��6P   $��Stretch=�6!   $��0,0,0,0q�6?   $
��Auto��6_   $ 4,4,0,0��6l   ��50      �� 51   "   $ 0.494,0.889&�66   $ 0.494,0.028&�6M   .��52   &   � $ 	#99FFFFFF��63   $ 0Q�6E   � 53   &   $ 	#33FFFFFF��63   $ 1Q�6E   54   #   56      57      58      59       5:      P� 5;       � �� . . �5<      �� $ shine��60     �@J�Opacity$	! 0.4Q�6C   �� 5=      $ border��63     � ��! �	#CC0000006G   �� 5>      $
 glow��63     � y�$! Hidden=�6E   5?      P� 5@       �@J�IsMouseOver . " 5A      �� 5B      8��  6*   # 5D      �� 5E      $$ Timeline2_BeginStoryboard��6*   8�� 6M   5G      5I      5J      .��5K      � ��5L   
   �� 5M      % �� ImageSource$)% #resx/gray-abstract-wallpaper-17.jpg��6   & �� Opacity$	& 0.3Q�6K   .+�5O   
   �� $��52��6   $��	0,0,0.2,0q�6    
���   �$	��Top=�6B   $��0��6Z   ' J� Effect' 5P      3  -System.Windows.Media.Effects.DropShadowEffect  5Q      ��5S      �� 5T      $ 0.5,1&�6&   $ 0.5,0&�67   .��5U      � $ 	#FF343D46��6#   $ 0Q�65   � 5V      $ 	#FF4E5D6C��6#   $ 1Q�65   5W      5Y      �� 5Z   
   $��Anmelden��6   $
��Left=�6#   $��	10,10,0,0q�6>   $	��Top=�6Q   $��	Agency FB)�6i   $��24(�6�   $��47��6�   $	��572��6�   $
��Bold"�6�   
�������6�   �� 5[   
   $
��API:��6   $
��Left=�6   $��	10,84,0,0q�6:   $	��Top=�6M   �� 5\   
   -   5        ( ��Name$( txtAPI��5\      $
��Left=�6"   $��23��6=   $��	99,87,0,0q�6I   ) �� TextWrapping$
) Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��448��6�   �� 5]   
   $��	Benutzer:��6   $
��Left=�6$   $��
10,124,0,0q�6?   $	��Top=�6S   �� 5^   
   -   5        $( txtUsername��5^      $
��Left=�6'   $��23��6B   $��
99,127,0,0q�6N   $
) Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��448��6�   �� 5_   
   $��	Passwort:��6   $
��Left=�6$   $��
10,164,0,0q�6?   $	��Top=�6S   G� 5`   
   -   5        * G�Name$* txtPassword��5`      $
��Left=�6+   $��23��6F   $��
99,167,0,0q�6R   $	��Top=�6f   $	��448��6~   6�   �� 5a   
   -   5        $��Anmelden��5a      $��	0,0,47,28q�6$   $��36��67   $��Bottom=�6C   $��Right=�6^   $	��102��6z   ���	#FFFFFFFF#���   6�   5b      5c      