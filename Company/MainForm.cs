﻿using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;


namespace Company
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            _ = LoadCompaniesAsync(); 
        }

        private async Task LoadCompaniesAsync()
        {
            try
            {
                string query = @"
                SELECT CompanyID, CompanyName, AddressLine1, AddressLine2, ZipCode, Telephone 
                FROM Company 
                WHERE IsActive = 1";

                var companies = await DatabaseHelper.ExecuteQueryAsync(query);
                dataGridViewCompanies.DataSource = companies;
                ConfigureGridColumns();
                AddActionColumn();
            }
            catch (Exception ex)
            {
                ShowError($"An error occurred while loading companies: {ex.Message}");
            }
        }

        private void ConfigureGridColumns()
        {
            if (dataGridViewCompanies.Columns.Contains("btnActions"))
                return;

            dataGridViewCompanies.Columns["CompanyID"].Width = 70;
            dataGridViewCompanies.Columns["CompanyName"].Width = 250;
            dataGridViewCompanies.Columns["AddressLine1"].Width = 250;
            dataGridViewCompanies.Columns["AddressLine2"].Width = 250;
            dataGridViewCompanies.Columns["ZipCode"].Width = 100;
            dataGridViewCompanies.Columns["Telephone"].Width = 130;
        }

        private void AddActionColumn()
        {
            if (dataGridViewCompanies.Columns.Contains("btnActions"))
                return;

            var btnActions = new DataGridViewImageColumn
            {
                Name = "btnActions",
                HeaderText = "Actions",
                Image = LoadImage("Assets/actions.png"),
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            };

            dataGridViewCompanies.Columns.Add(btnActions);
            dataGridViewCompanies.CellContentClick += HandleCellContentClick;
        }

        private void HandleCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != dataGridViewCompanies.Columns["btnActions"].Index)
                return;

            var companyId = Convert.ToInt32(dataGridViewCompanies.Rows[e.RowIndex].Cells["CompanyID"].Value);
            ShowContextMenu(dataGridViewCompanies, companyId, e.RowIndex);
        }

        private void ShowContextMenu(DataGridView grid, int companyId, int rowIndex)
        {
            var contextMenuStrip = new ContextMenuStrip();

            var viewItem = new ToolStripMenuItem("View Contact") { Image = LoadImage("Assets/view.png") };
            var editItem = new ToolStripMenuItem("Edit") { Image = LoadImage("Assets/edit.png") };
            var deleteItem = new ToolStripMenuItem("Delete") { Image = LoadImage("Assets/bin.png") };

            viewItem.Click += async (s, e) => await ViewCompanyAsync(companyId);
            editItem.Click += async (s, e) => await EditCompanyAsync(companyId);
            deleteItem.Click += async (s, e) => await DeleteCompanyAsync(companyId);

            contextMenuStrip.Items.AddRange(new ToolStripItem[] { viewItem, editItem, deleteItem });
            contextMenuStrip.Show(grid, grid.PointToClient(Cursor.Position));
        }

        private async Task EditCompanyAsync(int companyId)
        {
            var form = new FormCompany(companyId);
            form.ShowDialog();
            await LoadCompaniesAsync();
        }

        private async Task ViewCompanyAsync(int companyId)
        {
            var form = new FormContactManagement(companyId);
            form.ShowDialog();
        }

        private async Task DeleteCompanyAsync(int companyId)
        {
            try
            {
                var confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete the company with ID: {companyId}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirmResult != DialogResult.Yes)
                    return;

                string deactivateCompany = "UPDATE Contact SET IsActive = 0 WHERE CompanyID = @CompanyId; UPDATE Company SET IsActive = 0 WHERE CompanyID = @CompanyId;";

                var parameters = new[] { new SqlParameter("@CompanyId", companyId) };
                await DatabaseHelper.ExecuteNonQueryAsync(deactivateCompany, parameters);

                await LoadCompaniesAsync();
                ShowInfo("Company and associated contacts were successfully deactivated!");
            }
            catch (Exception ex)
            {
                ShowError($"An error occurred while deleting the company: {ex.Message}");
            }
        }

       

        private Image LoadImage(string relativePath)
        {
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            return Image.FromFile(fullPath);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                _ = SearchCompaniesAsync(searchText);
            }
            else
            {
                _ = LoadCompaniesAsync();
            }
        }
        private void btnAddCompany_Click(object sender, EventArgs e)
        {
            FormCompany form = new FormCompany();
            form.ShowDialog();
            _ = LoadCompaniesAsync();
        }

        private async Task SearchCompaniesAsync(string searchText)
        {
            string query = @"
                 SELECT CompanyID, CompanyName, AddressLine1, AddressLine2, ZipCode, Telephone 
                 FROM Company 
                 WHERE IsActive = 1 
                 AND (CompanyName LIKE @SearchText 
                 OR AddressLine1 LIKE @SearchText 
                 OR AddressLine2 LIKE @SearchText 
                 OR ZipCode LIKE @SearchText 
                 OR Telephone LIKE @SearchText)";

            string connectionString = ConfigurationManager.ConnectionStrings["CompanyDb"].ConnectionString;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                MessageBox.Show("Search text cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Use parameters properly to prevent SQL injection.
                        command.Parameters.Add(new SqlParameter("@SearchText", SqlDbType.NVarChar) { Value = $"%{searchText}%" });

                        await connection.OpenAsync();

                        // Use DataTable to load data manually.
                        DataTable companies = new DataTable();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            companies.Load(reader); // Load data into DataTable.
                        }

                        // Safely update the UI.
                        dataGridViewCompanies.Invoke((Action)(() =>
                        {
                            dataGridViewCompanies.DataSource = companies;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching companies: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            _ = LoadCompaniesAsync();
        }
    }

}
