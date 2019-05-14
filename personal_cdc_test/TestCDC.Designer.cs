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
            this.addHDSButton = new System.Windows.Forms.Button();
            this.deleteHDSButton = new System.Windows.Forms.Button();
            this.connectToSQL = new System.Windows.Forms.Button();
            this.connectToSnow = new System.Windows.Forms.Button();
            this.updateHdsButton = new System.Windows.Forms.Button();
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
            this.buttonTableLayout.Controls.Add(this.deleteHDSButton, 0, 0);
            this.buttonTableLayout.Controls.Add(this.addHDSButton, 0, 2);
            this.buttonTableLayout.Controls.Add(this.connectToSQL, 0, 4);
            this.buttonTableLayout.Controls.Add(this.connectToSnow, 0, 3);
            this.buttonTableLayout.Controls.Add(this.updateHdsButton, 0, 1);
            this.buttonTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTableLayout.Location = new System.Drawing.Point(264, 93);
            this.buttonTableLayout.Name = "buttonTableLayout";
            this.buttonTableLayout.RowCount = 5;
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.buttonTableLayout.Size = new System.Drawing.Size(255, 465);
            this.buttonTableLayout.TabIndex = 2;
            this.buttonTableLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonTableLayout_Paint);
            // 
            // addHDSButton
            // 
            this.addHDSButton.Location = new System.Drawing.Point(35, 221);
            this.addHDSButton.Margin = new System.Windows.Forms.Padding(35);
            this.addHDSButton.Name = "addHDSButton";
            this.addHDSButton.Size = new System.Drawing.Size(185, 23);
            this.addHDSButton.TabIndex = 4;
            this.addHDSButton.Text = "Add to HDS";
            this.addHDSButton.UseVisualStyleBackColor = true;
            this.addHDSButton.Click += new System.EventHandler(this.AddHDSButton_Click);
            // 
            // deleteHDSButton
            // 
            this.deleteHDSButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteHDSButton.Location = new System.Drawing.Point(35, 35);
            this.deleteHDSButton.Margin = new System.Windows.Forms.Padding(35);
            this.deleteHDSButton.Name = "deleteHDSButton";
            this.deleteHDSButton.Size = new System.Drawing.Size(185, 23);
            this.deleteHDSButton.TabIndex = 3;
            this.deleteHDSButton.Text = "Delete from HDS";
            this.deleteHDSButton.UseVisualStyleBackColor = true;
            this.deleteHDSButton.Click += new System.EventHandler(this.DeleteHDSButton_Click);
            // 
            // connectToSQL
            // 
            this.connectToSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectToSQL.Location = new System.Drawing.Point(35, 407);
            this.connectToSQL.Margin = new System.Windows.Forms.Padding(35);
            this.connectToSQL.Name = "connectToSQL";
            this.connectToSQL.Size = new System.Drawing.Size(185, 23);
            this.connectToSQL.TabIndex = 0;
            this.connectToSQL.Text = "Connect to SQL server";
            this.connectToSQL.UseVisualStyleBackColor = true;
            this.connectToSQL.Click += new System.EventHandler(this.connectToSQL_Click);
            // 
            // connectToSnow
            // 
            this.connectToSnow.Location = new System.Drawing.Point(35, 314);
            this.connectToSnow.Margin = new System.Windows.Forms.Padding(35);
            this.connectToSnow.Name = "connectToSnow";
            this.connectToSnow.Size = new System.Drawing.Size(185, 23);
            this.connectToSnow.TabIndex = 2;
            this.connectToSnow.Text = "Connect to Snowflake";
            this.connectToSnow.UseVisualStyleBackColor = true;
            this.connectToSnow.Click += new System.EventHandler(this.connectToSnow_Click);
            // 
            // updateHdsButton
            // 
            this.updateHdsButton.Location = new System.Drawing.Point(35, 128);
            this.updateHdsButton.Margin = new System.Windows.Forms.Padding(35);
            this.updateHdsButton.Name = "updateHdsButton";
            this.updateHdsButton.Size = new System.Drawing.Size(185, 23);
            this.updateHdsButton.TabIndex = 5;
            this.updateHdsButton.Text = "Update HDS";
            this.updateHdsButton.UseVisualStyleBackColor = true;
            this.updateHdsButton.Click += new System.EventHandler(this.UpdateHdsButton_Click);
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
        private System.Windows.Forms.Button connectToSQL;
        private System.Windows.Forms.Button connectToSnow;
        private System.Windows.Forms.Button deleteHDSButton;
        private System.Windows.Forms.Button addHDSButton;
        private System.Windows.Forms.Button updateHdsButton;
    }
}

