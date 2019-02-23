namespace HashTableLibTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnTestDuplicateKey = new System.Windows.Forms.Button();
            this.btnTestRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(12, 79);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(692, 331);
            this.txtMsg.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(12, 26);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(343, 26);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnTestDuplicateKey
            // 
            this.btnTestDuplicateKey.Location = new System.Drawing.Point(96, 26);
            this.btnTestDuplicateKey.Name = "btnTestDuplicateKey";
            this.btnTestDuplicateKey.Size = new System.Drawing.Size(120, 23);
            this.btnTestDuplicateKey.TabIndex = 3;
            this.btnTestDuplicateKey.Text = "测试 重复 Key";
            this.btnTestDuplicateKey.UseVisualStyleBackColor = true;
            this.btnTestDuplicateKey.Click += new System.EventHandler(this.btnTestDuplicateKey_Click);
            // 
            // btnTestRemove
            // 
            this.btnTestRemove.Location = new System.Drawing.Point(222, 26);
            this.btnTestRemove.Name = "btnTestRemove";
            this.btnTestRemove.Size = new System.Drawing.Size(115, 23);
            this.btnTestRemove.TabIndex = 4;
            this.btnTestRemove.Text = "测试 Remove";
            this.btnTestRemove.UseVisualStyleBackColor = true;
            this.btnTestRemove.Click += new System.EventHandler(this.btnTestRemove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 422);
            this.Controls.Add(this.btnTestRemove);
            this.Controls.Add(this.btnTestDuplicateKey);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtMsg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnTestDuplicateKey;
        private System.Windows.Forms.Button btnTestRemove;
    }
}

