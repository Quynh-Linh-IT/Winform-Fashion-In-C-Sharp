﻿using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using PdfSharp.Charting;
using NPOI.SS.Formula.Functions;

namespace GUI
{
    public partial class AccountGUI : Form
    {
        private int currentIndex = 0;
        private AccountBUS accountBUS = new AccountBUS();
        public AccountGUI( string role_Manipulative)
        {
            InitializeComponent();
            if (!role_Manipulative.Equals("Được thay đổi"))
            {
                guna2Button1.Enabled = false;
                guna2Button4.Enabled = false;
                guna2Button2.Enabled = false;
                guna2Button6.Enabled = false;
            }

        }



        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AccountAddOrUpdateGUI accountAddOrUpdateGUI = new AccountAddOrUpdateGUI("Add", null);
            accountAddOrUpdateGUI.Show();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AccountDTO accountDTO = null;
            AccountAddOrUpdateGUI accountAddOrUpdateGUI = null;
            Console.WriteLine(currentIndex);
            if (currentIndex >= 0) {
                accountDTO = new AccountDTO(Int32.Parse(dataGridViewAccount.Rows[currentIndex].Cells[0].Value.ToString()), dataGridViewAccount.Rows[currentIndex].Cells[1].Value.ToString(), dataGridViewAccount.Rows[currentIndex].Cells[2].Value.ToString(), dataGridViewAccount.Rows[currentIndex].Cells[4].Value.ToString(), dataGridViewAccount.Rows[currentIndex].Cells[3].Value.ToString(), dataGridViewAccount.Rows[currentIndex].Cells[5].Value.ToString());
                 accountAddOrUpdateGUI = new AccountAddOrUpdateGUI("Update", accountDTO);
            }
            else {
                accountDTO = new AccountDTO();
                 accountAddOrUpdateGUI = new AccountAddOrUpdateGUI("Add", accountDTO);
            }
            accountAddOrUpdateGUI.Show();
        }

        private void AccountGUI_Load(object sender, EventArgs e)
        {
            dataGridViewAccount.DataSource = accountBUS.getAllAccount();
        }

        private void dataGridViewAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentIndex = e.RowIndex;
            Console.WriteLine(currentIndex);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click_1(object sender, EventArgs e) {
            SaveFileDialog saved = new SaveFileDialog();
            saved.Title = "Xuất -->> - - - ->";
            saved.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if(saved.ShowDialog() == DialogResult.OK) {
                try {
                    exportExcel(saved.FileName, dataGridViewAccount);
                    MessageBox.Show("Xuất thành công <3");
                }catch(Exception ex) {
                    MessageBox.Show("Xuất thất bai :< Errors : "+ex.Message);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e) {
            OpenFileDialog opened = new OpenFileDialog();
            opened.Title = "Nhập -->> - - - ->";
            opened.Filter = "Excel (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls";
            if (opened.ShowDialog() == DialogResult.OK) {
                try {
                    System.Data.DataTable dataTable = importExcel(opened.FileName);
                    accountBUS.insertAccounts(dataTable);
                    System.Data.DataTable dataTable2 = accountBUS.getAllAccount();
                    dataGridViewAccount.DataSource = dataTable2;
                    MessageBox.Show("nhập thành công <3");
                }
                catch (FormatException ex1) {
                    MessageBox.Show("ID không đúng định dạng (int) thay vì (string)");
                }
                catch (ApplicationException ex2) {
                    MessageBox.Show(ex2.Message);
                }
                catch (ArgumentException ex3) {
                    MessageBox.Show("Định dạng cột không đúng");
                }
            }
        }

        public static void exportExcel(string exportUrl, DataGridView dataGridView) {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            // create header rows 
            for (int i = 0; i < dataGridView.Columns.Count; i++) {
                application.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }
            // insert rows
            for (int i = 0; i < dataGridView.Rows.Count; i++) {
                for (int j = 0; j < dataGridView.Columns.Count; j++) {
                    application.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value;
                }
            }
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(exportUrl);
            application.ActiveWorkbook.Saved = true;
        }
        public static System.Data.DataTable importExcel(string importUrl) {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(importUrl))) {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                System.Data.DataTable dataTable = new System.Data.DataTable();
                for (int i = worksheet.Dimension.Start.Column; i <= worksheet.Dimension.End.Column; i++) {
                    dataTable.Columns.Add(worksheet.Cells[1, i].Value.ToString());
                }
                for (int i = (worksheet.Dimension.Start.Row + 1); i <= worksheet.Dimension.End.Row; i++) {
                    List<string> rows = new List<string>();
                    for (int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++) {
                        rows.Add(worksheet.Cells[i, j].Value.ToString());
                    }
                    dataTable.Rows.Add(rows.ToArray());
                }
                return dataTable;
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e) {
            List<AccountDTO> accounts = accountBUS.getAllAccountBySearchKey(searchAccount.Text);
            dataGridViewAccount.DataSource = accounts;
        }
    }
}
