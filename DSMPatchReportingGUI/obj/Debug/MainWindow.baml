   M S B A M L   `   `   `  ����l  hSystem.Windows.Controls.DataVisualization.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null�rclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit)System.Windows.Controls.DataVisualization  5   	   �{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	   O KDSMPatchReportingGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:DSMPatchReportingGUIDSMPatchReportingGUI 5   	   %   DSMPatchReportingGUI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   �DVrclr-namespace:System.Windows.Controls.DataVisualization;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   �DVC{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   4local"clr-namespace:DSMPatchReportingGUI     5   	     � Title$  DSM Management Suite��5
   	   $	��800��6&   $��1167.333��63    � WindowState$ 	Maximized=�6D    � Icon$ resx/Logo.png��6\    � 	Resources 5         GlassButton �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty	 O� KeyTime
 O� Value �� CornerRadius ��Name � RowDefinitions J� Opacity �� RelativeTransform �� ScaleX �� ScaleY �� AngleX �� AngleY �� Angle T� X T� Y � Color � Offset �� EndPoint �� 
StartPoint �� Triggers P� Property P� Value �� 
TargetName �� Property  �� Value! N� EnterActions  	Timeline1" N� ExitActions# ��Name  	Timeline2  TextBoxBaseControlTemplate$ ��Name��%�	  &	        7  7  &	 �    �� 5   
   " ��6.    5      ���� 5      	Timeline1 .��5      U� $ 00:00:00X�64   $
 glow��6I   $ (UIElement.Opacity)�6f   .\�5      �� $	 00:00:00.3000000��6/   $
 1Q�6J   5      5      �� 5      	Timeline2 .��5      U� $ 00:00:00X�64   $
 glow��6I   $ (UIElement.Opacity)�6f   .\�5      �� $	 00:00:00.3000000��6/   $
 0Q�6J   5      5      5      .R�5      �� ���	#FFFFFFFF$��1,1,1,1q�6-   $ 4,4,4,4��6G   ._�5      �� $ border��6   ���	#7F000000���	#FF000000$��1,1,1,1q�6X   $ 4,4,4,4��6r   ._�5      �  5      �� 5      $��0.507* �6,   �� 5      $��0.493* �6,   .+�5       �� $
 glow��6K   $ 0Q�6!   $��Stretch=�6-   $
��Auto��6Y   $��2��6f   $ 4,4,4,4��6w   ��5!      
� 5"   "    5#   &   V� 5$   *   .~�5%   .   �� $ 1.702Q�6=   $ 2.243Q�6L   �� 5&   .   $ 0Q�6<   $ 0Q�6G   �� 5'   .   $ 0Q�6>   T� 5(   .   $ -0.368Q�6A   $ -0.152Q�6L   5)   +   .��5+   &   � $ 	#B28DBDFF��63   $ 0Q�6E   � 5,   &   $ 	#008DBDFF��63   $ 1Q�6E   5-   #   5/      �� 50      $��Center=�6+   $��Center=�6H   $
��Auto��6c   $��2��6p   �� 51      $ shine��6P   $��Stretch=�6!   $��0,0,0,0q�6?   $
��Auto��6_   $ 4,4,0,0��6l   ��52      �� 53   "   $ 0.494,0.889&�66   $ 0.494,0.028&�6M   .��54   &   � $ 	#99FFFFFF��63   $ 0Q�6E   � 55   &   $ 	#33FFFFFF��63   $ 1Q�6E   56   #   58      59      5:      5;       5<      P� 5=       � �� . . �5>      �� $ shine��60    �@J�Opacity$	  0.4Q�6C   �� 5?      $ border��63    � ��  �	#CC0000006G   �� 5@      $
 glow��63    � y�$  Hidden=�6E   5A      P� 5B       �@J�IsMouseOver . ! 5C      �� 5D      8��  6*   " 5F      �� 5G      $# Timeline2_BeginStoryboard��6*   8�� 6M   5I      5K      �� 5L   
   " ��6=   .R�5M      �� $ Bd��5N      #��z��5M      
���   �#��z��5O      $ 10��6E   ._�5P      �� $$ PART_ContentHost��6   5Q       5R      P� 5S       � {� .  . �5T      �� #  � ��63   $ Bd��6u    � ���� 5U      #  � ��63    � ��5V      P� 5W       � ��$
 Auto��6+   . �5X      ��  � ��$	  100��61   5Y      P� 5Z       � ��$
 Auto��6,   . �5[      ��  � ��$  20��62   5\      5^      5_      .��5`      � ��5a   
   �� 5b      % �� ImageSource$)% #resx/gray-abstract-wallpaper-17.jpg��6   & �� Opacity$	& 0.3Q�6K   .+�5e   
   o� $��27��6   $	��Top=�6   $��1,0,0,0q�63   ���	#FF4E5D6C.@�5f      m� $��	Dashboard��6   $��27��6*   $��70��66   
