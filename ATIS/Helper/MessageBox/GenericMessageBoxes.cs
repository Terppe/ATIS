using System.Windows;

namespace ATIS.Ui.Helper.MessageBox
{
    public class GenericMessageBoxes<T>
    {
        public bool NoDatasetSelectedInfoMessageBox(T selectedName)
        {
            if (selectedName == null)
            {
                System.Windows.MessageBox.Show("Select Dataset",
                    "Required",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            return false;
        }

    }
}
