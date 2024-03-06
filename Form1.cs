using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace ooameTextChange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePass = $@"{textBox1.Text}\{textBox2.Text}.txt";
            string outputFile = $@"{textBox1.Text}\{textBox2.Text}_shusei.txt"; // 結果を保存するファイルのパス

            // ファイルの存在チェック
            if (!File.Exists(filePass))
            {
                MessageBox.Show("指定されたファイルが見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var lines = File.ReadAllLines(filePass);
                using (StreamWriter sw = new StreamWriter(outputFile, false)) // false でファイルを新規作成または上書き
                {
                    foreach (var line in lines)
                    {
                        // 「.」の前後の空白を削除
                        var modifiedLine = Regex.Replace(line, @"\s*\.\s*", ".");
                        // アルファベットの後ろにある空白を削除（アルファベットの前の空白は削除しない）
                        modifiedLine = Regex.Replace(modifiedLine, @"([a-zA-Z])\s+", "$1");
                        // 残りの空白をすべてコンマに置き換える
                        modifiedLine = Regex.Replace(modifiedLine, @"\s+", ",");
                        sw.WriteLine(modifiedLine); // 修正した行を書き込む
                    }
                }
                MessageBox.Show($"ファイルが正常に保存されました: {outputFile}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ファイルの処理中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = folderBrowserDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
