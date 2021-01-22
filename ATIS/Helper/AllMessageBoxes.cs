using System;
using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Media;
using ATIS.Dal.Models;
using ATIS.Ui.Helper.MsgBoxes;

namespace ATIS.Ui.Helper
{
    public class ErrorMsgDelegate : MsgBoxExDelegate
    {
        public override MessageBoxResult PerformAction(string message, string details = null)
        {
            Message = message;
            Details = details;
            MessageDate = DateTime.Now;

            // for this sample, we're just showing another messagebox
            var result = MessageBoxEx.Show("You're about to do something because this is an error message (clicking yes with play a beep sound). Are you sure?",
                "Send Error Message To Support",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //indicate that they clicked yes
                SystemSounds.Beep.Play();
            }
            return result;
        }
    }

    public class AllMessageBoxes : ViewModelBase
    {
        readonly MsgBoxExCheckBoxData _checkboxData = new MsgBoxExCheckBoxData() { CheckBoxIsChecked = false, CheckBoxText = "Don't show this message any more" };
        readonly ErrorMsgDelegate _errorDelegate = new ErrorMsgDelegate();

        private bool _playSystemSounds;
        public bool PlaySystemSounds
        {
            get => _playSystemSounds;
            set
            {
                if (value != _playSystemSounds)
                {
                    _playSystemSounds = value;
                    //           NotifyPropertyChanged();
                    MessageBoxEx.SetAsSilent(!_playSystemSounds);
                }
            }
        }

        public AllMessageBoxes()
        {
            InitMessageBox();

        }

        private void InitMessageBox()
        {
            //  MessageBoxEx.SetParentWindow(this);
            MessageBoxEx.SetMessageForeground(Colors.White);
            //MessageBoxEx.SetMessageBackground(Colors.Black);
            MessageBoxEx.SetMessageBackground(Colors.DarkTurquoise);
            //MessageBoxEx.SetButtonBackground(MessageBoxEx.ColorFromString("#333333"));
            MessageBoxEx.SetButtonBackground(Colors.DarkGray);
            MessageBoxEx.SetButtonTemplateName("AefCustomButton");
            MessageBoxEx.SetMaxFormWidth(600);
            MessageBoxEx.SetErrorDelegate(new ErrorMsgDelegate());
            // if you want to make the messagebox silent when you use icons, uncomment the next line
            //MessageBoxEx.SetAsSilent(true);
        }

        #region [MessageBoxes]

        public bool IdSelectInComboBoxNotBe0InfoMessageBox(int? id)
        {
            if (id != 0) return false;
            //System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
            //    MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBoxEx.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
        //public bool CheckIfDatasetAlreadyExistInfoMessageBox(int collectionCount, int? id, string caption)
        //{
        //    if (collectionCount != 0 && id == 0) return false;
        //    MessageBoxEx.Show(CultRes.StringsRes.DatasetExist, caption,
        //        MessageBoxButton.OK, MessageBoxImage.Information);
        //    return true;
        //}

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
            MessageBoxEx.Show(CultRes.StringsRes.DatasetSelect, CultRes.StringsRes.RequiredComboSelect,
               MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool NoDatasetFoundInfoMessageBox(int collectionCount)
        {
            if (collectionCount != 0) return false;
            MessageBoxEx.Show(CultRes.StringsRes.DatasetNotExist, CultRes.StringsRes.DatasetNot,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }
        public bool DoNotDeleteDatasetInfoMessageBox(int collectionCount, string caption)
        {
            if (collectionCount <= 0) return false;
            MessageBoxEx.Show(CultRes.StringsRes.DeleteNot,
                caption + " " + CultRes.StringsRes.ConnectedDataset,
                MessageBoxButton.OK, MessageBoxImage.Information);
            return true;
        }

        public bool DeleteDatasetQuestionMessageBox(string caption)
        {
            return MessageBoxEx.Show(CultRes.StringsRes.DeleteQuestion,
                caption + " " + CultRes.StringsRes.ConnectedDataset,
                MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        public bool SaveDatasetQuestionMessageBox(string caption)
        {
            return MessageBoxEx.Show(CultRes.StringsRes.SaveQuestion2, caption,
                       MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes;
        }
        //------------------------------------------------------------------
        public void InfoMessageBox(string message, string caption)
        {
            //System.Windows.MessageBox.Show(message, caption,
            //    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            MessageBoxEx.Show(message, caption,
               MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void WarningMessageBox(string message, string caption)
        {
            MessageBox.Show(message, caption,
               MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ErrorMessageBox(string message, string caption)
        {
            // show clickable error icon
            MessageBoxEx.SetErrorDelegate(this._errorDelegate);
            MessageBoxEx.Show(message + "Show an error message. The icon is clickable.",
                caption,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        public void DetailErrorMessageBox(string message, string caption)
        {
            if (this._checkboxData != null && !this._checkboxData.CheckBoxIsChecked)
            {
                // show box with checkbox and details 
                MessageBoxEx.ShowWithCheckBoxAndDetails(
                    "This is possibly pointless and can be permenantly dismissed by clicking the checkbox below.",
                    _checkboxData,
                    message,
                    caption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBoxEx.Show("But you said not to show the message anymore. Make up your mind.", "Hypocrite Notice");
            }
        }


        #endregion

    }
}
