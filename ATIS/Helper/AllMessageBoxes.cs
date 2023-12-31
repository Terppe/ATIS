﻿using System;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Media;
using ATIS.Ui.Helper.MessageBox;

namespace ATIS.Ui.Helper
{

    public class AllMessageBoxes : ViewModelBase
    {

        public AllMessageBoxes()
        {
            InitMessageBox();
            //this.PlaySystemSounds = true;

        }

        private void InitMessageBox()
        {
            // font family name is validate against fonts installed in windows.
            //WPFMessageEx.SetFont("Verdana", 14);

            //MessageBoxEx.SetMessageForeground(Colors.White);
            //MessageBoxEx.SetMessageBackground(Colors.MediumTurquoise);
            ////     MessageBoxEx.SetButtonBackground(MessageBoxEx.ColorFromString("#333333"));
            //MessageBoxEx.SetButtonBackground(Colors.DarkGray);

            //// template name is validated and if not found in your app, will not be applied
            //MessageBoxEx.SetButtonTemplateName("AefCustomButton");

            //// default max width is the width of the primary screen's work area minus 100 pixels
            //MessageBoxEx.SetMaxFormWidth(1000);

            // if you want to make the messagebox silent when you use icons, uncomment the next line
            //MessageBoxEx.SetAsSilent(true);

        }

        #region [MessageBoxes]

        public bool IdSelectInComboBoxNotBe0InfoMessageBox(int? id)
        {
            if (id != 0) return false;
            //System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
            //    MessageBoxButton.OK, MessageBoxImage.Information);
            WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return true;
        }

