namespace HardkorowyKodsuThickClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_Load = new Button();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            SuspendLayout();

            // 
            // btn_Load
            // 
            btn_Load.Location = new Point(12, 12);
            btn_Load.Name = "btn_Load";
            btn_Load.Size = new Size(200, 30);
            btn_Load.TabIndex = 0;
            btn_Load.Text = "Load Tables and Views";
            btn_Load.UseVisualStyleBackColor = true;
            btn_Load.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // 
            // listBox1
            // 
            listBox1.DisplayMember = "Name";
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 50);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(400, 400);
            listBox1.TabIndex = 1;
            listBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;

            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.Location = new Point(430, 50);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(400, 400);
            listBox2.TabIndex = 2;
            listBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(850, 500);
            Controls.Add(btn_Load);
            Controls.Add(listBox1);
            Controls.Add(listBox2);
            Name = "MainForm";
            Text = "Database Schema Viewer";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Load;
        private ListBox listBox1;
        private ListBox listBox2;
    }
}
