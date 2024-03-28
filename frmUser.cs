using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BOOKS
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //thêm hàm tại đây
        //stat functions
        private void setEnable(bool check)
        {
            txtUserID.Enabled = false;
            txtFullName.Enabled = check;
            txtUserName.Enabled = check;
            txtPassWord.Enabled = check;
            txtPhone.Enabled = check;
            txtEmail.Enabled = check;
            txtDescription.Enabled = check;
            chbStatus.Enabled = check;
            btnSave.Enabled = check;
            btnCancel.Enabled = check;
            btnAddnew.Enabled = !check;
            btnEdit.Enabled = !check;
            btnDelete.Enabled = !check;
            btnEdit.Enabled = !check;
            dgvUser.Enabled = !check;
        }

        //test
        private void loadgridatta()
        {
            DBServices db = new DBServices();
            string sql = "select * from  Users where Status = 1 ";
            dgvUser.DataSource = db.getData(sql);
            setEnable(false);
        }
        
        //end funtions
        private void frmUser_Load(object sender, EventArgs e)
        {
            loadgridatta();
        }
        // add new
        bool AddNew = false;

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvUser_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if(i >= 0)
            {
                txtUserID.Text = dgvUser.Rows[i].Cells["UserID"].Value.ToString();
                txtUserName.Text = dgvUser.Rows[i].Cells["UserName"].Value.ToString();
                txtFullName.Text = dgvUser.Rows[i].Cells["FullName"].Value.ToString();
                txtPassWord.Text = dgvUser.Rows[i].Cells["Password"].Value.ToString();
                txtPhone.Text = dgvUser.Rows[i].Cells["Phone"].Value.ToString();
                txtEmail.Text = dgvUser.Rows[i].Cells["Email"].Value.ToString();
                txtDescription.Text = dgvUser.Rows[i].Cells["Description"].Value.ToString();
                if (dgvUser.Rows[i].Cells["Status"].Value.ToString() == "1")
                    chbStatus.Checked = true;
                else 
                    chbStatus.Checked = false;
            }
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            AddNew = true;
            setEnable(true);
            txtUserID.Clear();
            txtUserName.Clear();
            txtFullName.Focus();
            txtPassWord.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtDescription.Clear();
            chbStatus.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setEnable(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtFullName.Text.Trim() == "")
            {
                MessageBox.Show("thông tin fullname không được để trống", "Thông báo");
                txtFullName.Focus();
                return;
            }
            if(txtUserName.Text.Trim()=="")
            {
                MessageBox.Show("thông tin Username không được để trống", "Thông báo");
                txtUserName.Focus();
                return;
            }
            if (txtPassWord.Text.Trim() == "")
            {
                MessageBox.Show("thông tin Username không được để trống", "Thông báo");
                txtPassWord.Focus();
                return;
            }
            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("thông tin Username không được để trống", "Thông báo");
                txtPhone.Focus();
                return;
            }
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("thông tin Username không được để trống", "Thông báo");
                txtEmail.Focus();
                return;
            }

            string fn = txtFullName.Text;
            string un = txtUserName.Text;
            string pw = txtPassWord.Text;
            string ph = txtPhone.Text;
            string em = txtEmail.Text;
            string de = txtDescription.Text;
            int st = (chbStatus.Checked) ? 1 : 0;

            if(AddNew)
            {
                //phần này ghi khi nhấp vào thêm mới
                string sql = string.Format("INSERT INTO Users(FullName,UserName, Password, Phone,Email,Status,Description ) VALUES" +
                    "(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', '{5}', N'{6}')", fn, un, pw, ph, em,st, de);
                //chú thích
                //N'{0}' kiểu xâu có dấu cần thêm N
                //các số thứ tự của các biến truyền vào ở sau
                // ví dụ pw ---> 1
                DBServices db = new DBServices();
                db.runquery(sql);
                loadgridatta();
            } else
            {
                //Phần này ghi khi nhấp vào nút sửa
                string id = txtUserID.Text;
                string sql = string.Format("update Users set" +
                    "FullName = N'{0}'," +
                    "UserName = N'{1}'," +
                    "Password = N'{2}'," +
                    "Phone = N'{3}'," +
                    "Email = N'{4}'," +
                    "Status = '{5}'," +
                    "Descriptions = N'{6}' where UserID = {7}", fn, un, pw, ph, em, st, de, id
                    );
                DBServices db = new DBServices();
                db.runquery(sql);
                loadgridatta();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            AddNew = false;
            setEnable(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //thêm lệnh hỏi trước khi xoá
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thực hiện câu truy vấn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
          
            string id = txtUserID.Text;
            string sql = string.Format("delete from Users where UserID = '{0}'", id);
            DBServices db = new DBServices();
            db.runquery(sql);
            loadgridatta();
        }
    }
}
