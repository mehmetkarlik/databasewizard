   M S B A M L   `   `   `  ����I  EDatabaseWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DatabaseWizard.User_ControlsDatabaseWizard.User_Controls  5      -    'DatabaseWizard.User_Controls.FileFormat   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:DatabaseWizard.User_Controls     5      -   5        .��5	      � $��481.935��6   $��Bottom=�6   $��0,0,5.603,0q�67   .+�5
   
   �� -   5          ��Name$  label��5
      $��File Format��6   $
��Left=�65   $��10,20.795,0,0q�6P   $	��Top=�6g   ���	#FF198EC2$��	ExtraBold"�6�   $��21(�6�   $��41.589��6�   $��Yu Gothic UI Semibold)�6�   �� 5   
   ���	#FFF4F4F5$
��Left=�6%   $��414.551��6@   $"��-16.192,72.781,-59.37,-5.397q�6Q   
�������$	��Top=�6�   $��711.959��6�   �� 5   
   -   5         ��Name$ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��533.734,433.361,0,0q�6P   $	��Top=�6m   $��75��6�   6�   �� 5   
   -   5        $ 	btnFinish��5   #   {�.  $��Finish��66   $
��Left=�6G   $��453.734,433.361,0,0q�6b   $	��Top=�6   $��75��6�   �� 5   
   -   5        $ btnNext��5   "   {�. $�� Next >��63   $
��Left=�6H   $��373.734,433.361,0,0q�6c   $	��Top=�6�   $��75��6�   6�   �� 5   
   -   5        $ btnBack��5      $��< Back ��6"   $
��Left=�67   $��293.734,433.361,0,0q�6R   $	��Top=�6o   $��75��6�   6�   �� 5   
   -   5        $  label1��5      $
��Left=�6    $��	10,81,0,0q�6;   $	��Top=�6N   $��50��6f   $	��599��6r   .��5      �� $��WrapWithOverflow=�6   $����The wizard has analyzed your file and selected the most likely encoding and field seperation method below. Please review these settings and adjust them if necessary.��68   6�   5      �� 5   
   -   5        $  label2_Copy��5      $��Field Seperation:��6%   $
��Left=�6A   $��
33,128,0,0q�6\   $	��Top=�6p   �� 5   
   -	   5         ��Name$ cmbSeperator��5      #��["�6)    �� SelectedIndex$ 0��6f   $
��Left=�6x   $��130,130,0,0q�6�   $	��Top=�6�   $	��190��6�   $��25��6�   .@�5      �� .��6   Tab6    �� 5      .��6   Comma6"   �� 5      .��6   	Semicolon6&   �� 5      .��6   Custom Seperator Character67   5      �� 5   
   -
   5        $  lblSeperator��5   "   {�.  $��Custom Seperator:��68   $
��Left=�6T   $��329,128,0,0q�6o   $	��Top=�6�   �� 5   
   -   5         ��Name$ txtSeperator��5   $   {�.   �� TextAlignment$ Center=�6:    �� 	MaxLength$ 1��6Q   $
��Left=�6_   $��23��6z   $��442,131,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $�� ��6�   $	��Top=�6�   $��42��6�   6�   �� 5   
   $��10,160,10.397,64.935q�6   .@�5      �� $��Fields��6   $��	-2,0,-2,2q�6&   .��5      ��  �� Orientation$ Vertical=�6   .+�5      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 dataGrid��5      $	��203��61   6>   �� 5       $ 
Horizontal=�6!   $��18��6:   .+�5!      �� $��18��6!   ��5"      �� 5#   "   
 �� ImageSource$$
 ../Images/information-icon.png��6-   6\   5%      �� 5&      $��5,0,0,0q�6    $S��MWarning data preview may not contain all rows. Up to 1000 rows will be shown.��62   6�   5'      5(      5)      5*      5,      5.      