﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelPhoneApp
{
    public partial class MainForm : Form
    {
        Orders oList = new Orders();
        public MainForm()
        {
            InitializeComponent();
        }
        private void UpdateWorkDisplay(Orders os)
        {
            int sum = 0;
            workDisplay.Rows.Clear();
            for (int i = 0; i < os.Count; ++i)
            {
                workDisplay.Rows.Add(os[i].OrderNum, os[i].Name, os[i].Phone, os[i].Address,os[i].Cost) ;
            }
            for (int i = 0; i < oList.Count; ++i)
            {
                sum += oList[i].Cost;
            }
            sum_box.Text = sum.ToString() + "\\";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtPhone.Text != "" && txtAddress.Text != "" && int.TryParse(txtCost.Text, out _))
            {
                Order o = new Order(txtName.Text, txtPhone.Text, txtAddress.Text, int.Parse(txtCost.Text));
                oList.Add(o);

                txtName.Text = "";
                txtPhone.Text = "";
                txtAddress.Text = "";
                txtCost.Text = "";
                txtName.Focus();
                UpdateWorkDisplay(oList);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string name = txtSearch.Text;
            if (name == "")
                return;
            int nameSum = 0;
            namesum_groupbox.Text = name + "의 총 매출액";
            for (int i = 0; i < oList.Count; i++)
            {
                if (name == oList[i].Name) nameSum += oList[i].Cost;
            }
            namesum_box.Text = nameSum.ToString() + "\\";
            txtSearch.Text = "";
            txtSearch.Focus();

            Orders tlist = new Orders();
            for (int i = 0; i < oList.Count; ++i)
            {
                if (oList[i].Name == name) tlist.Add(oList[i]);
            }

            UpdateWorkDisplay(tlist);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string on = txtRemove.Text;
            if (on == "" || !int.TryParse(on, out _)) return;
            txtRemove.Text = "";
            txtRemove.Focus();

            for (int i = 0; i < oList.Count; ++i)
                if (oList[i].OrderNum == int.Parse(on))
                {
                    oList.Remove(i);
                }
            UpdateWorkDisplay(oList);
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            UpdateWorkDisplay(oList);
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            DateTime localTime = DateTime.Now;
            string fileName = localTime.ToString("yyyyMMdd_HHmmss") + ".txt"; // 파일 이름을 현재 시간으로 생성
            string filePath = "";
            string inputText = "";
            if (workDisplay.Rows.Count - 1 == 0)
            {
                MessageBox.Show("출력할 데이터가 없음!!");
                return;
            }
            // 배달원
            if (txtSearch.Text!="")
            {
                inputText += "===== " + txtSearch.Text + "=====\n\n";
            }
            // 텍스트 파일 경로
            if (txtPath.Text == "")
            {
                string directoryPath = Path.GetDirectoryName(Application.StartupPath);
                directoryPath = Path.GetDirectoryName(directoryPath);
                directoryPath = Path.Combine(directoryPath, "bill");
                filePath = Path.Combine(directoryPath, fileName);
            }
            else
            {
                filePath = Path.Combine(txtPath.Text, fileName);
            }

            for (int i = 0; i < workDisplay.Rows.Count-1; i++)
            {
                for (int j = 0; j < workDisplay.Columns.Count; j++)
                {
                    inputText += workDisplay.Rows[i].Cells[j].OwningColumn.HeaderText;
                    inputText += " : ";
                    inputText += workDisplay.Rows[i].Cells[j].Value;
                    inputText += "   ";
                }
                inputText += "\n";
            }

            try
            {
                // 텍스트 파일 생성 및 텍스트 입력
                File.WriteAllText(filePath, inputText);

                MessageBox.Show(filePath+"\n파일이 저장되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류가 발생했습니다: " + ex.Message);
            }
        }

        private void txtRemove_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
