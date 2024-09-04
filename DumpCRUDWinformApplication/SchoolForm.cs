using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DumpCRUDWinformApplication
{
    public partial class SchoolForm : Form
    {
        private SchoolInforDbContext _schoolContext;
        private BindingSource _bindingSource;
        private Expression<Func<StudentDTO, bool>> _filter;
        public SchoolForm()
        {
            InitializeComponent();
            _schoolContext = new SchoolInforDbContext();
            _bindingSource = new BindingSource();
            _filter = null;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btnView")
            {
                var id = (Guid)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;
                ViewStudent(id);
            }    
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var newStudentForm = new NewStudentForm();
            if (newStudentForm.ShowDialog() == DialogResult.OK)
            {
                _schoolContext.Students.Add(newStudentForm.student);
                _schoolContext.SaveChanges();
                LoadData();
            }
        }

        private void LoadData()
        {
            var students = (from student in _schoolContext.Students
                            join cl in _schoolContext.Classses on
                            student.ClassId equals cl.Id
                            select new StudentDTO
                            {
                                Id = student.Id,
                                Name = student.Name,
                                ClassCode = cl.ClassCode
                            }).ToList();
            if (_filter != null) { 
                students = students.AsQueryable().Where(_filter).ToList();
            }

            _bindingSource.DataSource = students;
            dataGridView1.DataSource = _bindingSource;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void SchoolForm_Load(object sender, EventArgs e)
        {
            LoadData();
            AddButtonColumns();
        }

        private void AddButtonColumns()
        {
            DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
            btnView.HeaderText = "View";
            btnView.Name = "btnView";
            btnView.Text = "View";
            btnView.UseColumnTextForButtonValue = true;

            btnView.FlatStyle = FlatStyle.Flat;
            btnView.DefaultCellStyle.BackColor = Color.LightBlue;
            btnView.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            btnView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.Columns.Add(btnView);



            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Select";
            checkColumn.Name = "checkColumn";
            dataGridView1.Columns.Add(checkColumn);


            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["ClassCode"].Width = 100;
            dataGridView1.Columns["btnView"].Width = 75;
            dataGridView1.Columns["btnView"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["checkColumn"].Width = 50;
            dataGridView1.Columns["checkColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void ViewStudent(Guid id)
        {
            var student = _schoolContext.Students.Where(s => s.Id.Equals(id)).FirstOrDefault();
            var classCode = _schoolContext.Classses.Where(c => c.Id == student.ClassId).FirstOrDefault().ClassCode;
            MessageBox.Show($"Hoc sinh ten la {student.Name}, {student.Age.ToString()} tuoi, dia chi o {student.Address}. Hoc sinh hoc lop {classCode}", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            List<Guid> lstStudentId = new List<Guid>();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the checkbox is checked
                bool isChecked = Convert.ToBoolean(row.Cells["checkColumn"].Value);

                if (!isChecked) continue;
                
                // Get the studentId from the appropriate cell
                Guid studentId = Guid.Parse(row.Cells["Id"].Value.ToString());
                lstStudentId.Add(studentId);
               
            }    

            foreach(var id in lstStudentId)
            {
                var student = _schoolContext.Students.Where(s => s.Id == id).FirstOrDefault();
                _schoolContext.Students.Remove(student);
                _schoolContext.SaveChanges();
            }
            LoadData();

        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            string search = searchTextBox.Text.ToLower();
            _filter = (s => (s.ClassCode.ToLower().Contains(search) || s.Name.ToLower().Contains(search)));
            LoadData();
        }


    }
}

public class StudentDTO
{
    public Guid Id { set; get; }
    public string Name { set; get; }
    public string ClassCode { set; get; }
}