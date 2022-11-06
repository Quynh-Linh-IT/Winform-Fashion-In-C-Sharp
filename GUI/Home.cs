﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using BUS;
using DTO;
using FontAwesome.Sharp;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
using Color = System.Drawing.Color;

namespace GUI
{
    public partial class Home : Form
    {
        //Field
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        private RoleBUS role_BUS = new RoleBUS();
        private AccountBUS account_BUS = new AccountBUS();
        private AccountDTO account_DTO;
        public Home(AccountDTO account_DTO)
        {
            InitializeComponent();
            this.account_DTO = account_DTO;
            lblNameCustommer.Text = account_DTO.Full_Name;
            lbl_Role.Text = role_BUS.get_Role_Name_From_Role_Id(account_DTO.Role_Id);
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 53);
            panelMenu.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            ActivateButton(btnHome, RGBColor.color1);
        }
        //Structs
        private struct RGBColor
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
            public static Color color7 = Color.FromArgb(0, 255, 255);
            public static Color color8 = Color.FromArgb(219, 112, 147);
            public static Color color9 = Color.FromArgb(255, 160, 122);
            public static Color color10 = Color.FromArgb(255, 182, 193);
            public static Color color11 = Color.FromArgb(0, 255, 0);
            public static Color color12 = Color.FromArgb(255, 165, 0);
            public static Color color13 = Color.FromArgb(255, 255, 0);
            public static Color color14 = Color.FromArgb(238, 130, 238);

        }

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableButon();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //icon Current Child Form
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                //iconCurrentChildForm.IconColor = Color.FromArgb(255,255,255);
                iconCurrentChildForm.IconColor = color;
                lblTitleChildForm.Text = currentBtn.Text;
            }
        }
        //
        private void DisableButon()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(0,0,0);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form form)
        {
            if (currentChildForm != null)
            {
                //open only form
                currentChildForm.Close();
            }
            currentChildForm = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelDestop.Controls.Add(form);
            pictureBox1.Hide();
            form.Show();
        }

        private void Reset()
        {
            ActivateButton(btnHome, RGBColor.color1);
            pictureBox1.Image = GUI.Properties.Resources.posterMLL;
            if(currentChildForm!=null)
                currentChildForm.Close();
        }

        // Drag Form
        [DllImport("User32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("User32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
                WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblTimeNow.Text = DateTime.Now.ToLongTimeString();
            lblDateNow.Text = DateTime.Now.ToLongDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeNow.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void btnLogoHome_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
            pictureBox1.Image = GUI.Properties.Resources.posterMLL;
            if (currentChildForm != null)
                currentChildForm.Close();
            pictureBox1.Show();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
            OpenChildForm(new SellGui());
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
            OpenChildForm(new ImportProductsGUI());
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color5);
            OpenChildForm(new ProductsGUI());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {

            ActivateButton(sender, RGBColor.color6);
            OpenChildForm(new CategoryGUI());
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color7);
            OpenChildForm(new BillGui());
        }

        private void btnImportBill_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color8);
        }

        private void btnExportBill_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color9);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color10);
            OpenChildForm(new BrandGUI());
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color11);
            OpenChildForm(new DiscountGUI());
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color12);
            OpenChildForm(new AccountGUI());
        }

        private void btnPermission_Click(object sender, EventArgs e)
        {
            String description = role_BUS.get_Role_From_Id(account_DTO.Role_Id).Role_Desciption;
            string[] description_Split = description.Split(new string[] { ", " }, StringSplitOptions.None);
            String[] role;// Lưu trữ 1 tên quyền và khả năng thao tác của quyền đó
            ArrayList role_Name = new ArrayList(); // Lưu trữ mảng tên quyền
            ArrayList role_Manipulative = new ArrayList();// Lưu trữ mảng khả năng thao tác của các quyền
            
            //Thực hiện tách chuỗi và truyền data vào 2 mảng 
            for (int i = 0; i < description_Split.Length; i++)
            {
                role = description_Split[i].Split(new string[] { " : " }, StringSplitOptions.None);
                role_Name.Add(role[0]);
                role_Manipulative.Add(role[1]);
            }

            if (role_Name.Contains("Quyền")){
                int index = role_Name.IndexOf("Quyền");
                ActivateButton(sender, RGBColor.color13);
                OpenChildForm(new RoleGUI(role_Manipulative[index].ToString()));
            }else 
                MessageBox.Show("Bạn không đủ quyền hạn để truy cập trang này");
        }
            
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color14);
            OpenChildForm(new CustomerGUI());
        }

        private void btnProfileCustommer_Click(object sender, EventArgs e)
        {
            LoginGUI lg = new LoginGUI();
            lg.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color13);
            OpenChildForm(new ChartGUI());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
