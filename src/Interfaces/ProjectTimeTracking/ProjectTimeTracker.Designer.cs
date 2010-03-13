namespace ProjectTimeTracking
{
    partial class ProjectTimeTracker
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
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.NewProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.NewProjectName = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ExistingProjectSelectBox = new System.Windows.Forms.DomainUpDown();
            this.ExistingProject = new System.Windows.Forms.Label();
            this.ExistingProjectSelectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeSpentLabel = new System.Windows.Forms.Label();
            this.TimeGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AverageTimeSpentLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CumulativeTimeSpentLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.inputGroupBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CompanyNameTextBox = new System.Windows.Forms.TextBox();
            this.clockGroupBox = new System.Windows.Forms.GroupBox();
            this.ClockTime = new System.Windows.Forms.Label();
            this.displayStopwatch = new System.Windows.Forms.Timer(this.components);
            this.TimeGroupBox.SuspendLayout();
            this.inputGroupBox.SuspendLayout();
            this.clockGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(41, 465);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(262, 88);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Visible = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(330, 465);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(255, 85);
            this.StopButton.TabIndex = 1;
            this.StopButton.Text = "STOP";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Visible = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // NewProjectNameTextBox
            // 
            this.NewProjectNameTextBox.Location = new System.Drawing.Point(22, 47);
            this.NewProjectNameTextBox.Name = "NewProjectNameTextBox";
            this.NewProjectNameTextBox.Size = new System.Drawing.Size(262, 20);
            this.NewProjectNameTextBox.TabIndex = 2;
            // 
            // NewProjectName
            // 
            this.NewProjectName.AutoSize = true;
            this.NewProjectName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewProjectName.Location = new System.Drawing.Point(81, 15);
            this.NewProjectName.Name = "NewProjectName";
            this.NewProjectName.Size = new System.Drawing.Size(138, 18);
            this.NewProjectName.TabIndex = 3;
            this.NewProjectName.Text = "New Project Name";
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(25, 195);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(258, 41);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ExistingProjectSelectBox
            // 
            this.ExistingProjectSelectBox.Location = new System.Drawing.Point(311, 47);
            this.ExistingProjectSelectBox.Name = "ExistingProjectSelectBox";
            this.ExistingProjectSelectBox.Size = new System.Drawing.Size(255, 20);
            this.ExistingProjectSelectBox.TabIndex = 5;
            // 
            // ExistingProject
            // 
            this.ExistingProject.AutoSize = true;
            this.ExistingProject.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExistingProject.Location = new System.Drawing.Point(378, 15);
            this.ExistingProject.Name = "ExistingProject";
            this.ExistingProject.Size = new System.Drawing.Size(116, 18);
            this.ExistingProject.TabIndex = 6;
            this.ExistingProject.Text = "Existing Project";
            // 
            // ExistingProjectSelectButton
            // 
            this.ExistingProjectSelectButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExistingProjectSelectButton.Location = new System.Drawing.Point(311, 195);
            this.ExistingProjectSelectButton.Name = "ExistingProjectSelectButton";
            this.ExistingProjectSelectButton.Size = new System.Drawing.Size(255, 41);
            this.ExistingProjectSelectButton.TabIndex = 7;
            this.ExistingProjectSelectButton.Text = "Select";
            this.ExistingProjectSelectButton.UseVisualStyleBackColor = true;
            this.ExistingProjectSelectButton.Click += new System.EventHandler(this.ExistingProjectSelectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Time Spent ";
            // 
            // TimeSpentLabel
            // 
            this.TimeSpentLabel.AutoSize = true;
            this.TimeSpentLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeSpentLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.TimeSpentLabel.Location = new System.Drawing.Point(285, 27);
            this.TimeSpentLabel.Name = "TimeSpentLabel";
            this.TimeSpentLabel.Size = new System.Drawing.Size(22, 24);
            this.TimeSpentLabel.TabIndex = 9;
            this.TimeSpentLabel.Text = "0";
            // 
            // TimeGroupBox
            // 
            this.TimeGroupBox.Controls.Add(this.label2);
            this.TimeGroupBox.Controls.Add(this.AverageTimeSpentLabel);
            this.TimeGroupBox.Controls.Add(this.label7);
            this.TimeGroupBox.Controls.Add(this.label4);
            this.TimeGroupBox.Controls.Add(this.CumulativeTimeSpentLabel);
            this.TimeGroupBox.Controls.Add(this.label6);
            this.TimeGroupBox.Controls.Add(this.label3);
            this.TimeGroupBox.Controls.Add(this.TimeSpentLabel);
            this.TimeGroupBox.Controls.Add(this.label1);
            this.TimeGroupBox.Location = new System.Drawing.Point(41, 304);
            this.TimeGroupBox.Name = "TimeGroupBox";
            this.TimeGroupBox.Size = new System.Drawing.Size(543, 155);
            this.TimeGroupBox.TabIndex = 10;
            this.TimeGroupBox.TabStop = false;
            this.TimeGroupBox.Text = "Time Group";
            this.TimeGroupBox.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(407, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "Hours";
            // 
            // AverageTimeSpentLabel
            // 
            this.AverageTimeSpentLabel.AutoSize = true;
            this.AverageTimeSpentLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageTimeSpentLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.AverageTimeSpentLabel.Location = new System.Drawing.Point(285, 104);
            this.AverageTimeSpentLabel.Name = "AverageTimeSpentLabel";
            this.AverageTimeSpentLabel.Size = new System.Drawing.Size(22, 24);
            this.AverageTimeSpentLabel.TabIndex = 15;
            this.AverageTimeSpentLabel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkRed;
            this.label7.Location = new System.Drawing.Point(6, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(211, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = " Average Time Spent ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(407, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "Hours";
            // 
            // CumulativeTimeSpentLabel
            // 
            this.CumulativeTimeSpentLabel.AutoSize = true;
            this.CumulativeTimeSpentLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CumulativeTimeSpentLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.CumulativeTimeSpentLabel.Location = new System.Drawing.Point(285, 66);
            this.CumulativeTimeSpentLabel.Name = "CumulativeTimeSpentLabel";
            this.CumulativeTimeSpentLabel.Size = new System.Drawing.Size(22, 24);
            this.CumulativeTimeSpentLabel.TabIndex = 12;
            this.CumulativeTimeSpentLabel.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(6, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(230, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "Cumulative Time Spent ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(407, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Hours";
            // 
            // inputGroupBox
            // 
            this.inputGroupBox.Controls.Add(this.label8);
            this.inputGroupBox.Controls.Add(this.CompanyNameTextBox);
            this.inputGroupBox.Controls.Add(this.ExistingProjectSelectButton);
            this.inputGroupBox.Controls.Add(this.ExistingProject);
            this.inputGroupBox.Controls.Add(this.ExistingProjectSelectBox);
            this.inputGroupBox.Controls.Add(this.SaveButton);
            this.inputGroupBox.Controls.Add(this.NewProjectName);
            this.inputGroupBox.Controls.Add(this.NewProjectNameTextBox);
            this.inputGroupBox.Location = new System.Drawing.Point(19, 18);
            this.inputGroupBox.Name = "inputGroupBox";
            this.inputGroupBox.Size = new System.Drawing.Size(576, 276);
            this.inputGroupBox.TabIndex = 11;
            this.inputGroupBox.TabStop = false;
            this.inputGroupBox.Text = "Input";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(81, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 18);
            this.label8.TabIndex = 9;
            this.label8.Text = "Company Name";
            // 
            // CompanyNameTextBox
            // 
            this.CompanyNameTextBox.Location = new System.Drawing.Point(22, 117);
            this.CompanyNameTextBox.Name = "CompanyNameTextBox";
            this.CompanyNameTextBox.Size = new System.Drawing.Size(262, 20);
            this.CompanyNameTextBox.TabIndex = 8;
            // 
            // clockGroupBox
            // 
            this.clockGroupBox.Controls.Add(this.ClockTime);
            this.clockGroupBox.Location = new System.Drawing.Point(91, 18);
            this.clockGroupBox.Name = "clockGroupBox";
            this.clockGroupBox.Size = new System.Drawing.Size(438, 276);
            this.clockGroupBox.TabIndex = 12;
            this.clockGroupBox.TabStop = false;
            this.clockGroupBox.Text = "Clock";
            this.clockGroupBox.Visible = false;
            // 
            // ClockTime
            // 
            this.ClockTime.AutoSize = true;
            this.ClockTime.Font = new System.Drawing.Font("Arial", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClockTime.ForeColor = System.Drawing.Color.Maroon;
            this.ClockTime.Location = new System.Drawing.Point(5, 81);
            this.ClockTime.Name = "ClockTime";
            this.ClockTime.Size = new System.Drawing.Size(427, 111);
            this.ClockTime.TabIndex = 0;
            this.ClockTime.Text = "00:00:00";
            // 
            // displayStopwatch
            // 
            this.displayStopwatch.Interval = 1000;
            this.displayStopwatch.Tick += new System.EventHandler(this.stopwatch_Tick);
            // 
            // ProjectTimeTracker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 674);
            this.Controls.Add(this.clockGroupBox);
            this.Controls.Add(this.inputGroupBox);
            this.Controls.Add(this.TimeGroupBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Name = "ProjectTimeTracker";
            this.Text = "Project Time Tracker";
            this.Load += new System.EventHandler(this.ProjectTimeTracker_Load);
            this.TimeGroupBox.ResumeLayout(false);
            this.TimeGroupBox.PerformLayout();
            this.inputGroupBox.ResumeLayout(false);
            this.inputGroupBox.PerformLayout();
            this.clockGroupBox.ResumeLayout(false);
            this.clockGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox NewProjectNameTextBox;
        private System.Windows.Forms.Label NewProjectName;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DomainUpDown ExistingProjectSelectBox;
        private System.Windows.Forms.Label ExistingProject;
        private System.Windows.Forms.Button ExistingProjectSelectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TimeSpentLabel;
        private System.Windows.Forms.GroupBox TimeGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label CumulativeTimeSpentLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox inputGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label AverageTimeSpentLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox CompanyNameTextBox;
        private System.Windows.Forms.GroupBox clockGroupBox;
        private System.Windows.Forms.Label ClockTime;
        private System.Windows.Forms.Timer displayStopwatch;
    }
}