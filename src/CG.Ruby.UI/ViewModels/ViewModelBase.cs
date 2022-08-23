
#region Local using statements
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#endregion

namespace CG.Ruby.UI.ViewModels
{
    /// <summary>
    /// This class is a base implementation of a view-model.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field backs the <see cref="Error"/> property.
        /// </summary>
        private string _error;

        #endregion

        // *******************************************************************
        // Events.
        // *******************************************************************

        #region Events

        /// <summary>
        /// This event is raised whenever a property value changes on the view-model.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the error message for the given property.
        /// </summary>
        /// <param name="columnName">The columnn name to use for the operation.</param>
        /// <returns></returns>
        public string this[string columnName] => OnValidate(columnName);

        // *******************************************************************

        /// <summary>
        /// This property contains the error for the view-model.
        /// </summary>
        public string Error
        {
            get => _error;
            set => SetValue(ref _error, value);
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method performs a validation for the given column (property).
        /// </summary>
        /// <param name="columnName">the column name to use for the operation.</param>
        /// <returns>The validation results for the given column.</returns>
        public virtual string OnValidate(string columnName)
        {
            return "";
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = ""
            )
        {
            // Raise the event.
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName)
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sets the value of the specified property's backing 
        /// field, then calls <see cref="OnPropertyChanged(string)"/> on behalf
        /// of the property.
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="backingField">The backing field associated with the property.</param>
        /// <param name="value">The value to set in the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        protected virtual void SetValue<T>(
            ref T backingField,
            T value,
            [CallerMemberName] string propertyName = null
            )
        {
            // Is the new value same as the old value?
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return; // Nothing to do!
            }

            // Set the value of the backing field.
            backingField = value;

            // Tell the world what we did.
            OnPropertyChanged(
                propertyName
                );
        }
                
        #endregion
    }
}
