using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DuckGame;

namespace SaveRecovery
{
	public class ReflectionDataControl<T> : FlowLayoutPanel
	{
		public event EventHandler ValueChanged;

		public ReflectionDataControl()
		{
			this._reflectedObjects = new List<T>();
			this._reflectedObjectTemplate = (T)((object)Activator.CreateInstance(typeof(T)));
			this.Dock = DockStyle.Fill;
			this.AutoSize = true;
			base.FlowDirection = FlowDirection.TopDown;
		}

		public void Reflect(string pMemberName)
		{
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
			{
				AutoSize = true
			};
			Label label = new Label
			{
				Text = pMemberName
			};
			label.Anchor = AnchorStyles.Left;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.Width = 150;
			flowLayoutPanel.Controls.Add(label);
			ClassMember member = Editor.GetMember(this._reflectedObjectTemplate.GetType(), pMemberName);
			if (member.type.IsEnum)
			{
				ComboBox comboBox = new ComboBox();
				foreach (string item in member.type.GetEnumNames())
				{
					comboBox.Items.Add(item);
				}
				this._memberMap[member] = comboBox;
				comboBox.Tag = member;
				comboBox.SelectedIndexChanged += this.ComboBox_SelectedIndexChanged;
				flowLayoutPanel.Controls.Add(comboBox);
			}
			else if (member.type == typeof(int))
			{
				TextBox textBox = new TextBox();
				this._memberMap[member] = textBox;
				textBox.Tag = member;
				textBox.TextChanged += this.TextBox_Changed;
				flowLayoutPanel.Controls.Add(textBox);
			}
			base.Controls.Add(flowLayoutPanel);
		}

		public void SetReflectedObjects(List<T> pObjects)
		{
			this._settingObject = true;
			this._reflectedObjects = pObjects;
			foreach (KeyValuePair<ClassMember, Control> keyValuePair in this._memberMap)
			{
				if (keyValuePair.Value is TextBox)
				{
					(keyValuePair.Value as TextBox).Text = ((this._reflectedObjects.Count > 1) ? "<multiple>" : ((int)keyValuePair.Key.GetValue(this._reflectedObjects[0])).ToString());
				}
				else if (keyValuePair.Value is ComboBox)
				{
					(keyValuePair.Value as ComboBox).SelectedIndex = ((this._reflectedObjects.Count > 1) ? 0 : keyValuePair.Key.type.GetEnumNames().ToList<string>().IndexOf(keyValuePair.Key.GetValue(this._reflectedObjects[0]).ToString()));
				}
			}
			this._settingObject = false;
		}

		private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this._settingObject)
			{
				return;
			}
			ClassMember classMember = (sender as ComboBox).Tag as ClassMember;
			foreach (T t in this._reflectedObjects)
			{
				classMember.SetValue(t, (sender as ComboBox).SelectedIndex);
			}
			this.ValueChanged(this, e);
		}

		private void TextBox_Changed(object sender, EventArgs e)
		{
			if (this._settingObject)
			{
				return;
			}
			ClassMember classMember = (sender as TextBox).Tag as ClassMember;
			foreach (T t in this._reflectedObjects)
			{
				try
				{
					if (classMember.type == typeof(int))
					{
						classMember.SetValue(t, Convert.ToInt32((sender as TextBox).Text));
					}
					if (classMember.type == typeof(uint))
					{
						classMember.SetValue(t, Convert.ToUInt32((sender as TextBox).Text));
					}
					if (classMember.type == typeof(float))
					{
						classMember.SetValue(t, Convert.ToSingle((sender as TextBox).Text));
					}
					if (classMember.type == typeof(byte))
					{
						classMember.SetValue(t, Convert.ToByte((sender as TextBox).Text));
					}
					if (classMember.type == typeof(sbyte))
					{
						classMember.SetValue(t, Convert.ToSByte((sender as TextBox).Text));
					}
					if (classMember.type == typeof(long))
					{
						classMember.SetValue(t, Convert.ToInt64((sender as TextBox).Text));
					}
					if (classMember.type == typeof(ulong))
					{
						classMember.SetValue(t, Convert.ToUInt64((sender as TextBox).Text));
					}
					if (classMember.type == typeof(short))
					{
						classMember.SetValue(t, Convert.ToInt16((sender as TextBox).Text));
					}
					if (classMember.type == typeof(ushort))
					{
						classMember.SetValue(t, Convert.ToUInt16((sender as TextBox).Text));
					}
				}
				catch (Exception)
				{
				}
			}
			this.ValueChanged(this, e);
		}

		private List<T> _reflectedObjects;

		private T _reflectedObjectTemplate;

		private Map<ClassMember, Control> _memberMap = new Map<ClassMember, Control>();

		private bool _settingObject;
	}
}
