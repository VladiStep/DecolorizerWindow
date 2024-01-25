namespace DecolorizerWindow
{
    partial class SettingsForm
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
            components = new System.ComponentModel.Container();
            resolutionLabel = new Label();
            widthTextBox = new TextBox();
            xLabel = new Label();
            heightTextBox = new TextBox();
            saveButton = new Button();
            resolutionPanel = new Panel();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // resolutionLabel
            // 
            resolutionLabel.AutoSize = true;
            resolutionLabel.Font = new Font("Segoe UI", 11F);
            resolutionLabel.Location = new Point(38, 29);
            resolutionLabel.Name = "resolutionLabel";
            resolutionLabel.Size = new Size(123, 20);
            resolutionLabel.TabIndex = 0;
            resolutionLabel.Text = "Размер области:";
            // 
            // widthTextBox
            // 
            widthTextBox.Font = new Font("Segoe UI", 11F);
            widthTextBox.Location = new Point(164, 26);
            widthTextBox.MaxLength = 4;
            widthTextBox.Name = "widthTextBox";
            widthTextBox.Size = new Size(50, 27);
            widthTextBox.TabIndex = 1;
            widthTextBox.Text = "576";
            widthTextBox.Validating += widthTextBox_Validating;
            widthTextBox.Validated += textBox_Validated;
            // 
            // xLabel
            // 
            xLabel.AutoSize = true;
            xLabel.Font = new Font("Segoe UI", 11F);
            xLabel.Location = new Point(216, 29);
            xLabel.Name = "xLabel";
            xLabel.Size = new Size(16, 20);
            xLabel.TabIndex = 2;
            xLabel.Text = "x";
            // 
            // heightTextBox
            // 
            heightTextBox.Font = new Font("Segoe UI", 11F);
            heightTextBox.Location = new Point(234, 26);
            heightTextBox.MaxLength = 4;
            heightTextBox.Name = "heightTextBox";
            heightTextBox.Size = new Size(50, 27);
            heightTextBox.TabIndex = 3;
            heightTextBox.Text = "320";
            heightTextBox.Validating += heightTextBox_Validating;
            heightTextBox.Validated += textBox_Validated;
            // 
            // saveButton
            // 
            saveButton.Font = new Font("Segoe UI", 11F);
            saveButton.Location = new Point(110, 84);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(107, 32);
            saveButton.TabIndex = 4;
            saveButton.Text = "Сохранить";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // resolutionPanel
            // 
            resolutionPanel.BorderStyle = BorderStyle.FixedSingle;
            resolutionPanel.Location = new Point(10, 14);
            resolutionPanel.Name = "resolutionPanel";
            resolutionPanel.Size = new Size(306, 50);
            resolutionPanel.TabIndex = 5;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(327, 128);
            Controls.Add(saveButton);
            Controls.Add(heightTextBox);
            Controls.Add(xLabel);
            Controls.Add(widthTextBox);
            Controls.Add(resolutionLabel);
            Controls.Add(resolutionPanel);
            Icon = Properties.Resources.mainIcon;
            MaximizeBox = false;
            Name = "SettingsForm";
            Text = "Настройки обесцветывателя";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label resolutionLabel;
        private TextBox widthTextBox;
        private Label xLabel;
        private TextBox heightTextBox;
        private Button saveButton;
        private Panel resolutionPanel;
        private ErrorProvider errorProvider;
    }
}