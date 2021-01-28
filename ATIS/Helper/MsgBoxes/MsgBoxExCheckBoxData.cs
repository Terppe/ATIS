using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ATIS.Ui.Helper.MsgBoxes
{
	// This class doesn't have to be inherted because its use is highly specific.
	/// <summary>
	/// Reresents the object that allows the checkbox state to be discoevered externally of the 
	/// messagebox.
	/// </summary>
	public class MsgBoxExCheckBoxData : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		private bool isModified = false;
		public bool IsModified { get { return this.isModified; } set { if (value != this.isModified) { this.isModified = true; this.NotifyPropertyChanged(); } } }
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Notifies that the property changed, and sets IsModified to true.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				if (propertyName != "IsModified")
				{
					this.IsModified = true;
				}
            }
        }
	
		#endregion INotifyPropertyChanged

		private string _checkBoxText;
		private bool   _checkBoxIsChecked;

		/// <summary>
		/// Get/set the text content of the checkbox
		/// </summary>
		public string CheckBoxText      { get { return this._checkBoxText;      } set { if (value != this._checkBoxText     ) { this._checkBoxText      = value; this.NotifyPropertyChanged(); } } }
		/// <summary>
		/// Get/set the flag that indicates whether the checkbox is checked
		/// </summary>
		public bool   CheckBoxIsChecked { get { return this._checkBoxIsChecked; } set { if (value != this._checkBoxIsChecked) { this._checkBoxIsChecked = value; this.NotifyPropertyChanged(); } } }
	}
}