        public bool IdSelectInComboBoxMoreThenOneInfoMessageBox()
        {
            //System.Windows.MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredComboSelect,
            //    MessageBoxButton.OK, MessageBoxImage.Information);
            WpfMessageBox.Show("Auswahl Combobox", "Mehr als einen gewählt",
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
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
            WpfMessageBox.Show(CultRes.StringsRes.DatasetSelect, CultRes.StringsRes.RequiredComboSelect,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return true;
        }

        public bool NoDatasetFoundInfoMessageBox(int collectionCount)
        {
            if (collectionCount != 0) return false;
            WpfMessageBox.Show(CultRes.StringsRes.DatasetNotExist, CultRes.StringsRes.DatasetNot,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return true;
        }

        public bool DoNotDeleteDatasetInfoMessageBox(int collectionCount, string caption)
        {
            if (collectionCount <= 0) return false;
            WpfMessageBox.Show(CultRes.StringsRes.DeleteNot,
                caption + " " + CultRes.StringsRes.ConnectedDataset,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            return true;
        }

        public bool DeleteDatasetQuestionMessageBox(string caption)
        {
            return WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion,
                caption,
                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        public bool SaveDatasetQuestionMessageBox(string caption)
        {
            return WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, caption,
                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes;
        }

        //------------------------------------------------------------------
        public void InfoMessageBox(string message, string caption)
        {
            //System.Windows.MessageBox.Show(message, caption,
            //    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            WpfMessageBox.Show(message, caption,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public void WarningMessageBox(string message, string caption)
        {
            WpfMessageBox.Show(message, caption,
                MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }

        public void ErrorMessageBox(string message, string caption)
        {
            //   MessageBoxEx.SetErrorDelegate(this.errorDelegate, true);
            //MessageBoxEx.Show("Show an error message. The icon is clickable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            //var ext = new MsgBoxExtendedFunctionality()
            //{
            //    MessageDelegate = _errorDelegate
            //    ,
            //    ExitAfterAction = true
            //};
            //    MessageBoxEx.ShowEx("Show an error message. The icon is clickable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error, ext);

            //// show clickable error icon
            //MessageBoxEx.SetErrorDelegate(this._errorDelegate);
            WpfMessageBox.Show(message + "Show an error message. The icon is clickable.",
                caption,
                MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Error);
        }

        public void DetailErrorMessageBox(string message, string caption)
        {
            //if (_checkboxData != null && !_checkboxData.CheckBoxIsChecked)
            //{
            //    // show box with checkbox and details 
            //    //MessageBoxEx.ShowWithCheckBoxAndDetails("This is possibly pointless and can be permenantly dismissed by clicking the checkbox below." 
            //    //										, this.checkboxData, this.Testmsg(), "Checkbox and Details Sample"
            //    //										, MessageBoxButton.OK, MessageBoxImage.Information);

            //    var ext = new MsgBoxExtendedFunctionality()
            //    {
            //        CheckBoxData = _checkboxData
            //        ,
            //        DetailsText = Testmsg()
            //    };
            //    MessageBoxEx.ShowEx("This is possibly pointless and can be permenantly dismissed by clicking the checkbox below."
            //        , "Checkbox and Details Sample"
            //        , MessageBoxButton.OK, MessageBoxImage.Information, ext);
            //}
            //else
            //{
            //    WPFMessageBox.Show("But you said not to show the message anymore. Make up your mind.", "Hypocrite Notice");
            //}
        }

        public void UrlMessageBox(string message, string caption)
        {
            //_checkboxData.CheckBoxIsChecked = false;
            //var ext = new MsgBoxExtendedFunctionality()
            //{
            //    URL = new MsgBoxUrl()
            //    {
            //        URL = new Uri("http://www.google.com")
            //        ,
            //        DisplayName = "Google"
            //        ,
            //        Foreground = Colors.LightBlue
            //    }
            //};
            //MessageBoxEx.ShowEx("Example showing a clickable URL.", "URL Example", ext);

        }

        public void DetailCheckUrlErrorMessageBox(string message, string caption)
        {
            //var ext = new MsgBoxExtendedFunctionality()
            {
                //    // clickable icon
                //    MessageDelegate = _errorDelegate,
                //    ExitAfterAction = true,
                //    // you can also set the tooltip text, but for the purposes of this demo, we're 
                //    // using the default value
                //    //,DelegateToolTip = null

                //    // checkbox
                //    CheckBoxData = _checkboxData,

                //    // details
                //    DetailsText = Testmsg(),

                //    // url
                //    URL = new MsgBoxUrl()
                //    {
                //        URL = new Uri("http://www.google.com"),
                //        DisplayName = "Google",
                //        Foreground = Colors.LightBlue
                //    }
                //};
                //MessageBoxEx.ShowEx("Example showing a clickable icon, a checkbox, details, and a clickable URL, all in the same messagebox."
                //    , "Full Monte"
                //    , MessageBoxImage.Information, ext);
            }

            //private string Testmsg()
            //{
            //    StringBuilder msg = new StringBuilder();
            //    msg.AppendLine("using System;");
            //    msg.AppendLine("using System.Collections.Generic;");
            //    msg.AppendLine("using System.Linq;");
            //    msg.AppendLine("using System.Text;");
            //    msg.AppendLine("using System.Threading.Tasks;");
            //    msg.AppendLine("using System.Windows;");
            //    msg.AppendLine("using System.Windows.Controls;");
            //    msg.AppendLine("using System.Windows.Data;");
            //    msg.AppendLine("using System.Windows.Documents;");
            //    msg.AppendLine("using System.Windows.Input;");
            //    msg.AppendLine("using System.Windows.Media;");
            //    msg.AppendLine("using System.Windows.Media.Imaging;");
            //    msg.AppendLine("using System.Windows.Navigation;");
            //    msg.AppendLine("using System.Windows.Shapes;");
            //    msg.AppendLine("using WpfCommon.Controls;");
            //    msg.AppendLine("using ObjectExtensions;");
            //    msg.AppendLine("using EntityFactory.ViewModels;");
            //    msg.AppendLine("using System.Windows.Controls.Primitives;");
            //    msg.AppendLine("using System.Diagnostics;");
            //    msg.AppendLine("using System.ComponentModel;");
            //    msg.AppendLine("using System.Threading;");
            //    msg.AppendLine("using EntityFactory.Common;");
            //    msg.AppendLine("using EntityFactory.Database;");
            //    msg.AppendLine("using EntityFactory.Windows;");
            //    msg.AppendLine("using PaddedwallDAL;");
            //    msg.AppendLine(
            //        "public VMStoredProcItems StoredProcedures  { get { return this.storedProcedures;  } set { if (value != this.storedProcedures ) { this.storedProcedures  = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "public VMDbNameItem      DBName            { get { return this.dbName;            } set { if (value != this.dbName           ) { this.dbName = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "//public VMDbNameItems     DBNames           { get { return this.dbNames;           } set { if (value != this.dbNames          ) { this.dbNames = value; this.NotifyPropertyChanged(); this.NotifyPropertyChanged(\"CanEnableCombo\"); } } }");
            //    msg.AppendLine(
            //        "public string            CombinedClassName { get { return this.combinedClassName; } set { if (value != this.combinedClassName) { this.combinedClassName = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "public bool              CanEnablePage     { get { return this.canEnablePage;     } set { if (value != this.canEnablePage    ) { this.canEnablePage     = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "//public bool              CanEnableCombo    { get { return (this.DBNames != null && this.DBNames.Count > 0); } }");
            //    msg.AppendLine(
            //        "public bool              EnableShowButtons { get { return this.enableShowButtons; } set { if (value != this.enableShowButtons) { this.enableShowButtons = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine("public Visibility        Waiting");
            //    msg.AppendLine("{");
            //    msg.AppendLine("	get { return this.waiting;           }");
            //    msg.AppendLine(
            //        "	set { if (value != this.waiting          ) { this.waiting           = value; this.NotifyPropertyChanged(); } }");
            //    msg.AppendLine("}");
            //    msg.AppendLine(
            //        "public int               ListViewColSpan { get { return (this.cbShowHelp.IsChecked == true) ? 1 : 3; } }");
            //    msg.AppendLine("using System;");
            //    msg.AppendLine("using System.Collections.Generic;");
            //    msg.AppendLine("using System.Linq;");
            //    msg.AppendLine("using System.Text;");
            //    msg.AppendLine("using System.Threading.Tasks;");
            //    msg.AppendLine("using System.Windows;");
            //    msg.AppendLine("using System.Windows.Controls;");
            //    msg.AppendLine("using System.Windows.Data;");
            //    msg.AppendLine("using System.Windows.Documents;");
            //    msg.AppendLine("using System.Windows.Input;");
            //    msg.AppendLine("using System.Windows.Media;");
            //    msg.AppendLine("using System.Windows.Media.Imaging;");
            //    msg.AppendLine("using System.Windows.Navigation;");
            //    msg.AppendLine("using System.Windows.Shapes;");
            //    msg.AppendLine("using WpfCommon.Controls;");
            //    msg.AppendLine("using ObjectExtensions;");
            //    msg.AppendLine("using EntityFactory.ViewModels;");
            //    msg.AppendLine("using System.Windows.Controls.Primitives;");
            //    msg.AppendLine("using System.Diagnostics;");
            //    msg.AppendLine("using System.ComponentModel;");
            //    msg.AppendLine("using System.Threading;");
            //    msg.AppendLine("using EntityFactory.Common;");
            //    msg.AppendLine("using EntityFactory.Database;");
            //    msg.AppendLine("using EntityFactory.Windows;");
            //    msg.AppendLine("using PaddedwallDAL;");
            //    msg.AppendLine(
            //        "public VMStoredProcItems StoredProcedures  { get { return this.storedProcedures;  } set { if (value != this.storedProcedures ) { this.storedProcedures  = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "public VMDbNameItem      DBName            { get { return this.dbName;            } set { if (value != this.dbName           ) { this.dbName = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "//public VMDbNameItems   DBNames           { get { return this.dbNames;           } set { if (value != this.dbNames          ) { this.dbNames = value; this.NotifyPropertyChanged(); this.NotifyPropertyChanged(\"CanEnableCombo\"); } } }");
            //    msg.AppendLine(
            //        "public string            CombinedClassName { get { return this.combinedClassName; } set { if (value != this.combinedClassName) { this.combinedClassName = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "public bool              CanEnablePage     { get { return this.canEnablePage;     } set { if (value != this.canEnablePage    ) { this.canEnablePage     = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine(
            //        "//public bool            CanEnableCombo    { get { return (this.DBNames != null && this.DBNames.Count > 0); } }");
            //    msg.AppendLine(
            //        "public bool              EnableShowButtons { get { return this.enableShowButtons; } set { if (value != this.enableShowButtons) { this.enableShowButtons = value; this.NotifyPropertyChanged(); } } }");
            //    msg.AppendLine("public Visibility        Waiting");
            //    msg.AppendLine("{");
            //    msg.AppendLine("	get { return this.waiting;           }");
            //    msg.AppendLine(
            //        "	set { if (value != this.waiting          ) { this.waiting           = value; this.NotifyPropertyChanged(); } }");
            //    msg.AppendLine("}");
            //    msg.AppendLine(
            //        "public int               ListViewColSpan { get { return (this.cbShowHelp.IsChecked == true) ? 1 : 3; } }");
            //    msg.AppendLine("//==================================== END OF TEST MSG ===================");
            //    return msg.ToString();
            //}


            #endregion

        }
    }
}
