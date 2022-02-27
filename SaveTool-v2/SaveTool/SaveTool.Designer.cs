namespace SaveRecovery
{
	public partial class SaveTool : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveTool));
            this.ProfilesDropdown = new System.Windows.Forms.ComboBox();
            this.ProfilesLabel = new System.Windows.Forms.Label();
            this.ChallengeGroup = new System.Windows.Forms.GroupBox();
            this.ChallengeDetailsGroup = new System.Windows.Forms.GroupBox();
            this.ChallengeDataPanel = new System.Windows.Forms.Panel();
            this.ChallengePreview = new System.Windows.Forms.PictureBox();
            this.ChallengeList = new System.Windows.Forms.ListView();
            this.TrophyIcons = new System.Windows.Forms.ImageList(this.components);
            this.SaveButton = new System.Windows.Forms.Button();
            this.LevelCheckBox = new System.Windows.Forms.CheckBox();
            this.ChallengeGroup.SuspendLayout();
            this.ChallengeDetailsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChallengePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // ProfilesDropdown
            // 
            this.ProfilesDropdown.FormattingEnabled = true;
            this.ProfilesDropdown.Location = new System.Drawing.Point(47, 4);
            this.ProfilesDropdown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ProfilesDropdown.Name = "ProfilesDropdown";
            this.ProfilesDropdown.Size = new System.Drawing.Size(181, 21);
            this.ProfilesDropdown.TabIndex = 1;
            this.ProfilesDropdown.SelectedIndexChanged += new System.EventHandler(this.ProfilesDropdown_SelectedIndexChanged);
            // 
            // ProfilesLabel
            // 
            this.ProfilesLabel.AutoSize = true;
            this.ProfilesLabel.Location = new System.Drawing.Point(8, 6);
            this.ProfilesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ProfilesLabel.Name = "ProfilesLabel";
            this.ProfilesLabel.Size = new System.Drawing.Size(36, 13);
            this.ProfilesLabel.TabIndex = 2;
            this.ProfilesLabel.Text = "Profile";
            this.ProfilesLabel.Click += new System.EventHandler(this.ProfilesLabel_Click);
            // 
            // ChallengeGroup
            // 
            this.ChallengeGroup.Controls.Add(this.ChallengeDetailsGroup);
            this.ChallengeGroup.Controls.Add(this.ChallengeList);
            this.ChallengeGroup.Location = new System.Drawing.Point(11, 26);
            this.ChallengeGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeGroup.Name = "ChallengeGroup";
            this.ChallengeGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeGroup.Size = new System.Drawing.Size(617, 378);
            this.ChallengeGroup.TabIndex = 3;
            this.ChallengeGroup.TabStop = false;
            this.ChallengeGroup.Text = "ChallengeData";
            // 
            // ChallengeDetailsGroup
            // 
            this.ChallengeDetailsGroup.Controls.Add(this.ChallengeDataPanel);
            this.ChallengeDetailsGroup.Controls.Add(this.ChallengePreview);
            this.ChallengeDetailsGroup.Location = new System.Drawing.Point(379, 16);
            this.ChallengeDetailsGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeDetailsGroup.Name = "ChallengeDetailsGroup";
            this.ChallengeDetailsGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeDetailsGroup.Size = new System.Drawing.Size(237, 354);
            this.ChallengeDetailsGroup.TabIndex = 3;
            this.ChallengeDetailsGroup.TabStop = false;
            this.ChallengeDetailsGroup.Text = "<Challenge Name>";
            // 
            // ChallengeDataPanel
            // 
            this.ChallengeDataPanel.Location = new System.Drawing.Point(4, 112);
            this.ChallengeDataPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeDataPanel.Name = "ChallengeDataPanel";
            this.ChallengeDataPanel.Size = new System.Drawing.Size(229, 241);
            this.ChallengeDataPanel.TabIndex = 1;
            // 
            // ChallengePreview
            // 
            this.ChallengePreview.Location = new System.Drawing.Point(4, 16);
            this.ChallengePreview.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengePreview.Name = "ChallengePreview";
            this.ChallengePreview.Size = new System.Drawing.Size(99, 92);
            this.ChallengePreview.TabIndex = 0;
            this.ChallengePreview.TabStop = false;
            // 
            // ChallengeList
            // 
            this.ChallengeList.HideSelection = false;
            this.ChallengeList.Location = new System.Drawing.Point(8, 16);
            this.ChallengeList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChallengeList.Name = "ChallengeList";
            this.ChallengeList.Size = new System.Drawing.Size(373, 355);
            this.ChallengeList.SmallImageList = this.TrophyIcons;
            this.ChallengeList.TabIndex = 2;
            this.ChallengeList.UseCompatibleStateImageBehavior = false;
            this.ChallengeList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ChallengeList_ItemSelectionChanged);
            this.ChallengeList.SelectedIndexChanged += new System.EventHandler(this.ChallengeList_SelectedIndexChanged);
            // 
            // TrophyIcons
            // 
            this.TrophyIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.TrophyIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.TrophyIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(163, 408);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(322, 49);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save And Restart DG";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // LevelCheckBox
            // 
            this.LevelCheckBox.AutoSize = true;
            this.LevelCheckBox.Location = new System.Drawing.Point(241, 5);
            this.LevelCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LevelCheckBox.Name = "LevelCheckBox";
            this.LevelCheckBox.Size = new System.Drawing.Size(151, 17);
            this.LevelCheckBox.TabIndex = 5;
            this.LevelCheckBox.Text = "Give Level 5 and Furniture";
            this.LevelCheckBox.UseVisualStyleBackColor = true;
            // 
            // SaveTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 463);
            this.Controls.Add(this.LevelCheckBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.ChallengeGroup);
            this.Controls.Add(this.ProfilesLabel);
            this.Controls.Add(this.ProfilesDropdown);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SaveTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DG Save Tool";
            this.ChallengeGroup.ResumeLayout(false);
            this.ChallengeDetailsGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ChallengePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.ComboBox ProfilesDropdown;

		private global::System.Windows.Forms.Label ProfilesLabel;

		private global::System.Windows.Forms.GroupBox ChallengeGroup;

		private global::System.Windows.Forms.ListView ChallengeList;

		private global::System.Windows.Forms.ImageList TrophyIcons;

		private global::System.Windows.Forms.GroupBox ChallengeDetailsGroup;

		private global::System.Windows.Forms.PictureBox ChallengePreview;

		private global::System.Windows.Forms.Panel ChallengeDataPanel;

		private global::System.Windows.Forms.Button SaveButton;

		private global::System.Windows.Forms.CheckBox LevelCheckBox;
	}
}
