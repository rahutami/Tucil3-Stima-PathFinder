using System;
using System.Collections.Generic;
using Viewer = Microsoft.Msagl.GraphViewerGdi.GViewer;
using MsaglGraph = Microsoft.Msagl.Drawing.Graph;
using Form = System.Windows.Forms.Form;
using Color = Microsoft.Msagl.Drawing.Color;
using Drawing = Microsoft.Msagl.Drawing;
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
            
            //create a viewer object 
            Viewer viewer = new Viewer();
            //create a graph object 
            MsaglGraph graphh = new MsaglGraph("graphh");
            //create the graph content 

            foreach (Node node in map.GetNodes())
            {
                foreach (int adjNode in node.GetAdjList())
                {
                    if (node.GetID() < adjNode) { 
                        var Edge = graphh.AddEdge(node.GetName(), map.GetNode(adjNode).GetName());
                        Edge.Attr.ArrowheadAtTarget = Drawing.ArrowStyle.None;
                        Edge.LabelText = node.CalculateDistance(map.GetNode(adjNode)).ToString() + " m";
                    }
                }
            }

            viewer.Graph = graphh;
            //associate the viewer with the form 
            panel1.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            if (panel1.Controls.Count != 0) panel1.Controls.RemoveAt(0);
            panel1.Controls.Add(viewer);
            panel1.ResumeLayout();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindPath();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FindPath();
        }

        private void FindPath(){
            if(comboBox1.Text == "" || comboBox2.Text == "") return;

            PathFinder findPath = new PathFinder(comboBox1.Text, comboBox2.Text, map);

            List<Node> path = findPath.GetPath();
            //create a viewer object 
            Viewer viewer = new Viewer();
            //create a graph object 
            MsaglGraph graphhh = new MsaglGraph("graphhh");
            //create the graph content 

            List<string> highlightedEdges = new List<string>();
            for(int i = 0; i < path.Count - 1; i++)
            {
                var Edge = graphhh.AddEdge(path[i].GetName(), path[i+1].GetName());
                highlightedEdges.Add(path[i].GetID() + " " + path[i+1].GetID());
                highlightedEdges.Add(path[i+1].GetID() + " " + path[i].GetID());
                Edge.Attr.Color = Color.Coral;
                Edge.Attr.ArrowheadAtTarget = Drawing.ArrowStyle.None;
                Edge.LabelText = path[i].CalculateDistance(path[i+1]).ToString() + " m";
            }

            foreach (Node node in map.GetNodes())
            {
                foreach (int adjNode in node.GetAdjList())
                {
                    if (node.GetID() < adjNode && !highlightedEdges.Contains(node.GetID() + " " + adjNode)) { 
                        var Edge = graphhh.AddEdge(node.GetName(), map.GetNode(adjNode).GetName());
                        Edge.Attr.ArrowheadAtTarget = Drawing.ArrowStyle.None;
                        Edge.LabelText = node.CalculateDistance(map.GetNode(adjNode)).ToString() + " m";
                    }
                }
            }

            foreach (Drawing.Node node in graphhh.Nodes)
            {
                node.Attr.Color = Color.LightBlue;
            }

            foreach (Node node in path){
                graphhh.FindNode(node.GetName()).Attr.FillColor = Color.Yellow;
            }

            viewer.Graph = graphhh;
            //associate the viewer with the form 
            panel1.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            if(panel1.Controls.Count != 0) panel1.Controls.RemoveAt(0);
            panel1.Controls.Add(viewer);
            panel1.ResumeLayout();

            if(findPath.GetDistance() == -1) richTextBox5.Text = "- m";
            else richTextBox5.Text = findPath.GetDistance().ToString() + " m";
        }
    }
}
