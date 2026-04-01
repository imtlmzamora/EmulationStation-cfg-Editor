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
        private EsSystem curSystem = new EsSystem();//define the system we are editing
        private EsSystem oriSystem = new EsSystem();//original system before changes

        struct launchConfig
        {
            public bool fullScreen { get; set; }
            public string fullScreenCmd { get; set; }
            public bool useBash { get; set; }
            public string bash { get; set; }
            public string libreteto { get; set; }

        }

        //I literally copy this from the github of the emulationstation project :P
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

            systems = getSystems();
            lstbx_Systems.DataSource = null;
            lstbx_Systems.DataSource = systems;
            lstbx_Systems.DisplayMember = "Name";
            RefreshSystemList();

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

        public List<EsSystem> getSystems()
        {
            return EmulationStationCfgReader.LoadSystems(Properties.Settings.Default.strSettingFilePath);
        }
        public void RefreshSystemList()
        {
            string selectedSystem = curSystem.Name;

            lstbx_Systems.DataSource = null;
            lstbx_Systems.DataSource = systems;
            lstbx_Systems.DisplayMember = "Name";

            //check is the system still exists
            if (selectedSystem == null)
            {
                lstbx_Systems.SelectedIndex = 0;
                return;
            }
            if (systems.Any(x => string.Equals(x.Name, selectedSystem, StringComparison.OrdinalIgnoreCase)))
            {
                //exist! we need the index
                int index = systems.FindIndex(x => string.Equals(x.Name, curSystem.Name, StringComparison.OrdinalIgnoreCase));
                lstbx_Systems.SelectedIndex = index;
            }else
            {
                lstbx_Systems.SelectedIndex = 0;
            }

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
            //new system selected.. reset all
            errorProvider1.Clear();

            ClearEditor();

            if (lstbx_Systems.SelectedIndex < 0)
                return;
            var temp = (EsSystem)lstbx_Systems.SelectedItem;

                curSystem = new EsSystem
                {
                    Name = temp.Name,
                    FullName = temp.FullName,
                    Path = temp.Path,
                    Extensions = temp.Extensions,
                    Command = temp.Command,
                    Platform = temp.Platform,
                    Theme = temp.Theme
                };

                if (curSystem != null)
                    oriSystem = new EsSystem()
                    {
                        Name = curSystem.Name,
                        FullName = curSystem.FullName,
                        Path = curSystem.Path,
                        Extensions = curSystem.Extensions,
                        Command = curSystem.Command,
                        Platform = curSystem.Platform,
                        Theme = curSystem.Theme
                    };

            if (curSystem == null)
                return; //TODO: replace by error message

            XmlSystemHighlighter.Highlight(rTxtBx_SystemPreview, BuildSystemXmlPreview(curSystem));

            if (!cmbx_platform.Items.Contains(curSystem.Name))
            {
                cmbx_platform.Text = curSystem.Name;
                errorProvider1.SetError(cmbx_platform,"platform not supported!!");
            }


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
                cmbx_libretro.SelectedIndex = 0;//set core to NA
            else
            {
                string corePattern = @"-L\s+.*[\\/]([^\\/]+_libretro)";
                match = Regex.Match(curSystem.Command, corePattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    core = match.Groups[1].Value;
                    if (cmbx_libretro.Items.Contains(core))
                    {
                        cmbx_libretro.SelectedIndex = cmbx_libretro.FindStringExact(core);
                    }
                    else
                    {
                        cmbx_libretro.Text = core;
                        errorProvider1.SetError(cmbx_libretro, "not installed core");
                    }
                }
                else
                {
                    cmbx_libretro.SelectedItem = 0;
                    errorProvider1.SetError(cmbx_libretro, "not core configured");
                }
            }

            //get extras
            chk_fullscrn.Checked = Regex.IsMatch(curSystem.Command, @"\s-(f|-fullscreen)\b", RegexOptions.IgnoreCase);
            chkbox_bash.Checked = Regex.IsMatch(curSystem.Command, @"\s-(b|-batch)\b", RegexOptions.IgnoreCase);

            button4.Enabled = true;
            btnUpdate.Enabled = true;
            updateSystem();
        }

        private bool updateSystem()
        {
            if (cmbx_platform.SelectedItem == null)
                return false;

            try
            {
                curSystem.Name = cmbx_platform.SelectedItem.ToString();
                curSystem.FullName = txtbx_FullName.Text;
                curSystem.Path = txtbx_GamesPath.Text;
                curSystem.Extensions = txtbx_Extensions.Text;
                curSystem.Command = buildCommand();//new command here
                curSystem.Platform = txtbx_platform.Text;
                curSystem.Theme = txtbx_theme.Text;


                XmlSystemHighlighter.Highlight(rTxtBx_SystemPreview, BuildSystemXmlPreview(curSystem));

                //are we updating a existing system?
                return !string.Equals(oriSystem.Name, curSystem.Name, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.FullName, curSystem.FullName, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.Path, curSystem.Path, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.Extensions, curSystem.Extensions, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.Command, curSystem.Command, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.Platform, curSystem.Platform, StringComparison.Ordinal) ||
                       !string.Equals(oriSystem.Theme, curSystem.Theme, StringComparison.Ordinal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public string buildCommand()
        {
            string newCommand = "";
            string launcherPath = "";
            string corePath = "";
            if (cmbx_launcher.SelectedItem != null)
                launcherPath = Properties.Settings.Default.launchersList.FirstOrDefault(x => x.Name == cmbx_launcher.SelectedItem.ToString()).Path;

            if (cmbx_libretro.SelectedItem == null)
                return "";

            if (!string.IsNullOrEmpty(launcherPath))
            {
                corePath = Path.GetDirectoryName(launcherPath);
                corePath = $"{corePath}\\cores\\{cmbx_libretro.SelectedItem.ToString()}.dll";
            }

            //set the new launcher            
            newCommand = $"{launcherPath} ";
            string fullScreenParam = chk_fullscrn.Checked ? "-f " : " ";
            if (string.Equals(cmbx_launcher.SelectedItem?.ToString(), "retroarch"))
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
            errorProvider1.SetError(cmbx_platform,"");
            if (cmbx_platform.SelectedItem == null)
                return;

            if (curSystem.Name != cmbx_platform.SelectedItem.ToString())
            {
                //the name changed, check if a system with the same name existe
                if (systems.Any(x => string.Equals(x.Name, cmbx_platform.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase)))
                {
                    txtbx_FullName.Text = "";
                    txtbx_platform.Text = "";
                    txtbx_theme.Text = "";
                    errorProvider1.SetError(cmbx_platform, "this system already exist");
                    return;
                }
                curSystem.Name = cmbx_platform.SelectedItem.ToString();
            }

            txtbx_FullName.Text = platformsID[curSystem.Name];
            txtbx_platform.Text = curSystem.Name;
            txtbx_theme.Text = curSystem.Name;

            btn_GamesPath.Enabled = cmbx_platform.SelectedItem != null; //if platform full name is empty then is a invalid platform
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

        public static XDocument BuildSystemsXml(List<EsSystem> systems)
        {
            return new XDocument(
                new XElement("systemList",
                    systems.Select(s => new XElement("system",
                        new XElement("name", s.Name ?? ""),
                        new XElement("fullname", s.FullName ?? ""),
                        new XElement("path", s.Path ?? ""),
                        new XElement("extension", s.Extensions ?? ""),
                        new XElement("command", s.Command ?? ""),
                        new XElement("platform", s.Platform ?? ""),
                        new XElement("theme", s.Theme ?? "")
                    ))
                )
            );
        }

        public static void SaveWithBackup(string filePath, List<EsSystem> systems)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Invalid file path");

            string backupPath = filePath + ".bak";
            string tempPath = filePath + ".tmp";

            // 1. Crear XML
            XDocument doc = BuildSystemsXml(systems);

            // 2. Guardar en archivo temporal
            doc.Save(tempPath);

            // 3. Crear backup si existe el original
            if (File.Exists(filePath))
            {
                File.Copy(filePath, backupPath, true);
            }

            // 4. Reemplazar archivo original
            File.Copy(tempPath, filePath, true);

            // 5. Limpiar temp
            File.Delete(tempPath);
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

        public static List<string> ParseStringToList(string extensionText)
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
            btnSave.Enabled = updateSystem();
        }


        private void cmbx_launcher_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkbox_bash_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chk_fullscrn_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cmbx_libretro_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.SetError(cmbx_libretro, "");
            if (cmbx_launcher.SelectedItem == null)
                return;

            if (cmbx_libretro.SelectedItem.ToString() != "NA")
            {
                string infopath = "";
                string launcherPath = Properties.Settings.Default.launchersList.FirstOrDefault(x => x.Name == cmbx_launcher.SelectedItem.ToString()).Path;

                if (!string.IsNullOrEmpty(launcherPath))
                {
                    infopath = Path.GetDirectoryName(launcherPath);
                    infopath = $"{infopath}\\info\\{cmbx_libretro.SelectedItem.ToString()}.info";
                    

                    //if the core has changed a new list is created
                    if (HasCoreChanged(curSystem))
                        txtbx_Extensions.Text = "";

                    //we validate that all extension exist in the current list
                    var currExt = ParseStringToList(txtbx_Extensions.Text);
                    var validExtension = GetSupportedExtensions(infopath);

                    foreach (var ext in validExtension)
                    {
                        if (!currExt.Contains(ext))
                            currExt.Add(ext);
                    }
                    txtbx_Extensions.Text = string.Join(" ", currExt);
                }
            }
        }

        public bool HasCoreChanged(EsSystem system)
        {
            string curLauncherPath = "";
            string currCorePath = "";

            if (cmbx_launcher.SelectedItem != null)
                curLauncherPath = Properties.Settings.Default.launchersList.FirstOrDefault(x => x.Name == cmbx_launcher.SelectedItem.ToString()).Path;

            if (!string.IsNullOrEmpty(curLauncherPath))
            {
                currCorePath = Path.GetDirectoryName(curLauncherPath);
                currCorePath = $"{currCorePath}\\cores\\{cmbx_libretro.SelectedItem.ToString()}.dll";
            }

            if (string.IsNullOrWhiteSpace(currCorePath))
                return false;

            string currentCore = Path.GetFileNameWithoutExtension(currCorePath);
            string lastCore = "";

            if (!string.IsNullOrWhiteSpace(system.Command))
            {
                string corePattern = @"-L\s+.*[\\/]([^\\/]+_libretro)";
                Match match = Regex.Match(curSystem.Command, corePattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    lastCore = match.Groups[1].Value;                    
                }                
            }

            if (!string.Equals(lastCore, currentCore, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //it is a update if the name doesnt changed
            if (oriSystem.Name == curSystem.Name)
            {
                //get index
                int index = systems.FindIndex(x => string.Equals(x.Name, curSystem.Name, StringComparison.OrdinalIgnoreCase));
                if (index < 0 || index >= systems.Count)
                    return;

                //this overwrite the selected system by index
                systems[index].Name = cmbx_platform.SelectedItem.ToString();
                systems[index].FullName = txtbx_FullName.Text;
                systems[index].Path = txtbx_GamesPath.Text;
                systems[index].Extensions = txtbx_Extensions.Text;
                systems[index].Command = buildCommand();//new command here
                systems[index].Platform = txtbx_platform.Text;
                systems[index].Theme = txtbx_theme.Text;
            }
            else
            {
                //the name changed, we need to check there is not other with the same name
                if (systems.Any(x => string.Equals(x.Name, curSystem.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("a system with this name already exist, selected it to edit that one instead", "duplicated system", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                    //this add a new system
                    systems.Add(curSystem);
            }
            
            

            RefreshSystemList();
        }

        private void setRomPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveWithBackup(Properties.Settings.Default.strSettingFilePath,systems);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DeleteCurrentSystem();
        }

        private void DeleteCurrentSystem()
        {
            int index = lstbx_Systems.SelectedIndex;

            if (index < 0 || index >= systems.Count)
                return;

            systems.RemoveAt(index);

            RefreshSystemList();

            if (systems.Count == 0)
            {
                ClearEditor();
                rTxtBx_SystemPreview.Clear();
                btnSave.Enabled = false;
                return;
            }

            int newIndex = index;
            if (newIndex >= systems.Count)
                newIndex = systems.Count - 1;

            lstbx_Systems.SelectedIndex = newIndex;
        }

        private void ClearEditor()
        {
            txtbx_FullName.Text = "";
            txtbx_GamesPath.Text = "";
            txtbx_Extensions.Text = "";
            txtbx_platform.Text = "";
            txtbx_theme.Text = "";

            cmbx_platform.SelectedItem = null;
            cmbx_launcher.SelectedItem = null;
            cmbx_libretro.SelectedItem = null;

            chkbox_bash.Checked = false;
            chk_fullscrn.Checked = false;

            btnUpdate.Enabled = false;
            btnSave.Enabled = false;
            button4.Enabled = false;

            rTxtBx_SystemPreview.Text = "";
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
