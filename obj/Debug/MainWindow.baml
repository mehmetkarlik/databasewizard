   M S B A M L   `   `   `  ����I  EDatabaseWizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:DatabaseWizardDatabaseWizard  5   	   K*clr-namespace:DatabaseWizard.User_ControlsDatabaseWizard.User_Controls  5   	   ]3clr-namespace:DatabaseWizard.User_Controls.Database%DatabaseWizard.User_Controls.Database  5   	       DatabaseWizard.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:DatabaseWizard     5   	   @	uccontrol*clr-namespace:DatabaseWizard.User_Controls     5   	   J
ucDatabase3clr-namespace:DatabaseWizard.User_Controls.Database     5   	     � Title$  Database Setup Wizard��5
   	   $��510.935��6'   $��642.397��68    � Icon$ Database.ico��6H    � 
ResizeMode$ NoResize=�6\    � WindowStyle$ ThreeDBorderWindow=�6r   .��5      � $��481.935��6   $��Bottom=�6   .+�5   
   .   (DatabaseWizard.User_Controls.FirstScreen  -   5          Name$ firstScreen��5       67   -   'DatabaseWizard.User_Controls.SelectFile  5   
   -   5          Name$ 
selectFile��5      $y�Hidden=�63   6I   -   'DatabaseWizard.User_Controls.FileFormat  5   
   -   5          Name$ 
fileFormat��5      $y�Hidden=�63   6I   /   )DatabaseWizard.User_Controls.ChooseFields  5   
   -   5          Name$ chooseFields��5   !   $y�Hidden=�67   6M   /   )DatabaseWizard.User_Controls.DataSettings  5   
   -   5          Name$ dataSettings��5   !   $y�Hidden=�68   6N   /   )DatabaseWizard.User_Controls.ResultScreen  5   
   -   5        	  Name$	 resultScreen��5   !   $y�Hidden=�67   6N   7   1DatabaseWizard.User_Controls.Database.MySQLScreen  5   
   -   5        
  Name$
 mysqlScreen��5   !   $y�Hidden=�66   6L   ;   5DatabaseWizard.User_Controls.Database.SqlServerScreen  5   
   -   5          Name$ sqlserverScreen��5   %   $y�Hidden=�6>   6T   8	   2DatabaseWizard.User_Controls.Database.OracleScreen	  5   
   -	   5         	 Name$ oracleScreen��5   "   $y�Hidden=�68   6N   <
   6DatabaseWizard.User_Controls.Database.PostgreSqlScreen
  5   
   -
   5         
 Name$ postgresqlScreen��5   &   $y�Hidden=�6@   6V   :   4DatabaseWizard.User_Controls.Database.FirebirdScreen  5   
   -   5          Name$ firebirdScreen��5   $   $y�Hidden=�6<   6R   6   0DatabaseWizard.User_Controls.Database.WSDLScreen  5   
   -   5          Name$ 
wsdlScreen��5       $y�Hidden=�64   6J   5      5      