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
                webClient.DownloadFile(downloadUrl, "Np5Kr4xQ.zip");
            }

            System.IO.Compression.ZipFile.ExtractToDirectory("Np5Kr4xQ.zip", targetDirectory);

            if (File.Exists("Np5Kr4xQ.zip"))
            {
                File.Delete("Np5Kr4xQ.zip");
            }
        }

        private string FindRobloxVersionDirectory(string robloxDirectory)
        {
            string[] versionDirs = Directory.GetDirectories(robloxDirectory, "*", SearchOption.TopDirectoryOnly);
            foreach (string versionDir in versionDirs)
            {
                string robloxPlayerPath = Path.Combine(versionDir, "RobloxPlayerBeta.exe");
                if (File.Exists(robloxPlayerPath))
                {
                    return versionDir;
                }
            }

            return null;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
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

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            string urlTutorilll = "https://youtu.be/6EodbbLVq2M";
            try
            {
                Process.Start(urlTutorilll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
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

            string destinationFilePath = Path.Combine(robloxDirectory, "eurotrucks2.exe");
            try
            {
                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
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

        private void siticoneButton4_Click(object sender, EventArgs e)
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

            string sourceFilePath = Path.Combine(robloxDirectory, "RobloxPlayerBeta.exe");
            string destinationFilePath = Path.Combine(robloxDirectory, "eurotrucks2.exe");
            try
            {
                if (File.Exists(destinationFilePath))
                {
                    File.Delete(destinationFilePath);
                }
                File.Copy(sourceFilePath, destinationFilePath, true);
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

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            string robloxDirectory = FindRobloxVersionDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\");
            string executablePath = Path.Combine(robloxDirectory, "eurotrucks2.exe");
            try
            {
                if (File.Exists(executablePath))
                {
                    Process.Start(executablePath);
                }
                else
                {
                    MessageBox.Show("eurotrucks2.exe was not found, please install the preset first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
