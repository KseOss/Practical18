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
            if (selected != null)
            {
                MessageBox.Show("Выберите запись для удаления");
                return;
            }
            try
            {
                var workerToDelete = _db.WorkersInfos.FirstOrDefault(w => w.WorkerId == selected.WorkerId);
                if (workerToDelete != null)
                {
                    _db.WorkersInfos.Remove(workerToDelete);
                    _db.SaveChanges();
                    LoadData();
                    MessageBox.Show("Запись  успешно удалена");
                }
                else
                {
                    MessageBox.Show("Запись не найдена в бд");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete {ex.Message}");
            }
            
        }
        private void RefreshData_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void Query1_Click(object sender, RoutedEventArgs e)
        {
            var query = _db.WorkersInfos.Where(w => w.SalaryAmount > 50000).ToList();
            MessageBox.Show($"Найдено {query.Count} рабочих с зарплатой выше 50.000");
        }
        private void Query2_Click(object sender, RoutedEventArgs e)
        {
            var query = _db.WorkersInfos.Where(w => w.Experience > 5).ToList();
            MessageBox.Show($"Найдено {query.Count} рабочих со стажем более 5 лет");
        }
        private void Query3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var worker in _db.WorkersInfos)
                {
                    worker.SalaryAmount += 1.10m; //увеличение зп на 10%
                }
                _db.SaveChanges();
                MessageBox.Show("Зарплата всех рабочих успешно увеличена на 10 %");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        private void Query4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var workersToUpdate = _db.WorkersInfos.Where(w => w.WorkerRank == 5);
                foreach (var worker in workersToUpdate)
                {
                    worker.Position = "Старший мастер";
                }
                _db.SaveChanges();
                MessageBox.Show("Должность успешно изменена для всех рабочих с разрядом 5.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
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
                MessageBox.Show($"Ошибка при удалении данных: {ex.Message}");
            }
        }
    }
}