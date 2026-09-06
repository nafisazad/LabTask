using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CafeOrderManagementSystem
{
    public partial class Form1 : Form
    {
        // SQL Server connection
        string connectionString =
            @"Data Source=.\SQLEXPRESS;Initial Catalog=CafeDB;Integrated Security=True";

        // List for ordered food items
        List<string> orderedItems = new List<string>();

        public Form1()
        {
            InitializeComponent();

            // Button events
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;

            // Make the checkboxes behave like radio buttons
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;

            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
        }


        // =========================================================
        // INSERT
        // =========================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string name = textBox1.Text.Trim();
            string phone = textBox2.Text.Trim();
            string gender = GetGender();
            string type = GetMembershipType();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check duplicate name
                    string checkQuery =
                        "SELECT COUNT(*) FROM Customers WHERE Name = @Name";

                    using (SqlCommand checkCmd =
                        new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@Name", name);

                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show(
                                "Customer with this name already exists!",
                                "Duplicate Customer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    // Insert customer
                    string query =
                        @"INSERT INTO Customers
                          (Name, Phone, Gender, MembershipType)
                          VALUES
                          (@Name, @Phone, @Gender, @MembershipType)";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@MembershipType", type);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(
                        "Customer inserted successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // SEARCH
        // =========================================================

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();

            if (name == "")
            {
                MessageBox.Show(
                    "Enter customer name to search.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query =
                        @"SELECT Phone, Gender, MembershipType
                          FROM Customers
                          WHERE Name = @Name";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox2.Text =
                                    reader["Phone"].ToString();

                                string gender =
                                    reader["Gender"].ToString();

                                string type =
                                    reader["MembershipType"].ToString();

                                // Set gender checkbox
                                if (gender == "Male")
                                {
                                    checkBox1.Checked = true;
                                    checkBox2.Checked = false;
                                }
                                else
                                {
                                    checkBox1.Checked = false;
                                    checkBox2.Checked = true;
                                }

                                // Set membership checkbox
                                if (type == "Regular")
                                {
                                    checkBox3.Checked = true;
                                    checkBox4.Checked = false;
                                }
                                else
                                {
                                    checkBox3.Checked = false;
                                    checkBox4.Checked = true;
                                }

                                MessageBox.Show(
                                    "Customer found!\n\n" +
                                    "Name: " + name + "\n" +
                                    "Phone: " + reader["Phone"] + "\n" +
                                    "Gender: " + gender + "\n" +
                                    "Type: " + type,
                                    "Customer Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Customer not found.",
                                    "Search",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // UPDATE
        // =========================================================

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string name = textBox1.Text.Trim();
            string phone = textBox2.Text.Trim();
            string gender = GetGender();
            string type = GetMembershipType();

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query =
                        @"UPDATE Customers
                          SET Phone = @Phone,
                              Gender = @Gender,
                              MembershipType = @MembershipType
                          WHERE Name = @Name";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@MembershipType", type);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show(
                                "Customer updated successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Customer not found.",
                                "Update",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // DELETE
        // =========================================================

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();

            if (name == "")
            {
                MessageBox.Show(
                    "Enter customer name to delete.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query =
                        "DELETE FROM Customers WHERE Name = @Name";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show(
                                "Customer deleted successfully!",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show(
                                "Customer not found.",
                                "Delete",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error: " + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        // =========================================================
        // GET GENDER
        // =========================================================

        private string GetGender()
        {
            if (checkBox1.Checked)
                return "Male";

            if (checkBox2.Checked)
                return "Female";

            return "";
        }


        // =========================================================
        // GET MEMBERSHIP TYPE
        // =========================================================

        private string GetMembershipType()
        {
            if (checkBox3.Checked)
                return "Regular";

            if (checkBox4.Checked)
                return "Premium";

            return "";
        }


        // =========================================================
        // VALIDATE INPUT
        // =========================================================

        private bool ValidateInput()
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please enter customer name.");

                textBox1.Focus();
                return false;
            }

            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show(
                    "Please enter phone number.");

                textBox2.Focus();
                return false;
            }

            if (!checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show(
                    "Please select gender.");

                return false;
            }

            if (!checkBox3.Checked && !checkBox4.Checked)
            {
                MessageBox.Show(
                    "Please select membership type.");

                return false;
            }

            return true;
        }


        // =========================================================
        // CLEAR FORM
        // =========================================================

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();

            checkBox1.Checked = false;
            checkBox2.Checked = false;

            checkBox3.Checked = false;
            checkBox4.Checked = false;

            orderedItems.Clear();
        }


        // =========================================================
        // GENDER CHECKBOXES
        // =========================================================

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkBox2.Checked = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkBox1.Checked = false;
        }


        // =========================================================
        // MEMBERSHIP CHECKBOXES
        // =========================================================

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                checkBox4.Checked = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                checkBox3.Checked = false;
        }


        // =========================================================
        // FOOD ITEM LIST
        // =========================================================

        // This method prevents the same food item
        // from being added to an order more than once.

        private bool AddFoodItem(string foodItem)
        {
            if (orderedItems.Contains(foodItem))
            {
                MessageBox.Show(
                    "This food item is already in the order.",
                    "Duplicate Food Item",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            orderedItems.Add(foodItem);

            return true;
        }
    }
}
