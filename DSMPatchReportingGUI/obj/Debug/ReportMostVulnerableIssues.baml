   M S B A M L   `   `   `  ����O  KDSMPatchReportingGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:DSMPatchReportingGUIDSMPatchReportingGUI  5   	   5    /DSMPatchReportingGUI.ReportMostVulnerableIssues   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:DSMPatchReportingGUI     5   	     � Title$1  +Report - Die häufigsten Sicherheitslücken��5   	   $��372.316��6;   $	��638��6L    � WindowStartupLocation$ CenterScreen=�6X    � WindowStyle$ 
ToolWindow=�6}    � 	Resources 5	         GlassButton �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty	 O� KeyTime
 O� Value �� CornerRadius ��Name � RowDefinitions J� Opacity �� RelativeTransform �� ScaleX �� ScaleY �� AngleX �� AngleY �� Angle T� X T� Y � Color � Offset �� EndPoint �� 
StartPoint �� Triggers P� Property P� Value �� 
TargetName �� Property  �� Value! N� EnterActions  	Timeline1" N� ExitActions# ��Name  	Timeline2  TextBoxBaseControlTemplate$ ��Name��%�	  &	        7  7  &	 �    �� 5
   
   " ��6.    5      ���� 5      	Timeline1 .��5      U� $ 00:00:00X�64   $
 glow��6I   $ (UIElement.Opacity)�6f   .\�5      �� $	 00:00:00.3000000��6/   $
 1Q�6J   5      5      �� 5      	Timeline2 .��5      U� $ 00:00:00X�64   $
 glow��6I   $ (UIElement.Opacity)�6f   .\�5      �� $	 00:00:00.3000000��6/   $
 0Q�6J   5      5      5      .R�5      �� ���	#FFFFFFFF$��1,1,1,1q�6-   $ 4,4,4,4��6G   ._�5      �� $ border��6   ���	#7F000000���	#FF000000$��1,1,1,1q�6X   $ 4,4,4,4��6r   ._�5      �  5      �� 5      $��0.507* �6,   �� 5      $��0.493* �6,   .+�5      �� $
 glow��6K   $ 0Q�6!   $��Stretch=�6-   $
��Auto��6Y   $��2��6f   $ 4,4,4,4��6w   ��5      
� 5    "    5!   &   V� 5"   *   .~�5#   .   �� $ 1.702Q�6=   $ 2.243Q�6L   �� 5$   .   $ 0Q�6<   $ 0Q�6G   �� 5%   .   $ 0Q�6>   T� 5&   .   $ -0.368Q�6A   $ -0.152Q�6L   5'   +   .��5)   &   � $ 	#B28DBDFF��63   $ 0Q�6E   � 5*   &   $ 	#008DBDFF��63   $ 1Q�6E   5+   #   5-      �� 5.      $��Center=�6+   $��Center=�6H   $
��Auto��6c   $��2��6p   �� 5/      $ shine��6P   $��Stretch=�6!   $��0,0,0,0q�6?   $
��Auto��6_   $ 4,4,0,0��6l   ��50      �� 51   "   $ 0.494,0.889&�66   $ 0.494,0.028&�6M   .��52   &   � $ 	#99FFFFFF��63   $ 0Q�6E   � 53   &   $ 	#33FFFFFF��63   $ 1Q�6E   54   #   56      57      58      59       5:      P� 5;       � �� . . �5<      �� $ shine��60    �@J�Opacity$	  0.4Q�6C   �� 5=      $ border��63    � ��  �	#CC0000006G   �� 5>      $
 glow��63    � y�$  Hidden=�6E   5?      P� 5@       �@J�IsMouseOver . ! 5A      �� 5B      8��  6*   " 5D      �� 5E      $# Timeline2_BeginStoryboard��6*   8�� 6M   5G      5I      �� 5J   
   " ��6=   .R�5K      �� $ Bd��5L      #��z��5K      
���   �#��z��5M      $ 10��6E   ._�5N      �� $$ PART_ContentHost��6   5O       5P      P� 5Q       � {� .  . �5R      �� #  � ��63   $ Bd��6u    � ���� 5S      #  � ��63    � ��5T      P� 5U       � ��$
 Auto��6+   . �5V      ��  � ��$	  100��61   5W      P� 5X       � ��$
 Auto��6,   . �5Y      ��  � ��$  20��62   5Z      5\      5]      .��5^      � ��5_   
   �� 5`      % �� ImageSource$)% #resx/gray-abstract-wallpaper-17.jpg��6   & �� Opacity$	& 0.3Q�6K   .+�5b   
   �� $��52��6   $��	0,0,0.2,0q�6    
���   �$	��Top=�6B   $��0��6Z   ' J� Effect' 5c      3  -System.Windows.Media.Effects.DropShadowEffect  5d      ��5f      �� 5g      $ 0.5,1&�6&   $ 0.5,0&�67   .��5h      � $ 	#FF343D46��6#   $ 0Q�65   � 5i      $ 	#FF4E5D6C��6#   $ 1Q�65   5j      5l      �� 5m   
   $+��%Report: Häufigste Sicherheitslücken��6   $
��Left=�6>   $��	10,10,0,0q�6Y   $	��Top=�6l   $��	Agency FB)�6�   $��24(�6�   $��47��6�   $	��332��6�   $
��Bold"�6�   
�������6�   � 5o   
   $��0,62,0,0q�6   .+�5p      �� $��	Datei Typ��6   $
��Left=�6(   $��	19,90,0,0q�6C   $	��Top=�6V   �� 5q      -   5        ( ��Name$( DataTypeComboBox��5q   |   $
��Left=�6   $��
144,94,0,0q�62   $	��Top=�6F   $	��451��6^   ) �� SelectedIndex$) 0��6j   .@�5r      �� .��6   CSV6$   �� 5s      .��6   XLSX (Excel)6-   �� 5t      .��6   HTML6%   5u      �� 5v      $��Filter��6   $
��Left=�6%   $��
19,155,0,0q�6@   $	��Top=�6T   �� 5w      -   5        * ��Name$* checkBox��5w      $��Computername: ��6)   $
��Left=�6B   $��144,161,0,0q�6]   $	��Top=�6r   �� 5x      -   5        + ��Name$+ ComputerNameTextbox��5x      $
��Left=�63   $��23��6N   $��301,158,0,0q�6Z   , �� TextWrapping$
, Wrap=�6o   #��[ 6�   $	��Top=�6�   $	��294��6�   {�6�   �� *	IsChecked+- �� ElementName$- checkBox���� 5y      -   5        $��Report erzeugen��5y      $
��Left=�6/   $��457,216,0,0q�6J   $	��Top=�6_   $	��138��6w   $��38��6�   
�������#���   6�   6�   �� 5z      $
��Left=�6   $��WrapWithOverflow=�63   $��	19,10,0,0q�6S   $	��Top=�6f   ���	#FF2FB3FF. �� Padding$. 8,8,9,8q�6�   $	��576��6�   $
��Bold"�6�   
���   ��� 6�   $f�`Beschreibung: Dieser Report erstellt Ihnen ein Übersicht. Welche Sicherheitslücken bei Ihnen i��6�   �� 6E  $�n Ihrer DSM Umgebung��6I  �� 6g  $9�3 oder auf einem einzelnen Computer, am häufigsten ��6k  �� 6�  $�v��6�  �� 6�  $�ertreten sind.��6�  6�  5|      5}      5~      