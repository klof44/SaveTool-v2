using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DuckGame;
using Microsoft.Xna.Framework.Graphics;

namespace SaveRecovery
{
	public partial class SaveTool : Form
	{
		private void AddProfile(Profile pProfile)
		{
			string text = pProfile.name;
			if (pProfile.steamID != 0UL)
			{
				text = text + " (" + pProfile.steamID.ToString() + ")";
			}
			if (!this._profiles.ContainsKey(text))
			{
				this._profiles.Add(text, pProfile);
				this.ProfilesDropdown.Items.Add(text);
			}
		}

		private void AddChallenge(string pGUID, string pGroup)
		{
			ChallengeData challenge = Challenges.GetChallenge(pGUID);
			if (challenge != null)
			{
				string text = string.Concat(new string[]
				{
					pGroup,
					": ",
					challenge.name,
					"         (",
					pGUID,
					")"
				});
				if (!this._profiles.ContainsKey(text))
				{
					this._challenges.Add(text, challenge);
					this.ChallengeList.Items.Add(text, 0);
				}
			}
		}

		private void LoadProfile(Profile pProfile)
		{
			this._currentProfile = pProfile;
			foreach (object obj in this.ChallengeList.Items)
			{
				((ListViewItem)obj).ImageIndex = 0;
			}
			foreach (KeyValuePair<string, ChallengeSaveData> keyValuePair in pProfile.challengeData)
			{
				ChallengeSaveData value = keyValuePair.Value;
				foreach (object obj2 in this.ChallengeList.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj2;
					if (listViewItem.Text.Contains(value.challenge))
					{
						listViewItem.ImageIndex = (int)value.trophy;
					}
				}
			}
		}

		private void LoadChallenge(List<ChallengeData> pChallenges)
		{

			List<ChallengeSaveData> list = new List<ChallengeSaveData>();
			foreach (ChallengeData challengeData in pChallenges)
			{
				if (pChallenges.Count == 1)
				{
					this.ChallengeDetailsGroup.Text = challengeData.name;
					if (challengeData.preview != null)
                    {
						this.ChallengePreview.Visible = true;
						Texture2D texture2D = Editor.StringToTexture(challengeData.preview);
						MemoryStream stream = new MemoryStream();
						texture2D.SaveAsPng(stream, texture2D.Width, texture2D.Height);
						Bitmap image = new Bitmap(stream);
						this.ChallengePreview.Image = image;
                    }
					else
                    {
						this.ChallengePreview.Visible = false;
                    }
				}
				else
				{
					this.ChallengePreview.Image = null;
					this.ChallengeDetailsGroup.Text = "<Multiple Challenges>";
				}
				ChallengeSaveData challengeSaveData = new ChallengeSaveData();
				if (this._currentProfile != null)
				{
					this._currentProfile.challengeData.TryGetValue(challengeData.levelID, out challengeSaveData);
				}
				if (challengeSaveData == null)
				{
					Dictionary<string, ChallengeSaveData> challengeData2 = this._currentProfile.challengeData;
					string levelID = challengeData.levelID;
					ChallengeSaveData challengeSaveData2 = new ChallengeSaveData();
					challengeSaveData2.challenge = challengeData.levelID;
					challengeSaveData = challengeSaveData2;
					challengeData2[levelID] = challengeSaveData2;
				}
				list.Add(challengeSaveData);
			}
			this._challengeDataControl.SetReflectedObjects(list);
		}

		public SaveTool()
		{
			this.InitializeComponent();
			this.AddProfile(Profiles.experienceProfile);
			this.AddProfile(Profiles.DefaultPlayer1);
			foreach (Profile pProfile in Profiles.allCustomProfiles)
			{
				this.AddProfile(pProfile);
			}
			ArcadeLevel arcadeLevel = new ArcadeLevel(Content.GetLevelID("arcade", LevelLocation.Content));
			arcadeLevel.DoInitialize();
			Layer.ClearLayers();
			this.ChallengeList.View = View.SmallIcon;
			ColumnHeader columnHeader = new ColumnHeader();
			columnHeader.Width = 1000;
			this.ChallengeList.Columns.Add(columnHeader);
			foreach (ArcadeMachine arcadeMachine in arcadeLevel._challenges)
			{
				foreach (string pGUID in arcadeMachine.data.challenges)
				{
					this.AddChallenge(pGUID, arcadeMachine.name);
				}
			}
			foreach (ChallengeData challengeData in Challenges.GetAllChancyChallenges(null))
			{
				this.AddChallenge(challengeData.levelID, "Chancy Challenge");
			}
			this._challengeDataControl = new ReflectionDataControl<ChallengeSaveData>();
			this._challengeDataControl.Reflect("trophy");
			this._challengeDataControl.Reflect("bestTime");
			this._challengeDataControl.Reflect("targets");
			this._challengeDataControl.Reflect("goodies");
			this.ChallengeDataPanel.Controls.Add(this._challengeDataControl);
			this._challengeDataControl.ValueChanged += this.ChallengeData_Changed;
			this.ProfilesDropdown.SelectedItem = this.ProfilesDropdown.Items[0];


			if (Resolution.current != Options.LocalData.windowedResolution)
            {
				LastRes = Resolution.current;
				Resolution.Set(Options.LocalData.windowedResolution);
				WasNotWindowed = true;
            }

            AppDomain.CurrentDomain.ProcessExit += Closed;
		}
        private static bool WasNotWindowed = false;
		private static Resolution LastRes;

