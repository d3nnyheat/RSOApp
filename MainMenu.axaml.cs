using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace RSOApp;

public partial class MainMenu : Window
{
    public MainMenu()
    {
        InitializeComponent();
    }

    private void MembersButton_OnClick(object? sender, RoutedEventArgs e)
    {
        MembersForm membersForm = new MembersForm();
        this.Hide();
        membersForm.Show();
    }

    private void Logon_OnClick(object? sender, RoutedEventArgs e)
    {
        LoginWindow lw = new LoginWindow();
        this.Hide();
        lw.Show();
    }
}