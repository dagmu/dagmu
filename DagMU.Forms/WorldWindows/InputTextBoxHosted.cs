using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System;
using System.Windows.Media;

namespace DagMU.Forms
{
	[Designer(typeof(ControlDesigner))]
	//[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class InputTextBoxHosted : ElementHost
	{
		public InputTextBoxHosted()
		{
			box = new TextBox();
			base.Child = box;
			box.TextChanged += (s, e) => OnTextChanged(e);
			box.KeyDown += (s, e) => OnKeyDown(new System.Windows.Forms.KeyEventArgs(e.Key.ToForms()));
			box.PreviewKeyDown += (s, e) => OnPreviewKeyDown(new System.Windows.Forms.PreviewKeyDownEventArgs(e.Key.ToForms()));
			box.MouseWheel += (s, e) => OnMouseWheel(e.ToForms());
			box.SpellCheck.IsEnabled = true;
			box.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			this.Size = new System.Drawing.Size(100, 20);
		}

		public override System.Drawing.Color ForeColor
		{
			get { return box.Foreground.ToFormsColor(); }
			set { box.Foreground = value.ToWPFBrush(); }
		}

		public override System.Drawing.Color BackColor
		{
			get {
				if (box == null) return System.Drawing.Color.Black;//HACK
				return box.Background.ToFormsColor();
			}
			set { box.Background = value.ToWPFBrush(); }
		}

		public override System.Drawing.Font Font
		{
			get {
				if (box == null) return new System.Drawing.Font("Arial", 10);//HACK
				double size = box.FontSize * 72 / 96;
				string family = box.FontFamily.ToString();
				return new System.Drawing.Font(family, (float)size);
			}
			set {
				box.FontFamily = new System.Windows.Media.FontFamily(value.Name);

				box.FontWeight = value.Bold
					? System.Windows.FontWeights.Bold
					: System.Windows.FontWeights.Regular;

				box.FontStyle = value.Italic
					? System.Windows.FontStyles.Italic
					: System.Windows.FontStyles.Normal;

				box.FontSize = value.SizeInPoints * 96 / 72;
			}
		}

		public override string Text
		{
			get { return box.Text; }
			set { box.Text = value; }
		}

		public int TextLength
		{
			get { return box.Text.Length; }
		}

		public int SelectionStart
		{
			get { return box.SelectionStart; }
			set { box.SelectionStart = value; }
		}

		public void Select(int start, int length)
		{
			box.Select(start, length);
		}

		public System.Windows.Media.Brush SelectionBrush
		{
			get { return box.SelectionBrush; }
			set { box.SelectionBrush = value; }
		}

		public System.Drawing.Color SelectionColor
		{
			get {
				if (box.SelectionBrush is SolidColorBrush) {
					return (box.SelectionBrush as SolidColorBrush).Color.ToFormsColor();
				} else {
					throw new NotImplementedException();
				}
			}
			set { box.SelectionBrush = value.ToWPFBrush(); }
		}

		public void Clear()
		{
			box.Clear();
		}

		[DefaultValue(false)]
		public bool Multiline {
			get { return box.AcceptsReturn; }
			set { box.AcceptsReturn = value; }
		}

		[DefaultValue(true)]
		public bool WordWrap {
			get { return box.TextWrapping != TextWrapping.NoWrap; }
			set { box.TextWrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new System.Windows.UIElement Child {
			get { return base.Child; }
			set { /* Do nothing to solve a problem with the serializer !! */ }
		}

		private TextBox box;
	}
}
