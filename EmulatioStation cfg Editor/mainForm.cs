using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Diagnostics;

using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmulatioStation_cfg_Editor
{
    public partial class mainForm : Form
    {
        List<EsSystem> systems = new List<EsSystem>();
        EsSystem curSystem = new EsSystem();//define the system we are editing

        struct launchConfig
        {
            public bool fullScreen { get; set; }
            public string fullScreenCmd { get; set; }
            public bool useBash { get; set; }
            public string bash { get; set; }
            public string libreteto { get; set; }

        }
        //I literally copy this from the github of emulationstation :P
        Dictionary<string, string> platformsID = new Dictionary<string, string>
        {
            { "unknown", "" },// nothing set
            { "3do", "3do" },
            { "amiga", "amiga" },
            { "amstradcpc", "amstrad CPC" },
            { "apple2", "apple 2" },
            { "arcade", "arcade" },
            { "atari800", "atari 800" },
            { "atari2600", "atari 2600" },
            { "atari5200", "atari 5200" },
            { "atari7800", "atari 800" },
            { "atarilynx", "atari lynx" },
            { "atarist", "atarist" },
            { "atarijaguar", "atari jaguar" },
            { "atarijaguarcd", "atari jaguar cd" },
            { "atarixe", "atari xe" },
            { "colecovision", "ColecoVision" },
            { "c64", "commodore 64" }, // commodore 64
            { "intellivision", "intellivision" },
            { "macintosh", "macintosh" },
            { "xbox", "xbox" },
            { "xbox360", "xbox 360" },
            { "msx", "msx" },
            { "neogeo", "Neo Geo" },
            { "ngp", "Neo Geo Pocket" }, // neo geo pocket
            { "ngpc", "Neo Geo Pocket Color" }, // neo geo pocket color
            { "n3ds", "Nintendo 3DS" }, // nintendo 3DS
            { "n64", "Nintendo 64" }, // nintendo 64
            { "nds", "Nintendo DS" }, // nintendo DS
            { "nes", "Nintendo Entertainment System" }, // nintendo entertainment system
            { "gb", "Game Boy" }, // game boy
            { "gba", "Game Boy Advance" }, // game boy advance
            { "gbc", "Game Boy Color" }, // game boy color
            { "gc", "Game Cube" }, // gamecube
            { "wii", "WII" },
            { "wiiu", "WII U" },
            { "pc", "PC" },
            { "sega32x", "Sega 32x" },
            { "segacd", "Sega CD" },
            { "dreamcast", "DreamCast" },
            { "gamegear", "GameGear" },
            { "genesis", "Sega Genesis" }, // sega genesis
            { "mastersystem",  "Sega Master System" },// sega master system
            { "megadrive", "Sega Megadrive" }, // sega megadrive
            { "saturn", "Sega Saturn" }, // sega saturn
            { "psx", "PlayStation" },
            { "ps2", "PlayStation 2" },
            { "ps3", "PlayStation 3" },
            { "ps4", "PlayStation 4" },
            { "psvita", "PlayStation Vita" },
            { "psp",  "PlayStation Potable" },// playstation portable
            { "snes",  "Super Nintendo Entertainment System" },// super nintendo entertainment system
            { "pcengine", "turbografx-16" }, // turbografx-16/pcengine
            { "wonderswan", "wonderswan" },
            { "wonderswancolor", "wonderswancolor" },
            { "zxspectrum" ,"zxspectrum" },

            { "ignore", ""}, // do not allow scraping for this system
            { "invalid",""}
        };
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.strSettingFilePath))
            {
                MessageBox.Show("need the path of the setting csg file");
                load_es_systems_cfg();
            }
            if (Properties.Settings.Default.launchersList == null)
                Properties.Settings.Default.launchersList = new launcherList();
            
            if (Properties.Settings.Default.launchersList.Count <= 0)
            {
                //need to add a launcher

                MessageBox.Show("need to add in least one launcher");
                addLauncherFile();
            }

            cmbx_platform.DataSource = platformsID.Keys.ToList();
            updateLauncherListView();
            loadSettings();

        }

        private void btnDltLauncher_Click(object sender, EventArgs e)
        {
            if (lstbx_Launchers.SelectedItem != null)
            {
                typeLauncher toDelete = Properties.Settings.Default.launchersList.Contains("");
                Properties.Settings.Default.launchersList.Remove(toDelete);
                Properties.Settings.Default.Save();
               lstbx_Launchers.Items.Remove(lstbx_Launchers.SelectedItem);

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddLauncher.Enabled = lstbx_Launchers.SelectedItem != null;
            btnDltLauncher.Enabled = lstbx_Launchers.SelectedItem != null;
        }

        private void loadEsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load_es_systems_cfg();
        }

        private void load_es_systems_cfg()
        {
            //need the config cfg
            OpenFileDialog opfileDialog = new OpenFileDialog()
            {
                DefaultExt = ".cfg",
                Filter = "Executable (.cfg)|*.cfg"
            };

            // Show open file dialog box
            if (opfileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = opfileDialog.FileName;
                Properties.Settings.Default.strSettingFilePath = filename;
                Properties.Settings.Default.Save();
            }
            else
            {
                this.Close();
            }
        }

        private void btnAddLauncher_Click(object sender, EventArgs e)
        {
            addLauncherFile();
        }

        public void addLauncherFile()
        {
            OpenFileDialog opfileDialog = new OpenFileDialog()
            {
                DefaultExt = ".exe",
                Filter = "Executable (.exe)|*.exe"
            };

            // Show open file dialog box
            if (opfileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = opfileDialog.FileName;

                Properties.Settings.Default.launchersList.Add(new typeLauncher() { Name = Path.GetFileNameWithoutExtension(filename), Path = filename });
                Properties.Settings.Default.Save();
                updateLauncherListView();
            }
            else
            {
                this.Close();
            }

        }

        public void updateLauncherListView()
        {
            lstbx_Launchers.Items.Clear();
            foreach (var launcher in Properties.Settings.Default.launchersList)
            {
                lstbx_Launchers.Items.Add(launcher.Name);
                if (launcher.Name.ToLower().StartsWith("retroarch"))
                {
                    //update list libretetos
                    var files = Directory.GetFiles($"{Path.GetDirectoryName(launcher.Path)}\\cores");
                    List<string> strLibretrosLst = new List<string>();
                    string fileNoExt;
                    foreach (var file in files)
                    {
                        strLibretrosLst.Add(Path.GetFileNameWithoutExtension(file));
                    }
                    strLibretrosLst.Insert(0, "NA");
                    cmbx_libretro.DataSource = strLibretrosLst;
                }
            }

            //update combobox launchers
            cmbx_launcher.DataSource = lstbx_Launchers.Items;
        }

        public void loadSettings()
        {
            lstbx_Systems.Items.Clear();
            systems = EmulationStationCfgReader.LoadSystems(Properties.Settings.Default.strSettingFilePath);

            foreach (var system in systems)
            {
                if (!platformsID.Keys.Contains(system.Name))
                    continue;

                lstbx_Systems.Items.Add(system.Name);
            }

            lstbx_Systems.SelectedIndex = 0;
        }

        public class EsSystem
        {
            public string Name { get; set; } = "unknown";
            public string FullName { get; set; } = "";
            public string Path { get; set; } = "";
            public string Extensions { get; set; } = "";
            public string Command { get; set; } = "";
            public string Platform { get; set; } = "";
            public string Theme { get; set; } = "";
        }

        public static class EmulationStationCfgReader
        {
            public static List<EsSystem> LoadSystems(string filePath)
            {

                var doc = XDocument.Load(filePath);

                var systems = doc
                    .Root
                    .Elements("system")
                    .Select(x => new EsSystem
                    {
                        Name = (string)x.Element("name") ?? "",
                        FullName = (string)x.Element("fullname") ?? "",
                        Path = (string)x.Element("path") ?? "",
                        Extensions = (string)x.Element("extension") ?? "",
                        Command = (string)x.Element("command") ?? "",
                        Platform = (string)x.Element("platform") ?? "",
                        Theme = (string)x.Element("theme") ?? ""
                    })
                    .ToList();

                return systems;
            }
        }

        private void lstbx_Systems_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            btnUpdate.Enabled = false;

            if (lstbx_Systems.SelectedIndex < 0)
                return;
            string selectedName = lstbx_Systems.SelectedItem.ToString();

            curSystem = systems.FirstOrDefault(x => x.Name == selectedName);

            if (curSystem == null)
                return; //TODO: replace by error message

            XmlSystemHighlighter.Highlight(rTxtBx_SystemPreview, BuildSystemXmlPreview(curSystem));

            if (!cmbx_platform.Items.Contains(curSystem.Name))
                curSystem.Name = platformsID.FirstOrDefault().ToString(); //set as unknown


            //get set platform in UX

            //get extensions
            txtbx_Extensions.Text = curSystem.Extensions;
            //get Path
            //does path exist
            if (!Directory.Exists(curSystem.Path))
                errorProvider1.SetError(txtbx_GamesPath, "path doesn exist in this machine");

            txtbx_GamesPath.Text = curSystem.Path;

            var idx = cmbx_platform.FindStringExact(curSystem.Name);
            cmbx_platform.SelectedIndex = idx;
            //this auto sets the Full name, platform and theme

            //validate command
            if (string.IsNullOrWhiteSpace(curSystem.Command))
                curSystem.Command = Properties.Settings.Default.launchersList.FirstOrDefault().Path; //set defualt launcher

            //try to get the launcher
            string launcherPattern = @"[\\/]+([^\\/]+)(?=\.exe)";

            Match match = Regex.Match(curSystem.Command, launcherPattern, RegexOptions.IgnoreCase);

            string launcher = "";
            if (match.Success)
            {
                launcher = match.Groups[1].Value;
                if (cmbx_launcher.Items.Contains(launcher))
                    cmbx_launcher.SelectedIndex = cmbx_launcher.FindStringExact(launcher);
                else
                {
                    cmbx_launcher.SelectedItem = null;
                    errorProvider1.SetError(cmbx_launcher, "not installed launcher");
                }
            }
            //get Core
            string core = "";
            if (!string.Equals(launcher, "retroarch"))
                cmbx_libretro.SelectedIndex = 1;//set core to NA
            else
            {
                string corePattern = @"-L\s+.*[\\/]([^\\/]+_libretro)";
                match = Regex.Match(curSystem.Command, corePattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    core = match.Groups[1].Value;
                    if (cmbx_libretro.Items.Contains(core))
                        cmbx_libretro.SelectedIndex = cmbx_libretro.FindStringExact(core);
                    else
                    {
                        cmbx_libretro.SelectedItem = null;
                        errorProvider1.SetError(cmbx_libretro, "not installed core");
                    }
                }
                else
                {
                    cmbx_libretro.SelectedItem = null;
                    errorProvider1.SetError(cmbx_libretro, "not core configured");
                }
            }

            //get extras
            chk_fullscrn.Checked = Regex.IsMatch(curSystem.Command, @"\s-(f|-fullscreen)\b", RegexOptions.IgnoreCase);
            chkbox_bash.Checked = Regex.IsMatch(curSystem.Command, @"\s-(b|-batch)\b", RegexOptions.IgnoreCase);

            btnUpdate.Enabled = true;
        }

        private void updateSystem()
        {
            curSystem.Name = cmbx_platform.SelectedItem.ToString();
            curSystem.FullName = txtbx_FullName.Text;
            curSystem.Path = txtbx_GamesPath.Text;
            curSystem.Extensions = txtbx_Extensions.Text;
            curSystem.Command = buildCommand();//new command here
            curSystem.Platform = txtbx_platform.Text;
            curSystem.Theme = txtbx_theme.Text;

            XmlSystemHighlighter.Highlight(rTxtBx_SystemPreview, BuildSystemXmlPreview(curSystem));
        }

        public string buildCommand()
        {
            string newCommand = "";
            string launcherPath = Properties.Settings.Default.launchersList.FirstOrDefault(x => x.Name == cmbx_launcher.SelectedItem.ToString()).Path;
            string corePath = Path.GetDirectoryName(launcherPath);
            corePath = $"{corePath}\\cores\\{cmbx_libretro.SelectedItem.ToString()}.dll";
            //set the new launcher            
            newCommand = $"{launcherPath} ";
            string fullScreenParam = chk_fullscrn.Checked ? "-f " : " ";
            if (string.Equals(cmbx_launcher.SelectedItem.ToString(), "retroarch"))
            {
                //build a command for retroarch here
                newCommand += $"{fullScreenParam}-L \"{corePath}\" ";
            }
            else
            {
                //build command for no retroarch launcher
                string extraParam = chkbox_bash.Checked ? " --batch " : " ";
                newCommand += $"{extraParam}{fullScreenParam} --exec=";
            }
            newCommand += "\"%ROM_RAW%\"";
            return newCommand;
        }

        private void cmbx_platform_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbx_platform.SelectedItem.ToString()))
                return;

            txtbx_FullName.Text = platformsID[cmbx_platform.SelectedItem.ToString()];
            txtbx_platform.Text = cmbx_platform.SelectedItem.ToString();
            txtbx_theme.Text = cmbx_platform.SelectedItem.ToString();

            btn_GamesPath.Enabled = !string.IsNullOrEmpty(curSystem.FullName); //if platform full name is empty then is a invalid platform
        }


        public static string BuildSystemXmlPreview(EsSystem system)
        {
            var xml = new XElement("system",
                new XElement("name", system.Name),
                new XElement("fullname", system.FullName),
                new XElement("path", system.Path),
                new XElement("extension", system.Extensions),
                new XElement("command", system.Command),
                new XElement("platform", system.Platform),
                new XElement("theme", system.Theme)
            );

            return xml.ToString();
        }


        public static string NormalizeExtension(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return null;

            ext = ext.Trim().ToLowerInvariant();

            if (!ext.StartsWith("."))
                ext = "." + ext;

            return ext;
        }

        public static List<string> ParseExtensions(string extensionText)
        {
            if (string.IsNullOrWhiteSpace(extensionText))
                return new List<string>();

            return extensionText
                .Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => NormalizeExtension(x))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }
        public static List<string> GetSupportedExtensions(string infoFilePath)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(infoFilePath))
                return result;

            if (!File.Exists(infoFilePath))
                return result;

            string[] lines = File.ReadAllLines(infoFilePath);

            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine))
                    continue;

                string line = rawLine.Trim();

                if (line.StartsWith("supported_extensions", StringComparison.OrdinalIgnoreCase))
                {
                    int index = line.IndexOf('=');
                    if (index < 0)
                        continue;

                    string value = line.Substring(index + 1).Trim();

                    // Quitar comillas si existen
                    if (value.StartsWith("\"") && value.EndsWith("\"") && value.Length >= 2)
                        value = value.Substring(1, value.Length - 2);

                    result = value
                        .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => x.Trim())
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Select(x => x.StartsWith(".") ? x.ToLower() : "." + x.ToLower())
                        .Distinct()
                        .ToList();

                    break;
                }
            }

            return result;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateSystem();
        }
    }
    public static class XmlSystemHighlighter
    {
        public static void Highlight(RichTextBox box, string xml)
        {
            box.SuspendLayout();

            box.Text = xml;

            // reset color
            box.SelectAll();
            box.SelectionColor = Color.Black;

            // system
            HighlightTag(box, "system", Color.Blue);

            // elementos individuales
            HighlightTag(box, "name", Color.MediumPurple);
            HighlightTag(box, "fullname", Color.DarkBlue);
            HighlightTag(box, "path", Color.SaddleBrown);
            HighlightTag(box, "extension", Color.DarkGreen);
            HighlightTag(box, "command", Color.Firebrick);
            HighlightTag(box, "platform", Color.DarkOrange);
            HighlightTag(box, "theme", Color.Gray);

            box.Select(0, 0);
            box.ResumeLayout();
        }

        private static void HighlightTag(RichTextBox box, string tag, Color color)
        {
            // etiqueta de apertura <tag>
            HighlightPattern(box, $@"<{tag}>", color);

            // etiqueta de cierre </tag>
            HighlightPattern(box, $@"</{tag}>", color);
        }

        private static void HighlightPattern(RichTextBox box, string pattern, Color color)
        {
            foreach (Match match in Regex.Matches(box.Text, pattern))
            {
                box.Select(match.Index, match.Length);
                box.SelectionColor = color;
            }
        }
    }


    [Serializable]
    public class typeLauncher
    {
        public string Name { get; set; }
        public string Path { get; set; }

        // Constructor vacío necesario para el Diseñador
        public typeLauncher() { }
    }

    [Serializable]
    public class launcherList : List<typeLauncher>
    {
        // No necesita código extra, hereda todo de List
        public launcherList() { }

        public typeLauncher Contains(string name)
        {
            return this.FirstOrDefault(l => l.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