        private void Closed(object sender, EventArgs e)
        {
            if (WasNotWindowed)
            {
				Resolution.Set(LastRes);
            }
        }

		private void ProfilesDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			string key = this.ProfilesDropdown.SelectedItem as string;
			this.LoadProfile(this._profiles[key]);
			this.ChallengeList.Items[0].Selected = true;
		}

		private void ProfilesLabel_Click(object sender, EventArgs e)
		{
		}

		private void ChallengeList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void ChallengeData_Changed(object sender, EventArgs e)
		{
			this.LoadProfile(this._currentProfile);
		}

		private void ChallengeList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			if (this.ChallengeList.SelectedItems.Count > 0)
			{
				List<ChallengeData> list = new List<ChallengeData>();
				foreach (object obj in this.ChallengeList.SelectedItems)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					list.Add(this._challenges[listViewItem.Text]);
				}
				this.LoadChallenge(list);
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			if (this.LevelCheckBox.Checked)
			{
				if (Profiles.experienceProfile.xp < DuckNetwork.GetLevel(5).xpRequired)
				{
					Profiles.experienceProfile.xp = DuckNetwork.GetLevel(5).xpRequired;
				}
				if (Profiles.experienceProfile.GetTotalFurnitures() < 50)
				{
					foreach (Furniture furniture in UIGachaBox.GetRandomFurniture(Rarity.Common, 32, 1f, false, 0, true, false))
					{
						Profiles.experienceProfile.SetNumFurnitures((int)furniture.index, Profiles.experienceProfile.GetNumFurnitures((int)furniture.index) + 1);
					}
					foreach (Furniture furniture2 in UIGachaBox.GetRandomFurniture(Rarity.Rare, 16, 0.6f, false, 0, true, false))
					{
						Profiles.experienceProfile.SetNumFurnitures((int)furniture2.index, Profiles.experienceProfile.GetNumFurnitures((int)furniture2.index) + 1);
					}
					foreach (Furniture furniture3 in UIGachaBox.GetRandomFurniture(Rarity.VeryVeryRare, 8, 0.4f, false, 0, true, false))
					{
						Profiles.experienceProfile.SetNumFurnitures((int)furniture3.index, Profiles.experienceProfile.GetNumFurnitures((int)furniture3.index) + 1);
					}
					foreach (Furniture furniture4 in UIGachaBox.GetRandomFurniture(Rarity.SuperRare, 1, 0.3f, false, 0, true, false))
					{
						Profiles.experienceProfile.SetNumFurnitures((int)furniture4.index, Profiles.experienceProfile.GetNumFurnitures((int)furniture4.index) + 1);
					}
					if (Profiles.experienceProfile.littleManBucks < 500)
					{
						Profiles.experienceProfile.littleManBucks = 500;
					}
				}
			}
			using (Dictionary<string, Profile>.Enumerator enumerator2 = this._profiles.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<string, Profile> keyValuePair = enumerator2.Current;
					Profiles.Save(keyValuePair.Value);
				}
				goto IL_230;
			}
			IL_22B:
			Cloud.Update();
			IL_230:
			if (!Cloud.processing)
			{
				DuckGame.Program.crashed = true;
				Process.Start(Application.ExecutablePath, DuckGame.Program.commandLine);
				Application.Exit();
				return;
			}
			goto IL_22B;
		}

		private Dictionary<string, Profile> _profiles = new Dictionary<string, Profile>();

		private Dictionary<string, ChallengeData> _challenges = new Dictionary<string, ChallengeData>();

		private Profile _currentProfile;

		private ReflectionDataControl<ChallengeSaveData> _challengeDataControl;
	}
}
