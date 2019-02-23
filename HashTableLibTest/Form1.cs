using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HashTableLib;

namespace HashTableLibTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                HashTable<string, string> ht = new HashTable<string, string>();

                WriteMsg("HashTable Capacity: " + ht.Capacity);
                WriteMsg("HashTable WhenEnlarge: " + ht.WhenEnlarge);
                WriteMsg("HashTable EnlargeRatio: " + ht.EnlargeRatio);
                WriteLine();

                string key = "aa";
                string value = "小明";
                uint i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                key = "bb";
                value = "小刚";
                i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                key = "哈哈";
                value = "小红";
                i = ht.Add(key, value);
                WriteAddMsg(key, value, i);


                key = "ccc";
                value = "小花";
                i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                key = "ddd";
                value = "小白";
                i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                WriteLine();

                key = "aa";
                value = ht[key];
                WriteGetMsg(key, value);

                key = "bb";
                value = ht[key];
                WriteGetMsg(key, value);

                key = "哈哈";
                value = ht[key];
                WriteGetMsg(key, value);

                key = "ccc";
                value = ht[key];
                WriteGetMsg(key, value);

                key = "ddd";
                value = ht[key];
                WriteGetMsg(key, value);

                WriteLine();
            }
            catch(Exception ex)
            {
                WriteMsg(ex.ToString());
                WriteLine();
            }
        }

        private void WriteAddMsg(string key, string value, uint i)
        {
            WriteMsg("Add { key: \"" + key + "\" value: \"" + value + "\" } ， key 进行 hash 计算得到的在数组中的 index 是 " + i + " 。");
        }

        private void WriteGetMsg(string key, string value)
        {
            WriteMsg("get by key: \"" + key + "\" ， value: \"" + value + "\" 。");
        }

        private void WriteMsg(string msg)
        {
            txtMsg.AppendText(DateTime.Now.ToString("HH:mm:ss.fff") + "  " + msg + "\r\n");
        }

        private void WriteLine()
        {
            txtMsg.AppendText("\r\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMsg.Clear();
        }

        private void btnTestDuplicateKey_Click(object sender, EventArgs e)
        {
            try
            {
                HashTable<string, string> ht = new HashTable<string, string>();

                string key = "aa";
                string value = "小明";
                uint i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                key = "aa";
                value = "小刚";
                i = ht.Add(key, value);
                WriteAddMsg(key, value, i);

                WriteLine();
            }
            catch (Exception ex)
            {
                WriteMsg(ex.ToString());
                WriteLine();
            }
        }

        private void btnTestRemove_Click(object sender, EventArgs e)
        {
            try
            {
                HashTable<string, string> ht = new HashTable<string, string>();

                string key = "aa";
                string value = "小明";
                uint i = ht.Add(key, value);

                WriteAddMsg(key, value, i);

                WriteMsg("HashTable Count: " + ht.Count + " 。");

                ht.Remove("aa");

                WriteMsg("Remove: \"aa\" 。");

                WriteMsg("HashTable Count: " + ht.Count + " 。");

                WriteLine();
            }
            catch (Exception ex)
            {
                WriteMsg(ex.ToString());
                WriteLine();
            }
        }
    }
}
