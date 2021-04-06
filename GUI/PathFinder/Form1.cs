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
        private List<Node> nodes;
        private List<List<bool>> adjMatrix;
        public string[] adjNode;
        public string[] identity;
        private Graph map;
        private Node node;
        private List<Node> path;
        double distance;
     
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


            nodes = new List<Node>();

            StreamReader graphFile = new StreamReader(filename);
            int n;

            Int32.TryParse(graphFile.ReadLine(), out n);

            for (int i = 1; i <= n; i++)
            {
                double latitude, longitude;
                string name = "";

                // format per line latitude longitude Nama
                identity = graphFile.ReadLine().Split(" ");

                latitude = Double.Parse(identity[0], System.Globalization.CultureInfo.InvariantCulture);
                longitude = Double.Parse(identity[1], System.Globalization.CultureInfo.InvariantCulture);

                // Double.TryParse(identity[0], out latitude);
                // Double.TryParse(identity[1], out longitude);

                for (int j = 2; j < identity.Length; j++)
                {
                    name += identity[j];


                    if (j != identity.Length - 1) name += " ";
                   
                }

                node = new Node(name, i, latitude, longitude);
                InsertNode(node);
            }

            List<List<bool>> adjMatrix = new List<List<bool>>();


            for (int i = 0; i < n; i++)
            {
                adjNode = graphFile.ReadLine().Split(" ");
                List<bool> adjMatrixRow = new List<bool>();

                for (int j = 0; j < n; j++)
                {
                    if (adjNode[j] == "1")
                    {
                        GetNode(i + 1).insertAdjNode(j + 1);
                        adjMatrixRow.Add(true);

                    }
                    else
                    {
                        adjMatrixRow.Add(false);
                    }
                }
                adjMatrix.Add(adjMatrixRow);


            }

            
            //MSAGL
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graphh = new Microsoft.Msagl.Drawing.Graph("graphh");
            //create the graph content 

            foreach (Node node in nodes)
            {
                foreach (int adjNode in node.GetAdjList())
                {
                    if (node.GetID() < GetNode(adjNode).GetID()) { 
                        var Edge = graphh.AddEdge(node.GetName(), GetNode(adjNode).GetName());
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.Diamond;
                    }
                }
            }

            viewer.Graph = graphh;
            //associate the viewer with the form 
            panel1.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Controls.Add(viewer);
            panel1.ResumeLayout();
            InsertAdjMatrix(adjMatrix);

            graphFile.Close();

        }

        public List<string> GetNodeNames()
        {
            List<string> names = new List<string>();

            foreach (Node node in nodes)
            {
                names.Add(node.GetName());
            }

            return names;
        }
        public void InsertAdjMatrix(List<List<bool>> adjMatrix)
        {
            this.adjMatrix = new List<List<bool>>(adjMatrix);
        }

        private void InsertNode(Node node)
        {
            nodes.Add(node);
        }

        private Node GetNode(int id)
        {
            foreach (Node node in nodes)
            {
                if (node.GetID() == id) return node;
            }
            return null;
        }

        private Node GetNode(string name)
        {
            foreach (Node node in nodes)
            {
                if (node.GetName().ToLower() == name.ToLower()) return node;
            }
            return null;
        }

        public void PrintNodes()
        {
            foreach (Node node in nodes)
            {
                Console.Write(node.GetName() + " ");
                foreach (int adjID in node.GetAdjList())
                {
                    Console.Write(GetNode(adjID).GetName() + " ");
                }
                Console.WriteLine();
            }
        }

        public void Clear()
        {
            foreach (Node node in nodes)
            {
                node.SetDistanceFromStart(-1);
                node.SetEstimatedDistance(-1);
                node.SetParentID(0);
                node.UnVisit();
            }
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

            Node startNode = map.GetNode(map.GetNode(richTextBox6.Text).GetName());
            Node destNode = map.GetNode(map.GetNode(textBox5.Text).GetName());
            Node currentNode = startNode;
            SortedQueue queue = new SortedQueue();
            currentNode.SetDistanceFromStart(0);
            currentNode.SetEstimatedDistance(currentNode.CalculateDistance(destNode));
            currentNode.Visit();
            while (currentNode != null)
            {
                foreach (int adjID in currentNode.GetAdjList())
                {
                    currentNode.Visit();
                    Node adjNode = map.GetNode(adjID);

                    if (adjNode.CalculateDistance(currentNode) + adjNode.CalculateDistance(destNode) + currentNode.GetDistanceFromStart() < adjNode.GetEstimatedDistance() || adjNode.GetEstimatedDistance() == -1)
                    {
                        adjNode.SetParentID(currentNode.GetID());

                        adjNode.SetDistanceFromStart(adjNode.CalculateDistance(currentNode) + currentNode.GetDistanceFromStart());

                        adjNode.SetEstimatedDistance(adjNode.GetDistanceFromStart() + adjNode.CalculateDistance(destNode));
                    }

                    // Kalo node belom divisit dan node bukan destination maka dimasukkin ke queue
                    if (!adjNode.GetVisited() && !Equals(adjNode, destNode))
                    {
                        queue.Enqueue(adjNode);
                    }
                }
                currentNode = queue.Dequeue();
            }
           

            Node currentNodee = destNode;
            path = new List<Node>();

            if (destNode.GetParentID() != 0)
            {
                do
                {
                    path.Add(currentNodee);
                    currentNodee = map.GetNode(currentNodee.GetParentID());
                } while (!Equals(startNode, currentNodee));
                path.Add(currentNodee);
                path.Reverse();
            }
            distance = destNode.GetDistanceFromStart();
            map.Clear();

            //MSAGL
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graphhh = new Microsoft.Msagl.Drawing.Graph("graphhh");
            //create the graph content 

            foreach (Node node1 in path)
            {
                foreach (int adjNode1 in node1.GetAdjList())
                {
                    if (node1.GetID() < GetNode(adjNode1).GetID())
                    {
                        var Edge = graphhh.AddEdge(node1.GetName(), GetNode(adjNode1).GetName());
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.Diamond;
                    }
                }
            }

            viewer.Graph = graphhh;
            //associate the viewer with the form 
            panel2.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            panel2.Controls.Add(viewer);
            panel2.ResumeLayout();

            richTextBox5.Text = findPath.GetDistance().ToString();
        }

        
        private void richTextBox5_TextChanged(object sender, EventArgs e){}
    }
}
