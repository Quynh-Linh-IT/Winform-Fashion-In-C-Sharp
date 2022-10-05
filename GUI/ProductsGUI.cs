﻿using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class ProductsGUI : Form
    {
        //Field
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;
        public ProductsGUI()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
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

        }
        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
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
                iconCurrentChildForm.IconColor = color;
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
        }
        //
        private void DisableButon()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(0, 0, 0);
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

        }

        private void Reset()
        {
            DisableButon();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.Violet;
            lblTitleChildForm.Text = "Home";
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

        private void iconCurrentChildForm_Click(object sender, EventArgs e)
        {
            LoginGUI lg = new LoginGUI();
            lg.Show();

        }
       

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
                WindowState = FormWindowState.Normal;
        }
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // hiệu ứng khi nhấn vào menu
        private void btnHomeClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
        }

        private void btnCartsClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
        }

        private void btnOrdersClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
        }

        private void btnNhapHangClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
        }

        private void btnProductsClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color5);
        }

        private void btnCategorysClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
        }

        private void btnBillsClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
        }

        private void btnPhieuNhapClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
        }

        private void btnPhieuXuatClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
        }

        private void btnNhaCungCapClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color5);
        }

        private void btnDiscountClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
        }

        private void btnAccountClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
        }

        private void btnpermissionClick(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
        }

        private void btnCustomerClicks(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
        }
    }
}