�������$
��Bold"�6T   .@�5g      m� -   5        ' m�Name$' LoginMenuButton��5g   -   $��Anmelden��6   
���   �6Y   m� 5h      -   5        $' ManageUsersMenuButton��5h   7   $��Benutzer verwalten��6   
���   �6i   m� 5i      -   5        $��Web-Dashboard öffnen��5i      
���   �6L   m� 5j      $��Anwendung beenden��6   
���   �66   5k      m� 5l      -   5        $' MenuReportingCategory��5l   T   $��	Reporting��6   $��27��6*   $��70��66   
�������.@�5m      m� -   5        $ ��Neusten Sicherheitslücken��5m      $
��Left=�6>   $��24��6Y   $	��250��6e   
���   �5n      m� 5o      -   5        $$��Die meisten Sicherheitslücken��5o      $
��Left=�6B   $��24��6]   $	��250��6i   
���   �5p      m� 5q      -   5        $��Compliance Report��5q      $
��Left=�66   $��24��6Q   $	��250��6]   
���   �5r      m� 5s      -   5        $' CummulativeReport��5s   =   $��Zusammenfassender Report��6   $
��Left=�6X   $��24��6s   $	��250��6   
���   �5t      m� 5u      -	   5        $ ��Report Verzeichnis öffnen��5u      $
��Left=�6>   $��24��6Y   $	��250��6e   
���   �6�   5v      m� 5w      $��?��6   $��27��6"   $��24��6.   
�������.@�5x      m� -
   5        $��Dokumentation��5x      $
��Left=�62   $��24��6M   $	��175��6Y   $��0,0,-20.2,0q�6e   
���   �6�   m� 5y      -   5        $��Support Kontaktieren��5y      $
��Left=�69   $��24��6T   $	��250��6`   $��0,0,-22.2,0q�6l   
���   �6�   m� 5z      -   5        $��Version��5z      $
��Left=�6,   $��24��6G   $	��153��6S   
���   �6�   m� 5{      -   5        $��Lizenz��5{      $
��Left=�6+   $��24��6F   $	��153��6R   
���   �6�   5|      �� 5}      6   �� 5~      -   5        ( ��Name$( txtFilterComputer��5~   *   $�� ��6   $	��240��6   #��[ 6E   6~   �� 5      -   5        $	��...��5      ���	#FFFFFFFF#���   6O   $��18��6x   6�   �� 5�      -   5        $��Filter��5�      $	��120��6A   ���	#FFFFFFFF#���   6d   6�   5�      �� 5�   
   $��52��6   $��
0,27,0.2,0q�6    
���   �$	��Top=�6C   $��0��6[   ) J� Effect) 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�      ��5�      �� 5�      $ 0.5,1&�6&   $ 0.5,0&�67   .��5�      � $ 	#FF343D46��6#   $ 0Q�65   � 5�      $ 	#FF4E5D6C��6#   $ 1Q�65   5�      5�      �� 5�   
   $#��DSM Patch Reporting Dashboard��6   $
��Left=�68   $��10,32,-97,0q�6S   $	��Top=�6h   $��	Agency FB)�6�   $��24(�6�   $��47��6�   $	��879��6�   $
��Bold"�6�   
�������6�   � 5�   
   $��0,84,0,0q�6    5�      �� 5�      $	��320 �6    6/   �� 5�      6"   .+�5�      � $��0��6   * � ColumnDefinitions* 5�      �� 5�      $��* �6'   63   �� 5�      $��* �6'   63   .+�5�      � $��0��6   .+�5�      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��mcChart��5�   ?   
+ �� Top$+ 1��6    , �� Left$, 1��6/   
�������$
��Auto��6g   -   Title$- Aktueller Patch Status��6t   ) 5�        5�      .   Series.. 5�      B   <System.Windows.Controls.DataVisualization.Charting.PieSeries  H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries/   IndependentValueBinding/ 6(   �� 0 �� Path$	0 Key�1   DependentValueBinding1 6U   �� $0 Value�5�      5�      5�      � 5�      $��1��6   .+�5�        -   5        2  Name$2 mcChart_ein_Copy��5�       
�������$- Offene Patche nach Datum��6R   ) 5�        5�      .. 5�      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  ?   9System.Windows.Controls.DataVisualization.Charting.Series3   Title$3 Offene Patche��6)   / 6?   �� *Key+1 6g   �� *Value+  5�      $3 Trend��6)   / 67   �� *Key+1 6_   �� *Value+5�      5�      5�      � 5�      $��1��6   $��0,0,0,24q�6    .+�5�        -   5        $��mcChart_ein��5�   ;   $+ 1��6   $, 1��6+   
�������$(- "Die häufigsten Sicherheitslücken��6g   ) 5�        5�      .. 5�      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  / 6'   �� $	0 Key�1 6T   �� $0 Value�5�      5�      5�      5�      �� 5�   
   $��23��6   $��0,0,0.2,-0.4q�6    $��Bottom=�66   ���	#FF4E5D6C.@�5�      �� -   5        4 ��Name$4 ProcessStatusText��5�      $��-��67   $��23��6C   $	��Top=�6O   � 5�      -   5        5 �Name$5 ProcessStatusBar��5�      6 � Minimum$6 0Q�64   7 � Maximum$	7 100Q�6@   $	��200��6N   $��20��6Z   $y�Hidden=�6f   �� 5�      -   5        $4 LiteVersionInfo��5�      $����Hinweis: In der LITE Version sehen Sie nur die 5 häufigsten Sicherheitslücken. Im Trend können Sie nur die 5 letzten Scans sehen��65   $��23��6�   $	��Top=�6�   
�������6�   5�      5�      5�      