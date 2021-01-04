using System.Windows;

namespace ATIS.Ui.Helper
{
    public class AllMessageBoxes
    {
        #region [MessageBoxes]

        public bool IdSelectInComboBoxNotBe0InfoMessageBox(int? id)
        {
            if (id != 0) return false;
            System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        //public bool RefIdSelectInComboBoxNotBeNullInfoMessageBox(int? refExpertId, int? refSourceId, int? refAuthorId)
        //{
        //    if (refExpertId == 0 &&
        //        refSourceId == 0 &&
        //        refAuthorId == 0)
        //    {
        //        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
        //            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        //        return true;
        //    }
        //    return false;
        //}

        public bool NoDatasetSelectedInfoMessageBox(object selected)
        {
            if (selected != null) return false;
            System.Windows.MessageBox.Show(CultRes.StringsRes.DatasetSelect, CultRes.StringsRes.RequiredComboSelect,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool NoDatasetFoundInfoMessageBox(int collectionCount)
        {
            if (collectionCount != 0) return false;
            System.Windows.MessageBox.Show(CultRes.StringsRes.DatasetNotExist, CultRes.StringsRes.DatasetNot,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
        public bool DoNotDeleteDatasetInfoMessageBox(int collectionCount, string caption)
        {
            if (collectionCount <= 0) return false;
            System.Windows.MessageBox.Show(CultRes.StringsRes.DeleteNot,
                caption + " " + CultRes.StringsRes.ConnectedDataset,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool DeleteDatasetQuestionMessageBox(string caption)
        {
            return System.Windows.MessageBox.Show(CultRes.StringsRes.DeleteQuestion,
                caption + " " + CultRes.StringsRes.ConnectedDataset,
                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        public bool SaveDatasetQuestionMessageBox(string caption)
        {
            return System.Windows.MessageBox.Show(CultRes.StringsRes.SaveQuestion2, caption,
                       MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes;
        }
        //------------------------------------------------------------------
        public void InfoMessageBox(string message, string caption)
        {
            System.Windows.MessageBox.Show(message, caption,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public void WarningMessageBox(string message, string caption)
        {
            System.Windows.MessageBox.Show(message, caption,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }


        #endregion

    }
}
