using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HouseClient
{
    public partial class HouseForm : Form
    {
        bsite.HouseService houseService = new bsite.HouseService();
        public HouseForm()
        {
            InitializeComponent();
        }

        private void HouseForm_Load(object sender, EventArgs e)
        {
            List<bsite.House> houses = houseService.GetAll().ToList();
            gridHouse.DataSource = houses;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String keyword = txtKeyword.Text.Trim();
            List<bsite.House> houses = houseService.Search(keyword).ToList();
            gridHouse.DataSource = houses;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bsite.House newHouse = new bsite.House()
            {
                ID = int.Parse(txtID.Text.Trim()),
                Owner = txtOwner.Text.Trim(),
                Type = int.Parse(txtType.Text.Trim()),
                Price = int.Parse(txtPrice.Text.Trim()),
                Address = txtAddress.Text.Trim()
            };
            bool result = houseService.Update(newHouse);
            if (result)
            {
                List<bsite.House> houses = houseService.GetAll().ToList();
                gridHouse.DataSource = houses;
            }
            else
            {
                MessageBox.Show("SORRY BABY!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bsite.House house = new bsite.House()
            {
                ID = 0,
                Owner = txtOwner.Text.Trim(),
                Type = int.Parse(txtType.Text.Trim()),
                Price = int.Parse(txtPrice.Text.Trim()),
                Address = txtAddress.Text.Trim()
            };
            bool result = houseService.AddNew(house);
            if (result)
            {
                List<bsite.House> houses = houseService.GetAll().ToList();
                gridHouse.DataSource = houses;
            }
            else
            {
                MessageBox.Show("SORRY BABY");
            }
        }

        private void gridHouse_SelectionChanged(object sender, EventArgs e)
        {
            if (gridHouse.SelectedRows.Count == 1)
            {
                int id = int.Parse(gridHouse.SelectedRows[0].Cells["ID"].Value.ToString());
                bsite.House house = houseService.GetDetails(id);
                if (house != null)
                {
                    txtID.Text = house.ID.ToString();
                    txtOwner.Text = house.Owner;
                    txtType.Text = house.Type.ToString();
                    txtPrice.Text = house.Price.ToString();
                    txtAddress.Text = house.Address;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ARE U SURE?", "CONFIRMATION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int id = int.Parse(txtID.Text.Trim());
                bool result = houseService.Delete(id);
                if (result)
                {
                    List<bsite.House> houses = houseService.GetAll().ToList();
                    gridHouse.DataSource = houses;
                }
                else
                {
                    MessageBox.Show("SORRY BABY");
                }
            }
        }
    }
}
