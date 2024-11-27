using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company
{
    public partial class FormCompany : Form
    {
        private readonly int? companyId;

        public FormCompany(int? companyId = null)
        {
            InitializeComponent();
            this.companyId = companyId;

            btnSave.Visible = true;
            btnCancel.Visible = companyId.HasValue;

            if (companyId.HasValue)
            {
                _ = LoadCompanyDetailsAsync();
            }
        }

        private async Task LoadCompanyDetailsAsync()
        {
            try
            {
                const string query = "SELECT * FROM Company WHERE CompanyID = @CompanyID";
                var parameters = new[] { new SqlParameter("@CompanyID", companyId) };

                DataTable company = await DatabaseHelper.ExecuteQueryAsync(query, parameters);

                if (company.Rows.Count > 0)
                {
                    var row = company.Rows[0];
                    txtCompanyName.Text = row["CompanyName"]?.ToString();
                    txtAddressLine1.Text = row["AddressLine1"]?.ToString();
                    txtAddressLine2.Text = row["AddressLine2"]?.ToString();
                    txtZipCode.Text = row["ZipCode"]?.ToString();
                    txtTelephone.Text = row["Telephone"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error loading company details: {ex.Message}");
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                string query = companyId.HasValue
                    ? "UPDATE Company SET CompanyName = @CompanyName, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, ZipCode = @ZipCode, Telephone = @Telephone, ModifiedOn = GETDATE(), ModifiedBy = 'Admin' WHERE CompanyID = @CompanyID"
                    : "INSERT INTO Company (CompanyName, AddressLine1, AddressLine2, ZipCode, Telephone, CreatedBy) VALUES (@CompanyName, @AddressLine1, @AddressLine2, @ZipCode, @Telephone, 'Admin')";

                var parameters = GetCompanyParameters();

                if (companyId.HasValue)
                {
                    Array.Resize(ref parameters, parameters.Length + 1);
                    parameters[parameters.Length - 1] = new SqlParameter("@CompanyID", companyId);
                }

                await DatabaseHelper.ExecuteNonQueryAsync(query, parameters);
                MessageBox.Show("Company saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error saving company: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void btnEdit_Click(object sender, EventArgs e)
        {
            ToggleInputFields(true);
            btnSave.Visible = true;
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (!companyId.HasValue)
            {
                ShowWarningMessage("No company selected for deletion.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this company?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes) return;

            try
            {
                const string query = "DELETE FROM Company WHERE CompanyID = @CompanyID";
                var parameters = new[] { new SqlParameter("@CompanyID", companyId) };

                int rowsAffected = await DatabaseHelper.ExecuteNonQueryAsync(query, parameters);

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Company deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    ShowErrorMessage("Error deleting the company.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error deleting company: {ex.Message}");
            }
        }

        private void ToggleInputFields(bool enabled)
        {
            txtCompanyName.Enabled = enabled;
            txtAddressLine1.Enabled = enabled;
            txtAddressLine2.Enabled = enabled;
            txtZipCode.Enabled = enabled;
            txtTelephone.Enabled = enabled;
        }

        private SqlParameter[] GetCompanyParameters()
        {
            return new[]
            {
            new SqlParameter("@CompanyName", txtCompanyName.Text.Trim()),
            new SqlParameter("@AddressLine1", txtAddressLine1.Text.Trim()),
            new SqlParameter("@AddressLine2", txtAddressLine2.Text.Trim()),
            new SqlParameter("@ZipCode", txtZipCode.Text.Trim()),
            new SqlParameter("@Telephone", txtTelephone.Text.Trim())
        };
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                string.IsNullOrWhiteSpace(txtAddressLine1.Text) ||
                string.IsNullOrWhiteSpace(txtZipCode.Text) ||
                string.IsNullOrWhiteSpace(txtTelephone.Text))
            {
                ShowWarningMessage("Please fill out all required fields.");
                return false;
            }

            if (!Regex.IsMatch(txtZipCode.Text, "^[0-9]{1,6}$"))
            {
                ShowWarningMessage("Zip Code must be numeric and up to 6 digits.");
                return false;
            }

            if (!Regex.IsMatch(txtTelephone.Text, "^[0-9]{1,15}$"))
            {
                ShowWarningMessage("Telephone must be numeric and up to 15 digits.");
                return false;
            }

            return true;
        }

        private void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
