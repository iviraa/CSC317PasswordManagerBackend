/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: Handles the AddPasswordView page for adding new passwords.

*/

using System.Diagnostics.Metrics;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];
        
        //Determines if a password was generated at least once.
        generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        //Called when the Cancel button is clicked.
        Navigation.PopAsync();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        //Called when the Submit button is clicked for a password manually
        //entered.  (i.e., existing password).
        string platform = txtNewPlatform.Text;
        string username = txtNewUserName.Text;
        string password = txtNewPassword.Text;
        string verifyPassword = txtNewPasswordVerify.Text;

        string errorMessage = lblErrorExistingPassword.Text;

        // If any field is empty prompt error message
        if (string.IsNullOrEmpty(platform) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(verifyPassword))
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "PLease fill all fields";
            DisplayAlert("Error", "Please fill out all the fields.", "OK");
            return;
        }

        // If passwords do not match, return error message
        if (password != verifyPassword)
        {
            lblErrorExistingPassword.IsVisible = true;
            lblErrorExistingPassword.Text = "Password doesnt match";
            DisplayAlert("Error", "Passwords do not match", "OK");
            return;
        }

        App.PasswordController.AddPassword(platform, username, password);
        Navigation.PushAsync(new PasswordListView());

    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        //Called when the submit button for a Generated password is clicked.
        string platform = txtNewPlatform.Text;
        string username = txtNewUserName.Text;
        string password = lblGenPassword.Text;

        // If password has not been generated, return error message
        if (password == "<No Password Generated>")
        {
            lblErrorGeneratedPassword.IsVisible = true;
            lblErrorGeneratedPassword.Text = "No Password generated yet";
            return;
        }

        // If any field is empty, return error message
        if (string.IsNullOrEmpty(platform) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            lblErrorGeneratedPassword.IsVisible = true;
            lblErrorGeneratedPassword.Text = "Please fill all fields";
            return;
        }

        App.PasswordController.AddPassword(platform, username, password);
        Navigation.PushAsync(new PasswordListView());

    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        //Called when the Generate Password button is clicked.
        bool includeUppercase = chkUpperLetter.IsChecked;
        bool includeDigits = chkDigit.IsChecked;
        bool includeSymbols = chkSymbols.IsChecked;
        string symbols = txtReqSymbols.Text;
        string reqSymbols = txtReqSymbols.Text;
        int minLength = (int)sprPassLength.Value;

        string generatedPassword = PasswordGeneration.BuildPassword(includeUppercase, includeDigits, reqSymbols, minLength);

        lblGenPassword.Text = generatedPassword;
    }
}