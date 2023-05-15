using Library;
using System;
using System.Collections.Generic;
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

namespace WPFTo_Do {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        List<TaskModel> tasksList = new List<TaskModel>();
        public MainWindow() {
            InitializeComponent();
            tasksList = SqliteDataAccess.LoadTasks();       //nacteni databaze s Tasky
            foreach (TaskModel task in tasksList) {
                ListBoxAllTasks.Items.Add(task);
            }



        }
     

        private void Button_Click(object sender, RoutedEventArgs e) {
            TaskModel newTask = new TaskModel(TextBoxTask.Text, DaysComboBox.SelectionBoxItem.ToString(), TextBoxNotes.Text);   //vytvoreni instance Task
            ListBoxAllTasks.Items.Add(newTask);  //pridavam Task jako TaskModel object
            SqliteDataAccess.saveTask(newTask);     //pridani do databaze
            //tasks.Add(newTask);
        }

        private void RemoveTaskBtn_Click(object sender, RoutedEventArgs e) {
            TaskModel selectedTask = (TaskModel)ListBoxAllTasks.SelectedItem;
            if (selectedTask != null) {     //hlidam si, ze jsem zvolil nejaky Task
                ListBoxAllTasks.Items.Remove(selectedTask);
                SqliteDataAccess.deleteTask(selectedTask);
            }
        }

        private void RemoveAllButton_Click(object sender, RoutedEventArgs e) {
            ListBoxAllTasks.Items.Clear();
            SqliteDataAccess.deleteAllTasks();
        }

        private void MainWindow1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            this.DragMove();            //nepouzivam default window style, takze okno nebylo dragable, musel jsem vyuzit event handler
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        //private void LoadTasksButton_Click(object sender, RoutedEventArgs e) {
        //    //tasksList = SqliteDataAccess.LoadTasks();
        //    //foreach (TaskModel task in tasksList) {
        //    //    ListBoxAllTasks.Items.Add(task);
        //    //}
        //}
    }
}
