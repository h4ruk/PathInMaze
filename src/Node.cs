using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace PathInMaze
{
    public class Node : Control
    {
        //area of a node
        public static readonly int Area = 40;

        //color
        private Color circleColor = ExtendColor.BlueBerry;
        private Color wallColor = ExtendColor.White;
        private Color backColor = ExtendColor.BlueBerry;
        private Color startPathNodeCircleColor = ExtendColor.Yellow;
        private Color endPathNodeCircleColor = ExtendColor.Green;

        private Maze maze;
        private Wall walls;
        public Position position;
        public List<Node> adjNodes;

        public Node(Maze maze, int row, int column)
        {
            this.maze = maze;
            this.walls = Wall.ALL;
            this.position = new(row, column);
            this.adjNodes = new List<Node>();

            this.Location = new Point(column * Area, row * Area);
            this.Size = new Size(Area, Area);
            this.BackColor = backColor;
        }

        public Node GetRandomNeighborNode()
        {
            int row = position.row;
            int column = position.column;
            int rows = maze.rows;
            int columns = maze.columns;
            Node[,] graph = maze.graph;
            List<Node> neighbors = new List<Node>();

            //right node
            if( column != columns - 1 &&
                graph[row, column + 1].walls == Wall.ALL)
                    neighbors.Add(graph[row,column + 1]);
            //left node
            if( column != 0 && 
                graph[row, column - 1].walls == Wall.ALL)
                    neighbors.Add(graph[row, column - 1]);
            //bottom node
            if( row != rows - 1 &&
                graph[row + 1, column].walls == Wall.ALL)
                    neighbors.Add(graph[row + 1, column]);
            //top node
            if( row != 0 &&
                graph[row - 1, column].walls == Wall.ALL)
                    neighbors.Add(graph[row - 1, column]);

            return (neighbors.Count != 0) ? neighbors[new Random().Next(neighbors.Count)] : null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawCircle(e);
            DrawWall(e);
        }
        private void DrawWall(PaintEventArgs e)
        {
            using(Pen pen = new Pen(wallColor, 3f))
            {
                if((walls & Wall.RIGHT) == Wall.RIGHT)
                    e.Graphics.DrawLine(pen, Area, 0, Area, Area);
                if((walls & Wall.LEFT) == Wall.LEFT)
                    e.Graphics.DrawLine(pen, 0, 0, 0, Area);
                if((walls & Wall.BOTTOM) == Wall.BOTTOM)
                    e.Graphics.DrawLine(pen, 0, Area, Area, Area);
                if((walls & Wall.TOP) == Wall.TOP)
                    e.Graphics.DrawLine(pen, 0, 0, Area, 0);
            }
        }
        private void DrawCircle(PaintEventArgs e)
        {
            Brush brush = new SolidBrush(circleColor);
            RectangleF circleArea = new RectangleF(Area/7f,Area/7f, 5*Area / 7f, 5*Area / 7f);
            e.Graphics.FillEllipse(brush, circleArea);
            brush.Dispose();
        }

        protected override void OnClick(EventArgs e)
        {
            if(maze.startPathNode != null && maze.endPathNode != null) return;

            if(maze.startPathNode == null) 
            {
                maze.startPathNode = this;
                ChangeCircleColor(startPathNodeCircleColor);
            }
            else if (maze.endPathNode == null)
            {
                maze.endPathNode = this;
                ChangeCircleColor(endPathNodeCircleColor);
                maze.form.NextState();
            }    
        }

        public void ChangeCircleColor(Color color) 
        {
            this.circleColor = color;
            this.Invalidate();
        }

        public void Adjude(Node adjNode)
        {
            ModifyWall(adjNode);
            AddAdjNode(adjNode);
            ReDrawn(adjNode);
        }
        private void ModifyWall(Node adjNode)
        {
            Position direction = adjNode.position - this.position;

            //adjNode is on top(bottom) of baseNode
            if(direction.row != 0)
            {
                this.walls = (direction.row == -1) ? this.walls ^ Wall.TOP : this.walls ^ Wall.BOTTOM;
                adjNode.walls = (direction.row == -1) ? adjNode.walls ^ Wall.BOTTOM : adjNode.walls ^ Wall.TOP;
            }

            //adjNode is on left(right) of baseNode
            if(direction.column != 0)
            {
                this.walls = (direction.column == -1) ? this.walls ^ Wall.LEFT : this.walls ^ Wall.RIGHT;
                adjNode.walls = (direction.column == -1) ? adjNode.walls ^ Wall.RIGHT : adjNode.walls ^ Wall.LEFT;
            }

        }
        private void AddAdjNode(Node adjNode)
        {
            this.adjNodes.Add(adjNode);
            adjNode.adjNodes.Add(this);
        }
        private void ReDrawn(Node adjNode)
        {
            adjNode.Invalidate();
            this.Invalidate();
        }
    }
}