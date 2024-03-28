using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BOOKS
{
    public partial class frmRentals : Form
    {
        DBServices db = new DBServices();
        bool AddNew = false;
        public frmRentals()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void setEnable( bool check)
        {
            txtRentalID.Enabled = false;
            lbUser.Enabled = check;
            cbCustomer.Enabled = check;
            txtRentalDate.Enabled = check;
            txtReturnDate.Enabled = check;
            txtDeposit.Enabled = check;
            txtDescription.Enabled = check;
            btnAddNew.Enabled = !check;
            btnSave.Enabled = check;
            btnCancel.Enabled = check;
            btnDelete.Enabled = !check;
            btnEdit.Enabled = !check;
            dataGridView1.Enabled = !check;

        }

        //tạo hàm lấy dữ liệu lên datagrifview
        private void laydulieuGridview()
        {
            string sql = "select * from Rentals";
            dataGridView1.DataSource = db.getData(sql);

        }

        // tạo hàm đưa dữ liệu từ bảng users lên listbox
        private void laydulieuUser()
        {
            string sql = "select * from Users where Status = 1";
            //thuộc tính DisplayMember gán cho fullname là trương hiển thị lên list box
            lbUser.DisplayMember = "FullName";
            //thuộc tính ValueMember gán cho trường usersID trong bảng Users
            //trường sẽ lấy giá trị tương ứng khi chọn vào tên trên listbox
            //giá trị này sẽ được lưu trong banr rentalss tương ứng
            lbUser.ValueMember = "UserID";
            lbUser.DataSource = db.getData(sql);
        }

        //tạo hàm đưa dữ liệu  từ bảng Customer lên combobox
        private void laydulieuCustomer()
        {
            string sql = "select * from Customers";
            cbCustomer.DisplayMember = "FullName";
            cbCustomer.ValueMember = "CustomerID";
            cbCustomer.DataSource = db.getData(sql);
        }

        private void frmRentals_Load(object sender, EventArgs e)
        {
            setEnable(false);
            laydulieuGridview();
            laydulieuUser();
            laydulieuCustomer();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if(i >= 0 )
            {
                txtRentalID.Text = dataGridView1.Rows[i].Cells["RentalID"].Value.ToString();
                lbUser.SelectedValue = int.Parse(dataGridView1.Rows[i].Cells["UserID"].Value.ToString());
                cbCustomer.SelectedValue = int.Parse(dataGridView1.Rows[i].Cells["CustomerID"].Value.ToString());
                txtRentalDate.Text = dataGridView1.Rows[i].Cells["RentalDate"].Value.ToString();
                txtLimitedDate.Text = dataGridView1.Rows[i].Cells["LimitedDate"].Value.ToString();
                txtReturnDate.Text = dataGridView1.Rows[i].Cells["ReturnDate"].Value.ToString();
                txtDeposit.Text = dataGridView1.Rows[i].Cells["Deposit"].Value.ToString();
                txtDescription.Text = dataGridView1.Rows[i].Cells["Description"].Value.ToString();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNew = true;
            setEnable(true);
            txtRentalID.Clear();
            txtRentalDate.Clear();
            txtLimitedDate.Clear();
            txtReturnDate.Clear();
            txtDeposit.Clear();
            txtDescription.Clear();
            

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string us = lbUser.SelectedValue.ToString();
            string cn = cbCustomer.SelectedValue.ToString();
            string rd = txtRentalDate.Text;
            string ld = txtLimitedDate.Text;
            string re = txtReturnDate.Text;
            //float dp = float.Parse( txtDeposit.Text);
            string dp = txtDeposit.Text;
            string de = txtDescription.Text;

            if(AddNew)
            {
                string sql = string.Format("insert into Rentals values " +
 
                    "'0', '1', N'2', N'3', N'4', '5', N'6',", us, cn, rd, ld, re, dp, de);
                db.runquery(sql);
                laydulieuGridview();
                 //Kiểm tra xem UserID tồn tại trong bảng Users
             
            }
            else
            {
                //Phần này ghi khi nhấp vào nút sửa
                string id = txtRentalID.Text;
                string sql = string.Format("update Rentals set " +
                    "UserID = '{0}'," +
                    "CustomerID = '{1}'," +
                    "RentalDate = N'{2}'," +
                    "LimitedDate = N'{3}'," +
                    "ReturnDate = N'{4}'," +
                    "Deposit = '{5}'," +
                    "Description = N'{6}' where RentalID = '{7}'", us, cn, rd, ld, re, dp, de, id
                    );
                DBServices db = new DBServices();
                db.runquery(sql);
                laydulieuGridview();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtRentalID.Text;
            string sql = string.Format(" delete from RentalDetails where RentalID = {0} " +
                " delete from Rentals where RentalID = {0}", id);
            if(MessageBox.Show("Bạn có muốn xoá bạn ghi này hay không?", "Thông báo",
                MessageBoxButtons.YesNo,MessageBoxIcon.Error) == DialogResult.Yes)
            {
                db.runquery(sql);
                laydulieuGridview();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddNew = false;
            setEnable(true);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setEnable(false);
        }
    }
}
