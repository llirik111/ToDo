using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ToDo
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Tasks = new ObservableCollection<TaskItem>();
            tablica.ItemsSource = Tasks;

            create.Click += CreateTask_Click;
            complete.Click += CompleteTask_Click;
            delete.Click += DeleteTask_Click;

        }

        public class TaskItem : INotifyPropertyChanged
        {
            private string _title;
            private DateTime? _dueDate;
            private bool _isCompleted;

            public string Title
            {
                get => _title;
                set
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
            public DateTime? DueDate
            {
                get => _dueDate;
                set
                {
                    _dueDate = value;
                    OnPropertyChanged();
                }



            }
            public bool IsCompleted
            {
                get => _isCompleted;
                set
                {
                    _isCompleted = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DisplayText));
                }
            }
            public string DisplayText
            {
                get
                {
                    string status = IsCompleted ? "✓ Выполнено" : "○ В процессе";
                    string date = DueDate?.ToString("dd.MM.yyyy") ?? "Без срока";
                    return $"{status} | {Title} | {date}";
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public override string ToString()
            {
                return DisplayText;
            }

        }
        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem newTask = new TaskItem
            {
                Title = vvod.Text,
                DueDate = Data.SelectedDate,
                IsCompleted = false
            };

            Tasks.Add(newTask);
            vvod.Text = "";
            Data.SelectedDate = null;
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = tablica.SelectedItem as TaskItem;
            selectedTask.IsCompleted = true;
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem selectedTask = tablica.SelectedItem as TaskItem;
            Tasks.Remove(selectedTask);
        }
    }
}


        