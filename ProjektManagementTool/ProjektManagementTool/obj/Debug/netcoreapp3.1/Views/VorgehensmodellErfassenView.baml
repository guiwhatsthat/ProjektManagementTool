   M S B A M L   `   `   `  ����P  LProjektManagementTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ProjektManagementTool.ViewsProjektManagementTool.Views  5   	   =    7ProjektManagementTool.Views.VorgehensmodellErfassenView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ProjektManagementTool.Views     5   	     � Title$  Vorgehensmodell erfassen��5   	   $	��319��6*   $	��319��67   .��5	      � .+�5
   
   �� $��
26,13,0,10q�6   .+�5      �� $
��Name��6   $��0,0,0,0q�6#   �� 5      $��5,0,0,0q�63   $	��200��6D   $
��Left=�6P   ��6   �� *
ModellName+6P   �� 5      $��Beschreibung��6   $��0,0,0,0q�6+   �� 5      $��5,0,0,0q�6;   $	��200��6L   $
��Left=�6X   ��6   �� *ModellBeschreibung+6X   �� 5      $��Phasen��6   $��5,5,0,0q�6'   $
��Left=�68   $	��200��6S   .��5      �� .+�5      �� $��80��6_   ��6   �� *Phasen+ �� SelectedIndex 6>   �� *Index+5      � 5       � ColumnDefinitions 5      �� 5      $��* �6/   �� 5      $��* �6/    � RowDefinitions 5      �� 5      $��* �6,   .+�5      �� .��6!   
Button 16+   �� 5      $��0��6!   $��5,5,5,5q�61   ��6C   �� *CMDPhaseErfassen+.��6h   
Erfassen6r   �� 5      $��1��6!   ��61   �� *CMDPhaseBearbeiten+.��6X   
Bearbeiten6d   5      5      5       � 5!      $��10,5,0,0q�6   $
��Left=�6%   $	��190��6@    5"      �� 5#      $��* �6'   �� 5$      $��* �6'    5&      �� 5'      $��* �6$   .+�5)      �� .��6   
Button 16#   �� 5*      $��0��6   $��5,5,5,5q�6)   ��6;   �� *CMDModellSpeichern+.��6b   	Speichern6m   �� 5+      $��1��6   .��6)   
Löschen62   5,      5-      5.      5/      