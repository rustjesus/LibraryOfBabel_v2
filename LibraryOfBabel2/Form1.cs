using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace LibraryOfBabel2
{
    public partial class Form1 : Form
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz ,."; 
        private const int PageLength = 3200;

        private int StartHexagon = 0;
        private int EndHexagon = 10;

        private const int MaxWalls = 5;
        private const int MaxShelves = 10;
        private const int MaxVolumes = 20;
        private const int PagesPerVolume = 10;

        private bool stopSearch = false;

        public Form1()
        {
            InitializeComponent();
            flpResults.AutoScroll = true;

            txtStartHex.Text = StartHexagon.ToString();
            txtEndHex.Text = EndHexagon.ToString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }




        // ---------------------------------------------------------------------
        // Deterministic location from phrase
        // ---------------------------------------------------------------------
        private (int hexagon, int wall, int shelf, int volume, int page, int offset) PhraseToLocation(string phrase)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(phrase.ToLower()));

                int range = (EndHexagon - StartHexagon) + 1;

                int hexagon = StartHexagon + (hash[0] % range);
                int wall = hash[1] % MaxWalls;
                int shelf = hash[2] % MaxShelves;
                int volume = hash[3] % MaxVolumes;
                int page = hash[4] % PagesPerVolume;

                int offset = hash[5] % (PageLength - phrase.Length);
                return (hexagon, wall, shelf, volume, page, offset);
            }
        }

        // ---------------------------------------------------------------------
        // Generate a page
        // ---------------------------------------------------------------------
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

        // ---------------------------------------------------------------------
        // Quick deterministic search
        // ---------------------------------------------------------------------
        private List<string> SearchLibrary(string phrase)
        {
            phrase = phrase.ToLower();
            var locations = new List<string>();

            var loc = PhraseToLocation(phrase);
            locations.Add($"Hexagon {loc.hexagon}, Wall {loc.wall}, Shelf {loc.shelf}, Volume {loc.volume}, Page {loc.page}");

            return locations;
        }

        // ---------------------------------------------------------------------
        // Full brute-force search
        // ---------------------------------------------------------------------
        private async Task<List<string>> SearchLibraryFullAsync(string phrase)
        {
            phrase = phrase.ToLower();
            var locations = new List<string>();

            int hexCount = (EndHexagon - StartHexagon + 1);
            int totalPages = hexCount * MaxWalls * MaxShelves * MaxVolumes * PagesPerVolume;

            int processedPages = 0;

            if (StartHexagon > EndHexagon)
            {
                MessageBox.Show("Start hexagon is greater than end hexagon!");
                return new List<string>(); // return EMPTY list to satisfy the Task<List<string>>
            }

            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalPages;
            progressBar1.Value = 0;

            await Task.Run(() =>
            {
                for (int hex = StartHexagon; hex <= EndHexagon && !stopSearch; hex++)
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
                                    double percent = (processedPages * 100.0) / totalPages;

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

        // ---------------------------------------------------------------------
        // Display results
        // ---------------------------------------------------------------------
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
                    var parts = loc.Split(new[] { "Hexagon ", "Wall ", "Shelf ", "Volume ", "Page ", ", " }, StringSplitOptions.RemoveEmptyEntries);
                    int h = int.Parse(parts[0]);
                    int w = int.Parse(parts[1]);
                    int sIdx = int.Parse(parts[2]);
                    int v = int.Parse(parts[3]);
                    int p = int.Parse(parts[4]);

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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string phrase = txtSearch.Text;
            progressBar1.Value = 0;

            txtStartHex.Enabled = false;
            txtEndHex.Enabled = false;
            btnSearch.Enabled = false;

            stopSearch = false;

            try
            {
                var results = await SearchLibraryFullAsync(phrase);
                DisplayResults(results);
            }
            finally
            {
                txtStartHex.Enabled = true;
                txtEndHex.Enabled = true;
                btnSearch.Enabled = true;
            }
        }

        private void btnLoadPage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pathTb.Text))
            {
                MessageBox.Show("Please enter a path.");
                return;
            }

            var parts = pathTb.Text.Split(new[] { "Hexagon ", "Wall ", "Shelf ", "Volume ", "Page ", ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 5)
            {
                MessageBox.Show("Invalid path.");
                return;
            }

            int hexagon = int.Parse(parts[0]);
            int wall = int.Parse(parts[1]);
            int shelf = int.Parse(parts[2]);
            int volume = int.Parse(parts[3]);
            int page = int.Parse(parts[4]);

            string phrase = txtSearch.Text.ToLower();
            int offset = -1;

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
                    phrase = null;
                }
            }

            string pageContent = GeneratePage(hexagon, wall, shelf, volume, page);
            PageForm pageForm = new PageForm(pageContent, phrase);
            pageForm.Show();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            stopSearch = true;
        }

        private void txtStartHex_TextChanged_1(object sender, EventArgs e)
        {
            if (int.TryParse(txtStartHex.Text, out int value))
            {
                StartHexagon = value;

                if (StartHexagon > EndHexagon)
                {
                    EndHexagon = StartHexagon;
                    txtEndHex.Text = EndHexagon.ToString();
                }
            }

        }

        private void txtEndHex_TextChanged(object sender, EventArgs e)
        {
            
            if (int.TryParse(txtEndHex.Text, out int value))
                EndHexagon = value;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(pathTb.Text))
            {
                MessageBox.Show("Please enter a path.");
                return;
            }

            // Parse the path
            var parts = pathTb.Text.Split(new[] { "Hexagon ", "Wall ", "Shelf ", "Volume ", "Page ", ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 5)
            {
                MessageBox.Show("Invalid path.");
                return;
            }

            int hexagon = int.Parse(parts[0]);
            int wall = int.Parse(parts[1]);
            int shelf = int.Parse(parts[2]);
            int volume = int.Parse(parts[3]);
            int page = int.Parse(parts[4]);

            // Generate the page content
            string pageContent = GeneratePage(hexagon, wall, shelf, volume, page);

            try
            {
                // Create "Pages" folder if it doesn't exist
                string pagesFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pages");
                if (!System.IO.Directory.Exists(pagesFolder))
                {
                    System.IO.Directory.CreateDirectory(pagesFolder);
                }

                // Save file in the "Pages" folder
                string fileName = $"Hex{hexagon}_Wall{wall}_Shelf{shelf}_Vol{volume}_Page{page}.txt";
                string filePath = System.IO.Path.Combine(pagesFolder, fileName);

                System.IO.File.WriteAllText(filePath, pageContent);

                MessageBox.Show($"Page saved successfully to:\n{filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save page: {ex.Message}");
            }
        }


        private async void preloadButton_Click(object sender, EventArgs e)
        {
            txtStartHex.Enabled = false;
            txtEndHex.Enabled = false;
            preloadButton.Enabled = false;
            stopSearch = false;

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PreloadedLibrary.json");

            int hexCount = (EndHexagon - StartHexagon + 1);
            int totalPages = hexCount * MaxWalls * MaxShelves * MaxVolumes * PagesPerVolume;
            int processedPages = 0;

            progressBar1.Minimum = 0;
            progressBar1.Maximum = totalPages;
            progressBar1.Value = 0;

            try
            {
                using (var stream = new System.IO.StreamWriter(filePath))
                using (var writer = new JsonTextWriter(stream))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject(); // start dictionary

                    for (int hex = StartHexagon; hex <= EndHexagon && !stopSearch; hex++)
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
                                        string key = $"{hex}-{wall}-{shelf}-{vol}-{page}";

                                        writer.WritePropertyName(key);
                                        writer.WriteValue(content);

                                        processedPages++;
                                        if (processedPages % 50 == 0 || processedPages == totalPages)
                                        {
                                            double percent = (processedPages * 100.0) / totalPages;
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
                    }

                    writer.WriteEndObject();
                }

                MessageBox.Show($"All pages saved successfully to:\n{filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save JSON: {ex.Message}");
            }
            finally
            {
                txtStartHex.Enabled = true;
                txtEndHex.Enabled = true;
                preloadButton.Enabled = true;
            }
        }


        private async void btnSearchSaved_Click(object sender, EventArgs e)
        {
            string phrase = txtSearch.Text.ToLower();
            if (string.IsNullOrWhiteSpace(phrase))
            {
                MessageBox.Show("Please enter a search phrase.");
                return;
            }

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PreloadedLibrary.json");
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("PreloadedLibrary.json not found. Please preload the library first.");
                return;
            }

            txtStartHex.Enabled = false;
            txtEndHex.Enabled = false;
            btnSearch.Enabled = false;
            btnSearchSaved.Enabled = false;
            stopSearch = false;

            var results = new List<string>();

            await Task.Run(() =>
            {
                int processedPages = 0;
                int updateInterval = 50; // update progress every 50 pages

                using (var sr = new System.IO.StreamReader(filePath))
                using (var reader = new JsonTextReader(sr))
                {
                    while (reader.Read())
                    {
                        if (stopSearch) break;

                        if (reader.TokenType == JsonToken.PropertyName)
                        {
                            string key = (string)reader.Value;

                            if (!reader.Read()) break;

                            string content = reader.Value as string;
                            if (content != null && content.IndexOf(phrase, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                var parts = key.Split('-');
                                string loc = $"Hexagon {parts[0]}, Wall {parts[1]}, Shelf {parts[2]}, Volume {parts[3]}, Page {parts[4]}";
                                lock (results)
                                {
                                    results.Add(loc);
                                }
                            }

                            processedPages++;

                            // Update progress every updateInterval pages
                            if (processedPages % updateInterval == 0)
                            {
                                int displayCount = processedPages; // local copy for UI thread
                                Invoke((Action)(() =>
                                {
                                    progressBar1.Value = Math.Min(displayCount, progressBar1.Maximum);
                                    loadingPercentLabel.Text = $"Processed pages: {displayCount}";
                                }));
                            }
                        }
                    }

                    // Final progress update
                    Invoke((Action)(() =>
                    {
                        progressBar1.Value = Math.Min(processedPages, progressBar1.Maximum);
                        loadingPercentLabel.Text = $"Processed pages: {processedPages}";
                    }));

                }
            });

            if (results.Count == 0)
                MessageBox.Show("Phrase not found in preloaded library.");
            else
                DisplayResults(results);

            txtStartHex.Enabled = true;
            txtEndHex.Enabled = true;
            btnSearch.Enabled = true;
            btnSearchSaved.Enabled = true;
        }


    }

    // ---------------------------------------------------------------------
    // PageForm (unchanged)
    // ---------------------------------------------------------------------
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
                HighlightPhrase(phrase);
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

            rtbPage.SelectionStart = 0;
            rtbPage.SelectionLength = 0;
            rtbPage.SelectionColor = rtbPage.ForeColor;
        }
    }
}
