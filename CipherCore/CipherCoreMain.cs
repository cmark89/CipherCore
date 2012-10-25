using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CipherCore;

namespace CipherCore
{
    public partial class CipherCoreMain : Form
    {
        public CipherCoreMain()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                //Input is valid, begin the encryption process
                messageBox.Text = CipherCoreManager.Encrypt(messageBox.Text);
            }
            else
            {
                return;
            }

        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                //Input is valid, begin the decryption process
                messageBox.Text = CipherCoreManager.Decrypt(messageBox.Text);
            }
            else
            {
                return;
            }

        }

        private bool ValidateInput()
        {
            CipherCoreManager.SetKeyword(keywordBox.Text);

            //Returns true if the keyword and message both have input, and only contain legal characters
            string messageText = messageBox.Text.Trim();

            //If input contains carriage returns, remove them; or at least report the findings to the user.
            if (messageText.Contains("\n"))
            {
                Console.WriteLine("\\n detected.");
            }

            if (CipherCoreManager.ContainsOnlyLegalCharacters(keywordBox.Text) && CipherCoreManager.ContainsOnlyLegalCharacters(messageText) && keywordBox.Text.Length > 0 && messageText.Length > 0)
            {
                CipherCoreManager.cipherTable.BuildTranslationTable(keywordBox.Text);
                return true;
            }
            else
            {
                //Find the relevant error to return

                if (!CipherCoreManager.ContainsOnlyLegalCharacters(messageText))
                {
                    //Error; message box contains an illegal character
                    MessageBox.Show("Message contains illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (!CipherCoreManager.ContainsOnlyLegalCharacters(keywordBox.Text))
                {
                    //Error; the keyword contains an illegal character
                    MessageBox.Show("Keyword contains illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (messageText.Length == 0)
                {
                    //Error; the message length is 0
                    MessageBox.Show("Please input text.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if(keywordBox.Text.Length == 0)
                {
                    //Error; the message length is 0
                    MessageBox.Show("Please input keyword.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return false;
            }
        }

        private void CipherCoreMain_Load(object sender, EventArgs e)
        {

        }

        private void saltedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CipherCoreManager.salted = saltedCheckBox.Checked;
        }
    }
}
