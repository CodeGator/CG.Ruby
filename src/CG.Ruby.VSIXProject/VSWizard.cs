
#region Local using statements
using CG.Ruby.UI;
using CG.Ruby.UI.ViewModels;
using EnvDTE;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Interop;
#endregion

namespace CG.Ruby.VSIXProject
{
    /// <summary>
    /// This class is a default implementation of the <see cref="IWizard"/>
    /// interface.
    /// </summary>
    public class VSWizard : IWizard
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field indicats whether or not the wizard was cancelled.
        /// </summary>
        private bool _weCancelled;

        /// <summary>
        /// This field indicates what type of repository to generate.
        /// </summary>
        private RepositoryTypes _repositoryType;

        /// <summary>
        /// This field indicates whether the repository type exists in the solution.
        /// </summary>
        private bool _repositoryTypeFound;

        /// <summary>
        /// This field indicates whether the repository will use a factory for data-contexts.
        /// </summary>
        private bool _useDataContext;

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public virtual void RunStarted(
            object automationObject, 
            Dictionary<string, string> replacementsDictionary, 
            WizardRunKind runKind, 
            object[] customParams
            )
        {
            try
            {
                // Make the compiler happy.
                ThreadHelper.ThrowIfNotOnUIThread();

                // Recover the VS object.
                var dte = automationObject as DTE;

                // Walk the entire Visual Studio solution tree looking for
                // potential model classes.
                var table = new Dictionary<string, Dictionary<string, List<string>>>();
                dte.FindClassesInSolution(table);

                // Look for the RepositoryException type in the solution.
                _repositoryTypeFound = table.RepositoryExceptionFound();

                // Create the wizard UI.
                var form = new WizardWindow();

                // Initialize the wizard form's view-model.
                var viewModel = (form.DataContext as WizardViewModel);
                if (null != viewModel)
                {
                    // Select the default(s) for the wizard UI.
                    viewModel.Table = table;
                    
                    viewModel.NameSpace = replacementsDictionary["$rootnamespace$"];
                    viewModel.ClassName = replacementsDictionary["$safeitemname$"];
                    viewModel.IFaceName = "I" + replacementsDictionary["$safeitemname$"];

                    viewModel.UseDataContextFactory = true;

                    viewModel.AddAnyAsync = true;
                    viewModel.AddCreateAsync = true;
                    viewModel.AddDeleteAsync = true;
                    viewModel.AddUpdateAsync = true;
                    viewModel.AddFindAsync = true;
                    viewModel.AddFindSingleAsync = true;
                }

                // Show the wizard UI to the caller.
                var result = form.ShowDialog();

                // Did the caller cancel?
                if (!result.HasValue || !result.Value)
                {
                    // Remember that we cancelled.
                    _weCancelled = true;
                    return; // Nothing left to do!
                }

                // Update the wizard state based on the UI.
                if (null != viewModel)
                {
                    _repositoryType = (RepositoryTypes)Enum.Parse(
                        typeof(RepositoryTypes),
                        viewModel.SelectedRepoType
                        );

                    _useDataContext = viewModel.UseDataContextFactory;

                    replacementsDictionary["$newnamespace$"] =
                        viewModel.NameSpace.TrimEnd('.').TrimStart('.');
                    replacementsDictionary["$newclassname$"] =
                        viewModel.ClassName.TrimEnd('.').TrimStart('.');
                    replacementsDictionary["$newifacename$"] = 
                        viewModel.IFaceName.TrimEnd('.').TrimStart('.');

                    replacementsDictionary["$addanyasync$"] =
                        viewModel.AddAnyAsync ? "true" : "false";
                    replacementsDictionary["$addcreateasync$"] =
                        viewModel.AddCreateAsync ? "true" : "false";
                    replacementsDictionary["$adddeleteasync$"] =
                        viewModel.AddDeleteAsync ? "true" : "false";
                    replacementsDictionary["$addfindasync$"] =
                        viewModel.AddFindAsync ? "true" : "false";
                    replacementsDictionary["$addfindsingleasync$"] =
                        viewModel.AddFindSingleAsync ? "true" : "false";
                    replacementsDictionary["$addupdateasync$"] =
                        viewModel.AddUpdateAsync ? "true" : "false";

                    replacementsDictionary["$modelproject$"] =
                        viewModel.SelectedModelProject;
                    replacementsDictionary["$modelnamespace$"] =
                        viewModel.SelectedModelNamespace;
                    replacementsDictionary["$modelclass$"] =
                        viewModel.SelectedModelClass;

                    if (viewModel.EFCoreTypeChosen)
                    {
                        replacementsDictionary["$efcorecontextproject$"] =
                            viewModel.SelectedEfCoreContextProject;
                        replacementsDictionary["$efcorecontextnamespace$"] =
                            viewModel.SelectedEfCoreContextNamespace;
                        replacementsDictionary["$efcorecontextclass$"] =
                            viewModel.SelectedEfCoreContextClass;

                        replacementsDictionary["$usedatacontextfactory$"] =
                            viewModel.UseDataContextFactory ? "true" : "false";
                    }
                }
            }
            catch (Exception ex)
            {
                // Don't add items if we crashed.
                _weCancelled = true;

                // Show the world what happened.
                MessageBox.Show(ex.ToString());
            }
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual bool ShouldAddProjectItem(
            string filePath
            )
        {
            // Don't add anything if we were cancelled.
            if (_weCancelled)
            {
                return false;
            }

            // Strip out just the file name.
            var fileName = Path.GetFileNameWithoutExtension(filePath);

            // Always allow these files.
            if ("IRepository" == fileName)
            {
                return true;
            }

            // Sometimes allow these files.
            if ("RepositoryException" == fileName)
            {
                // Add it only if it doesn't already exist.
                return !_repositoryTypeFound;
            }

            // Only allow these files if the user wants them.
            var retValue = true;
            switch(_repositoryType)
            {
                case RepositoryTypes.EfCore:
                    retValue = _useDataContext 
                        ? "EfCoreRepository" == fileName
                        : "EfCoreRepository2" == fileName;
                    break;
                case RepositoryTypes.Default:
                    retValue = "DefaultRepository" == fileName;
                    break;
            }

            // Return the results.
            return retValue;
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual void ProjectItemFinishedGenerating(
            ProjectItem projectItem
            )
        {
            // Deliberately empty.
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual void ProjectFinishedGenerating(
            Project project
            )
        {
            // Deliberately empty.
        }

        // *******************************************************************

        /// <inheritdoc/>
        public virtual void BeforeOpeningFile(
            ProjectItem projectItem
            )
        {
            // Deliberately empty.
        }
                
        // *******************************************************************

        /// <inheritdoc/>
        public virtual void RunFinished()
        {
            // Deliberately empty.
        }

        #endregion
    }
}
