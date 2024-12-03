/*

Program Author: Chetanchal Saud

USM wID: w10171032

Assignment: Password Manager, PT2, Backend

Description: This class manages the login view and handles input authentication.

*/

using CSC317PassManagerP2Starter.Modules.Controllers;
namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void ProcessLogin(object sender, EventArgs e)
    {
        //Complete Process Login Functionality.  Called by Submit Button
        string username = txtUserName.Text;
        string password = txtPassword.Text;

        if (username == null || password == null)
        {
            ShowErrorMessage("Form Incomplete : Please fill all the fields");
            return;
        }

        var ErrorMessage = App.LoginController.Authenticate(username, password);

        // If no error exists
        if (ErrorMessage == AuthenticationError.NONE)
        {
            Navigation.PushAsync(new PasswordListView());
        }

        // If username or password is invalid.
        if (ErrorMessage == AuthenticationError.INVALIDUSERNAME)
        {
            ShowErrorMessage("Invalid Username : Username does not exist");
        }
        else if ( ErrorMessage == AuthenticationError.INVALIDPASSWORD) 
        {
            ShowErrorMessage("Invalid Password : Password does not match the username");
        }
    }

    private void ShowErrorMessage(string message)
    {
        //Complete ShowError Message functionality.  Supports ProcessLogin.
        lblError.Text = message;
        lblError.IsVisible = true;
    }
}