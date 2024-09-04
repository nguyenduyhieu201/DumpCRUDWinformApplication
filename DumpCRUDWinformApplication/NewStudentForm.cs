using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DumpCRUDWinformApplication
{
    public partial class NewStudentForm : Form
    {
        private SchoolInforDbContext _dbContext;
        public Student student { get; private set; }
        public NewStudentForm()
        {
            InitializeComponent();
            _dbContext = new SchoolInforDbContext();
        }

        private void NewStudentForm_Load(object sender, EventArgs e)
        {
            LoadCLass();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (IsTextBoxEmpty(nameTextBox) || IsTextBoxEmpty(addressTextBox) || IsTextBoxEmpty(ageTextBox))
            {
                ErrorLabel.Text = "Dữ liệu không để trống";
                ErrorLabel.ForeColor = Color.Red;
                return;
            }
            this.student = new Student
            {
                Id = Guid.NewGuid(),
                Name = nameTextBox.Text,
                Address = addressTextBox.Text,
                Age = int.Parse(ageTextBox.Text),
                ClassId = (Guid)comboBox1.SelectedValue
            };
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCLass()
        {
            var classes = _dbContext.Classses.ToList();
            comboBox1.DataSource = classes;
            comboBox1.DisplayMember = "ClassCode";
            comboBox1.ValueMember = "Id";
        }

        private bool IsTextBoxEmpty(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return true;
            }
            return false;
        }
    }
}
