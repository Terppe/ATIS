using System.Windows;

namespace ATIS.Ui.Views.Database.DatabaseHelper
{
    public class AllMessageBoxes
    {
        #region [MessageBoxes]

        //public bool NoDatasetSelectedInfoMessageBox(Tbl03Regnum SelectedRegnum)
        //{
        //    if (SelectedRegnum == null)
        //    {
        //        MessageBox.Show("Select Dataset",
        //            "Required",
        //            MessageBoxButton.OK, MessageBoxImage.Information);
        //        return true;
        //    }
        //    return false;
        //}
        //public bool NoDatasetSelectedInfoMessageBox(Tbl90Reference selectedReferenceExpert)
        //{
        //    if (selectedReferenceExpert == null)
        //    {
        //        MessageBox.Show("Select Dataset",
        //            "Required",
        //            MessageBoxButton.OK, MessageBoxImage.Information);
        //        return true;
        //    }
        //    return false;
        //}

        //public bool NoDatasetSelectedInfoMessageBox(Tbl93Comment selectedComment)
        //{
        //    if (selectedComment == null)
        //    {
        //        MessageBox.Show("Select Dataset",
        //            "Required",
        //            MessageBoxButton.OK, MessageBoxImage.Information);
        //        return true;
        //    }
        //    return false;
        //}

        public bool NoDatasetFoundInfoMessageBox(int collectionCount)
        {
            if (collectionCount == 0)
            {
                MessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
        public bool DoNotDeleteDatasetInfoMessageBox(int collectionCount, string caption)
        {
            if (collectionCount > 0)
            {
                MessageBox.Show("Not to Delete",
                    caption + " " + "ConnectedDataset",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }


        public bool DeleteDatasetQuestionMessageBox(string caption)
        {
            return MessageBox.Show("Wollen Sie Datensätze löschen ?",
                caption + " " + "ConnectedDataset",
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        public bool SaveDatasetQuestionMessageBox(string caption)
        {
            return MessageBox.Show(CultRes.StringsRes.SaveQuestion2, caption,
                       MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
        }
        //------------------------------------------------------------------
        public void InfoMessageBox(string message, string caption)
        {
            MessageBox.Show(message, caption,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void WarningMessageBox(string message, string caption)
        {
            MessageBox.Show(message, caption,
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        #endregion

    }
}
