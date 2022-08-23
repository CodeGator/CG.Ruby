using CG.Ruby.UI;
using CG.Ruby.UI.ViewModels;
using System;
using System.Collections.Generic;
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

namespace TestUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create the window so we can test it.
            var window = new WizardWindow();

            // Get the view-model.
            var viewModel = window.DataContext as WizardViewModel;

            // Setup the view-model just like it would be in VS.
            viewModel.AddCreateAsync = true;
            viewModel.AddDeleteAsync = false;
            viewModel.AddFindAsync = true;
            viewModel.AddFindSingleAsync = false;
            viewModel.AddUpdateAsync = true;
            viewModel.NameSpace = "Repositories";
            viewModel.IFaceName = "IFooRepository";
            viewModel.ClassName = "FooRepository";
            viewModel.Table = new Dictionary<string, Dictionary<string, List<string>>>()
            {
                { "ClassLibrary1", new Dictionary<string, List<string>>() 
                {
                    { "Models", new List<string>() { "Foo" } },
                    { "EfCore", new List<string>() { "TestDataContext" } }
                }}
            };

            // We OWN this window!!
            window.Owner = this;

            // Show the window.
            window.ShowDialog();
        }
    }
}
