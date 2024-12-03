/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: Displays list of passwords, search bar and defines edit, copy and delete function.

*/

using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        //once logged in, generate a set of test passwords for the user.
        App.PasswordController.GenTestPasswords();

        //Populates the list of shown passwords  This should also be called in the search
        //bar event method to implement the search filter.
        App.PasswordController.PopulatePasswordView(_rows);

        //Binds the Collection View to the password rows.
        collPasswords.ItemsSource = _rows;
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        //Is binded to the Search Bar.  Calls PopulatePasswords from the Password Controller.
        //to update the list of shown passwords.
        string searchText = e.NewTextValue;
        App.PasswordController.PopulatePasswordView(_rows, searchText);

    }

    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        //Is called when Copy is clicked.  Looks up the password by its ID
        //and copies it to the clipboard.

        //Example of how to get the ID of the password selected.
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        var passwordRow = _rows.FirstOrDefault(r => r.PasswordID == ID);
        if (passwordRow != null)
        {
            Clipboard.SetTextAsync(passwordRow.PlatformPassword);
            DisplayAlert("Successful", "The password has been successfully copied to clipboard.", "OK");
        }
    }

    private void EditPassword(object sender, EventArgs e)
    {
        var btnSender = (sender as Button);
        int ID = Convert.ToInt32(btnSender.CommandParameter);
        var passwordRow = _rows.FirstOrDefault(r => r.PasswordID == ID);


        if (passwordRow.Editing)
        {
            passwordRow.SavePassword();
            passwordRow.Editing = false;
            btnSender.Text = "Edit";
            DisplayAlert("Successful", "The password has been successfully edited.", "OK");
        }
        else
        {
            passwordRow.Editing = true;
            btnSender.Text = "Save";
           
        }
    }

    private void DeletePassword(object sender, EventArgs e)
    {
        //Called when Delete is clicked.
        var btnSender = (sender as Button);
        int ID = Convert.ToInt32(btnSender.CommandParameter);
        var passwordRow = _rows.FirstOrDefault(r => r.PasswordID == ID);

        _rows.Remove(passwordRow);
        App.PasswordController.RemovePassword(passwordRow.PasswordID);

        DisplayAlert("Successful", "The password has been successfully deleted.", "OK");
    }

    private void ButtonAddPassword(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddPasswordView());
    }
}