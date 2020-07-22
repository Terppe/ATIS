using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ATIS.Ui.Views.Database.DatabaseHelper
{
    public class GenericMessageBoxes<T>
    {
        public bool NoDatasetSelectedInfoMessageBox(T selectedName)
        {
            if (selectedName == null)
            {
                MessageBox.Show("Select Dataset",
                    "Required",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            return false;
        }

    }
}
