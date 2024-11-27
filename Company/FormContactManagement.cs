using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class FormContactManagement : Form
    {
        private int _companyId;
        private int _contactId;

        public FormContactManagement(int companyId)
        {
            InitializeComponent();
            _companyId = companyId;
        }

        private async void FormContactManagement_Load(object sender, EventArgs e)
        {
            await LoadContactsAsync();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await SearchContactsAsync(txtSearch.Text.Trim());
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadContactsAsync();
        }

        private async void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (dataGridViewContacts.SelectedRows.Count > 0)
            {
                int contactId = Convert.ToInt32(dataGridViewContacts.SelectedRows[0].Cells["ContactID"].Value);
                await DeleteContactAsync(contactId);
            }
            else
            {
                ShowInfo("Please select a contact to delete.");
            }
        }

        private async Task LoadContactsAsync()
        {
            try
            {
                string query = @"SELECT ContactID, FirstName, LastName, Telephone, Email 
                             FROM Contact 
                             WHERE IsActive = 1 AND CompanyID = @CompanyID";
                var parameters = new[] { new SqlParameter("@CompanyID", _companyId) };

                DataTable contacts = await DatabaseHelper.ExecuteQueryAsync(query, parameters);
                BindContactsToGrid(contacts);
            }
            catch (Exception ex)
            {
                ShowError($"Error loading contacts: {ex.Message}");
            }
        }

        private void BindContactsToGrid(DataTable contacts)
        {
            dataGridViewContacts.DataSource = contacts;
            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            var columnWidths = new (string Column, int Width)[]
            {
            ("ContactID", 70), ("FirstName", 150), ("LastName", 150),
            ("Telephone", 150), ("Email", 200)
            };

            foreach (var (column, width) in columnWidths)
            {
                if (dataGridViewContacts.Columns.Contains(column))
                    dataGridViewContacts.Columns[column].Width = width;
            }

            AddActionButtons();
        }

        private void AddActionButtons()
        {
            if (dataGridViewContacts.Columns.Contains("Actions")) return;

            string iconsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
            var actionsColumn = new DataGridViewImageColumn
            {
                Name = "Actions",
                HeaderText = "Actions",
                Image = LoadIcon(Path.Combine(iconsPath, "actions.png")),
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };

            dataGridViewContacts.Columns.Add(actionsColumn);

            dataGridViewContacts.CellContentClick += async (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == actionsColumn.Index)
                {
                    ShowActionsMenu(e.RowIndex);
                }
            };
        }

        private Image LoadIcon(string path)
        {
            try
            {
                return File.Exists(path) ? Image.FromFile(path) : null;
            }
            catch
            {
                ShowError($"Error loading icon from {path}");
                return null;
            }
        }

        private void ShowActionsMenu(int rowIndex)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem editItem = new ToolStripMenuItem("Edit");
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Delete");

            editItem.Click += async (s, e) => await EditContactAsync(GetContactId(rowIndex));
            deleteItem.Click += async (s, e) => await DeleteContactAsync(GetContactId(rowIndex));

            menu.Items.Add(editItem);
            menu.Items.Add(deleteItem);

            menu.Show(Cursor.Position);
        }

        private int GetContactId(int rowIndex) =>
            Convert.ToInt32(dataGridViewContacts.Rows[rowIndex].Cells["ContactID"].Value);

        private async Task SearchContactsAsync(string searchText)
        {
            try
            {
                string query = @"SELECT ContactID, FirstName, LastName, Telephone, Email 
                             FROM Contact 
                             WHERE IsActive = 1 AND CompanyID = @CompanyID 
                             AND (FirstName LIKE @SearchText OR LastName LIKE @SearchText 
                                  OR Telephone LIKE @SearchText OR Email LIKE @SearchText)";

                var parameters = new[]
                {
                new SqlParameter("@CompanyID", _companyId),
                new SqlParameter("@SearchText", $"%{searchText}%")
            };

                DataTable contacts = await DatabaseHelper.ExecuteQueryAsync(query, parameters);
                if (contacts.Rows.Count > 0)
                    BindContactsToGrid(contacts);
                else
                    ShowInfo("No contacts found matching your search criteria.");
            }
            catch (Exception ex)
            {
                ShowError($"Error searching contacts: {ex.Message}");
            }
        }

        private async Task EditContactAsync(int contactId)
        {
            try
            {
                _contactId = contactId;

                string query = @"SELECT FirstName, LastName, Telephone, Email 
                             FROM Contact 
                             WHERE ContactID = @ContactID AND IsActive = 1";
                var parameters = new[] { new SqlParameter("@ContactID", contactId) };

                var contact = (await DatabaseHelper.ExecuteQueryAsync(query, parameters))
                    .Rows.Cast<DataRow>().FirstOrDefault();

                if (contact != null)
                {
                    txtFirstName.Text = contact["FirstName"].ToString();
                    txtLastName.Text = contact["LastName"].ToString();
                    txtPhone.Text = contact["Telephone"].ToString();
                    txtEmail.Text = contact["Email"].ToString();
                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error editing contact: {ex.Message}");
            }
        }

        private async Task DeleteContactAsync(int contactId)
        {
            try
            {
                if (ConfirmAction($"Are you sure you want to delete Contact ID: {contactId}?"))
                {
                    string query = @"UPDATE Contact SET IsActive = 0 WHERE ContactID = @ContactID";
                    var parameters = new[] { new SqlParameter("@ContactID", contactId) };

                    await DatabaseHelper.ExecuteNonQueryAsync(query, parameters);
                    await LoadContactsAsync();
                    ShowInfo("Contact deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error deleting contact: {ex.Message}");
            }
        }

        private bool ConfirmAction(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        private void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        private void ShowInfo(string message) =>
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateContactFields(out string firstName, out string lastName, out string phone, out string email))
                    return;

                var createdBy = "Admin";
                var currentDate = DateTime.Now;
                string query;
                SqlParameter[] parameters;

                if (_contactId == 0)
                {
                    query = @"INSERT INTO Contact (FirstName, LastName, Telephone, Email, CompanyID, CreatedBy, CreatedOn) 
                      VALUES (@FirstName, @LastName, @Phone, @Email, @CompanyID, @CreatedBy, @CreatedOn)";
                    parameters = new[]
                    {
                            new SqlParameter("@FirstName", firstName),
                            new SqlParameter("@LastName", lastName),
                            new SqlParameter("@Phone", phone),
                            new SqlParameter("@Email", email),
                            new SqlParameter("@CompanyID", _companyId),
                            new SqlParameter("@CreatedBy", createdBy),
                            new SqlParameter("@CreatedOn", currentDate)
                    };

                    await SaveContactAsync(query, parameters, "Contact saved successfully.");
                }
                else
                {
                    query = @"UPDATE Contact 
                      SET FirstName = @FirstName, LastName = @LastName, Telephone = @Phone, Email = @Email, 
                          ModifiedBy = @ModifiedBy, ModifiedOn = @ModifiedOn 
                      WHERE ContactID = @ContactID";
                    parameters = new[]
                    {
                            new SqlParameter("@FirstName", firstName),
                            new SqlParameter("@LastName", lastName),
                            new SqlParameter("@Phone", phone),
                            new SqlParameter("@Email", email),
                            new SqlParameter("@ModifiedBy", createdBy),
                            new SqlParameter("@ModifiedOn", currentDate),
                            new SqlParameter("@ContactID", _contactId)
                    };

                    await SaveContactAsync(query, parameters, "Contact updated successfully.");
                }

                ClearContactFields();
                await LoadContactsAsync();
                ResetSaveButton();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the contact: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateContactFields(out string firstName, out string lastName, out string phone, out string email)
        {
            firstName = txtFirstName.Text.Trim();
            lastName = txtLastName.Text.Trim();
            phone = txtPhone.Text.Trim();
            email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill out all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(firstName, "^[a-zA-Z]+$") || !Regex.IsMatch(lastName, "^[a-zA-Z]+$"))
            {
                MessageBox.Show("First name and last name should only contain characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(phone, "^[0-9]{1,15}$"))
            {
                MessageBox.Show("Telephone should only contain numbers and not more than 15 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Invalid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task SaveContactAsync(string query, SqlParameter[] parameters, string successMessage)
        {
            await DatabaseHelper.ExecuteNonQueryAsync(query, parameters);
            MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ClearContactFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
        }

        private void ResetSaveButton()
        {
            _contactId = 0;
            btnSave.Text = "Save";
        }

        private void btnResetFrom_Click(object sender, EventArgs e)
        {
            ResetSaveButton();
            ClearContactFields();
        }

    }

}
