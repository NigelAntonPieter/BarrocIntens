// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using BarrocIntens.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace BarrocIntens
{
    public sealed partial class ClientNotesListWindow : Window
    {
        private int userId;
        private List<Note> userNotes;
        public DateTimeOffset AppointmentDate { get; set; }

        public ClientNotesListWindow(int userId)
        {
            this.userId = userId;
            this.InitializeComponent();
            LoadUserNotes();
        }

        private void LoadUserNotes()
        {
            using (var db = new AppDbContext())
            {
                userNotes = db.Notes.Where(n => n.UserId == userId)
                    .Include(m => m.Company)
                    .ToList();

                notesGridView.ItemsSource = userNotes;
            }
        }

        private void CreateNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateClientNoteWindow(userId);
            window.Closed += Window_Closed;
            window.Activate();
        }

        private void EditNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ClientNotesWindow(userId);
            window.Closed += Window_Closed;
            window.Activate();
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            LoadUserNotes();
        }

    }
}
