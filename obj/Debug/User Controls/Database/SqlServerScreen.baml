   M S B A M L   `   `   `  ����I  EDatabaseWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:DatabaseWizard.User_Controls.Database%DatabaseWizard.User_Controls.Database  5      ;    5DatabaseWizard.User_Controls.Database.SqlServerScreen   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Elocal3clr-namespace:DatabaseWizard.User_Controls.Database     5      -   5        .��5	      � $��481.935��6   $��Bottom=�6   $��0,0,5.603,0q�67   .+�5
   
   �� -   5          ��Name$  label��5
      $#��Microsoft SQL Server Database��6   $
��Left=�6G   $��10,20.795,0,0q�6b   $	��Top=�6y   ���	#FF198EC2$��	ExtraBold"�6�   $��21(�6�   $��41.589��6�   $��Yu Gothic UI Semibold)�6�   �� 5   
   ���	#FFF4F4F5$
��Left=�6%   $��414.551��6@   $"��-16.192,72.781,-59.37,-5.397q�6Q   
�������$	��Top=�6�   $��711.959��6�   �� 5   
   -   5         ��Name$ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��533.734,433.361,0,0q�6P   $	��Top=�6m   $��75��6�   6�   �� 5   
   -   5        $ 	btnFinish��5   #   {�.  $��Finish��66   $
��Left=�6G   $��453.734,433.361,0,0q�6b   $	��Top=�6   $��75��6�   �� 5   
   -   5        $ btnNext��5   "   {�. $�� Next >��63   $
��Left=�6H   $��373.734,433.361,0,0q�6c   $	��Top=�6�   $��75��6�   $��7��6�   �� 5   
   -   5        $ btnBack��5      $��< Back ��6"   $
��Left=�67   $��293.734,433.361,0,0q�6R   $	��Top=�6o   $��75��6�   6�   �� 5   
   -   5        $  label1��5      $
��Left=�6    $��100,86,-62.603,0q�6;   $	��Top=�6U   $��27��6m   $	��599��6y   .��5      �� $��WrapWithOverflow=�6   $"��Enter Connection information��68   5      �� 5   
   -   5        $  label2��5      $��Server:��6    $
��Left=�62   $��136,110,0,0q�6M   $	��Top=�6b   6|   �� 5   
   -	   5        $  label1_Copy��5      $
��Left=�6%   $��100,172,-62.603,0q�6@   $	��Top=�6[   $��27��6s   $	��599��6   .��5      �� $��WrapWithOverflow=�6   $$��Choose an authentication mode:��68   5      �� 5   
   -
   5        $  label2_Copy1��5      $��
User Name:��6&   $
��Left=�6;   $��136,246,0,0q�6V   $	��Top=�6k   �� 5   
   -   5        $  label2_Copy2��5      $��	Password:��6&   $
��Left=�6:   $��136,277,0,0q�6U   $	��Top=�6j    J� RenderTransformOrigin$ 3.645,1.058&�6�   �� 5   
   -   5         ��Name$ txtUserName��5      $
��Left=�6'   $��23��6B   $��216,249,0,0q�6N    �� TextWrapping$
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��313��6�   $��4��6�   � 5   
   $
��Left=�6   $��45��6*   $��119,199,0,0q�66   $	��Top=�6K   $	��420��6c   .+�5      	� -   5         	�Name$ 	rdbWinAut��5      $6��0Use current credentials (Windows Authentication)��6-   $��
0,0,139,30q�6h   $��2��6|   6�   	� 5      -   5        $ 
rdbDataAut��5       d� 	IsChecked$
 TrueY�6.   $>��8Specify user name and password (Database Authentication)��6?   $��
0,20,87,10q�6�   $��3��6�   5      �� 5   
   -   5        $  label1_Copy1��5      $
��Left=�6&   $��100,320,-62.603,0q�6A   $	��Top=�6\   $��28��6t   $	��599��6�   .��5       �� $��WrapWithOverflow=�6   $&�� Select or enter a database name:��68   5"      �� 5#   
   -   5        $  label2_Copy3��5#      $��	Database:��6&   $
��Left=�6:   $��136,348,0,0q�6U   $	��Top=�6j   �� 5$   
   -   5        $ txtDatabase��5$      $
��Left=�6'   $��22��6B   $��216,352,0,0q�6N   $
 Wrap=�6c   $�� ��6w   $	��Top=�6   $	��313��6�   $��6��6�   �� 5%   
   -   5        $ 	txtServer��5%      $
��Left=�6%   $��23��6@   $��216,113,0,0q�6L   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��313��6�   $��0��6�   G� 5&   
   -   5         G�Name$ txtPassword��5&      $
��Left=�6+   $��216,280,0,0q�6F   $	��Top=�6[   $��23��6s   $	��313��6   $��5��6�   5)      5+      