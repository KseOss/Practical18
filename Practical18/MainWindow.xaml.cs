using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Practical18.Models;
using System.Linq;

namespace Practical18
{
    public partial class MainWindow : Window
    {
        private WorkerDbContext _db;
        private List<WorkersInfo> _workerChache;
        public MainWindow()
        {
            InitializeComponent();
            _db = new WorkerDbContext();
            LoadData();
        }
        private void LoadData()
        {
            dataGrid.ItemsSource = _db.WorkersInfos.ToList();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddEditWindow(new WorkersInfo(), _db);
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem as WorkersInfo;
            if (selected != null)
            {
                MessageBox.Show("Выберите запись для редактирования");
                return;
            }
            var editWindow = new AddEditWindow(selected, _db);
            if (editWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }
        private void View_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem as WorkersInfo;
            if (selected != null)
            {
                MessageBox.Show("Выберите запись для просмотра.");
                return;
            }
                MessageBox.Show($"ФИО: {selected.LastName} {selected.FirstName} {selected.MiddleName}\n" +
                $"Цех: {selected.DepartmentName}\n" +
                $"Дата поступления: {selected.HireDate}\n" +
                $"Размер ЗП: {selected.SalaryAmount}\n" +
                $"Стаж работы: {selected.Experience}\n" +
                $"Разряд: {selected.WorkerRank}\n" +
                $"Должность: {selected.Position}\n");
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dataGrid.SelectedItem as WorkersInfo;
            if (selected == null)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить студента?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _db.WorkersInfos.Remove(selected);
                _db.SaveChanges();
                LoadData();
            }
        }  
        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void Query1_Click(object sender, RoutedEventArgs e)
        {
            _workerChache = _db.WorkersInfos.Where(w => w.SalaryAmount > 50000).ToList();
            MessageBox.Show($"Найдено {_workerChache.Count} рабочих с зарплатой выше 50.000");
            LoadData();
        }
        private void Query2_Click(object sender, RoutedEventArgs e)
        {
            _workerChache = _db.WorkersInfos.Where(w => w.Experience > 5).ToList();
            MessageBox.Show($"Найдено {_workerChache.Count} раюочих со стажем более 5 лет");
            LoadData();
        }
        private void Query3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var worker in _db.WorkersInfos)
                {
                    worker.SalaryAmount += 1.10m;
                }
                _db.SaveChanges();
                MessageBox.Show("Зарплата всех рабочих увелчена на 10%");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных {ex.Message}");
            }
            LoadData();
        }
        private void Query4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var workersToUpdate = _db.WorkersInfos.Where(w => w.WorkerRank == 4);
                foreach (var worker in workersToUpdate)
                {
                    worker.Position = "Старший мастер";
                }
                _db.SaveChanges();
                MessageBox.Show("Должность успешно изменена для всех рабочих с раздрядом 4");
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Ошибка {ex.Message}");
            }
            LoadData();
        }
        private void Query5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var workersToDelete = _db.WorkersInfos.Where(w => w.Experience < 1).ToList();
                _db.WorkersInfos.RemoveRange(workersToDelete);
                _db.SaveChanges();
                MessageBox.Show($"Удалено {workersToDelete.Count} рабочих со стажем менее 1 года");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка {ex.Message}");
            }
            LoadData();
        }
    }
}
