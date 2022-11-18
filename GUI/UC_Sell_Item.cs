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

namespace GUI
{
    public partial class UC_Sell_Item : UserControl
    {
        DiscountDTO discountDTO = new DiscountDTO();
        SellGui sellGUI;
        //SellGui sell = new SellGui();
        public UC_Sell_Item(SellGui sellGUI)
        {
            InitializeComponent();
            this.sellGUI = sellGUI;
        }
        public ProductDTO data;
        public ProductDTO getData()
        {
            return data;
        }
        public void setData(ProductDTO data)
        {
            this.data = data;
            lb_nameProductItem.Text = data.Product_Name;
            double gia = data.Product_Price;
            double thue = (5 * gia) / 100; // gia sp tang 5% so vs nhap hang
            double tienloi = gia + thue;
            lb_priceProductItem.Text = tienloi.ToString("#,#,#") + "đ";
            //lb_priceProductItem.Text = data.Product_Price.ToString("#,#,#") + "đ";
            _uriproduct = @"Image\" + data.Image;
            loadImageAsync();
            
        }
        public Task<Image> loadImageFromFileAsync(string uri)
        {
            return Task.Run(() =>
            {
                return Image.FromFile(uri);
            });
        }
        public string _uriproduct;
     
        public async void loadImageAsync()
        {
            var image = await loadImageFromFileAsync(this._uriproduct);
            ptc_imageProductItem.Image = image;
        }

        private void ptc_imageProductItem_Click(object sender, EventArgs e)
        {
            String size = "";
            if (this.data.Size_id == 1) size = "S";
            else if (this.data.Size_id == 2) size = "M";
            else if (this.data.Size_id == 3) size = "L";
            else if (this.data.Size_id == 4) size = "XL";

            if (checkOrderExits(this.data) != null)
            {
                Sell_DetailGUI sell_DetailGUI = new Sell_DetailGUI(checkOrderExits(this.data), this.sellGUI, "Update Detail Product in Bill", size);
                sell_DetailGUI.Show();
            }
            else
            {
                Sell_DetailGUI sell_DetailGUI = new Sell_DetailGUI(this.data, this.sellGUI, "Add new Product to Bill",size);
                sell_DetailGUI.Show();
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void UC_Sell_Item_MouseLeave(object sender, EventArgs e)
        {
            this.BorderStyle = BorderStyle.None;
        }

        //Kiểm tra sp đã có trong bill hay chưa
        public ProductDTO checkOrderExits(ProductDTO product)
        {
            for (int i = 0; i < sellGUI.listOder.Count; i++)
            {
                ProductDTO p = (ProductDTO)sellGUI.listOder[i];
                if (product.Product_Name.Equals(p.Product_Name) && product.Size_id.Equals(p.Size_id))
                    return p;
            }
            return null;
        }
    }
}
