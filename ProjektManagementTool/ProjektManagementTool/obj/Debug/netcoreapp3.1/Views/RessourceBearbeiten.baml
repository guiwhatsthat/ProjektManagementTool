   M S B A M L   `   `   `  ����P  LProjektManagementTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProjektManagementTool.ViewsProjektManagementTool.Views  5   	   5    /ProjektManagementTool.Views.RessourceBearbeiten   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProjektManagementTool.Views     5   	     � Title$  Ressource bearbeiten��5   	   $	��439��6&   $	��506��63   .��5	      � .+�5
   
   �� $	��225��6   $	��380��6   $��
26,13,0,20q�6,   $
��Left=�6@   .��5      �� .+�5      �� $
��Name��6   $��0,0,0,0q�6'   $	��205��68   $
��Left=�6D   �� 5      $��5,0,0,0q�61   $	��205��6B   $
��Left=�6N   ��6   �� *Name+6N   �� 5      $��	Kostenart��6   $��0,0,0,0q�6,   $	��205��6=   $
��Left=�6I   �� 5      $	��205��6W   $
��Left=�6c   ��6   �� *Arten+ �� SelectedValue 69   �� *Art+6�   �� 5      $+��%Art oder Funktion (je nach Kostenart)��6   $��0,0,0,0q�6H   $	��205��6Y   $
��Left=�6e   �� 5      $��5,0,0,0q�68   $	��205��6I   $
��Left=�6U   ��6   �� *ArtFunktion+6U   �� 5      $��Kosten Geplant��6   $��0,0,0,0q�61   $	��205��6B   $
��Left=�6N   �� 5      $��5,0,0,0q�64   $	��205��6E   $
��Left=�6Q   ��6   �� *	KostenG+6Q   �� 5      $��Kosten��6   $��0,0,0,0q�6)   $	��205��6:   $
��Left=�6F   �� 5      $��5,0,0,0q�63   $	��205��6D   $
��Left=�6P   ��6   �� *Kosten+6P   �� 5      $��
Startdatum��6   $��0,0,0,0q�6-   $	��205��6>   $
��Left=�6J   (  "System.Windows.Controls.DatePicker  5      $��5,0,0,0q�6A   $	��205��6R   $
��Left=�6^      SelectedDate 6   �� *
StartDatum+6{   �� 5      $��Enddatum��6   $��0,0,0,0q�6+   $	��205��6<   $
��Left=�6H     5      $��5,0,0,0q�6?   $	��205��6P   $
��Left=�6\    6   �� *
EndDatum+6y   � 5      $	��205��6   $��5,5,0,0q�6$   $
��Left=�65    � ColumnDefinitions 5      �� 5      $��* �6+   �� 5      $��* �6+    � RowDefinitions 5      �� 5       $��* �6(   .+�5"      �� $��0��6>   $��5,5,5,5q�6N   ��6   �� *CMDSpeichern+.��6`   	Speichern6k   �� 5#      $��1��6=   ��6   �� *CMDLoeschen+.��6M   
Löschen6V   5$      5%      5&      �� 5'   
   $��Verfügbare Resourcen��6   $
��Left=�61   $	��385��6L   $��
271,13,0,0q�6Y   $	��Top=�6m   $	��201��6�   .��5(      �� .+�5)      &   System.Windows.Controls.DataGrid  $	��160��6h   ��6   �� *
ResourcenP+ �� SelectedIndex 6>   �� *ResourcenPIndex+5+      �� 5,      ��6   �� *CMDWaehlenP+.��69   	Wählen6A     5-      $	��160��6h   ��6   �� *
ResourcenE+ 6>   �� *ResourcenEIndex+5/      �� 50      ��6   �� *CMDWaehlenE+.��69   	Wählen6A   51      52      54      55      