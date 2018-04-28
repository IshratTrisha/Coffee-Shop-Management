using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoffeeCustomerForm
{
   
    public partial class FormCustomer : Form
    {
        List<Customer> _CustomerList = new List<Customer>();
        int _ID;
        SqlConnection _connection;
        SqlDataAdapter _dataAdapter;
        SqlCommand _command;

        public FormCustomer()
        {
            InitializeComponent();

            string connetionString = @"Data Source=TRISHA-PC\SQLEXPRESS;Initial Catalog=CoffeeShopManagement;User ID=sa;Password=123";
            _connection = new SqlConnection(connetionString);
           
        }

        private void buttonSave_Click(object sender, EventArgs e) //SAVE button
        {
            if (!validate()) return;
            string message=string.Empty;

            if (buttonSave.Text == "Save")
            {
                _command = new SqlCommand("insert into Customer(Name, Gender, City, Type, Phone) values(@Name, @Gender, @City, @Type, @Phone)", _connection);
                message = "Record insertes successfully";
            }

            else
            {
                _command = new SqlCommand("update Customer set Name=@Name, Gender=@Gender, City=@City, Type=@Type, Phone=@Phone where Id=@Id", _connection);
                message = "Record updated successfully";
            }

            try
            {   
                _connection.Open();
                _command.Parameters.AddWithValue("@Id", _ID);
                _command.Parameters.AddWithValue("@Name", textBoxName.Text);
                _command.Parameters.AddWithValue("@Gender", comboBoxGender.Text);
                _command.Parameters.AddWithValue("@City", textBoxCity.Text);
                _command.Parameters.AddWithValue("@Type", comboBoxType.Text);
                _command.Parameters.AddWithValue("@Phone", textBoxPhone.Text);
                _command.ExecuteNonQuery();
                _connection.Close();

                MessageBox.Show(message);
            }
            catch (Exception)
            {
                _connection.Close();
                MessageBox.Show("Error saving data!");
            }
          
            RefreshGrid();
        }


        bool validate() 
        {
            bool isValid = true;
            int Num = 0;


            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                lblNameErr.Visible = true;
                isValid = false;
            }

            if (int.TryParse(textBoxName.Text, out Num))
            {
                lblNameErr.Visible = true;
                isValid = false;
            }
           
           if (!int.TryParse(textBoxPhone.Text, out Num))
            {
                lblPhoneErr.Visible = true;
                isValid = false;
            }

            if (string.IsNullOrEmpty(textBoxCity.Text))
            {
                lblCityErr.Visible = true;
                isValid = false;
            }

            if (int.TryParse(textBoxCity.Text, out Num))
            {
                lblCityErr.Visible = true;
                isValid = false;
            }

           if (string.IsNullOrEmpty(comboBoxGender.Text))
            {
                lblGenderErr.Visible = true;
                isValid = false;
            }

            if (int.TryParse(comboBoxGender.Text, out Num))
            {
                lblGenderErr.Visible = true;
                isValid = false;
            }
            if (string.IsNullOrEmpty(comboBoxType.Text))
            {
                lblTypeErr.Visible = true;
                isValid = false;
            }

            if (int.TryParse(comboBoxType.Text, out Num))
            {
                lblTypeErr.Visible = true;
                isValid = false;
            }
            return isValid;
        }

       
        void RefreshGrid() //method
        {
            try 
            {
                _connection.Open();

                DataTable dt = new DataTable();
                _dataAdapter = new SqlDataAdapter("select * from Customer", _connection);
                _dataAdapter.Fill(dt);

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dataGridViewCustomer.DataSource = bs;

                _connection.Close();
            }
            catch (Exception)
            {
                _connection.Close();
                MessageBox.Show("There is an error!");
            }

        }
        public void ClearData() //method
        {
            textBoxName.Text = string.Empty;
            comboBoxGender.Text = string.Empty;
            textBoxCity.Text = string.Empty;
            comboBoxType.Text = string.Empty;
            textBoxPhone.Text = string.Empty;
        }

        private void dataGridViewCustomer_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            _ID = Convert.ToInt32(dataGridViewCustomer.Rows[e.RowIndex].Cells[0].Value);

            textBoxName.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBoxGender.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
            
            comboBoxType.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBoxCity.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();

            textBoxPhone.Text = dataGridViewCustomer.Rows[e.RowIndex].Cells[5].Value.ToString();

            buttonSave.Text = "Edit";
            buttonDelete.Visible = true;
            buttonClear.Visible = true;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_ID != 0)
            {
                _command = new SqlCommand("delete Customer where ID=@id", _connection);

                _connection.Open();
                _command.Parameters.AddWithValue("@id", _ID);
                _command.ExecuteNonQuery();
                _connection.Close();

                MessageBox.Show("Record Deleted Successfully!");

                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

       

        private void buttonClear_Click(object sender, EventArgs e)
        {
            buttonSave.Text = "Save";
            ClearData();
            buttonDelete.Visible = false;
            buttonClear.Visible = false;
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            lblNameErr.Visible = false;
        }

        private void textBoxCity_TextChanged(object sender, EventArgs e)
        {
            lblCityErr.Visible = false;
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            lblPhoneErr.Visible = false;
        }

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblGenderErr.Visible = false;
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTypeErr.Visible = false;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
   
}