using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using UperGrafX_Companion;

namespace Convert_CCD_to_CDM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (txtFolderToLookForCCDFiles.Text != "" && Directory.Exists(txtFolderToLookForCCDFiles.Text))
                {
                    dlg.SelectedPath = txtFolderToLookForCCDFiles.Text;
                }
                else
                {
                    dlg.SelectedPath = Directory.GetCurrentDirectory();
                }
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtFolderToLookForCCDFiles.Text = dlg.SelectedPath;
                btnFindCCDFilesAndCreateCDM.Enabled = (txtFolderToLookForCCDFiles.Text != "");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // load settings if there are any and apply them.
            if (File.Exists(@"settings.ini"))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"settings.ini", FileMode.Open, FileAccess.Read);
                settings = (Settings)formatter.Deserialize(stream);
                stream.Close();

                radOverwritePceFile.Checked = settings.OverwritePCEFiles;
                radCreateNewPceFile.Checked = !settings.OverwritePCEFiles;
                
                radConvertToJapaneseRegion.Checked = settings.ConvertToJapaneseRegion;
                radConvertToUSRegion.Checked = !settings.ConvertToJapaneseRegion;

                radDelReplaceBinCue.Checked = settings.OverwriteBinCueFiles;
                radOutputCdmToOtherDir.Checked = !settings.OverwriteBinCueFiles;

                txtFolderToLookForCCDFiles.Text = settings.LocationOfCCDFiles;
                txtFolderToLookForPCEFiles.Text = settings.LocationOfPCEFiles;
                txtLocationOfIkaebi.Text = settings.LocationOfIkaebi;
                txtOutputCdmImagesToFolder.Text = settings.LocationOfCdmFiles;
                txtFolderForBinCueFiles.Text = settings.LocationOfBinCueFiles;

                btnScanFolderForPceAndFix.Enabled = (txtFolderToLookForPCEFiles.Text != "");
                btnConvertBinCueToCDM.Enabled = (txtFolderForBinCueFiles.Text != "");
            } else
            {
                settings = new Settings();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnFindCCDFilesAndCreateCDM.Enabled = (txtFolderToLookForCCDFiles.Text != "");
            settings.LocationOfCCDFiles = txtFolderToLookForCCDFiles.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnClearLog.Enabled = false;
            string[] allfiles = null;
            try
            {
                allfiles = Directory.GetFiles(txtFolderToLookForCCDFiles.Text, "*.ccd", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Could not access folder: " + txtFolderToLookForCCDFiles.Text + " exception: " + ex.ToString() + "\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            if (allfiles == null || allfiles.Length == 0)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("No .ccd files were found in the folder: " + txtFolderToLookForCCDFiles.Text + "! Are you sure you have .ccd files there?\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("Found: " + allfiles.Length + " .ccd files. Will start creating .cdm file from them in 2 seconds...\r\n");
            });
            Task.Delay(1000).Wait();
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("1 second...\r\n");
            });
            Task.Delay(1000).Wait();
            foreach (string file in allfiles)
            {
                string textInFile = File.ReadAllText(file);
                textInFile = textInFile.Replace("[CloneCD]", "[CDManipulator]");
                textInFile = textInFile.Replace("Version=3", "Version=2");
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Writing: " + file.Replace(".ccd", ".cdm") + "\r\n");
                });
                File.WriteAllText(file.Replace(".ccd", ".cdm"), textInFile);
            }

            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("\r\n");
                txtLog.AppendText("Done with ccd/cdm files!\r\n");
                txtLog.AppendText("Successfully wrote: " + allfiles.Length + " .cdm files\r\n");
            });
            btnClearLog.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            btnScanFolderForPceAndFix.Enabled = (txtFolderToLookForPCEFiles.Text != "");
            settings.LocationOfPCEFiles = txtFolderToLookForPCEFiles.Text;
        }
        private byte[] regionCheckSequence = new byte[] { 0xAD, 0x00, 0x10, 0x29, 0x40, 0xF0 };

        private void SetRegion()
        {
            if (radConvertToUSRegion.Checked)
            {
                regionCheckSequence[regionCheckSequence.Length - 1] = 0x80;
            }
            else
            {
                regionCheckSequence[regionCheckSequence.Length - 1] = 0xF0;
            }
        }

        private int RegionCheck(byte [] iBytes)
        {
            for (int i = 0; i < iBytes.Length - regionCheckSequence.Length; i++)
            {
                var sequenceFound = true;
                for (int j = 0; j < regionCheckSequence.Length; j++)
                {
                    if (regionCheckSequence[j] != iBytes[i+j])
                    {
                        sequenceFound = false;
                        break;
                    }
                }
                if (sequenceFound)
                {
                    return i;
                }
            }
            return -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnClearLog.Enabled = false;
            string[] allfiles = null;
            try
            {
                allfiles = Directory.GetFiles(txtFolderToLookForPCEFiles.Text, "*.pce", SearchOption.AllDirectories);
            } catch (Exception ex)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Could not access folder: " + txtFolderToLookForPCEFiles.Text + " exception: " + ex.ToString() + "\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            if (allfiles == null || allfiles.Length == 0)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("No .pce files were found in the folder: " + txtFolderToLookForPCEFiles.Text + "! Are you sure you have .pce files there?\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("Found: " + allfiles.Length + " .pce files. Will scan through all of them in 2 seconds...\r\n");
            });
            Task.Delay(1000).Wait();
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("1 second...\r\n");
            });
            Task.Delay(1000).Wait();
            var totalHeaderCleared = 0;
            var totalRegionFixed = 0;
            var filesErrorWriting = 0;
            var problematicFiles = new List<string>();

            SetRegion();
            foreach (string file in allfiles)
            {
                bool madeAnyAdjustment = false;
                long length = new System.IO.FileInfo(file).Length;
                var bytesToSkip = 0;
                if (length % 0x2000 != 0)
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText(file + " has wrong filesize (" + length + " % 0x2000 != 0), removed 512 header bytes in the beginning!\r\n");
                    });
                    totalHeaderCleared++;
                    bytesToSkip = 512;
                    madeAnyAdjustment = true;
                }
                // check file length after potential filesize change...
                if (length- bytesToSkip > 0x100000)
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText(file + " is way too big (" + length + "Byte), this game will not be accepted with the UperGrafX!\r\n");
                    });
                    problematicFiles.Add(file + " is way too big (" + length + "Byte), this game will not be accepted with the UperGrafX!\r\n");
                }
                else if (length- bytesToSkip > 524288)
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText(file + " is too big (" + length + "Byte > 512KiB), this game will not work with the UperGrafX!\r\n");
                    });
                    problematicFiles.Add(file + " is too big (" + length + "Byte > 512KiB), this game will not work with the UperGrafX!\r\n");
                }
                byte[] fileBytes = null;
                try
                {
                    fileBytes = File.ReadAllBytes(file).Skip(bytesToSkip).ToArray();
                }
                catch (Exception ex)
                {
                    txtLog.AppendText("Error, could not read file: " + file + " exception: " + ex.ToString() + "\r\n");
                    continue;
                }
                if (fileBytes == null)
                    continue;
                int indexOfRegion = RegionCheck(fileBytes);
                if (indexOfRegion != -1)
                {
                    if (radConvertToJapaneseRegion.Checked)
                    {
                        fileBytes[indexOfRegion + regionCheckSequence.Length - 1] = 0x80;
                    }
                    else
                    {
                        fileBytes[indexOfRegion + regionCheckSequence.Length - 1] = 0xF0;
                    }
                    totalRegionFixed++;
                    madeAnyAdjustment = true;
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        if (radConvertToJapaneseRegion.Checked)
                        {
                            txtLog.AppendText(file + " has region check sequence, switched byte 0xF0 to 0x80 (U) -> (J)!\r\n");
                        }
                        else
                        {
                            txtLog.AppendText(file + " has region check sequence, switched byte 0x80 to 0xF0 (J) -> (U)!\r\n");
                        }
                    });
                }

                if (madeAnyAdjustment)
                {
                    var toReplaceWith = txtOfRenamePCE.Text;
                    var replacedOriginal = false;
                    if (toReplaceWith == ".pce" || toReplaceWith == "" || radOverwritePceFile.Checked)
                    {
                        toReplaceWith = ".pce";
                        replacedOriginal = true;
                    }
                    string ugxName = file.Replace(".pce", toReplaceWith);
                    try
                    {
                        using (var fs = new FileStream(ugxName, FileMode.Create, FileAccess.Write))
                        {
                            fs.Write(fileBytes, 0, fileBytes.Length);
                        }
                        txtLog.Invoke((MethodInvoker)delegate ()
                        {
                            if (replacedOriginal)
                            {
                                txtLog.AppendText("Replaced original file: " + ugxName + " with fixed file!\r\n");
                            }
                            else
                            {
                                txtLog.AppendText("Wrote: " + ugxName + "!\r\n");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        txtLog.Invoke((MethodInvoker)delegate ()
                        {
                            txtLog.AppendText("Error, could not write file: " + ugxName + " exception: " + ex.ToString() + "\r\n");
                        });
                        filesErrorWriting++;
                    }

                }

            }

            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("\r\n");
                txtLog.AppendText("Done with pce files!\r\n");
                var total = (totalRegionFixed + totalHeaderCleared);
                txtLog.AppendText(total + "/" + allfiles.Length + " files needed fixing.\r\n");
                if (total != 0)
                {
                    var fromRegion = "U";
                    var toRegion = "J";
                    if (radConvertToUSRegion.Checked)
                    {
                        fromRegion = "J";
                        toRegion = "U";
                    }
                    txtLog.AppendText(totalRegionFixed + " region fixes from " + fromRegion + " to " + toRegion + " were done.\r\n");
                    txtLog.AppendText(totalHeaderCleared + " headers were fixed.\r\n");
                }
                if (problematicFiles.Count != 0)
                {
                    txtLog.AppendText(problematicFiles.Count + " .pce-files has too large filesize and won't work on the UperGrafX!\r\n");
                }
                
                if (filesErrorWriting != 0)
                {
                    txtLog.AppendText("WARNING: " + filesErrorWriting + " files could not be written, check log above!\r\n");
                } 
                else if (problematicFiles.Count == 0)
                {
                    txtLog.AppendText("Your library of .pce files are ready to be transferred to the UperGrafX!\r\n");
                }
            });
            btnClearLog.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (txtFolderToLookForPCEFiles.Text != "" && Directory.Exists(txtFolderToLookForPCEFiles.Text))
                {
                    dlg.SelectedPath = txtFolderToLookForPCEFiles.Text;
                }
                else
                {
                    dlg.SelectedPath = Directory.GetCurrentDirectory();
                }
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtFolderToLookForPCEFiles.Text = dlg.SelectedPath;
                settings.LocationOfPCEFiles = txtFolderToLookForPCEFiles.Text;
                btnScanFolderForPceAndFix.Enabled = (txtFolderToLookForPCEFiles.Text != "");
            }
        }
        private string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            txtOfRenamePCE.Text = ReplaceInvalidChars(txtOfRenamePCE.Text);
            settings.ReplacePCEWith = txtOfRenamePCE.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        [DllImport("USER32.DLL")]
        private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("USER32.DLL")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("USER32.DLL")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("KERNEL32.DLL")]
        static extern uint GetCurrentThreadId();
        [DllImport("USER32.DLL")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);
        public static void FocusWindow(IntPtr focusOnWindowHandle)
        {
            var GWL_STYLE = -16;
            var WS_MINIMIZE = 0x20000000L;
            var SW_RESTORE = 0x09;
            var SW_SHOW = 0x05;
            long style = (long)GetWindowLong(focusOnWindowHandle, GWL_STYLE);

            // Minimize and restore to be able to make it active.
            if ((style & WS_MINIMIZE) == WS_MINIMIZE)
            {
                ShowWindow(focusOnWindowHandle, SW_RESTORE);
            }

            uint currentlyFocusedWindowProcessId = GetWindowThreadProcessId(GetForegroundWindow(), IntPtr.Zero);
            uint appThread = GetCurrentThreadId();

            if (currentlyFocusedWindowProcessId != appThread)
            {
                AttachThreadInput(currentlyFocusedWindowProcessId, appThread, true);
                BringWindowToTop(focusOnWindowHandle);
                ShowWindow(focusOnWindowHandle, SW_SHOW);
                AttachThreadInput(currentlyFocusedWindowProcessId, appThread, false);
            }

            else
            {
                BringWindowToTop(focusOnWindowHandle);
                ShowWindow(focusOnWindowHandle, SW_SHOW);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // check if ikaebi.exe is already running:
            var runningProcessByName = Process.GetProcessesByName("ikaebi");
            if (runningProcessByName.Length != 0)
            {

                //MessageBox.Show("ikaebi.exe is already running :)", "Already running!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("ikaebi.exe is already running, bringing forth the program!\r\n");
                });
                //SetForegroundWindow(runningProcessByName[0].MainWindowHandle);
                FocusWindow(runningProcessByName[0].MainWindowHandle);
                return;
            }
            // check if ikaebi.exe exists:
            if (!File.Exists(txtLocationOfIkaebi.Text))
            {
                //MessageBox.Show("ikaebi.exe not found in same folder. Did you place this program in the same folder? :)", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("ikaebi.exe not found. Are you sure this is the correct path?\r\n");
                });
                return;
            }
            const int ERROR_CANCELLED = 1223; //The operation was canceled by the user.

            ProcessStartInfo info = new ProcessStartInfo(txtLocationOfIkaebi.Text);
            info.UseShellExecute = true;
            info.Verb = "runas";
            info.WorkingDirectory = Path.GetDirectoryName(txtLocationOfIkaebi.Text);
            try
            {
                Process.Start(info);
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ERROR_CANCELLED)
                    MessageBox.Show("Why you no select Yes?");
                else
                    throw;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (txtLocationOfIkaebi.Text != "" && File.Exists(txtLocationOfIkaebi.Text))
            {
                openFileDialogIkaebi.FileName = txtLocationOfIkaebi.Text;
            }
            else
            {
                openFileDialogIkaebi.FileName = Directory.GetCurrentDirectory();
            }
            openFileDialogIkaebi.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtLocationOfIkaebi.Text = openFileDialogIkaebi.FileName;
            settings.LocationOfIkaebi = txtLocationOfIkaebi.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            settings.LocationOfIkaebi = txtLocationOfIkaebi.Text;
        }

        private void tabControl1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtOfRenamePCE.Enabled = !(radOverwritePceFile.Checked);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtOfRenamePCE.Enabled = !(radOverwritePceFile.Checked);
            settings.OverwritePCEFiles = radOverwritePceFile.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            settings.OverwritePCEFiles = radOverwritePceFile.Checked;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            settings.ConvertToJapaneseRegion = radConvertToJapaneseRegion.Checked;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            settings.ConvertToJapaneseRegion = radConvertToJapaneseRegion.Checked;
        }


        private void lblDaemonTools_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseDaemonToolsCL_Click(object sender, EventArgs e)
        {
            openFileDialogDaemonTools.ShowDialog();
        }

        private void openFileDialogDaemonTools_FileOk(object sender, CancelEventArgs e)
        {
            txtDaemonToolsCL.Text = openFileDialogDaemonTools.FileName;
            settings.LocationOfDaemonToolsCL = txtDaemonToolsCL.Text;
        }

        private void txtDaemonToolsCL_TextChanged(object sender, EventArgs e)
        {
            btnFindCCDFilesAndCreateCDM.Enabled = (txtDaemonToolsCL.Text != "");
            settings.LocationOfDaemonToolsCL = txtDaemonToolsCL.Text;
        }

        private void btnConvertBinCueToCDM_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;
            var pathToCdm = "cdm\\CdManipulator.exe";
            // make sure cd manipulator is installed...
            if (!File.Exists(pathToCdm))
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Could not find CD Manipulator.exe at: " + pathToCdm + "!\r\n");
                });
                return;
            }
            if (!File.Exists(txtDaemonToolsCL.Text))
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Could not find DTCommandLine.exe at: " + txtDaemonToolsCL.Text + "!\r\n");
                });
                return;
            }

            // find all bin/cue files:
            btnClearLog.Enabled = false;
            string[] allCueFiles = null;
            string[] allBinFiles = null;
            try
            {
                allCueFiles = Directory.GetFiles(txtFolderForBinCueFiles.Text, "*.cue", SearchOption.AllDirectories);
                allBinFiles = Directory.GetFiles(txtFolderForBinCueFiles.Text, "*.bin", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Could not access folder: " + txtFolderForBinCueFiles.Text + " exception: " + ex.ToString() + "\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            if (allCueFiles == null || allCueFiles.Length == 0)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("No .cue files were found in the folder: " + txtFolderForBinCueFiles.Text + "! Are you sure you have .cue files there?\r\n");
                });
                btnClearLog.Enabled = true;
                return;
            }
            if (allBinFiles == null || allBinFiles.Length == 0)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("No .bin files were found in the folder: " + txtFolderForBinCueFiles.Text + "! Are you sure you have .bin files there?\r\n");
                    if (allCueFiles != null)
                    {
                        txtLog.AppendText("You do however have " + allCueFiles.Length + ".cue files in the folder! Are they already converted to .cdm?\r\n");
                    }
                });
                btnClearLog.Enabled = true;
                return;
            }
            var approvedCueFiles = new List<string>();
            long totalFileSize = 0;
            var approvedBinFiles = new List<string>();

            Dictionary<string, List<string>> binFilesPerCue = new Dictionary<string, List<string>>();
            foreach (string cueFile in allCueFiles)
            {
                // read cue file to figure out which binfile we are looking for:
                // try here?
                string[] lines = null;
                try
                {
                    lines = File.ReadAllLines(cueFile);
                }
                catch (Exception ex)
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Could not read: " + cueFile + "! " + ex.ToString() + "\r\n");
                    });
                }
                if (lines == null)
                    continue;
                var binFileName = "";
                var hadBinFile = false;
                // read all lines to figure out which bin files we should actually remove afterwards if we selected remove...
                foreach (var line in lines)
                {
                    if (line.StartsWith("FILE \"") && line.EndsWith("\" BINARY"))
                    {
                        binFileName = line.Substring("FILE \"".Length, line.LastIndexOf("\" BINARY") - "FILE \"".Length);
                        if (binFileName != "" && binFileName.EndsWith(".bin"))
                        {
                            foreach (string binFile in allBinFiles)
                            {
                                if (binFile.Contains(binFileName))
                                {
                                    if (approvedBinFiles.Contains(binFile) == false)
                                    {
                                        if (!binFilesPerCue.ContainsKey(cueFile))
                                        {
                                            binFilesPerCue.Add(cueFile, new List<string>());
                                        }
                                        binFilesPerCue[cueFile].Add(binFile);
                                        approvedBinFiles.Add(binFile);
                                        // get file size:
                                        totalFileSize += new FileInfo(binFile).Length;
                                    }
                                    hadBinFile = true;
                                }
                            }
                        }
                    }
                }
                
                if (hadBinFile)
                {
                    approvedCueFiles.Add(cueFile);
                }
            }

            double estimatedTime = 65.0 / 686330064.0 * totalFileSize;


            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("Found: " + approvedCueFiles.Count + " .cue files with .bin files. Estimated time to convert: " + Math.Round(estimatedTime, 2) + " seconds. Conversion starting in 2 seconds...\r\n");
            });
            Task.Delay(1000).Wait();
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("1 second...\r\n");
            });
            Task.Delay(1000).Wait();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            ArrayList driveLetters = new ArrayList(26); // Allocate space for alphabet
            for (int i = 65; i < 91; i++) // increment from ASCII values for A-Z
            {
                driveLetters.Add(Convert.ToChar(i)); // Add uppercase letters to possible drive letters
            }

            foreach (string drive in Directory.GetLogicalDrives())
            {
                driveLetters.Remove(drive[0]); // removed used drive letters from possible drive letters
            }

            if (driveLetters.Count == 0)
            {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("No drive letters free! Please clear atleast one drive letter for us to use with daemon tools ffs!\r\n");
                });
                return;
            }
            // use first best drive letter which is free...
            string driveToUse = "" + driveLetters[0];
            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("Drive letter: " + driveToUse + " will be used for conversion.\r\n");
            });
            bool mountedAtleastOne = false;
            bool canceled = false;
            var binFilesRemoved = 0;
            var countCueFilesProcessed = 0;
            // CONVERT!
            foreach (string filePath in approvedCueFiles)
            {
                DateTime startConvertTime = DateTime.Now;
                if (!File.Exists(filePath))
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Could not find cue any longer @" + filePath + "!\r\n");
                    });
                    continue;
                }
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = txtDaemonToolsCL.Text,
                        Arguments = @"-" + (mountedAtleastOne ? "M" : "m") + " -l " + driveToUse + " -p \"" + filePath + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                string outPut = proc.StandardOutput.ReadToEnd();
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Mounted .bin/.cue cd image: " + (countCueFilesProcessed + 1) + " / " + approvedCueFiles.Count + " in drive: " + driveToUse + ": with file: " + filePath + ". " + outPut + "\r\n");
                });
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
                proc.Close();

                if (outPut == "Such image is already mounted\r\n")
                {

                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Aborted..." + filePath + " is already mounted...\r\n");
                    });
                    btnClearLog.Enabled = true;
                    return;
                }
                mountedAtleastOne = true;

                // convert to cdm...
                int driveIndex = -1;
                foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom))
                {
                    driveIndex++;
                    if (drive.Name.Replace(":\\", "").ToLower() == driveToUse.ToLower())
                    {
                        txtLog.Invoke((MethodInvoker)delegate ()
                        {
                            txtLog.AppendText(drive.Name.Replace("\\", "") + " is " + (drive.IsReady ? "ready" : "NOT READY!") + "\r\n");
                        });
                        break;
                    }
                }
                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
                // need to output to different dir 1st because .cue file will be locked by daemon tools so we can't "replace it".
                var tempPath = "temp\\" + fileNameWithoutExtension + ".cdm";

                if (!Directory.Exists(Path.GetDirectoryName(tempPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tempPath));
                }

                // start conversion!
                proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = pathToCdm,
                        Arguments = @"-driveindex " + driveIndex + " -o \"" + tempPath + "\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                outPut = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                exitCode = proc.ExitCode;
                proc.Close();

                TimeSpan diffPerCD = DateTime.Now - startConvertTime;
                TimeSpan diffTotal = DateTime.Now - startTime;
                if (outPut == "Canceled")
                {
                    canceled = true;
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Cancelled converting: " + tempPath + " after " + Math.Round(diffPerCD.TotalSeconds, 2) + " seconds. Aborting the rest.\r\n");
                    });
                    break;
                }
                else
                {
                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Finished converting to: " + tempPath + " after " + Math.Round(diffPerCD.TotalSeconds, 2) + " seconds. Estimated remaining time: " + (estimatedTime - diffTotal.TotalSeconds) + "\r\n");
                    });
                    if (radDelReplaceBinCue.Checked == true)
                    {
                        // unmount the file:
                        proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = txtDaemonToolsCL.Text,
                                Arguments = @"-u -l " + driveToUse,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };
                        proc.Start();
                        outPut = proc.StandardOutput.ReadToEnd();
                        proc.WaitForExit();
                        exitCode = proc.ExitCode;
                        proc.Close();
                        foreach (string binFile in binFilesPerCue[filePath])
                        {
                            if (File.Exists(binFile))
                            {
                                File.Delete(binFile);
                                binFilesRemoved++;
                            }
                        }
                        // delete the cue file
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        txtLog.Invoke((MethodInvoker)delegate ()
                        {
                            txtLog.AppendText("Unmounted: " + driveToUse + ": and removed " + binFilesPerCue[filePath].Count + " .bin file(s) and 1 .cue file.\r\n");
                        });
                    }
                    var targetPath = Path.GetDirectoryName(filePath) + "\\";
                    var tempfiles = Directory.GetFiles(Path.GetDirectoryName(@tempPath));
                    if (radDelReplaceBinCue.Checked == false && txtOutputCdmImagesToFolder.Text != "")
                    {
                        if (txtOutputCdmImagesToFolder.Text.Contains(":"))
                        {
                            targetPath = txtOutputCdmImagesToFolder.Text + "\\";
                        }
                        else
                        {
                            targetPath = targetPath + "\\" + txtOutputCdmImagesToFolder.Text + "\\";
                        }
                        
                    }
                    if (!Directory.Exists(Path.GetDirectoryName(targetPath)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                    }
                    foreach (var tempfile in tempfiles)
                    {
                        File.Move(tempfile, targetPath + Path.GetFileName(tempfile));
                    }

                    txtLog.Invoke((MethodInvoker)delegate ()
                    {
                        txtLog.AppendText("Finished moving tempfile(s) to path: " + targetPath + "\r\n");
                    });
                }
                countCueFilesProcessed++;
            }
            if (mountedAtleastOne)
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = txtDaemonToolsCL.Text,
                        Arguments = @"-r -l " + driveToUse,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
                var outPut = proc.StandardOutput.ReadToEnd();
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Unmounted: " + driveToUse + " ~ " + outPut + "\r\n");
                });
                proc.WaitForExit();
                var exitCode = proc.ExitCode;
                proc.Close();
            }

            TimeSpan diff = DateTime.Now - startTime;

            txtLog.Invoke((MethodInvoker)delegate ()
            {
                txtLog.AppendText("Finished converting " + countCueFilesProcessed + " .bin/.cue files after " + Math.Round(diff.TotalSeconds, 2) + " seconds!\r\n");
            });
            if (binFilesRemoved != 0) {
                txtLog.Invoke((MethodInvoker)delegate ()
                {
                    txtLog.AppendText("Deleted " + binFilesRemoved + " .bin files!\r\n");
                });
            }

            btnClearLog.Enabled = true;
        }


        private void radDelReplaceBinCue_CheckedChanged(object sender, EventArgs e)
        {
            txtOutputCdmImagesToFolder.Enabled = !radDelReplaceBinCue.Checked;
            settings.OverwriteBinCueFiles = radDelReplaceBinCue.Checked;
        }
        private void radOutputCdmToOtherDir_CheckedChanged(object sender, EventArgs e)
        {
            txtOutputCdmImagesToFolder.Enabled = !radDelReplaceBinCue.Checked;
            settings.OverwriteBinCueFiles = radDelReplaceBinCue.Checked;
        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void txtFolderForBinCueFiles_TextChanged(object sender, EventArgs e)
        {
            btnConvertBinCueToCDM.Enabled = (txtFolderForBinCueFiles.Text != "");
            settings.LocationOfBinCueFiles = txtFolderForBinCueFiles.Text;
        }

        private void btnBrowseBinCueFolder_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (txtFolderForBinCueFiles.Text != "" && Directory.Exists(txtFolderForBinCueFiles.Text))
                {
                    dlg.SelectedPath = txtFolderForBinCueFiles.Text;
                }
                else
                {
                    dlg.SelectedPath = Directory.GetCurrentDirectory();
                }
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtFolderForBinCueFiles.Text = dlg.SelectedPath;
                settings.LocationOfBinCueFiles = txtFolderForBinCueFiles.Text;
                btnConvertBinCueToCDM.Enabled = (txtFolderForBinCueFiles.Text != "");
            }
        }

        private void txtOutputCdmImagesToFolder_TextChanged(object sender, EventArgs e)
        {
            settings.LocationOfCdmFiles = txtOutputCdmImagesToFolder.Text;
        }

        private void btnBrowseWhereCdmOutput_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (txtOutputCdmImagesToFolder.Text != "" && Directory.Exists(txtOutputCdmImagesToFolder.Text))
                {
                    dlg.SelectedPath = txtOutputCdmImagesToFolder.Text;
                }
                else
                {
                    dlg.SelectedPath = Directory.GetCurrentDirectory();
                }
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                txtOutputCdmImagesToFolder.Text = dlg.SelectedPath;
                settings.LocationOfCdmFiles = txtOutputCdmImagesToFolder.Text;
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkYoutube_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.youtube.com/user/Elrinth/");
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {

        }

        private void lblMyWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Elrinth/UperGrafX-Companion");
        }

        private void linkDonate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://streamlabs.com/Elrinth/tip");
            
        }
    }
}
