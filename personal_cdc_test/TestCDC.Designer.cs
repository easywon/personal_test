﻿namespace personal_cdc_test
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
            this.connectToSQL = new System.Windows.Forms.Button();
            this.connectToSnow = new System.Windows.Forms.Button();
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
            this.buttonTableLayout.Controls.Add(this.connectToSQL, 0, 0);
            this.buttonTableLayout.Controls.Add(this.connectToSnow, 0, 1);
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
            // connectToSQL
            // 
            this.connectToSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectToSQL.Location = new System.Drawing.Point(35, 35);
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
            this.connectToSnow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectToSnow.Location = new System.Drawing.Point(35, 128);
            this.connectToSnow.Margin = new System.Windows.Forms.Padding(35);
            this.connectToSnow.Name = "connectToSnow";
            this.connectToSnow.Size = new System.Drawing.Size(185, 23);
            this.connectToSnow.TabIndex = 2;
            this.connectToSnow.Text = "Connect to Snowflake";
            this.connectToSnow.UseVisualStyleBackColor = true;
            this.connectToSnow.Click += new System.EventHandler(this.connectToSnow_Click);
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
    }
}

