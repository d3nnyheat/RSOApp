using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;

namespace RSOApp;

public partial class MembersForm : Window
{
    public MembersForm()
    {
        InitializeComponent();
        string fullTableShow = "SELECT * FROM members;";
        ShowTable(fullTableShow);
        FiltrTable();
    }

    private List<Members> member;
    private string connString = "server=localhost;database=rso;User Id=root;password=landoNorris4";
    private MySqlConnection conn;
    private void ShowTable(string sql)
    {
        member = new List<Members>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentMembers = new Members()
            {
                ID = reader.GetInt32("ID"),
                Surname  = reader.GetString("Surname"),
                Name = reader.GetString("Name"),
                ID_Squad = reader.GetInt32("ID_Squad"),
                Age = reader.GetInt32("Age"),
                Phone = reader.GetString("Phone"),
                Adress = reader.GetString("Adress")
            };
            member.Add(currentMembers);
        }
        conn.Close();
        MemberGrid.ItemsSource = member;
    }

    private void Search_OnClick(object? sender, RoutedEventArgs e)
    {
        string searchText = "SELECT * FROM members  WHERE Name LIKE '%" + Search1.Text + "%' AND Surname LIKE '%" + Search2.Text + "%'";
        ShowTable(searchText);
    }
    private void FiltrTable()
    {
        member = new List<Members>();
        conn = new MySqlConnection(connString);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Members", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentMembers = new Members()
            {
                ID = reader.GetInt32("ID"),
                Surname  = reader.GetString("Surname"),
                Name = reader.GetString("Name"),
                ID_Squad = reader.GetInt32("ID_Squad"),
                Age = reader.GetInt32("Age"),
                Phone = reader.GetString("Phone"),
                Adress = reader.GetString("Adress")
            };
            member.Add(currentMembers);
        }
        conn.Close();
        var typecmb = this.Find<ComboBox>(name:"FiltrComboBox");
        typecmb.ItemsSource = member;
    }

    private void FiltrTable_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var TypeCmB = (ComboBox)sender;
        var currentMember = TypeCmB.SelectedItem as Members;
        var fltrmember = member
            .Where(x => x.ID == currentMember.ID)
            .ToList();
        MemberGrid.ItemsSource = fltrmember;
    }

    private void ResetTable_OnClick(object? sender, RoutedEventArgs e)
    {
        string resetTable = "SELECT * FROM members;";
        ShowTable(resetTable);
        Search1.Text = string.Empty;
        Search2.Text = string.Empty;
    }

    private void AddData(object? sender, RoutedEventArgs e)
    {
        Members newMember = new Members();
        RSOApp.AddUpdate addWindow = new AddUpdate(newMember, member);
        addWindow.Show();
        this.Close();
    }

    private void EditData(object? sender, RoutedEventArgs e)
    {
        Members currentMember = MemberGrid.SelectedItem as Members;
        if (currentMember == null)
        {
            return;
        }
        RSOApp.AddUpdate updateWindow = new AddUpdate(currentMember, member);
        updateWindow.Show();
        this.Close();
    }

    private void DeleteData(object? sender, RoutedEventArgs e)
    {
        try
        {
            Members currentMember = MemberGrid.SelectedItem as Members;
            if (currentMember == null)
            {
                return;
            }
            conn = new MySqlConnection(connString);
            conn.Open();
            string sql = "DELETE FROM Members WHERE ID = " + currentMember.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            member.Remove(currentMember);
            ShowTable("SELECT * FROM Members;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void BackToMenu(object? sender, RoutedEventArgs e)
    {
        MainMenu menu = new MainMenu();
        Close();
        menu.Show();
    }
}