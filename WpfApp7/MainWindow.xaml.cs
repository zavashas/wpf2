using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp7
{
    public class Note
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public Note()
        {
        }

        public Note(string title, string description, DateTime? date)
        {
            Title = title;
            Description = description;
            Date = date;
        }
    }

    public partial class MainWindow : Window
    {
        public List<Note> notes = new List<Note>();

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists("Note.xml"))
                notes = DeserializeSerialize.Deserialize<List<Note>>();
            else
                notes = new List<Note>();

            List.ItemsSource = notes;
            List.DisplayMemberPath = "Title";
            Date.SelectedDate = DateTime.Today;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (notes == null)
                notes = new List<Note>();

            Note note = new Note(Name.Text, Description.Text, Date.SelectedDate);
            notes.Add(note);
            List.ItemsSource = notes.Where(n => n.Date == Date.SelectedDate).ToList();
            DeserializeSerialize.Serialize(notes);
        }



        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if (List.SelectedItem != null)
            {
                Note selectedNote = (Note)List.SelectedItem;
                selectedNote.Title = Name.Text;
                selectedNote.Description = Description.Text;

                List.ItemsSource = notes.Where(note => note.Date == Date.SelectedDate).ToList();

                DeserializeSerialize.Serialize(notes);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                Note selectedNote = (Note)List.SelectedItem;
                notes.Remove(selectedNote);
                List.ItemsSource = notes.Where(note => note.Date == Date.SelectedDate).ToList();
                DeserializeSerialize.Serialize(notes);
            }

        }


        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                Note selectedNote = (Note)List.SelectedItem;
                Name.Text = selectedNote.Title;
                Description.Text = selectedNote.Description;
            }
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNotes();
        }

        private void UpdateNotes()
        {
            DateTime? selectedDate = Date.SelectedDate;
            if (selectedDate.HasValue)
            {
                List.ItemsSource = notes.Where(note => note.Date == selectedDate).ToList();
            }
            else
            {
                List.ItemsSource = null;
            }
        }
    }
}
