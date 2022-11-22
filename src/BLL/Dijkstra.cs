using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PathInMaze
{
    public class Dijkstra
    {
        //algorithm
        private Maze maze;
        private int[,] length;
        private bool[,] visited;
        private Node[,] track;

        //color
        private Color visitedColor = ExtendColor.Blue;
        private Color resetColor = ExtendColor.BlueBerry;
        private Color pathColor = ExtendColor.Pink;
        private Color startPathNodeCircleColor = ExtendColor.Yellow;
        private Color endPathNodeCircleColor = ExtendColor.Green;

        public Dijkstra(Maze maze)
        {
            this.maze = maze;
            this.visited = new bool[maze.rows, maze.columns];
            this.length = new int[maze.rows, maze.columns];
            this.track = new Node[maze.rows, maze.columns];

            //set up
            for(int row = 0; row < maze.rows; row++)
            {
                for(int column = 0; column < maze.columns; column++)
                {
                    visited[row, column] = false;
                    length[row, column] = int.MaxValue;
                    track[row, column] = null;
                }
            }

            //set start path node
            SetLength(maze.startPathNode, 0);
            SetTrack(maze.startPathNode, maze.startPathNode);
        }

        public void Run()
        {
            //timer use for delay purpose
            Timer timer = new Timer();
            timer.Interval = 13000 / (maze.rows * maze.columns);
            timer.Tick += new EventHandler((obj, e) => Execute(timer));
            timer.Enabled = true;
        }
        private void Execute(Timer timer)
        {
            //Dijsktra Algorithm
            if(!GetVisited(maze.endPathNode))
            {
                Node minNode = GetMinLengthNode();
                SetVisited(minNode);
                minNode.ChangeCircleColor(visitedColor);

                foreach(Node adjNode in minNode.adjNodes)
                {
                    int oldLength = GetLength(adjNode);
                    int newLength = GetLength(minNode) + 1;

                    if(oldLength > newLength)
                    {
                        SetLength(adjNode, newLength);
                        SetTrack(adjNode, minNode);
                    }
                }
            }
            else //draw path after algorithm finish
            {
                timer.Enabled = false;
                timer.Dispose();

                DrawPath();
                maze.form.NextState();
            }
        }
        private Node GetMinLengthNode()
        {
            Node node = null;
            int min = Int32.MaxValue;

            for (int row = 0; row  < maze.rows; row++)
            {
                for (int column = 0; column < maze.columns; column++)
                {
                    if(length[row, column] < min && !visited[row, column])
                    {
                        min = length[row, column];
                        node = maze.graph[row, column];
                    }
                }
            }
            
            return node;
        }  
        private void DrawPath()
        {
            //reset all node color
            foreach(Node item in maze.graph)
            {
                if(item.position == maze.startPathNode.position)
                    item.ChangeCircleColor(startPathNodeCircleColor);
                else if (item.position == maze.endPathNode.position)
                    item.ChangeCircleColor(endPathNodeCircleColor);
                else
                    item.ChangeCircleColor(resetColor);
            }
            //tracking
            Stack<Node> stack = new Stack<Node>();
            Node node = GetTrack(maze.endPathNode);
            while(node.position != maze.startPathNode.position)
            {
                stack.Push(node);
                node = GetTrack(node);
            }
            
            //draw path
            while(stack.Count != 0)
            {
                Node trackNode = stack.Pop();
                trackNode.ChangeCircleColor(pathColor);
            }
        }

        private void SetLength(Node node, int value) => length[node.position.row, node.position.column] = value;
        private int GetLength(Node node) => length[node.position.row, node.position.column];
        private void SetTrack(Node node, Node trackNode) => track[node.position.row, node.position.column] = trackNode;
        private Node GetTrack(Node node) => track[node.position.row, node.position.column];
        private void SetVisited(Node node) => visited[node.position.row, node.position.column] = true;
        private bool GetVisited(Node node) => visited[node.position.row, node.position.column];
    }
}