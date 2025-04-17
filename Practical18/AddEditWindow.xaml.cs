using Practical18.Models;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Practical18
{
    public partial class AddEditWindow : Window
    {
        private WorkersInfo _worker;
        private WorkerDbContext _db;

        public AddEditWindow(WorkersInfo worker, WorkerDbContext db)
        {
            InitializeComponent();
            _worker = worker;
            _db = db;
            txtLastName.Text = worker.LastName;
            txtFirstName.Text = worker.FirstName;
            txtMiddlename.Text = worker.MiddleName;
            txtDepartament.Text = worker.DepartmentName;
            dpHireDate.SelectedDate = worker.HireDate.ToDateTime(TimeOnly.MinValue);
            txtSalary.Text = worker.SalaryAmount.ToString();
            txtExperience.Text = worker.Experience.ToString();
            txtRank.Text = worker.WorkerRank.ToString();
            txtPosition.Text = worker.Position;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _worker.LastName = txtLastName.Text;
                _worker.FirstName = txtFirstName.Text;
                _worker.MiddleName = txtMiddlename.Text;
                _worker.DepartmentName = txtDepartament.Text;
                _worker.HireDate = DateOnly.FromDateTime(dpHireDate.SelectedDate ?? DateTime.Now); // Добавлена проверка на null
                _worker.SalaryAmount = decimal.Parse(txtSalary.Text);
                _worker.Experience = int.Parse(txtExperience.Text);
                _worker.WorkerRank = int.Parse(txtRank.Text);
                _worker.Position = txtPosition.Text;

                if (_worker.WorkerId == 0)
                {
                    _db.WorkersInfos.Add(_worker);
                }
                else
                {
                    _db.Entry(_worker).State = EntityState.Modified; // Добавлено обновление существующей записи
                }

                _db.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
        }
        private void Chancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
