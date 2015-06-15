using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicPickerDeviceApp.App;

namespace MusicPickerDeviceApp
{
    public partial class UploadForm : Form
    {

        private List<string> path;

        public UploadForm()
        {
            InitializeComponent();
            path = new List<string>();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void LoadForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                path.Add(folderBrowser.SelectedPath);
                foldersLabel.Text += string.Format("- {0} \n", folderBrowser.SelectedPath);
            }
        }

        private void buttonLoad_Click_1(object sender, EventArgs e)
        {
            if (path != null)
            {
                var allowedExtensions = new[] { "mp3", "wav" };
                foreach (string s in path)
                {
                    List<string> musics = Seeker.GetMusics(allowedExtensions, s);
                    List<Track> tracks = Seeker.GetTracks(musics);

                    //@TODO pour la BDD ?
                }
                
                //@TODO envoyer la musique sur le webservice
            }
        }
    }
}
