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
using System.Net;
using System.Diagnostics;

namespace Rovision
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DownloadAndExtract(string downloadUrl, string targetDirectory)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(downloadUrl, "3XadFqCk.zip");
            }

            System.IO.Compression.ZipFile.ExtractToDirectory("3XadFqCk.zip", targetDirectory);

            if (File.Exists("3XadFqCk.zip"))
            {
                File.Delete("3XadFqCk.zip");
            }
        }

        private string FindRobloxVersionDirectory(string robloxDirectory)
        {
            string[] versionDirs = Directory.GetDirectories(robloxDirectory, "*", SearchOption.TopDirectoryOnly);
            if (versionDirs.Length > 0)
            {
                return versionDirs[0];
            }
            else
            {
                return null;
            }
        }

        private void DeleteDirectory(string targetDir)
        {
            string[] files = Directory.GetFiles(targetDir);
            string[] dirs = Directory.GetDirectories(targetDir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(targetDir, false);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            string robloxDownloadUrl = "https://github.com/rizkwya/stackuniversal/raw/main/images/ClientSettings.zip";
            string robloxDirectory = FindRobloxVersionDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\");

            try
            {
                string clientSettingsFolderPath = Path.Combine(robloxDirectory, "ClientSettings");
                if (Directory.Exists(clientSettingsFolderPath))
                {
                    Directory.Delete(clientSettingsFolderPath, true);
                }

                DownloadAndExtract(robloxDownloadUrl, robloxDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string programFilesDownloadUrl = "https://github.com/rizkwya/stackuniversal/raw/main/images/NVIDIA%20Corporation.zip";
            string programFilesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

            try
            {
                string anselFolderPath = Path.Combine(programFilesDirectory, "NVIDIA Corporation", "Ansel");
                if (Directory.Exists(anselFolderPath))
                {
                    DeleteDirectory(anselFolderPath);
                }

                DownloadAndExtract(programFilesDownloadUrl, programFilesDirectory);
                MessageBox.Show("Preset Succesfully Installed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            string robloxDirectory = FindRobloxVersionDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\");

            try
            {
                string clientSettingsFolderPath = Path.Combine(robloxDirectory, "ClientSettings");
                if (Directory.Exists(clientSettingsFolderPath))
                {
                    Directory.Delete(clientSettingsFolderPath, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            string programFilesDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

            try
            {
                string anselFolderPath = Path.Combine(programFilesDirectory, "NVIDIA Corporation", "Ansel");
                if (Directory.Exists(anselFolderPath))
                {
                    DeleteDirectory(anselFolderPath);
                }
                MessageBox.Show("Preset Succesfully Uninstalled!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            string urlDikorr = "https://discord.com/invite/8BNhyDZ";
            try
            {
                Process.Start(urlDikorr);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
