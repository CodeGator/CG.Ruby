
#region Local using statements
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
#endregion

namespace CG.Ruby.UI.ViewModels
{
    /// <summary>
    /// This class is a view-model for the <see cref="WizardWindow"/> page.
    /// </summary>
    public class WizardViewModel : ViewModelBase
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field backs the <see cref="SummaryList"/> property.
        /// </summary>
        protected Dictionary<string, string> _summary =
            new Dictionary<string, string>();

        /// <summary>
        /// This field backs the <see cref="Table"/> property.
        /// </summary>
        protected Dictionary<string, Dictionary<string, List<string>>> _table =
            new Dictionary<string, Dictionary<string, List<string>>>();


        /// <summary>
        /// This field backs the <see cref="UseDataContextFactory"/> property.
        /// </summary>
        protected bool _useDataContextFactory;

        /// <summary>
        /// This field backs the <see cref="AddAnyAsync"/> property.
        /// </summary>
        protected bool _addAnyAsync;

        /// <summary>
        /// This field backs the <see cref="AddCreateAsync"/> property.
        /// </summary>
        protected bool _addCreateAsync;

        /// <summary>
        /// This field backs the <see cref="AddDeleteAsync"/> property.
        /// </summary>
        protected bool _addDeleteAsync;

        /// <summary>
        /// This field backs the <see cref="AddFindAsync"/> property.
        /// </summary>
        protected bool _addFindAsync;

        /// <summary>
        /// This field backs the <see cref="AddFindSingleAsync"/> property.
        /// </summary>
        protected bool _addFindSingleAsync;

        /// <summary>
        /// This field backs the <see cref="AddUpdateAsync"/> property.
        /// </summary>
        protected bool _addUpdateAsync;

        /// <summary>
        /// This field backs the <see cref="NameSpace"/> property.
        /// </summary>
        protected string _nameSpace;

        /// <summary>
        /// This field backs the <see cref="NameSpaceError"/> property.
        /// </summary>
        protected string _nameSpaceError;

        /// <summary>
        /// This field backs the <see cref="ClassName"/> property.
        /// </summary>
        protected string _className;

        /// <summary>
        /// This field backs the <see cref="ClassNameError"/> property.
        /// </summary>
        protected string _classNameError;

        /// <summary>
        /// This field backs the <see cref="IFaceName"/> property.
        /// </summary>
        protected string _ifaceName;

        /// <summary>
        /// This field backs the <see cref="IFaceNameError"/> property.
        /// </summary>
        protected string _ifaceNameError;

        /// <summary>
        /// This field backs the <see cref="FormIsValid"/> property.
        /// </summary>
        protected bool _formIsValid;

        /// <summary>
        /// This field backs the <see cref="EfCoreContextProjects"/> property.
        /// </summary>
        protected List<string> _efCoreContextProjects = new List<string>();

        /// <summary>
        /// This field backs the <see cref="ModelProjects"/> property.
        /// </summary>
        protected List<string> _modelProjects = new List<string>();

        /// <summary>
        /// This field backs the <see cref="EfCoreContextNamespaces"/> property.
        /// </summary>
        protected List<string> _efCoreContextNamespaces = new List<string>();

        /// <summary>
        /// This field backs the <see cref="ModelNamespaces"/> property.
        /// </summary>
        protected List<string> _modelNamespaces = new List<string>();

        /// <summary>
        /// This field backs the <see cref="EfCoreContextClasses"/> property.
        /// </summary>
        protected List<string> _efCoreContextClasses = new List<string>();

        /// <summary>
        /// This field backs the <see cref="ModelClasses"/> property.
        /// </summary>
        protected List<string> _modelClasses = new List<string>();

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextProject"/> property.
        /// </summary>
        protected string _selectedEfCoreContextProject;

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextProjectError"/> property.
        /// </summary>
        protected string _selectedEfCoreContextProjectError;

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextNamespace"/> property.
        /// </summary>
        protected string _selectedEfCoreContextNamespace;

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextNamespaceError"/> property.
        /// </summary>
        protected string _selectedEfCoreContextNamespaceError;

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextClass"/> property.
        /// </summary>
        protected string _selectedEfCoreContextClass;

