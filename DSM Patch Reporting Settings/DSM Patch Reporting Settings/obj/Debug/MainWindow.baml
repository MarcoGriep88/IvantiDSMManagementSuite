   M S B A M L   `   `   `  ����W  SDSM Patch Reporting Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DSM_Patch_Reporting_SettingsDSM_Patch_Reporting_Settings  5   	   -    'DSM_Patch_Reporting_Settings.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:DSM_Patch_Reporting_Settings     5   	     � Title$'  !DSM Patch Reporting Einstellungen��5   	   $��494.527��63   $	��800��6D   .��5	      � ��5
   
   �� 5       �� ImageSource$) #resx/gray-abstract-wallpaper-17.jpg��6    �� Opacity$	 0.3Q�6K   .+�5   
   o� $��27��6   $	��Top=�6   $��1,0,0,0q�63   ���	#FF4E5D6C.@�5      m� $��Einstellungen��6   $��27��6.   $	��180��6:   
�������$
��Bold"�6Y   .@�5      m� -   5        $��Verbindung testen��5      $
��Left=�66   $��24��6Q   $	��200��6]   
���   �6�   m� 5      -   5        $��	Speichern��5      $
��Left=�6.   $��24��6I   $	��200��6U   
���   �6�   m� 5      -   5        $��Beenden��5      $
��Left=�6,   $��24��6G   $	��200��6S   
���   �6r   5      5      �� 5   
   $��52��6   $��
0,27,0.2,0q�6    
���   �$	��Top=�6C   $��0��6[    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF343D46��6#    � Offset$ 0Q�65   � 5      $ 	#FF4E5D6C��6#   $ 1Q�65   5      5      �� 5   
   $'��!DSM Patch Reporting Einstellungen��6   $
��Left=�6<   $��	10,32,0,0q�6W   $	��Top=�6j   $��	Agency FB)�6�   $��24(�6�   $��48��6�   $	��728��6�   $
��Bold"�6�   
�������6�   �� 5    
   $��23��6   $��0,0,0.2,-0.4q�6    $��Bottom=�66   � 5!   
   $��10,100,10,30q�6   .+�5"      �� $��BLS Adresse und Port:��6   $��
0, 0, 0, 0q�64   �� 5#      -   5         ��Name$ txtBLS��5#      $
��Left=�6&   $��23��6A   	 �� TextWrapping$
	 Wrap=�6M   $��localhost:8080��6a   $	��Top=�6w   $	��280��6�   $��	200,0,0,0q�6�   �� 5%      $��BLS Benutzername��6   $��0,40,0,0q�6/   �� 5&      -   5        $ 
txtBLSUser��5&      $
��Left=�6*   $��23��6E   $
	 Wrap=�6Q   $��domain\user��6e   $	��Top=�6x   $	��280��6�   $��
200,40,0,0q�6�   �� 5(      $��BLS Passwort:��6   $��0,80,0,0q�6,   G� 5)      -   5        
 G�Name$
 txtBLSPassword��5)      $
��Left=�62   $��23��6M   $	��Top=�6Y   $	��280��6q   $��
200,80,0,0q�6}   �� 5+      $��Kontext:��6   $��	0,120,0,0q�6'   �� 5,      -   5        $ 
txtContext��5,      $
��Left=�6*   $��23��6E   $
	 Wrap=�6Q   $/��)emdb:\rootDSE\Managed Users & Computers\*��6e   $	��Top=�6�   $	��280��6�   $��200,120,0,0q�6�   �� 5.      $��API URL:��6   $��	0,160,0,0q�6'   �� 5/      -   5        $ txtAPI��5/      $
��Left=�6&   $��23��6A   $
	 Wrap=�6M   $��http://localhost:8095/api��6a   $	��Top=�6�   $	��280��6�   $��200,160,0,0q�6�   �� 51      $��Benutzername:��6   $��	0,200,0,0q�6,   �� 52      -	   5        $ txtUser��52      $
��Left=�6'   $��23��6B   $
	 Wrap=�6N   $��
ReportUser��6b   $	��Top=�6t   $	��280��6�   $��200,200,0,0q�6�   �� 54      $��	Passwort:��6   $��	0,240,0,0q�6(   G� 55      -
   5        $
 txtPassword��55      $
��Left=�6/   $��23��6J   $	��Top=�6V   $	��280��6n   $��200,240,0,0q�6z   �� 57      $��	Standort:��6   $��	0,280,0,0q�6(   �� 58      -   5        $ txtLocation��58      $
��Left=�6+   $��23��6F   $
	 Wrap=�6R   $��DefaultLocation��6f   $	��Top=�6}   $	��280��6�   $��200,280,0,0q�6�   59      5:      5;      