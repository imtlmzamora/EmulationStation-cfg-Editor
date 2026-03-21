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

namespace EmulatioStation_cfg_Editor
{
    public partial class mainForm : Form
    {
        Dictionary<string, launchConfig> DictLauncherConfig = new Dictionary<string, launchConfig>()
        {
            { "Dolphin",new launchConfig(){ fullScreen = true,fullScreenCmd = "--fullscreen",useBash = false,bash = "--batch --exec=\"%ROM_RAW%\"",libreteto = ""} },
            { "retroarch",new launchConfig(){ fullScreen = true,fullScreenCmd = "--fullscreen",useBash = false,bash = "\"%ROM_RAW%\"",libreteto = ""} }
        };

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

            updateLauncherListView();
            loadSettings();

            //update combobox launchers
            comboBox2.DataSource = listBox1.Items;
        }

        private void btnDltLauncher_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                typeLauncher toDelete = Properties.Settings.Default.launchersList.Contains("");
                Properties.Settings.Default.launchersList.Remove(toDelete);
                Properties.Settings.Default.Save();
               listBox1.Items.Remove(listBox1.SelectedItem);

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddLauncher.Enabled = listBox1.SelectedItem != null;
            btnDltLauncher.Enabled = listBox1.SelectedItem != null;
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
            listBox1.Items.Clear();
            foreach (var launcher in Properties.Settings.Default.launchersList)
            {
                listBox1.Items.Add(launcher.Name);
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
                    comboBox3.DataSource = strLibretrosLst;
                }
            }
        }

        public void loadSettings()
        {
            listBox2.Items.Clear();
            var systems = EmulationStationCfgReader.LoadSystems(Properties.Settings.Default.strSettingFilePath);

            foreach (var system in systems)
            {
                if (!platformsID.Keys.Contains(system.Name))
                    continue;

                listBox2.Items.Add(system.Name);
            }
        }

        public class EsSystem
        {
            public string Name { get; set; }
            public string FullName { get; set; }
            public string Path { get; set; }
            public string Extension { get; set; }
            public string Command { get; set; }
            public string Platform { get; set; }
            public string Theme { get; set; }
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
                        Extension = (string)x.Element("extension") ?? "",
                        Command = (string)x.Element("command") ?? "",
                        Platform = (string)x.Element("platform") ?? "",
                        Theme = (string)x.Element("theme") ?? ""
                    })
                    .ToList();

                return systems;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex < 0)
                return;


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
