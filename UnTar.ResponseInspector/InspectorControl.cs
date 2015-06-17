using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Tar;
using System.IO;
using System.Collections;

namespace UnTar.ResponseInspector
{
    public partial class InspectorControl : UserControl
    {

        public TarHelper tarHelper;

        public InspectorControl()
        {
            InitializeComponent();
            tarHelper = new TarHelper();
        }

        public void DoInspect(Session oSession)
        {
            DoLog("untar DoInspect");
            treeView1.Nodes.Clear();
            textBox1.Clear();
            tarHelper.Clear();
            tarHelper.initDicFromTarBytes(oSession.responseBodyBytes);
            foreach (String key in tarHelper.dic.Keys)
            {
                TreeNode node = new TreeNode();
                node.Text = key;
                treeView1.Nodes.Add(node);
            }

        }

        private void OnSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            if (tree != null)
            {
                TreeNode node = tree.SelectedNode;

                if (node != null)
                {
                    textBox1.Text = tarHelper.dic[node.Text];
                }

            }
        }

        private void DoLog(string log)
        {
            FiddlerApplication.Log.LogString(log);
        }

        private void DoLog(string format, params object[] args)
        {
            FiddlerApplication.Log.LogString(string.Format(format, args));
        }

    }
}
