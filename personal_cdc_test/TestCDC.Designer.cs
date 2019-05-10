namespace personal_cdc_test
{

    partial class TestCDC
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMessage = new System.Windows.Forms.Label();
            this.mainLayoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.connectToServer = new System.Windows.Forms.Button();
            this.testConnection = new System.Windows.Forms.Button();
            this.mainLayoutTable.SuspendLayout();
            this.buttonTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMessage
            // 
            this.mainMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMessage.Location = new System.Drawing.Point(264, 0);
            this.mainMessage.Name = "mainMessage";
            this.mainMessage.Size = new System.Drawing.Size(255, 90);
            this.mainMessage.TabIndex = 1;
            this.mainMessage.Text = "MainMessage";
            this.mainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mainMessage.Click += new System.EventHandler(this.mainMessage_Click);
            // 
            // mainLayoutTable
            // 
            this.mainLayoutTable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mainLayoutTable.ColumnCount = 3;
            this.mainLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainLayoutTable.Controls.Add(this.mainMessage, 1, 0);
            this.mainLayoutTable.Controls.Add(this.buttonTableLayout, 1, 1);
            this.mainLayoutTable.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutTable.Name = "mainLayoutTable";
            this.mainLayoutTable.RowCount = 2;
            this.mainLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainLayoutTable.Size = new System.Drawing.Size(784, 561);
            this.mainLayoutTable.TabIndex = 2;
            // 
            // buttonTableLayout
            // 
            this.buttonTableLayout.ColumnCount = 1;
            this.buttonTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonTableLayout.Controls.Add(this.connectToServer, 0, 0);
            this.buttonTableLayout.Controls.Add(this.testConnection, 0, 1);
            this.buttonTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTableLayout.Location = new System.Drawing.Point(264, 93);
            this.buttonTableLayout.Name = "buttonTableLayout";
            this.buttonTableLayout.RowCount = 5;
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.Size = new System.Drawing.Size(255, 465);
            this.buttonTableLayout.TabIndex = 2;
            // 
            // connectToServer
            // 
            this.connectToServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectToServer.Location = new System.Drawing.Point(35, 35);
            this.connectToServer.Margin = new System.Windows.Forms.Padding(35);
            this.connectToServer.Name = "connectToServer";
            this.connectToServer.Size = new System.Drawing.Size(185, 23);
            this.connectToServer.TabIndex = 0;
            this.connectToServer.Text = "Connect to server";
            this.connectToServer.UseVisualStyleBackColor = true;
            this.connectToServer.Click += new System.EventHandler(this.ConnectToServer_Click);
            // 
            // testConnection
            // 
            this.testConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testConnection.Location = new System.Drawing.Point(35, 128);
            this.testConnection.Margin = new System.Windows.Forms.Padding(35);
            this.testConnection.Name = "testConnection";
            this.testConnection.Size = new System.Drawing.Size(185, 23);
            this.testConnection.TabIndex = 2;
            this.testConnection.Text = "Test Connection";
            this.testConnection.UseVisualStyleBackColor = true;
            this.testConnection.Click += new System.EventHandler(this.TestConnection_Click);
            // 
            // TestCDC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainLayoutTable);
            this.Name = "TestCDC";
            this.Text = "     ";
            this.mainLayoutTable.ResumeLayout(false);
            this.buttonTableLayout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label mainMessage;
        private System.Windows.Forms.TableLayoutPanel mainLayoutTable;
        private System.Windows.Forms.TableLayoutPanel buttonTableLayout;
        private System.Windows.Forms.Button connectToServer;
        private System.Windows.Forms.Button testConnection;
    }
}

