using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryOfBabel2
{
    public partial class Form1 : Form
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz ,."; // allowed characters
        private const int PageLength = 3200; // characters per page
        private int MaxHexagons = 10;
        private const int MaxWalls = 5;
        private const int MaxShelves = 10;
        private const int MaxVolumes = 20;
        private const int PagesPerVolume = 10; 
        private bool stopSearch = false;


        public Form1()
        {
            InitializeComponent();
            flpResults.AutoScroll = true;
            txtMaxHexagons.Text = MaxHexagons.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // -------------------- Deterministic location from phrase --------------------
        // -------------------- Deterministic location from phrase --------------------
        private (int hexagon, int wall, int shelf, int volume, int page, int offset) PhraseToLocation(string phrase)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(phrase.ToLower()));

                int hexagon = hash[0] % MaxHexagons;
                int wall = hash[1] % MaxWalls;
                int shelf = hash[2] % MaxShelves;
                int volume = hash[3] % MaxVolumes;
                int page = hash[4] % PagesPerVolume;

                int offset = hash[5] % (PageLength - phrase.Length);
                return (hexagon, wall, shelf, volume, page, offset);
            }
        }


        // -------------------- Generate page --------------------
        private string GeneratePage(int hexagon, int wall, int shelf, int volume, int page)
        {
            string location = $"{hexagon}-{wall}-{shelf}-{volume}-{page}";
            byte[] hash;

            using (SHA256 sha = SHA256.Create())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(location));
            }

            Random rng = new Random(BitConverter.ToInt32(hash, 0));
            StringBuilder sb = new StringBuilder(PageLength);
            for (int i = 0; i < PageLength; i++)
                sb.Append(Alphabet[rng.Next(Alphabet.Length)]);

            return sb.ToString();
        }


        // -------------------- Search phrase --------------------
        private List<string> SearchLibrary(string phrase)
        {
            phrase = phrase.ToLower();
            var locations = new List<string>();

            var loc = PhraseToLocation(phrase); // deterministic location
            locations.Add($"Hexagon {loc.hexagon}, Wall {loc.wall}, Shelf {loc.shelf}, Volume {loc.volume}, Page {loc.page}");

            return locations;
        }

        private async Task<List<string>> SearchLibraryFullAsync(string phrase)
        {
            phrase = phrase.ToLower();
            var locations = new List<string>();

            int totalPages = MaxHexagons * MaxWalls * MaxShelves * MaxVolumes * PagesPerVolume;
            int processedPages = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalPages;
            progressBar1.Value = 0;

            await Task.Run(() =>
            {
                for (int hex = 0; hex < MaxHexagons && !stopSearch; hex++)
                {
                    for (int wall = 0; wall < MaxWalls && !stopSearch; wall++)
                    {
                        for (int shelf = 0; shelf < MaxShelves && !stopSearch; shelf++)
                        {
                            for (int vol = 0; vol < MaxVolumes && !stopSearch; vol++)
                            {
                                for (int page = 0; page < PagesPerVolume && !stopSearch; page++)
                                {
                                    string content = GeneratePage(hex, wall, shelf, vol, page);

                                    if (content.Contains(phrase))
                                    {
                                        Invoke((Action)(() =>
                                        {
                                            locations.Add(
                                                $"Hexagon {hex}, Wall {wall}, Shelf {shelf}, Volume {vol}, Page {page}"
                                            );
                                        }));
                                    }

                                    processedPages++;
                                    double percent = (processedPages * 100.0) / totalPages; // use double for decimal precision

                                    Invoke((Action)(() =>
                                    {
                                        progressBar1.Value = processedPages;
                                        loadingPercentLabel.Text = $"Loading: {percent:F2}%";
                                    }));

                                }
                            }
                        }
                    }
                }
            });

            return locations;
        }



        private void ShowPage(string phrase)
        {
            var loc = PhraseToLocation(phrase);
            string pageContent = GeneratePage(loc.hexagon, loc.wall, loc.shelf, loc.volume, loc.page);

            PageForm pageForm = new PageForm(pageContent, phrase);
            pageForm.Show();
        }



        // -------------------- Display results --------------------
        private void DisplayResults(List<string> locations)
        {
            flpResults.Controls.Clear();

            foreach (var loc in locations)
            {
                Panel panel = new Panel
                {
                    Width = flpResults.Width - 25,
                    Height = 30,
                    Margin = new Padding(0, 0, 0, 5)
                };

                Label lbl = new Label
                {
                    Text = loc,
                    AutoSize = true,
                    Location = new Point(0, 5)
                };

                Button btnShow = new Button
                {
                    Text = "Show Page",
                    Location = new Point(300, 0),
                    AutoSize = true
                };
                btnShow.Click += (s, e) =>
                {
                    // Parse location string
                    var parts = loc.Split(new[] { "Hexagon ", "Wall ", "Shelf ", "Volume ", "Page ", ", " }, StringSplitOptions.RemoveEmptyEntries);
                    int h = int.Parse(parts[0]);
                    int w = int.Parse(parts[1]);
                    int sIdx = int.Parse(parts[2]);
                    int v = int.Parse(parts[3]);
                    int p = int.Parse(parts[4]);

                    // Update the path textbox
                    pathTb.Text = $"Hexagon {h}, Wall {w}, Shelf {sIdx}, Volume {v}, Page {p}";

                    string phrase = txtSearch.Text.ToLower();
                    string pageContent = GeneratePage(h, w, sIdx, v, p);

                    PageForm pageForm = new PageForm(pageContent, phrase);
                    pageForm.Show();
                };


                panel.Controls.Add(lbl);
                panel.Controls.Add(btnShow);
                flpResults.Controls.Add(panel);
            }
        }

        // -------------------- Search button --------------------
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string phrase = txtSearch.Text;
            progressBar1.Value = 0;

            // Disable controls while searching
            txtMaxHexagons.Enabled = false;
            btnSearch.Enabled = false;

            stopSearch = false; // reset stop flag

            try
            {
                var results = await SearchLibraryFullAsync(phrase);

                DisplayResults(results);
            }
            finally
            {
                txtMaxHexagons.Enabled = true;
                btnSearch.Enabled = true;
            }
        }



        // -------------------- Load page button --------------------
        private void btnLoadPage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pathTb.Text))
            {
                MessageBox.Show("Please enter a path in the textbox or search for a phrase first.");
                return;
            }

            // Parse the path
            var parts = pathTb.Text.Split(new[] { "Hexagon ", "Wall ", "Shelf ", "Volume ", "Page ", ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 5)
            {
                MessageBox.Show("Invalid path format.");
                return;
            }

            int hexagon = int.Parse(parts[0]);
            int wall = int.Parse(parts[1]);
            int shelf = int.Parse(parts[2]);
            int volume = int.Parse(parts[3]);
            int page = int.Parse(parts[4]);

            // Get the phrase from the search box
            string phrase = txtSearch.Text.ToLower();
            int offset = -1;

            // Check if this page is the deterministic page for the phrase
            if (!string.IsNullOrWhiteSpace(phrase))
            {
                var phraseLoc = PhraseToLocation(phrase);
                if (phraseLoc.hexagon == hexagon &&
                    phraseLoc.wall == wall &&
                    phraseLoc.shelf == shelf &&
                    phraseLoc.volume == volume &&
                    phraseLoc.page == page)
                {
                    offset = phraseLoc.offset;
                }
                else
                {
                    // The phrase does not belong on this page
                    phrase = null;
                }
            }

            // Generate page content, inserting the phrase only if offset >= 0
            string pageContent = GeneratePage(hexagon, wall, shelf, volume, page);

            PageForm pageForm = new PageForm(pageContent, phrase);
            pageForm.Show();
        }


        private void pathTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaxHexagons_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtMaxHexagons.Text, out int value) && value > 0)
            {
                MaxHexagons = value;
            }
            else
            {
                MessageBox.Show("Please enter a valid positive integer for Max Hexagons.");
                txtMaxHexagons.Text = MaxHexagons.ToString();
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {

            stopSearch = true;
        }
    }

    public class PageForm : Form
    {
        private RichTextBox rtbPage;

        public PageForm(string pageContent, string phrase = null)
        {
            this.Text = "Page Viewer";
            this.Size = new Size(600, 400);

            rtbPage = new RichTextBox()
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                Text = pageContent,
                Font = new Font("Consolas", 10)
            };

            this.Controls.Add(rtbPage);

            if (!string.IsNullOrEmpty(phrase))
            {
                HighlightPhrase(phrase);
            }
        }

        private void HighlightPhrase(string phrase)
        {
            int index = 0;
            while ((index = rtbPage.Text.IndexOf(phrase, index, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                rtbPage.Select(index, phrase.Length);
                rtbPage.SelectionColor = Color.Red;
                index += phrase.Length;
            }

            // Deselect text
            rtbPage.SelectionStart = 0;
            rtbPage.SelectionLength = 0;
            rtbPage.SelectionColor = rtbPage.ForeColor;
        }
    }

}
