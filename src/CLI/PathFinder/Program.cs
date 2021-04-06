using System;
using System.Collections.Generic;
namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph map = new Graph("D:\\Users\\USER\\Documents\\OneDrive - Institut Teknologi Bandung\\Kuliah\\Semester 4\\IF2211 - Strategi Algoritma\\Tugas\\Tucil 3\\Path-Finder\\test\\jalanitb.txt");

            int start = Int32.Parse(Console.ReadLine());
            int destination = Int32.Parse(Console.ReadLine());

            PathFinder findPath = new PathFinder(map.GetNode(start).GetName(), map.GetNode(destination).GetName(), map);

            // Node node1 = map.GetNode("Gerbang Depan ITB");
            // Node node2 = map.GetNode("Rumah Sakit Santo Borromeus");
            // Console.WriteLine(node1.GetLatitude() + " " + node1.GetLongitude());
            // Console.WriteLine(node2.GetLatitude() + " " + node2.GetLongitude());
            // Console.WriteLine(node1.CalculateDistance(node2));
            // Console.WriteLine(HaversineFormula(node1.GetLatitude(), node1.GetLongitude(), node2.GetLatitude(), node2.GetLongitude()));
            findPath.PrintPath();
            Console.WriteLine(findPath.GetDistance());
        }
    }
}
