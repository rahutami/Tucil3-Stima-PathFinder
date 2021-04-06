using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace PathFinder
{
    public partial class Form1 : Form
    {
        public string[] adjNode;
        public string[] identity;
        private Graph map;
     
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            //string readfile = File.ReadAllText(filename);
            textBox1.Text = filename;

            map = new Graph(filename);

            List<string> a = map.GetNodeNames();
            List<string> b = map.GetNodeNames();
            comboBox1.DataSource = a;
            comboBox2.DataSource = b;
            
            //MSAGL
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graphh = new Microsoft.Msagl.Drawing.Graph("graphh");
            //create the graph content 

            foreach (Node node in map.GetNodes())
            {
                foreach (int adjNode in node.GetAdjList())
                {
                    if (node.GetID() < adjNode) { 
                        var Edge = graphh.AddEdge(node.GetName(), map.GetNode(adjNode).GetName());
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.LabelText = node.CalculateDistance(map.GetNode(adjNode)).ToString() + " m";
                    }
                }
            }

            viewer.Graph = graphh;
            //associate the viewer with the form 
            panel1.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);
            panel1.ResumeLayout();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            int selectedIndex1 = comboBox1.SelectedIndex;
            Object selectedItem = comboBox1.SelectedItem;
            richTextBox6.Text = selectedItem.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            int selectedIndex2 = comboBox2.SelectedIndex;
            Object selectedItem2 = comboBox2.SelectedItem;
            //string start = richTextBox1.Text;
            //string destination = selectedItem2.ToString();
            textBox5.Text = selectedItem2.ToString();

            PathFinder findPath = new PathFinder(map.GetNode(richTextBox6.Text).GetName(), map.GetNode(textBox5.Text).GetName(), map);

            List<Node> path = findPath.GetPath();
            //MSAGL
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graphhh = new Microsoft.Msagl.Drawing.Graph("graphhh");
            //create the graph content 


            for(int i = 0; i < path.Count - 1; i++)
            {
                var Edge = graphhh.AddEdge(path[i].GetName(), path[i+1].GetName());
            }

            viewer.Graph = graphhh;
            //associate the viewer with the form 
            panel2.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(viewer);
            panel2.ResumeLayout();

            richTextBox5.Text = findPath.GetDistance().ToString() + " m";
        }

        
        private void richTextBox5_TextChanged(object sender, EventArgs e){}

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
