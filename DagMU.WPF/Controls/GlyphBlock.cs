using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfTools
{
	public class GlyphBlock : TextBlock
	{
		public GlyphBlock()
		{
			Style = (Style)FindResource("FontAwesome");
			TextAlignment = TextAlignment.Center;
			FontSize = 100;
		}
	}
}
