   M S B A M L   `   `   `  ����P  LProjektManagementTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProjektManagementTool.ViewsProjektManagementTool.Views  5   	   ;    5ProjektManagementTool.Views.MeilensteinBearbeitenView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProjektManagementTool.Views     5   	     � Title$  Meilenstein��5   	   $	��382��6   $	��635��6*   .��5	      � .+�5
   
   �� $��Meilenstein��6   $
��Left=�6(   $	��340��6C   $��	26,10,0,0q�6P   $	��Top=�6c   $	��275��6{   .��5      �� .+�5      �� $
��Name��6   $��0,0,0,0q�6'   �� 5      $��5,0,0,0q�60   $	��200��6A   $
��Left=�6M   ��6   �� *Name+6M   �� 5      $��End Datum (Geplant)��6   $��0,0,0,0q�66   (  "System.Windows.Controls.DatePicker  5         SelectedDate 6   �� *DatumG+6?   �� 5      $��End Datum (Geplant)��6   $��0,0,0,0q�66   &   System.Windows.Controls.DataGrid  5      $	��175��6Z   ��6   �� *Phasen+ �� SelectedIndex 6:   �� *Index+6i   �� 5      ��6   �� *
CMDWaehlen+.��68   	Wählen6@   5      5      �� 5   
   ${�False��6   $��Informationen��6%   $
��Left=�6<   $	��161��6W   $��
329,10,0,0q�6d   $	��Top=�6x   $	��275��6�   .��5      �� .+�5      �� $��Datum��6   $��0,0,0,0q�6(   �� 5      $��5,0,0,0q�62   $	��200��6C   $
��Left=�6O   ��6   �� *Datum+6O   �� 5      $��Phase��6   $��0,0,0,0q�6(   �� 5      $��5,0,0,0q�66   $	��200��6G   $
��Left=�6S   ��6   �� *	PhaseName+6S   �� 5      $��Projekt��6   $��0,0,0,0q�6*   �� 5      $��5,0,0,0q�68   $	��200��6I   $
��Left=�6U   ��6   �� *ProjektName+6U   5      5      �� 5   
   $��Aktionen��6   $
��Left=�6%   $��98��6@   $��329,176,0,0q�6L   $	��Top=�6a   $	��275��6y   .��5       �� .+�5!      �� $	��100��6=   $
��Left=�6I   ��6   �� *CMDAbschliessen+.��6d   Abschliessen6r   �� 5"      $	��100��6:   $
��Left=�6F   $	��0,5q�6a   ��6   �� *CMDSpeichern+.��6n   	Speichern6y   �� 5#      $	��100��69   $
��Left=�6E   ��6   �� *CMDLoeschen+.��6`   
Löschen6i   5$      5%      5&      5'      