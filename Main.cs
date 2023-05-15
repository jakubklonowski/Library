namespace Library
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.buttonBorrow = new System.Windows.Forms.Button();
            this.buttonBooks = new System.Windows.Forms.Button();
            this.buttonClients = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonBorrow
            // 
            this.buttonBorrow.Location = new System.Drawing.Point(12, 62);
            this.buttonBorrow.Name = "buttonBorrow";
            this.buttonBorrow.Size = new System.Drawing.Size(123, 23);
            this.buttonBorrow.TabIndex = 0;
            this.buttonBorrow.Text = "Borrow a book";
            this.buttonBorrow.UseVisualStyleBackColor = true;
            this.buttonBorrow.Click += new System.EventHandler(this.buttonBorrow_Click);
            // 
            // buttonBooks
            // 
            this.buttonBooks.Location = new System.Drawing.Point(12, 91);
            this.buttonBooks.Name = "buttonBooks";
            this.buttonBooks.Size = new System.Drawing.Size(123, 23);
            this.buttonBooks.TabIndex = 1;
            this.buttonBooks.Text = "Books";
            this.buttonBooks.UseVisualStyleBackColor = true;
            this.buttonBooks.Click += new System.EventHandler(this.buttonBooks_Click);
            // 
            // buttonClients
            // 
            this.buttonClients.Location = new System.Drawing.Point(12, 120);
            this.buttonClients.Name = "buttonClients";
            this.buttonClients.Size = new System.Drawing.Size(123, 23);
            this.buttonClients.TabIndex = 2;
            this.buttonClients.Text = "Clients";
            this.buttonClients.UseVisualStyleBackColor = true;
            this.buttonClients.Click += new System.EventHandler(this.buttonClients_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Library System";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(148, 151);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClients);
            this.Controls.Add(this.buttonBooks);
            this.Controls.Add(this.buttonBorrow);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonBorrow_Click(object sender, EventArgs e)
        {
            Form formLibrary = new Library();
            formLibrary.Show();
        }

        private void buttonBooks_Click(object sender, EventArgs e)
        {
            Form formBooks = new Book();
            formBooks.Show();
        }

        private void buttonClients_Click(object sender, EventArgs e)
        {
            Form formClients = new Client();
            formClients.Show();
        }
    }
}