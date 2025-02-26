namespace Topo_usp_Abeceda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graf = new Graph();
        }

    }

    class Graph
    {
        private Dictionary<char, Node> nodes { get; set; }

        public Graph() 
        {
            nodes = new Dictionary<char, Node>()
            {
                {'a',new Node('a') },
                {'b',new Node('b') },
                {'c',new Node('c') },
                {'d',new Node('d') }
            };

            //	pro: b<c c<d a<d c<a
            nodes['b'].Succesors.Add(nodes['c']);
            nodes['c'].Predecesors.Add(nodes['b']);

            nodes['c'].Succesors.Add(nodes['d']);
            nodes['d'].Predecesors.Add(nodes['c']);

            nodes['a'].Succesors.Add(nodes['d']);
            nodes['d'].Predecesors.Add(nodes['a']);

            nodes['c'].Succesors.Add(nodes['a']);
            nodes['a'].Predecesors.Add(nodes['c']);

            DFS(nodes['a']);
        }

        private Node DFS(Node initialNode) // vrací buď stok nebo null
        {
            initialNode.NodeState = Node.State.Open;
            int Time = 0;
            Node stok = null;
            DFS2(initialNode);
            return stok;

            void DFS2(Node node)
            {
                node.NodeState = Node.State.Open;
                Time += 1;
                node.InTime = Time;
                if (node.Succesors.Count > 0) // je kam pokračovat
                {
                    foreach (Node successor in node.Succesors)
                    {
                        if (successor.NodeState == Node.State.Unvisited)
                        {
                            DFS2(successor);
                            if (stok != null) // pokud se našel stok
                                return;
                        }
                    }
                    node.NodeState = Node.State.Closed;
                    Time += 1;
                    node.OutTime = Time;
                }
                else // našli jsme stok
                {
                    stok = node;
                    return;
                }
                    
            }
        }
    }



    class Node
    {
        
        public Node(char letter)
        { 
            Name = letter;
            
            Succesors = new List<Node>();
            Predecesors = new List<Node>();

            NodeState = State.Unvisited;
            InTime = null;
            OutTime = null;
        }
        public char Name { get; }

        public List<Node> Succesors { get; set; }
        public List<Node> Predecesors { get; set; }

        public enum State { Open, Unvisited, Closed }
        public State NodeState { get; set; }

        public int? InTime { get; set; }
        public int? OutTime { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }

    }
}
