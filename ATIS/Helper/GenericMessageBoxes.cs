using System.Windows;

namespace ATIS.Ui.Helper
{
    public class GenericMessageBoxes<T>
    {
        public bool NoDatasetSelectedInfoMessageBox(T selectedName)
        {
            if (selectedName == null)
            {
                System.Windows.MessageBox.Show(CultRes.StringsRes.DatasetNotExist, CultRes.StringsRes.DatasetNot,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }


        public bool RefIdSelectInComboBoxNotBeNullInfoMessageBox(int? refExpertId, int? refSourceId, int? refAuthorId)
        {
            if (refExpertId == null &&
                refSourceId == null &&
                refAuthorId == null)
            {
                System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        public bool IdSelectInComboBoxNotBe0InfoMessageBox(int? id)
        {
            if (id == 0)
            {
                System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }
    }

}