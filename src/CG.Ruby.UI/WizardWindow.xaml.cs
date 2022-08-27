
#region Local using statements
using CG.Ruby.UI.ViewModels;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Tools.Controls;
#endregion

namespace CG.Ruby.UI
{
    /// <summary>
    /// This class is the code-behind for the <see cref="WizardWindow"/> window.
    /// </summary>
    public partial class WizardWindow 
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="WizardWindow"/>
        /// class.
        /// </summary>
        public WizardWindow()
        {
            // Set the Syncfusion theme.
            SfSkinManager.SetTheme(this, new Theme() { ThemeName = "MaterialDark" });

            // Make the designer happy.
            InitializeComponent();
        }

        #endregion

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods
        
        /// <summary>
        /// This method is called when the user presses the cancel button on
        /// the wizard.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void WizardControl_Cancel(
            object sender, 
            System.Windows.RoutedEventArgs e
            )
        {
            // Cancel the dialog and close it.
            DialogResult = false;
            Close();
        }

        // *******************************************************************

        /// <summary>
        /// This method is called when the user presses the finish button on
        /// the wizard.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void WizardControl_Finish(
            object sender, 
            System.Windows.RoutedEventArgs e
            )
        {
            // Accept the dialog and close it.
            DialogResult = true;
            Close();
        }

        // *******************************************************************

        /// <summary>
        /// This method is called when the user switches pages on the wizard.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void WizardControl_SelectedPageChanged(
            object sender, 
            System.Windows.RoutedEventArgs e
            )
        {
            // Get a reference to the wizard control.
            var wizardControl = sender as WizardControl;
            if (null != wizardControl)
            {
                // Get a reference to the view-model.
                var viewModel = DataContext as WizardViewModel;
                if (null != viewModel)
                {
                    // Update the selected page property and validate.
                    viewModel.SelectedPageName = wizardControl.SelectedWizardPage.Name;
                    viewModel.OnValidate("");
                }
            }
        }

        // *******************************************************************

        /// <summary>
        /// This method is called when the user presses the help button.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void WizardControl_Help(
            object sender, 
            System.Windows.RoutedEventArgs e
            )
        {
            // Open the help site.
            System.Diagnostics.Process.Start("https://github.com/CodeGator/CG.Ruby");
        }

        #endregion
    }
}
