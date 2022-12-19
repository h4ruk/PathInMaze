using System.Windows.Forms;
using System.Collections.Generic;

namespace PathInMaze
{
    public class Maze : Control
    {
        public int rows;
        public int columns;
        public Node[,] graph;

        public MazeForm form;
        public Node endPathNode;
        public Node startPathNode;

        public Maze(MazeForm form, int rows, int columns)
        {
            InitComponent(form, rows, columns);
            Generate();
        }
        private void InitComponent(MazeForm form, int rows, int columns)
        {
            this.form = form;
            this.rows = rows;
            this.columns = columns;
            this.graph = new Node[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    this.graph[row, column] = new Node(this, row, column);
                    this.Controls.Add(graph[row, column]);
                }
            }
        }
        private void Generate()
        {
            Stack<Node> backtrackStack = new Stack<Node>();
            backtrackStack.Push(graph[0, 0]);
            RecursiveBacktracking(backtrackStack);
        }
        private void RecursiveBacktracking(Stack<Node> stack)
        {
            if(stack.Count == 0) return;

            Node currentNode = stack.Pop();
            Node neighborNode = currentNode.GetRandomNeighborNode();

            if(neighborNode == null) RecursiveBacktracking(stack);
            else
            {
                currentNode.Adjude(neighborNode);
                stack.Push(currentNode);
                stack.Push(neighborNode);
                RecursiveBacktracking(stack);
            }
        }
    }
}