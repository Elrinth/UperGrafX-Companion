using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                textBox1.Text = dlg.SelectedPath;
                button2.Enabled = (textBox1.Text != "");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = (textBox1.Text != "");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            string[] allfiles = null;
            try
            {
                allfiles = Directory.GetFiles(textBox1.Text, "*.pce", SearchOption.AllDirectories);
            }
            catch (Exception ex)
            {
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("Could not access folder: " + textBox1.Text + " exception: " + ex.ToString() + "\r\n");
                });
                button5.Enabled = true;
                return;
            }
            if (allfiles == null || allfiles.Length == 0)
            {
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("No CCD files were found in the folder: " + textBox1.Text + "! Are you sure you have CCD files there?\r\n");
                });
                button5.Enabled = true;
                return;
            }
            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("Found: " + allfiles.Length + " .ccd files. Will start creating .cdm file from them in 2 seconds...\r\n");
            });
            Task.Delay(1000).Wait();
            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("1 second...\r\n");
            });
            Task.Delay(1000).Wait();
            foreach (string file in allfiles)
            {
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("Reading: " + file + "\r\n");
                });
                
                string textInFile = File.ReadAllText(file);
                textInFile = textInFile.Replace("[CloneCD]", "[CDManipulator]");
                textInFile = textInFile.Replace("Version=3", "Version=2");
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("Writing: " + file.Replace(".ccd", ".cdm") + "\r\n");
                });
                File.WriteAllText(file.Replace(".ccd", ".cdm"), textInFile);
            }

            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("\r\n");
                textBox2.AppendText("Done with ccd/cdm files!\r\n");
            });
            button5.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button3.Enabled = (textBox3.Text != "");
        }
        private byte[] regionCheckSequence = new byte[] { 0xAD, 0x00, 0x10, 0x29, 0x40, 0xF0 };

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
                    } else
                    {
                        int a = 2;
                        int b = a;
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
            button5.Enabled = false;
            string[] allfiles = null;
            try
            {
                allfiles = Directory.GetFiles(textBox3.Text, "*.pce", SearchOption.AllDirectories);
            } catch (Exception ex)
            {
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("Could not access folder: " + textBox3.Text + " exception: " + ex.ToString() + "\r\n");
                });
                button5.Enabled = true;
                return;
            }
            if (allfiles == null || allfiles.Length == 0)
            {
                textBox2.Invoke((MethodInvoker)delegate ()
                {
                    textBox2.AppendText("No PCE files were found in the folder: " + textBox3.Text + "! Are you sure you have PCE files there?\r\n");
                });
                button5.Enabled = true;
                return;
            }
            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("Found: " + allfiles.Length + " .pce files. Will scan through all of them in 2 seconds...\r\n");
            });
            Task.Delay(1000).Wait();
            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("1 second...\r\n");
            });
            Task.Delay(1000).Wait();
            var totalHeaderCleared = 0;
            var totalRegionFixed = 0;
            var filesErrorWriting = 0;
            foreach (string file in allfiles)
            {
                bool madeAnyAdjustment = false;
                long length = new System.IO.FileInfo(file).Length;
                var bytesToSkip = 0;
                if (length % 0x2000 != 0)
                {
                    textBox2.Invoke((MethodInvoker)delegate ()
                    {
                        textBox2.AppendText(file + " has wrong filesize (" + length + " % 0x2000 != 0), removed 512 header bytes in the beginning!\r\n");
                    });
                    totalHeaderCleared++;
                    bytesToSkip = 512;
                    madeAnyAdjustment = true;
                }
                byte[] fileBytes = null;
                try
                {
                    fileBytes = File.ReadAllBytes(file).Skip(bytesToSkip).ToArray();
                }
                catch (Exception ex)
                {
                    textBox2.AppendText("Error, could not read file: " + file + " exception: " + ex.ToString() + "\r\n");
                    continue;
                }
                if (fileBytes == null)
                    continue;
                int indexOfRegion = RegionCheck(fileBytes);
                if (indexOfRegion != -1)
                {
                    fileBytes[indexOfRegion + regionCheckSequence.Length - 1] = 0x80;
                    totalRegionFixed++;
                    madeAnyAdjustment = true;
                    textBox2.Invoke((MethodInvoker)delegate ()
                    {
                        textBox2.AppendText(file + " has region check sequence, switched byte 0xF0 to 0x80 (U) -> (J)!\r\n");
                    });
                }

                if (madeAnyAdjustment)
                {
                    var toReplaceWith = textBox4.Text;
                    var replacedOriginal = false;
                    if (toReplaceWith == ".pce" || toReplaceWith == "" || checkBox1.Checked)
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
                        textBox2.Invoke((MethodInvoker)delegate ()
                        {
                            if (replacedOriginal)
                            {
                                textBox2.AppendText("Replaced original file: " + ugxName + " with fixed file!\r\n");
                            }
                            else
                            {
                                textBox2.AppendText("Wrote: " + ugxName + "!\r\n");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        textBox2.Invoke((MethodInvoker)delegate ()
                        {
                            textBox2.AppendText("Error, could not write file: " + ugxName + " exception: " + ex.ToString() + "\r\n");
                        });
                        filesErrorWriting++;
                    }

                }

            }

            textBox2.Invoke((MethodInvoker)delegate ()
            {
                textBox2.AppendText("\r\n");
                textBox2.AppendText("Done with pce files!\r\n");
                var total = (totalRegionFixed + totalHeaderCleared);
                textBox2.AppendText(total + "/" + allfiles.Length + " files needed fixing.\r\n");
                if (total != 0)
                {
                    textBox2.AppendText(totalRegionFixed + " region fixes were done.\r\n");
                    textBox2.AppendText(totalHeaderCleared + " headers were fixed.\r\n");
                }
                
                if (filesErrorWriting != 0)
                {
                    textBox2.AppendText("WARNING: " + filesErrorWriting + " files could not be written, check log above!\r\n");
                } 
                else
                {
                    textBox2.AppendText("Your library of .pce files are ready to be transferred to the UperGrafX!\r\n");
                }
            });
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                textBox3.Text = dlg.SelectedPath;
                button3.Enabled = (textBox3.Text != "");
            }
        }
        private string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = ReplaceInvalidChars(textBox4.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = !(checkBox1.Checked);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