        /// <summary>
        /// This field backs the <see cref="SelectedEfCoreContextClassError"/> property.
        /// </summary>
        protected string _selectedEfCoreContextClassError;

        /// <summary>
        /// This field backs the <see cref="SelectedModelProject"/> property.
        /// </summary>
        protected string _selectedModelProject;

        /// <summary>
        /// This field backs the <see cref="SelectedModelProjectError"/> property.
        /// </summary>
        protected string _selectedModelProjectError;

        /// <summary>
        /// This field backs the <see cref="SelectedModelNamespace"/> property.
        /// </summary>
        protected string _selectedModelNamespace;

        /// <summary>
        /// This field backs the <see cref="SelectedModelNamespaceError"/> property.
        /// </summary>
        protected string _selectedModelNamespaceError;

        /// <summary>
        /// This field backs the <see cref="SelectedModelClass"/> property.
        /// </summary>
        protected string _selectedModelClass;

        /// <summary>
        /// This field backs the <see cref="SelectedModelClassError"/> property.
        /// </summary>
        protected string _selectedModelClassError;

        /// <summary>
        /// This field backs the <see cref="SelectedRepoType"/> property.
        /// </summary>
        protected string _selectedRepoType;

        /// <summary>
        /// This field backs the <see cref="SelectedRepoTypeError"/> property.
        /// </summary>
        protected string _selectedRepoTypeError;

        /// <summary>
        /// This field backs the <see cref="SelectedPageName"/> property.
        /// </summary>
        protected string _selectedPageName;        

        #endregion

        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates an EFCORE repository has been chosen.
        /// </summary>
        public bool EFCoreTypeChosen
        {
            get => SelectedRepoType == "EfCore";
        }

