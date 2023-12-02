using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATIS.WinUi.Helpers;
public class AllDialogs
{
    public AllDialogs()
    {

    }

    #region [MessageDialogs]

    public async Task NoDatasetFoundInfoMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
            "No Dataset found !", "OK");
    }
    public static async Task SaveSuccessInfoMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
            "Save successful !", "OK");
    }
    public async Task LogInSuccessInfoMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
        "Log in successful !", "OK");
    }
    public static async Task DatasetExistInfoMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Dataset Exist !", "OK");
    }
    public async Task NameRequiredWarnMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Name is Required !", "OK");
    }
    public async Task ReferenceIncorrectWarnMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Expert, Source or Author decision not correct !", "OK");
    }
    public async Task TooMuchIdsSelectedWarnMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Only one ID should be selected !", "OK");
    }
    public static async Task DatasetNotExistInfoMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
            "Dataset Not Exist !", "OK");
    }
    public async Task NoDatasetSelectedWarnMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "No Dataset selected !", "OK");
    }
    public static async Task NoServerConnectWarnMessageDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "No Server connected !", "OK");
    }

    public static async void NoDatasetFoundWarnMessageDialog()
    {
        await App.MainRoot.MessageDialogAsync("Warning",
            "No Dataset found !", "OK");
    }
    public static async void NoFileFoundInfoWarnDialogAsync()
    {
        await App.MainRoot.MessageDialogAsync("Warning",
            "No File found !", "OK");
    }

    //-------------------------------------------------
    public async Task WarningNoPermissionMessageDialogAsync(string datasetName)
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "You don´t have permission for this area " + datasetName, "OK");

    }
    public static async Task WarningNoDatasetSelectedMessageDialogAsync(string datasetName)
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Don´t delete dataset " + datasetName, "OK");
    }
    public async Task InfoSuccessfulDeleteMessageDialogAsync(string datasetName)
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
            "Dataset " + datasetName + " successful deleted !", "OK");
    }
    public async Task InfoSuccessfulSaveMessageDialogAsync(string datasetName)
    {
        await App.MainRoot.MessageDialogAsync(
            "Information",
            "Dataset " + datasetName + " successful saved !", "OK");
    }
    public static async Task WarningNoDatabaseWithSqlServerSelectedMessageDialogAsync(string database)
    {
        await App.MainRoot.MessageDialogAsync(
            "Warning",
            "Database does not exist, SQL-Server ok " + database, "OK");
    }

    public async Task ErrorMessageDialogAsync(string message)
    {
        await App.MainRoot.MessageDialogAsync(
            "Error Warning",
            message, "OK");
    }

    //-----------------------------------------------------------

    public async Task<bool> DeleteDatasetQuestionConfirmationDialogAsync(string datasetName)
    {
        var result = await App.MainRoot.ConfirmationDialogAsync("Question ", "Do you want to delete dataset " + datasetName + "?", "Yes", "No", "");
        return result != null && (bool)result;
    }

    //-----------------------------------------------------------

    public async Task<bool> SaveDatasetQuestionConfirmationDialogAsync(string datasetName)
    {
        var result = await App.MainRoot.ConfirmationDialogAsync("Question ", "Do you want to save the dataset " + datasetName + "?", "Yes", "No", "");
        return result != null && (bool)result;
    }

    public static async Task<bool> SaveResultQuestionConfirmationDialogAsync()
    {
        var result = await App.MainRoot.ConfirmationDialogAsync("Question ", "Do you want to save ?", "Yes", "No", "");
        return result != null && (bool)result;
    }

    public static async Task<bool> CloseAppQuestionConfirmationDialogAsync(string appName)
    {
        var result = await App.MainRoot.ConfirmationDialogAsync("Question ", "Do you want to close the Application " + appName + "?", "Yes", "No", "");
        return result != null && (bool)result;
    }

    //-----------------------------------------------------------

    //-----------------------------------------------------------

    public static void ErrorMessageBox(string message, string caption)
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
        //WpfMessageBox.Show(message + "Show an error message. The icon is clickable.",
        //    caption,
        //    MessageBoxButton.OK,
        //    System.Windows.MessageBoxImage.Error);
    }

    public static void DetailErrorMessageBox(string message, string caption)
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

    public static void UrlMessageBox(string message, string caption)
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

    #endregion [MessageDialogs]

}
