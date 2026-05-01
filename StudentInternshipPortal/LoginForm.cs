using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentInternshipPortal.Data;
using StudentInternshipPortal.Models;

namespace StudentInternshipPortal
{
  
    /// LoginForm: Entry point of the application.
    /// Handles user authentication and new student registration.
    /// Uses LINQ to query the Users table for credential verification.
  
    public class LoginForm : Form
    {
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnRegister = null!;
        private Label lblError = null!;

        public LoginForm()
        {
            InitializeComponent();
            EnsureDatabaseCreated();
        }

       
        /// Ensures the SQLite database and tables exist on first run.
        
        private void EnsureDatabaseCreated()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        private void InitializeComponent()
        {
            this.Text = "Login - Student Internship Portal";
            this.Size = new Size(400, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);

          
            Label lblTitle = new Label
            {
                Text = "Student Internship Portal",
                Left = 30, Top = 15, Width = 330,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblUsername = new Label { Text = "Username:", Left = 30, Top = 60, Width = 80, Font = new Font("Segoe UI", 10) };
            txtUsername = new TextBox { Left = 130, Top = 58, Width = 220, Font = new Font("Segoe UI", 10) };

            Label lblPassword = new Label { Text = "Password:", Left = 30, Top = 100, Width = 80, Font = new Font("Segoe UI", 10) };
            txtPassword = new TextBox { Left = 130, Top = 98, Width = 220, PasswordChar = '*', Font = new Font("Segoe UI", 10) };

            btnLogin = new Button
            {
                Text = "Login",
                Left = 130, Top = 145, Width = 105, Height = 38,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            btnRegister = new Button
            {
                Text = "Register",
                Left = 245, Top = 145, Width = 105, Height = 38,
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            lblError = new Label
            {
                Left = 30, Top = 200, Width = 330, Height = 40,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.AddRange(new Control[] { lblTitle, lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnRegister, lblError });

            this.AcceptButton = btnLogin;
        }

        /// Login handler: Uses LINQ (FirstOrDefault) to authenticate user credentials.
        /// Routes to AdminDashboardForm or StudentDashboardForm based on user role.
        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter username and password.";
                return;
            }

            using (var db = new AppDbContext())
            {
                // LINQ query to verify user credentials against the database
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    if (user.Role == "Admin")
                    {
                        AdminDashboardForm adminForm = new AdminDashboardForm(user.Id);
                        adminForm.Show();
                        this.Hide();
                    }
                    else if (user.Role == "Student")
                    {
                        StudentDashboardForm studentForm = new StudentDashboardForm(user.Id);
                        studentForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    lblError.Text = "Invalid username or password!";
                }
            }
        }

        /// Registration handler: Creates a new Student user in the database.
        /// Validates that username is not already taken using LINQ.
        private void BtnRegister_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter a username and password to register.";
                return;
            }

            if (password.Length < 6)
            {
                lblError.Text = "Password must be at least 6 characters.";
                return;
            }

            using (var db = new AppDbContext())
            {
                // LINQ: Check if username already exists
                bool exists = db.Users.Any(u => u.Username == username);
                if (exists)
                {
                    lblError.Text = "This username is already taken!";
                    return;
                }

                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    Role = "Student"
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show($"Registration successful! You can now login as '{username}'.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
