   M S B A M L   `   `   `  ����P  LProjektManagementTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProjektManagementTool.ViewsProjektManagementTool.Views  5   	   1    +ProjektManagementTool.Views.PhasenDatenView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProjektManagementTool.Views     5   	     � Title$  Phase bearbeiten��5   	   $	��427��6"   $	��635��6/   .��5	      � .+�5
   
   �� $��Projekt��6   $
��Left=�6$   $	��311��6?   $��	26,10,0,0q�6L   $	��Top=�6_   $	��275��6w   .��5      �� .+�5      �� $
��Name��6   $��0,0,0,0q�6'   �� 5       �� 
IsReadOnly$
 True��60   $��5,0,0,0q�6C   $	��200��6T   $
��Left=�6`   ��6   �� *Name+6`   �� 5      $��Status��6   $��0,0,0,0q�6)   �� 5      $��5,0,0,0q�63   $	��200��6D   $
��Left=�6P   ��6   �� *Status+6P   �� 5      $��Fortschritt��6   $��0,0,0,0q�6.   �� 5      $��5,0,0,0q�68   $	��200��6I   $
��Left=�6U   ��6   �� *Fortschritt+6U   �� 5      $��Start Datum (Geplant)��6   $��0,0,0,0q�68   (  "System.Windows.Controls.DatePicker  5         SelectedDate 6   �� *StartDatumG+6D   �� 5      $��End Datum (Geplant)��6   $��0,0,0,0q�66     5       6   �� *	EndDatumG+6B   �� 5      $��Review Datum (Geplant)��6   $��0,0,0,0q�69     5       6   �� *ReviewDatumG+6E   5      5      �� 5   
   ${�False��6   $��Informationen��6%   $
��Left=�6<   $	��249��6W   $��
329,10,0,0q�6d   $	��Top=�6x   $	��275��6�   .��5      �� .+�5      �� $��
Startdatum��6   $��0,0,0,0q�6-   �� 5      $��5,0,0,0q�67   $	��200��6H   $
��Left=�6T   ��6   �� *
StartDatum+6T   �� 5      $��Enddatum��6   $��0,0,0,0q�6+   �� 5      $��5,0,0,0q�65   $	��200��6F   $
��Left=�6R   ��6   �� *
EndDatum+6R   �� 5       $��Freigabedatum��6   $��0,0,0,0q�60   �� 5!      $��5,0,0,0q�6:   $	��200��6K   $
��Left=�6W   ��6   �� *FreigabeDatum+6W   �� 5"      $��Visum��6   $��0,0,0,0q�6(   �� 5#      $��5,0,0,0q�62   $	��200��6C   $
��Left=�6O   ��6   �� *Visum+6O   �� 5$      $��Reviewdatum��6   $��0,0,0,0q�6.   �� 5%      $��5,0,0,0q�68   $	��200��6I   $
��Left=�6U   ��6   �� *ReviewDatum+6U   5&      5'      �� 5(   
   $��Aktionen��6   $
��Left=�6%   $	��122��6@   $��329,260,0,0q�6M   $	��Top=�6b   $	��275��6z   .��5)      �� .+�5*      �  � ColumnDefinitions 5+      �� 5,      $��* �6+   �� 5-      $��* �6+    � RowDefinitions 5/      �� 50      $��* �6(   �� 51      $��* �6(   �� 52      $��* �6(   .+�54      �� $��0��6<   $��0��6I   $	��100��6Y   $
��Left=�6e   $	��0,5q�6�   ��6   �� *
CMDStarten+.��6�   	Starten6�   �� 55      $��0��6<   $��1��6I   $	��100��6Y   $
��Left=�6e   $	��0,5q�6�   ��6   �� *
CMDBeenden+.��6�   	Beenden6�   �� 56      $��1��6>   $��0��6K   $	��100��6[   $
��Left=�6g   $	��0,5q�6�   ��6   �� *CMDSpeichern+.��6�   	Speichern6�   �� 57      $��1��6>   $��1��6K   $	��100��6[   $
��Left=�6g   $	��0,5q�6�   ��6   �� *CMDFreigeben+.��6�   	Freigeben6�   �� 58      $��2��6;   $��0��6H   $	��120��6X   $
��Left=�6d   $	��0,5q�6   ��6   �� *	CMDReview+.��6�   Review durchgeführt6�   59      5:      5;      5<      5=      