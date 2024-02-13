using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RSOApp;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }

    private void LoginAttempt(object? sender, RoutedEventArgs e)
    {
        if (LoginTextBox.Text == "admin" && PasswordTextBox.Text == "admin")
        {
            MainMenu menu = new RSOApp.MainMenu();
            this.Hide();
            menu.Show(); 
        }
        else
        {
            LoginError.IsVisible = true;
        }
    }

    private void AppExit(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}