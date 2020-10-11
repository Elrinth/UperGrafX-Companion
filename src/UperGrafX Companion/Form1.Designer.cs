using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using UperGrafX_Companion;

namespace Convert_CCD_to_CDM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"settings.ini", FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, settings);
            stream.Close();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.openFileDialogIkaebi = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogDaemonTools = new System.Windows.Forms.OpenFileDialog();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radConvertToJapaneseRegion = new System.Windows.Forms.RadioButton();
            this.radConvertToUSRegion = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radCreateNewPceFile = new System.Windows.Forms.RadioButton();
            this.radOverwritePceFile = new System.Windows.Forms.RadioButton();
            this.txtOfRenamePCE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnScanFolderForPceAndFix = new System.Windows.Forms.Button();
            this.txtFolderToLookForPCEFiles = new System.Windows.Forms.TextBox();
            this.btnBrowsePceFolder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFindCCDFilesAndCreateCDM = new System.Windows.Forms.Button();
            this.btnBrowseCCDFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderToLookForCCDFiles = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseBinCueFolder = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolderForBinCueFiles = new System.Windows.Forms.TextBox();
            this.btnBrowseDaemonToolsCL = new System.Windows.Forms.Button();
            this.lblDaemonTools = new System.Windows.Forms.Label();
            this.txtDaemonToolsCL = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radDelReplaceBinCue = new System.Windows.Forms.RadioButton();
            this.btnBrowseWhereCdmOutput = new System.Windows.Forms.Button();
            this.radOutputCdmToOtherDir = new System.Windows.Forms.RadioButton();
            this.txtOutputCdmImagesToFolder = new System.Windows.Forms.TextBox();
            this.btnConvertBinCueToCDM = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLocationOfIkaebi = new System.Windows.Forms.TextBox();
            this.btnBrowseIkaebiExe = new System.Windows.Forms.Button();
            this.btnStartIkaebiExe = new System.Windows.Forms.Button();
            this.aboutPage = new System.Windows.Forms.TabPage();
            this.linkDonate = new System.Windows.Forms.LinkLabel();
            this.lblMyWebsite = new System.Windows.Forms.LinkLabel();
            this.lblAbout = new System.Windows.Forms.Label();
            this.linkYoutube = new System.Windows.Forms.LinkLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.aboutPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(5, 328);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(781, 222);
            this.txtLog.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Log";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(712, 299);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 9;
            this.btnClearLog.Text = "Clear log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.button5_Click);
            // 
            // openFileDialogIkaebi
            // 
            this.openFileDialogIkaebi.FileName = "openFileDialog1";
            this.openFileDialogIkaebi.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openFileDialogDaemonTools
            // 
            this.openFileDialogDaemonTools.FileName = "openFileDialog1";
            this.openFileDialogDaemonTools.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogDaemonTools_FileOk);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnScanFolderForPceAndFix);
            this.tabPage1.Controls.Add(this.txtFolderToLookForPCEFiles);
            this.tabPage1.Controls.Add(this.btnBrowsePceFolder);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(775, 266);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HuCard";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radConvertToJapaneseRegion);
            this.panel2.Controls.Add(this.radConvertToUSRegion);
            this.panel2.Location = new System.Drawing.Point(6, 141);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(754, 26);
            this.panel2.TabIndex = 18;
            // 
            // radConvertToJapaneseRegion
            // 
            this.radConvertToJapaneseRegion.AutoSize = true;
            this.radConvertToJapaneseRegion.Checked = true;
            this.radConvertToJapaneseRegion.Location = new System.Drawing.Point(7, 4);
            this.radConvertToJapaneseRegion.Name = "radConvertToJapaneseRegion";
            this.radConvertToJapaneseRegion.Size = new System.Drawing.Size(197, 17);
            this.radConvertToJapaneseRegion.TabIndex = 15;
            this.radConvertToJapaneseRegion.TabStop = true;
            this.radConvertToJapaneseRegion.Text = "Convert .pce files to japanese region";
            this.radConvertToJapaneseRegion.UseVisualStyleBackColor = true;
            this.radConvertToJapaneseRegion.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radConvertToUSRegion
            // 
            this.radConvertToUSRegion.AutoSize = true;
            this.radConvertToUSRegion.Location = new System.Drawing.Point(210, 4);
            this.radConvertToUSRegion.Name = "radConvertToUSRegion";
            this.radConvertToUSRegion.Size = new System.Drawing.Size(169, 17);
            this.radConvertToUSRegion.TabIndex = 16;
            this.radConvertToUSRegion.Text = "Convert .pce files to US region";
            this.radConvertToUSRegion.UseVisualStyleBackColor = true;
            this.radConvertToUSRegion.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radCreateNewPceFile);
            this.panel1.Controls.Add(this.radOverwritePceFile);
            this.panel1.Controls.Add(this.txtOfRenamePCE);
            this.panel1.Location = new System.Drawing.Point(6, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 32);
            this.panel1.TabIndex = 17;
            // 
            // radCreateNewPceFile
            // 
            this.radCreateNewPceFile.AutoSize = true;
            this.radCreateNewPceFile.Checked = true;
            this.radCreateNewPceFile.Location = new System.Drawing.Point(7, 4);
            this.radCreateNewPceFile.Name = "radCreateNewPceFile";
            this.radCreateNewPceFile.Size = new System.Drawing.Size(328, 17);
            this.radCreateNewPceFile.TabIndex = 15;
            this.radCreateNewPceFile.TabStop = true;
            this.radCreateNewPceFile.Text = "Place new file next to old .pce and replace .pce in filename with:";
            this.radCreateNewPceFile.UseVisualStyleBackColor = true;
            this.radCreateNewPceFile.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radOverwritePceFile
            // 
            this.radOverwritePceFile.AutoSize = true;
            this.radOverwritePceFile.Location = new System.Drawing.Point(497, 4);
            this.radOverwritePceFile.Name = "radOverwritePceFile";
            this.radOverwritePceFile.Size = new System.Drawing.Size(144, 17);
            this.radOverwritePceFile.TabIndex = 16;
            this.radOverwritePceFile.Text = "overwrite original .pce file";
            this.radOverwritePceFile.UseVisualStyleBackColor = true;
            this.radOverwritePceFile.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // txtOfRenamePCE
            // 
            this.txtOfRenamePCE.Location = new System.Drawing.Point(341, 3);
            this.txtOfRenamePCE.Name = "txtOfRenamePCE";
            this.txtOfRenamePCE.Size = new System.Drawing.Size(150, 20);
            this.txtOfRenamePCE.TabIndex = 12;
            this.txtOfRenamePCE.Text = "_ugx-02.pce";
            this.txtOfRenamePCE.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(708, 52);
            this.label4.TabIndex = 7;
            this.label4.Text = resources.GetString("label4.Text");
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // btnScanFolderForPceAndFix
            // 
            this.btnScanFolderForPceAndFix.Enabled = false;
            this.btnScanFolderForPceAndFix.Location = new System.Drawing.Point(584, 62);
            this.btnScanFolderForPceAndFix.Name = "btnScanFolderForPceAndFix";
            this.btnScanFolderForPceAndFix.Size = new System.Drawing.Size(176, 22);
            this.btnScanFolderForPceAndFix.TabIndex = 11;
            this.btnScanFolderForPceAndFix.Text = "Scan folder for .pce files and fix";
            this.btnScanFolderForPceAndFix.UseVisualStyleBackColor = true;
            this.btnScanFolderForPceAndFix.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFolderToLookForPCEFiles
            // 
            this.txtFolderToLookForPCEFiles.Location = new System.Drawing.Point(147, 63);
            this.txtFolderToLookForPCEFiles.Name = "txtFolderToLookForPCEFiles";
            this.txtFolderToLookForPCEFiles.Size = new System.Drawing.Size(350, 20);
            this.txtFolderToLookForPCEFiles.TabIndex = 8;
            this.txtFolderToLookForPCEFiles.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // btnBrowsePceFolder
            // 
            this.btnBrowsePceFolder.Location = new System.Drawing.Point(503, 62);
            this.btnBrowsePceFolder.Name = "btnBrowsePceFolder";
            this.btnBrowsePceFolder.Size = new System.Drawing.Size(75, 22);
            this.btnBrowsePceFolder.TabIndex = 10;
            this.btnBrowsePceFolder.Text = "Browse...";
            this.btnBrowsePceFolder.UseVisualStyleBackColor = true;
            this.btnBrowsePceFolder.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Folder to look for .pce files:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.aboutPage);
            this.tabControl1.Location = new System.Drawing.Point(5, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(783, 292);
            this.tabControl1.TabIndex = 17;
            this.tabControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabControl1_DragDrop);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(775, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CD Tools";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.btnFindCCDFilesAndCreateCDM);
            this.splitContainer1.Panel1.Controls.Add(this.btnBrowseCCDFolder);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtFolderToLookForCCDFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(773, 282);
            this.splitContainer1.SplitterDistance = 100;
            this.splitContainer1.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(611, 39);
            this.label3.TabIndex = 6;
            this.label3.Text = resources.GetString("label3.Text");
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnFindCCDFilesAndCreateCDM
            // 
            this.btnFindCCDFilesAndCreateCDM.Enabled = false;
            this.btnFindCCDFilesAndCreateCDM.Location = new System.Drawing.Point(532, 55);
            this.btnFindCCDFilesAndCreateCDM.Name = "btnFindCCDFilesAndCreateCDM";
            this.btnFindCCDFilesAndCreateCDM.Size = new System.Drawing.Size(237, 22);
            this.btnFindCCDFilesAndCreateCDM.TabIndex = 5;
            this.btnFindCCDFilesAndCreateCDM.Text = "Find .ccd files and create .cdm";
            this.btnFindCCDFilesAndCreateCDM.UseVisualStyleBackColor = true;
            this.btnFindCCDFilesAndCreateCDM.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnBrowseCCDFolder
            // 
            this.btnBrowseCCDFolder.Location = new System.Drawing.Point(451, 54);
            this.btnBrowseCCDFolder.Name = "btnBrowseCCDFolder";
            this.btnBrowseCCDFolder.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseCCDFolder.TabIndex = 2;
            this.btnBrowseCCDFolder.Text = "Browse...";
            this.btnBrowseCCDFolder.UseVisualStyleBackColor = true;
            this.btnBrowseCCDFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folder to look for .ccd  images: ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtFolderToLookForCCDFiles
            // 
            this.txtFolderToLookForCCDFiles.Location = new System.Drawing.Point(166, 56);
            this.txtFolderToLookForCCDFiles.Name = "txtFolderToLookForCCDFiles";
            this.txtFolderToLookForCCDFiles.Size = new System.Drawing.Size(279, 20);
            this.txtFolderToLookForCCDFiles.TabIndex = 0;
            this.txtFolderToLookForCCDFiles.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowseBinCueFolder);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtFolderForBinCueFiles);
            this.groupBox1.Controls.Add(this.btnBrowseDaemonToolsCL);
            this.groupBox1.Controls.Add(this.lblDaemonTools);
            this.groupBox1.Controls.Add(this.txtDaemonToolsCL);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.radDelReplaceBinCue);
            this.groupBox1.Controls.Add(this.btnBrowseWhereCdmOutput);
            this.groupBox1.Controls.Add(this.radOutputCdmToOtherDir);
            this.groupBox1.Controls.Add(this.txtOutputCdmImagesToFolder);
            this.groupBox1.Controls.Add(this.btnConvertBinCueToCDM);
            this.groupBox1.Location = new System.Drawing.Point(3, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(766, 176);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // btnBrowseBinCueFolder
            // 
            this.btnBrowseBinCueFolder.Location = new System.Drawing.Point(448, 80);
            this.btnBrowseBinCueFolder.Name = "btnBrowseBinCueFolder";
            this.btnBrowseBinCueFolder.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseBinCueFolder.TabIndex = 19;
            this.btnBrowseBinCueFolder.Text = "Browse...";
            this.btnBrowseBinCueFolder.UseVisualStyleBackColor = true;
            this.btnBrowseBinCueFolder.Click += new System.EventHandler(this.btnBrowseBinCueFolder_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Folder to look for .bin/.cue  images:";
            // 
            // txtFolderForBinCueFiles
            // 
            this.txtFolderForBinCueFiles.Location = new System.Drawing.Point(185, 81);
            this.txtFolderForBinCueFiles.Name = "txtFolderForBinCueFiles";
            this.txtFolderForBinCueFiles.Size = new System.Drawing.Size(257, 20);
            this.txtFolderForBinCueFiles.TabIndex = 17;
            this.txtFolderForBinCueFiles.TextChanged += new System.EventHandler(this.txtFolderForBinCueFiles_TextChanged);
            // 
            // btnBrowseDaemonToolsCL
            // 
            this.btnBrowseDaemonToolsCL.Location = new System.Drawing.Point(448, 53);
            this.btnBrowseDaemonToolsCL.Name = "btnBrowseDaemonToolsCL";
            this.btnBrowseDaemonToolsCL.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseDaemonToolsCL.TabIndex = 9;
            this.btnBrowseDaemonToolsCL.Text = "Browse...";
            this.btnBrowseDaemonToolsCL.UseVisualStyleBackColor = true;
            this.btnBrowseDaemonToolsCL.Click += new System.EventHandler(this.btnBrowseDaemonToolsCL_Click);
            // 
            // lblDaemonTools
            // 
            this.lblDaemonTools.AutoSize = true;
            this.lblDaemonTools.Location = new System.Drawing.Point(6, 57);
            this.lblDaemonTools.Name = "lblDaemonTools";
            this.lblDaemonTools.Size = new System.Drawing.Size(131, 13);
            this.lblDaemonTools.TabIndex = 10;
            this.lblDaemonTools.Text = "Location of Daemon tools:";
            this.lblDaemonTools.Click += new System.EventHandler(this.lblDaemonTools_Click);
            // 
            // txtDaemonToolsCL
            // 
            this.txtDaemonToolsCL.Location = new System.Drawing.Point(140, 54);
            this.txtDaemonToolsCL.Name = "txtDaemonToolsCL";
            this.txtDaemonToolsCL.Size = new System.Drawing.Size(303, 20);
            this.txtDaemonToolsCL.TabIndex = 8;
            this.txtDaemonToolsCL.Text = "C:\\Program Files\\DAEMON Tools Lite\\DTCommandLine.exe";
            this.txtDaemonToolsCL.TextChanged += new System.EventHandler(this.txtDaemonToolsCL_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(700, 52);
            this.label6.TabIndex = 16;
            this.label6.Text = resources.GetString("label6.Text");
            this.label6.Click += new System.EventHandler(this.label6_Click_1);
            // 
            // radDelReplaceBinCue
            // 
            this.radDelReplaceBinCue.AutoSize = true;
            this.radDelReplaceBinCue.Location = new System.Drawing.Point(529, 107);
            this.radDelReplaceBinCue.Name = "radDelReplaceBinCue";
            this.radDelReplaceBinCue.Size = new System.Drawing.Size(231, 17);
            this.radDelReplaceBinCue.TabIndex = 15;
            this.radDelReplaceBinCue.Text = "Output to same directory but delete .bin files";
            this.radDelReplaceBinCue.UseVisualStyleBackColor = true;
            this.radDelReplaceBinCue.CheckedChanged += new System.EventHandler(this.radDelReplaceBinCue_CheckedChanged);
            // 
            // btnBrowseWhereCdmOutput
            // 
            this.btnBrowseWhereCdmOutput.Location = new System.Drawing.Point(448, 106);
            this.btnBrowseWhereCdmOutput.Name = "btnBrowseWhereCdmOutput";
            this.btnBrowseWhereCdmOutput.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseWhereCdmOutput.TabIndex = 13;
            this.btnBrowseWhereCdmOutput.Text = "Browse...";
            this.btnBrowseWhereCdmOutput.UseVisualStyleBackColor = true;
            this.btnBrowseWhereCdmOutput.Click += new System.EventHandler(this.btnBrowseWhereCdmOutput_Click);
            // 
            // radOutputCdmToOtherDir
            // 
            this.radOutputCdmToOtherDir.AutoSize = true;
            this.radOutputCdmToOtherDir.Checked = true;
            this.radOutputCdmToOtherDir.Location = new System.Drawing.Point(6, 107);
            this.radOutputCdmToOtherDir.Name = "radOutputCdmToOtherDir";
            this.radOutputCdmToOtherDir.Size = new System.Drawing.Size(201, 17);
            this.radOutputCdmToOtherDir.TabIndex = 14;
            this.radOutputCdmToOtherDir.TabStop = true;
            this.radOutputCdmToOtherDir.Text = "Output .cdm images to other directory";
            this.radOutputCdmToOtherDir.UseVisualStyleBackColor = true;
            this.radOutputCdmToOtherDir.CheckedChanged += new System.EventHandler(this.radOutputCdmToOtherDir_CheckedChanged);
            // 
            // txtOutputCdmImagesToFolder
            // 
            this.txtOutputCdmImagesToFolder.Location = new System.Drawing.Point(213, 107);
            this.txtOutputCdmImagesToFolder.Name = "txtOutputCdmImagesToFolder";
            this.txtOutputCdmImagesToFolder.Size = new System.Drawing.Size(229, 20);
            this.txtOutputCdmImagesToFolder.TabIndex = 11;
            this.txtOutputCdmImagesToFolder.Text = "one_folder_above";
            this.txtOutputCdmImagesToFolder.TextChanged += new System.EventHandler(this.txtOutputCdmImagesToFolder_TextChanged);
            // 
            // btnConvertBinCueToCDM
            // 
            this.btnConvertBinCueToCDM.Location = new System.Drawing.Point(6, 132);
            this.btnConvertBinCueToCDM.Name = "btnConvertBinCueToCDM";
            this.btnConvertBinCueToCDM.Size = new System.Drawing.Size(754, 22);
            this.btnConvertBinCueToCDM.TabIndex = 7;
            this.btnConvertBinCueToCDM.Text = "Convert .bin/.cue isos to .cdm";
            this.btnConvertBinCueToCDM.UseVisualStyleBackColor = true;
            this.btnConvertBinCueToCDM.Click += new System.EventHandler(this.btnConvertBinCueToCDM_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.txtLocationOfIkaebi);
            this.tabPage3.Controls.Add(this.btnBrowseIkaebiExe);
            this.tabPage3.Controls.Add(this.btnStartIkaebiExe);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(775, 266);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "UperGrafX Control Panel";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.label7.Location = new System.Drawing.Point(7, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Location of ikaebi.exe";
            // 
            // txtLocationOfIkaebi
            // 
            this.txtLocationOfIkaebi.Location = new System.Drawing.Point(124, 3);
            this.txtLocationOfIkaebi.Name = "txtLocationOfIkaebi";
            this.txtLocationOfIkaebi.Size = new System.Drawing.Size(350, 20);
            this.txtLocationOfIkaebi.TabIndex = 19;
            this.txtLocationOfIkaebi.Text = "ikaebi.exe";
            this.txtLocationOfIkaebi.TextChanged += new System.EventHandler(this.textBox5_TextChanged);
            // 
            // btnBrowseIkaebiExe
            // 
            this.btnBrowseIkaebiExe.Location = new System.Drawing.Point(480, 2);
            this.btnBrowseIkaebiExe.Name = "btnBrowseIkaebiExe";
            this.btnBrowseIkaebiExe.Size = new System.Drawing.Size(75, 22);
            this.btnBrowseIkaebiExe.TabIndex = 20;
            this.btnBrowseIkaebiExe.Text = "Browse...";
            this.btnBrowseIkaebiExe.UseVisualStyleBackColor = true;
            this.btnBrowseIkaebiExe.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnStartIkaebiExe
            // 
            this.btnStartIkaebiExe.Location = new System.Drawing.Point(557, 2);
            this.btnStartIkaebiExe.Name = "btnStartIkaebiExe";
            this.btnStartIkaebiExe.Size = new System.Drawing.Size(225, 22);
            this.btnStartIkaebiExe.TabIndex = 18;
            this.btnStartIkaebiExe.Text = "Start UperGrafX control panel (ikaebi.exe)";
            this.btnStartIkaebiExe.UseVisualStyleBackColor = true;
            this.btnStartIkaebiExe.Click += new System.EventHandler(this.button6_Click);
            // 
            // aboutPage
            // 
            this.aboutPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.aboutPage.BackgroundImage = global::UperGrafX_Companion.Properties.Resources.turbo_grafx;
            this.aboutPage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.aboutPage.Controls.Add(this.linkDonate);
            this.aboutPage.Controls.Add(this.lblMyWebsite);
            this.aboutPage.Controls.Add(this.lblAbout);
            this.aboutPage.Controls.Add(this.linkYoutube);
            this.aboutPage.Location = new System.Drawing.Point(4, 22);
            this.aboutPage.Name = "aboutPage";
            this.aboutPage.Size = new System.Drawing.Size(775, 266);
            this.aboutPage.TabIndex = 3;
            this.aboutPage.Text = "About";
            // 
            // linkDonate
            // 
            this.linkDonate.AutoSize = true;
            this.linkDonate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.linkDonate.Location = new System.Drawing.Point(450, 250);
            this.linkDonate.Name = "linkDonate";
            this.linkDonate.Size = new System.Drawing.Size(322, 13);
            this.linkDonate.TabIndex = 3;
            this.linkDonate.TabStop = true;
            this.linkDonate.Text = "If you liked this program, you are free to donate here if you\'d like to.";
            this.linkDonate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDonate_LinkClicked);
            // 
            // lblMyWebsite
            // 
            this.lblMyWebsite.AutoSize = true;
            this.lblMyWebsite.Location = new System.Drawing.Point(219, 250);
            this.lblMyWebsite.Name = "lblMyWebsite";
            this.lblMyWebsite.Size = new System.Drawing.Size(109, 13);
            this.lblMyWebsite.TabIndex = 2;
            this.lblMyWebsite.TabStop = true;
            this.lblMyWebsite.Text = "This programs GitHub";
            this.lblMyWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblMyWebsite_LinkClicked);
            // 
            // lblAbout
            // 
            this.lblAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAbout.AutoSize = true;
            this.lblAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblAbout.Location = new System.Drawing.Point(7, 4);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(642, 143);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = resources.GetString("lblAbout.Text");
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // linkYoutube
            // 
            this.linkYoutube.ActiveLinkColor = System.Drawing.Color.RosyBrown;
            this.linkYoutube.AutoSize = true;
            this.linkYoutube.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.linkYoutube.Location = new System.Drawing.Point(4, 250);
            this.linkYoutube.Name = "linkYoutube";
            this.linkYoutube.Size = new System.Drawing.Size(103, 13);
            this.linkYoutube.TabIndex = 0;
            this.linkYoutube.TabStop = true;
            this.linkYoutube.Text = "My youtube channel";
            this.linkYoutube.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkYoutube_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 555);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "UperGrafx Companion";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.aboutPage.ResumeLayout(false);
            this.aboutPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Settings settings;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.OpenFileDialog openFileDialogIkaebi;
        private OpenFileDialog openFileDialogDaemonTools;
        private TabPage tabPage3;
        private Label label7;
        private TextBox txtLocationOfIkaebi;
        private Button btnBrowseIkaebiExe;
        private Button btnStartIkaebiExe;
        private TabPage tabPage2;
        private SplitContainer splitContainer1;
        private Label label3;
        private Button btnFindCCDFilesAndCreateCDM;
        private Button btnBrowseCCDFolder;
        private Label label1;
        private TextBox txtFolderToLookForCCDFiles;
        private GroupBox groupBox1;
        private Button btnBrowseBinCueFolder;
        private Label label8;
        private TextBox txtFolderForBinCueFiles;
        private Label label6;
        private RadioButton radDelReplaceBinCue;
        private Button btnBrowseWhereCdmOutput;
        private RadioButton radOutputCdmToOtherDir;
        private TextBox txtOutputCdmImagesToFolder;
        private Button btnConvertBinCueToCDM;
        private Label lblDaemonTools;
        private TextBox txtDaemonToolsCL;
        private Button btnBrowseDaemonToolsCL;
        private TabPage tabPage1;
        private Panel panel2;
        private RadioButton radConvertToJapaneseRegion;
        private RadioButton radConvertToUSRegion;
        private Panel panel1;
        private RadioButton radCreateNewPceFile;
        private RadioButton radOverwritePceFile;
        private TextBox txtOfRenamePCE;
        private Label label4;
        private Button btnScanFolderForPceAndFix;
        private TextBox txtFolderToLookForPCEFiles;
        private Button btnBrowsePceFolder;
        private Label label5;
        private TabControl tabControl1;
        private TabPage aboutPage;
        private LinkLabel linkYoutube;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label lblAbout;
        private LinkLabel linkDonate;
        private LinkLabel lblMyWebsite;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}

