namespace ProxyService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProxyServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ProxyServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ProxyServiceProcessInstaller
            // 
            this.ProxyServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ProxyServiceProcessInstaller.Password = null;
            this.ProxyServiceProcessInstaller.Username = null;
            // 
            // ProxyServiceInstaller
            // 
            this.ProxyServiceInstaller.ServiceName = "ProxyService";
            this.ProxyServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ProxyServiceProcessInstaller,
            this.ProxyServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ProxyServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ProxyServiceInstaller;
    }
}