        /// <summary>
        /// This property contains a flag for using an EFCORE data context in the repository.
        /// </summary>
        public bool UseDataContextFactory
        {
            get => _useDataContextFactory;
            set
            {
                // Set the summary.
                _summary[nameof(UseDataContextFactory)] = value ?
                    $"Using EFCORE data-context factory"
                    : "Using EFCORE data-context";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _useDataContextFactory, value);
            }
        }

        /// <summary>
        /// This property contains a flag for adding the AnyAsync method.
        /// </summary>
        public bool AddAnyAsync
        {
            get => _addAnyAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddAnyAsync)] = value ?
                    $"Adding an AddAnyAsync method"
                    : "Not adding an AddAnyAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addAnyAsync, value);
            }
        }

        /// <summary>
        /// This property contains a flag for adding the CreateAsync method.
        /// </summary>
        public bool AddCreateAsync
        {
            get => _addCreateAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddCreateAsync)] = value ? 
                    $"Adding an AddCreateAsync method"
                    : "Not adding an AddCreateAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addCreateAsync, value);                
            }
        }

        /// <summary>
        /// This property contains a flag for adding the DeleteAsync method.
        /// </summary>
        public bool AddDeleteAsync
        {
            get => _addDeleteAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddDeleteAsync)] = value ?
                    $"Adding an AddDeleteAsync method"
                    : "Not adding an AddDeleteAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addDeleteAsync, value);                
            }
        }

        /// <summary>
        /// This property contains a flag for adding the UpdateAsync method.
        /// </summary>
        public bool AddUpdateAsync
        {
            get => _addUpdateAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddUpdateAsync)] = value ?
                    $"Adding an AddUpdateAsync method"
                    : "Not adding an AddUpdateAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addUpdateAsync, value);                
            }
        }

        /// <summary>
        /// This property contains a flag for adding the FindAsync method.
        /// </summary>
        public bool AddFindAsync
        {
            get => _addFindAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddFindAsync)] = value ?
                    $"Adding an AddFindAsync method"
                    : "Not adding an AddFindAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addFindAsync, value);                
            }
        }

        /// <summary>
        /// This property contains a flag for adding the FindSingleAsync method.
        /// </summary>
        public bool AddFindSingleAsync
        {
            get => _addFindSingleAsync;
            set
            {
                // Set the summary.
                _summary[nameof(AddFindSingleAsync)] = value ?
                    $"Adding an AddFindSingleAsync method"
                    : "Not adding an AddFindSingleAsync method";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _addFindSingleAsync, value);                
            }
        }

        /// <summary>
        /// This property contains the namespace.
        /// </summary>
        public string NameSpace
        {
            get => _nameSpace;
            set
            {
                // Set the summary.
                _summary[nameof(NameSpace)] = $"Repository namespace: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _nameSpace, value);                
            }
        }

        /// <summary>
        /// This property contains the namespace error.
        /// </summary>
        public string NameSpaceError
        {
            get => _nameSpaceError;
            set => SetValue(ref _nameSpaceError, value);
        }

        /// <summary>
        /// This property contains the class name for the new item.
        /// </summary>
        public string ClassName
        {
            get => _className;
            set
            {
                // Set the summary.
                _summary[nameof(ClassName)] = $"Repository class: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _className, value);                
            }
        }

        /// <summary>
        /// This property contains the class name error.
        /// </summary>
        public string ClassNameError
        {
            get => _classNameError;
            set => SetValue(ref _classNameError, value);
        }

        /// <summary>
        /// This property contains the interface name for the new item.
        /// </summary>
        public string IFaceName
        {
            get => _ifaceName;
            set
            {
                // Set the summary.
                _summary[nameof(IFaceName)] = $"Repository interface: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _ifaceName, value);               
            }
        }

        /// <summary>
        /// This property contains the interface name error.
        /// </summary>
        public string IFaceNameError
        {
            get => _ifaceNameError;
            set => SetValue(ref _ifaceNameError, value);
        }

        /// <summary>
        /// This property indicates the form is valid.
        /// </summary>
        public bool FormIsValid
        {
            get => _formIsValid;
            set => SetValue(ref _formIsValid, value);
        }

        /// <summary>
        /// This property contains a table of projects, namespaces, and classes.
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> Table
        {
            get => _table;
            set
            {
                EfCoreContextProjects = value.Keys.ToList();
                ModelProjects = value.Keys.ToList();
                SetValue(ref _table, value);
            } 
        }

        /// <summary>
        /// This property contains a list of model projects.
        /// </summary>
        public List<string> ModelProjects
        {
            get => _modelProjects;
            set => SetValue(ref _modelProjects, value);
        }

        /// <summary>
        /// This property contains a list of EFCORE context projects.
        /// </summary>
        public List<string> EfCoreContextProjects
        {
            get => _efCoreContextProjects;
            set => SetValue(ref _efCoreContextProjects, value);
        }

        /// <summary>
        /// This property contains a list of model namespaces.
        /// </summary>
        public List<string> ModelNamespaces
        {
            get => _modelNamespaces;
            set => SetValue(ref _modelNamespaces, value);
        }

        /// <summary>
        /// This property contains a list of EFCORE context namespaces.
        /// </summary>
        public List<string> EfCoreContextNamespaces
        {
            get => _efCoreContextNamespaces;
            set => SetValue(ref _efCoreContextNamespaces, value);
        }

        /// <summary>
        /// This property contains a list of model classes.
        /// </summary>
        public List<string> ModelClasses
        {
            get => _modelClasses;
            set => SetValue(ref _modelClasses, value);
        }

        /// <summary>
        /// This property contains a list of EFCORE context classes.
        /// </summary>
        public List<string> EfCoreContextClasses
        {
            get => _efCoreContextClasses;
            set => SetValue(ref _efCoreContextClasses, value);
        }

        /// <summary>
        /// This property contains the currently selected EFCORE context project.
        /// </summary>
        public string SelectedEfCoreContextProject
        {
            get => _selectedEfCoreContextProject;
            set
            {
                // Set the summary.
                _summary[nameof(SelectedEfCoreContextProject)] = $"EFCORE context project: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the associated property.
                EfCoreContextNamespaces = _table[value].Keys.ToList();

                // Set the value.
                SetValue(ref _selectedEfCoreContextProject, value);
            }
        }

        /// <summary>
        /// This property contains the selected EFCORE context project error.
        /// </summary>
        public string SelectedEfCoreContextProjectError
        {
            get => _selectedEfCoreContextProjectError;
            set => SetValue(ref _selectedEfCoreContextProjectError, value);
        }

        /// <summary>
        /// This property contains the currently selected model project.
        /// </summary>
        public string SelectedModelProject
        {
            get => _selectedModelProject;
            set
            {
                // Set the summary.
                _summary[nameof(SelectedModelProject)] = $"Model project: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the associated property.
                ModelNamespaces = _table[value].Keys.ToList();

                // Set the value.
                SetValue(ref _selectedModelProject, value);                
            }
        }

        /// <summary>
        /// This property contains the selected model project error.
        /// </summary>
        public string SelectedModelProjectError
        {
            get => _selectedModelProjectError;
            set => SetValue(ref _selectedModelProjectError, value);
        }

        /// <summary>
        /// This property contains the currently selected model namespace.
        /// </summary>
        public string SelectedEfCoreContextNamespace
        {
            get => _selectedEfCoreContextNamespace;
            set
            {
                // Set the summary.
                _summary[nameof(SelectedEfCoreContextNamespace)] = $"EFCORE context namespace: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the associated property.
                EfCoreContextClasses = _table[_selectedEfCoreContextProject][value];

                // Set the value.
                SetValue(ref _selectedEfCoreContextNamespace, value);
            }
        }

        /// <summary>
        /// This property contains the selected EFCORE context namespace error.
        /// </summary>
        public string SelectedEfCoreContextNamespaceError
        {
            get => _selectedEfCoreContextNamespaceError;
            set => SetValue(ref _selectedEfCoreContextNamespaceError, value);
        }

        /// <summary>
        /// This property contains the currently selected EFCORE context namespace.
        /// </summary>
        public string SelectedModelNamespace
        {
            get => _selectedModelNamespace;
            set
            {
                // Set the summary.
                _summary[nameof(SelectedModelNamespace)] = $"Model namespace: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the associated property.
                ModelClasses = _table[_selectedModelProject][value];

                // Set the value.
                SetValue(ref _selectedModelNamespace, value);                
            }            
        }

        /// <summary>
        /// This property contains the selected model namespace error.
        /// </summary>
        public string SelectedModelNamespaceError
        {
            get => _selectedModelNamespaceError;
            set => SetValue(ref _selectedModelNamespaceError, value);
        }

        /// <summary>
        /// This property contains the currently selected EFCORE context class.
        /// </summary>
        public string SelectedEfCoreContextClass
        {
            get => _selectedEfCoreContextClass;
            set
            {
                // Set the summary.
                _summary[nameof(SelectedEfCoreContextClass)] = $"EFCORE context class: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _selectedEfCoreContextClass, value);
            }
        }

        /// <summary>
        /// This property contains the selected EFCORE context class error.
        /// </summary>
        public string SelectedEfCoreContextClassError
        {
            get => _selectedEfCoreContextClassError;
            set => SetValue(ref _selectedEfCoreContextClassError, value);
        }

        /// <summary>
        /// This property contains the currently selected model class.
        /// </summary>
        public string SelectedModelClass
        {
            get => _selectedModelClass;
            set 
            {
                // Set the summary.
                _summary[nameof(SelectedModelClass)] = $"Model class: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the associated property(s).
                if (!string.IsNullOrEmpty(value))
                {
                    IFaceName = $"I{value}Repository";
                    ClassName = $"{value}Repository";
                }
                else
                {
                    IFaceName = $"IRepository";
                    ClassName = $"Repository";
                }

                // Set the value.
                SetValue(ref _selectedModelClass, value);                
            }
        }

        /// <summary>
        /// This property contains the selected model class error.
        /// </summary>
        public string SelectedModelClassError
        {
            get => _selectedModelClassError;
            set => SetValue(ref _selectedModelClassError, value);
        }

        /// <summary>
        /// This property contains the currently selected repo type.
        /// </summary>
        public string SelectedRepoType
        {
            get => _selectedRepoType;
            set 
            {
                // Set the summary.
                _summary[nameof(SelectedRepoType)] = $"Repository type: {value}";
                OnPropertyChanged(nameof(SummaryList));

                // Set the value.
                SetValue(ref _selectedRepoType, value);

                // Notify the dependant property.
                OnPropertyChanged(nameof(EFCoreTypeChosen));
            }
        }

        /// <summary>
        /// This property contains the selected repo type error.
        /// </summary>
        public string SelectedRepoTypeError
        {
            get => _selectedRepoTypeError;
            set => SetValue(ref _selectedRepoTypeError, value);
        }

        /// <summary>
        /// This property contains the selected page name.
        /// </summary>
        public string SelectedPageName
        {
            get => _selectedPageName;
            set => SetValue(ref _selectedPageName, value);
        }

        /// <summary>
        /// This property contains the wizard summary list.
        /// </summary>
        public ObservableCollection<string> SummaryList
        {
            // We return an observable collection so the datagrid doesn't
            //   have a nervous breakdown when the underlying data changes.
            get => new ObservableCollection<string>(_summary.Values.ToList());
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public override string OnValidate(string columnName)
        {
            // Give the base class a chance.
            var error = base.OnValidate(columnName);

            // Validate the property.
            switch (columnName)
            {
                case nameof(NameSpace):
                    error = ValidateNameSpace();
                    break;
                case nameof(ClassName):
                    error = ValidateClassName();
                    break;
                case nameof(IFaceName):
                    error = ValidateIFaceName();
                    break;
                case nameof(SelectedModelProject):
                    error = ValidateSelectedModelProject();
                    break;
                case nameof(SelectedModelNamespace):
                    error = ValidateSelectedModelNamespace();
                    break;
                case nameof(SelectedModelClass):
                    error = ValidateSelectedModelClass();
                    break;
                case nameof(SelectedRepoType):
                    error = ValidateSelectedRepoType();
                    break;
                case nameof(SelectedEfCoreContextProject):
                    error = ValidateSelectedEfCoreContextProject();
                    break;
                case nameof(SelectedEfCoreContextNamespace):
                    error = ValidateSelectedEfCoreContextNamespace();
                    break;
                case nameof(SelectedEfCoreContextClass):
                    error = ValidateSelectedEfCoreContextClass();
                    break;
            }

            // The wizard validation depends on which page is visible.
            switch (SelectedPageName)
            {
                case "step1Page":
                    if (EFCoreTypeChosen)
                    {
                        FormIsValid = (SelectedRepoTypeError ?? "").Length == 0 &&
                            (SelectedEfCoreContextProjectError ?? "").Length == 0 &&
                            (SelectedEfCoreContextNamespaceError ?? "").Length == 0 &&
                            (SelectedEfCoreContextClassError ?? "").Length == 0;
                    }
                    else
                    {
                        FormIsValid = (SelectedRepoTypeError ?? "").Length == 0;
                    }
                    break;
                case "step2Page":
                    FormIsValid = (SelectedModelProjectError ?? "").Length == 0 &&
                        (SelectedModelNamespaceError ?? "").Length == 0 &&
                        (SelectedModelClassError ?? "").Length == 0;
                    break;
                case "step3Page":
                    FormIsValid = (NameSpaceError ?? "").Length == 0 &&
                        (ClassNameError ?? "").Length == 0 &&
                        (IFaceNameError ?? "").Length == 0;
                    break;
                default:
                    FormIsValid = true;
                    break;
            }

            // Return the results.
            return error;
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method validates the repository namespace.
        /// </summary>
        /// <returns>The value of the <see cref="NameSpaceError"/> property.</returns>
        protected string ValidateNameSpace()
        {
            // No error by default.
            NameSpaceError = "";

            // Is the value empty?
            var safeValue = (NameSpace ?? "");
            if (safeValue.Contains(" "))
            {
                // Set the error.
                NameSpaceError = $"May not contain spaces!";
            }
            else
            {
                foreach (var part in safeValue.Split('.'))
                {
                    if (Regex.IsMatch(part, @"^\d"))
                    {
                        // Set the error.
                        NameSpaceError = $"{part} may not start with a number!";
                        break;
                    }
                }
            }

            // Return the results.
            return NameSpaceError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the repository class name.
        /// </summary>
        /// <returns>The value of the <see cref="ClassNameError"/> property.</returns>
        protected string ValidateClassName()
        {
            // No error by default.
            ClassNameError = "";

            // Is the value empty?
            var safeValue = (ClassName ?? "");
            if (safeValue.Contains(" "))
            {
                // Set the error.
                ClassNameError = $"May not contain spaces!";
            }
            else
            {
                foreach (var part in safeValue.Split('.'))
                {
                    if (Regex.IsMatch(part, @"^\d"))
                    {
                        // Set the error.
                        ClassNameError = $"{part} may not start with a number!";
                        break;
                    }
                }
            }

            // Return the results.
            return ClassNameError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the repository interface name.
        /// </summary>
        /// <returns>The value of the <see cref="IFaceNameError"/> property.</returns>
        protected string ValidateIFaceName()
        {
            // No error by default.
            IFaceNameError = "";

            // Is the value empty?
            var safeValue = (IFaceName ?? "");
            if (safeValue.Contains(" "))
            {
                // Set the error.
                IFaceNameError = $"May not contain spaces!";
            }
            else
            {
                foreach (var part in safeValue.Split('.'))
                {
                    if (Regex.IsMatch(part, @"^\d"))
                    {
                        // Set the error.
                        IFaceNameError = $"{part} may not start with a number!";
                        break;
                    }
                }
            }

            // Return the results.
            return IFaceNameError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the model project.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedModelProjectError"/> property.</returns>
        protected string ValidateSelectedModelProject()
        {
            // No error by default.
            SelectedModelProjectError = "";

            // Is the value empty?
            var safeValue = (SelectedModelProject ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedModelProjectError = $"Must select a project!";
            }

            // Return the results.
            return SelectedModelProjectError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the model namespace.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedModelNamespaceError"/> property.</returns>
        protected string ValidateSelectedModelNamespace()
        {
            // No error by default.
            SelectedModelNamespaceError = "";

            // Is the value empty?
            var safeValue = (SelectedModelNamespace ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedModelNamespaceError = $"Must select a namespace!";
            }

            // Return the results.
            return SelectedModelNamespaceError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the model class.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedModelClassError"/> property.</returns>
        protected string ValidateSelectedModelClass()
        {
            // No error by default.
            SelectedModelClassError = "";

            // Is the value empty?
            var safeValue = (SelectedModelClass ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedModelClassError = $"Must select a class!";
            }

            // Return the results.
            return SelectedModelClassError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the repository type.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedRepoTypeError"/> property.</returns>
        protected string ValidateSelectedRepoType()
        {
            // No error by default.
            SelectedRepoTypeError = "";

            // Is the value empty?
            var safeValue = (SelectedRepoType ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedRepoTypeError = $"Must select a repository type!";
            }

            // Return the results.
            return SelectedRepoTypeError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the EFCORE context project.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedEfCoreContextProjectError"/> property.</returns>
        protected string ValidateSelectedEfCoreContextProject()
        {
            // No error by default.
            SelectedEfCoreContextProjectError = "";

            // Is the value empty?
            var safeValue = (SelectedEfCoreContextProject ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedEfCoreContextProjectError = $"Must select a project!";
            }

            // Return the results.
            return SelectedEfCoreContextProjectError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the EFCORE context namespace.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedEfCoreContextNamespaceError"/> property.</returns>
        protected string ValidateSelectedEfCoreContextNamespace()
        {
            // No error by default.
            SelectedEfCoreContextNamespaceError = "";

            // Is the value empty?
            var safeValue = (SelectedEfCoreContextNamespace ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedEfCoreContextNamespaceError = $"Must select a namespace!";
            }

            // Return the results.
            return SelectedEfCoreContextNamespaceError;
        }

        // *******************************************************************

        /// <summary>
        /// This method validates the EFCORE context class.
        /// </summary>
        /// <returns>The value of the <see cref="SelectedEfCoreContextClassError"/> property.</returns>
        protected string ValidateSelectedEfCoreContextClass()
        {
            // No error by default.
            SelectedEfCoreContextClassError = "";

            // Is the value empty?
            var safeValue = (SelectedEfCoreContextClass ?? "");
            if (string.IsNullOrEmpty(safeValue))
            {
                // Set the error.
                SelectedEfCoreContextClassError = $"Must select a class!";
            }

            // Return the results.
            return SelectedEfCoreContextClassError;
        }

        #endregion
    }
}
