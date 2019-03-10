namespace AssaultCubeHack
{
    partial class MainFrom
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.AimbotLabel = new System.Windows.Forms.Label();
            this.AimbotCheckBox = new System.Windows.Forms.CheckBox();
            this.ESPCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ESPLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Interval = 1;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // AimbotLabel
            // 
            this.AimbotLabel.AutoSize = true;
            this.AimbotLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AimbotLabel.ForeColor = System.Drawing.Color.Lime;
            this.AimbotLabel.Location = new System.Drawing.Point(12, 22);
            this.AimbotLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AimbotLabel.Name = "AimbotLabel";
            this.AimbotLabel.Size = new System.Drawing.Size(98, 23);
            this.AimbotLabel.TabIndex = 0;
            this.AimbotLabel.Text = "> Aimbot";
            // 
            // AimbotCheckBox
            // 
            this.AimbotCheckBox.AutoSize = true;
            this.AimbotCheckBox.Location = new System.Drawing.Point(117, 26);
            this.AimbotCheckBox.Name = "AimbotCheckBox";
            this.AimbotCheckBox.Size = new System.Drawing.Size(18, 17);
            this.AimbotCheckBox.TabIndex = 1;
            this.AimbotCheckBox.UseVisualStyleBackColor = true;
            this.AimbotCheckBox.CheckedChanged += new System.EventHandler(this.AimbotCheckBox_CheckedChanged);
            // 
            // ESPCheckBox
            // 
            this.ESPCheckBox.AutoSize = true;
            this.ESPCheckBox.Location = new System.Drawing.Point(117, 73);
            this.ESPCheckBox.Name = "ESPCheckBox";
            this.ESPCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ESPCheckBox.TabIndex = 3;
            this.ESPCheckBox.UseVisualStyleBackColor = true;
            this.ESPCheckBox.CheckedChanged += new System.EventHandler(this.ESPCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(235, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "A6ly";
            // 
            // ESPLabel
            // 
            this.ESPLabel.AutoSize = true;
            this.ESPLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.ESPLabel.ForeColor = System.Drawing.Color.Lime;
            this.ESPLabel.Location = new System.Drawing.Point(12, 69);
            this.ESPLabel.Name = "ESPLabel";
            this.ESPLabel.Size = new System.Drawing.Size(65, 23);
            this.ESPLabel.TabIndex = 5;
            this.ESPLabel.Text = "> ESP";
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(272, 113);
            this.Controls.Add(this.ESPLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ESPCheckBox);
            this.Controls.Add(this.AimbotCheckBox);
            this.Controls.Add(this.AimbotLabel);
            this.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrom";
            this.Text = "AssaultCube Hack!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Label AimbotLabel;
        private System.Windows.Forms.CheckBox AimbotCheckBox;
        private System.Windows.Forms.CheckBox ESPCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ESPLabel;
    }
}

