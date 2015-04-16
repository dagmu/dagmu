using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DagMUWPF.Windows
{
	public partial class Main : Window
	{
		#region viewmodel properties
		private ObservableCollection<String> ridemodes = new ObservableCollection<string>() { "Walk", "Hand", "Fly", "Ride" };
		public ObservableCollection<String> RideModes { get { return ridemodes; } }
		/*public IEnumerable<World> WorldsToolItems
		{
			get
			{
				IEnumerable<World> ActiveWorlds = Worlds.Where(w => w.Active);
				IEnumerable<World> InactiveWorlds = Worlds.Where(w => !w.Active);
				return
					ActiveWorlds
					.Concat(InactiveWorlds)
					.Concat(new List<World>() { new World("Add new world") });
			}
		}*/

		public Worlds winworlds;
		#endregion

		#region construction
		public Main()
		{
			InitializeComponent();
			//this.DataContext = this;
			/*Worlds = new ObservableCollection<viewmodelWorld>() {
				new viewmodelWorld("Dagon"),
				new viewmodelWorld("Shean"),
			};*/
		}
		#endregion

		public class RelayCommand : ICommand
		{
			Action action;
			Func<bool> canexecute;

			public RelayCommand(Action action) : this(action, null) { }
			public RelayCommand(Action action, Func<bool> canexecute)
			{
				if (null == action) throw new ArgumentNullException("action required");
				this.action = action;
				this.canexecute = canexecute;
			}

			public event EventHandler CanExecuteChanged
			{
				add { if (canexecute != null) CommandManager.RequerySuggested += value; }
				remove { if (canexecute != null) CommandManager.RequerySuggested -= value; }
			}

			public void Execute(object parameter)
			{
				action();
			}

			public bool CanExecute(object parameter)
			{
				return (null == canexecute) ? true : canexecute();
			}
		}

		private void Refocus()
		{
			InputBoxBox.Refocus();
		}

		private void New_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (winworlds == null)
				winworlds = new Worlds();
			winworlds.Show();
		}
	}
}
