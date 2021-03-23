using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DanielGitHubLoginProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var httpClientHandler = new HttpClientHandler()
            {
                Credentials = new NetworkCredential(textBox1.Text, textBox2.Text, "https://api.github.com"),
            };

            using (var client = new HttpClient(httpClientHandler))
            {
                client.BaseAddress = new Uri("https://api.github.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DanielProd", "test1"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"users/{textBox1.Text}").Result;

                if (response.IsSuccessStatusCode)
                   richTextBox1.Text =  response.Content.ReadAsStringAsync().Result;
                else
                {
                    string text = "Invalid Username";
                    MessageBox.Show(text);
                    richTextBox1.Text = string.Empty;
                }
            }
        }
    }
}