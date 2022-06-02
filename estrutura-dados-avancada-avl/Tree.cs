namespace estrutura_dados_avancada_avl
{
    public class Tree
    {
        private Node? _root;

        public void ShowTree()
        {
            if (_root == null) Console.WriteLine("Árvore vazia...");
            else ShowTreeReadble(_root);
        }

        public void ShowTreeReadble(Node node, int nivel = 0, int lado = 0)
        {
            while (true)
            {
                var ladoNome = lado switch
                {
                    0 => "Nó raiz: ",
                    1 => $"Nó esquerdo de {node.Dad!.Valor}: ",
                    2 => $"Nó Direito de {node.Dad!.Valor}: ",
                    _ => ""
                };

                Console.WriteLine("".PadLeft(nivel, ' ') + ladoNome + node.Valor);
                if (node.Left is not null) ShowTreeReadble(node.Left, nivel + 3, 1);
                if (node.Right is not null)
                {
                    node = node.Right;
                    nivel += 3;
                    lado = 2;
                    continue;
                }

                break;
            }
        }

        public void Insert(int valor)
        {
            var newItem = new Node(valor);
            _root = _root == null ? newItem : RecursiveInsert(_root, newItem);
        }
        private Node RecursiveInsert(Node? currentNode, Node insertNode)
        {
            if (currentNode == null)
            {
                currentNode = insertNode;
                return currentNode;
            }
            if (insertNode.Valor < currentNode.Valor)
            {
                currentNode.Left = RecursiveInsert(currentNode.Left, insertNode);
                currentNode.Left.Dad = currentNode;
                currentNode = BalanceTree(currentNode);
            }
            else if (insertNode.Valor > currentNode.Valor)
            {
                currentNode.Right = RecursiveInsert(currentNode.Right, insertNode);
                currentNode.Right.Dad = currentNode;
                currentNode = BalanceTree(currentNode);
            }
            return currentNode;
        }
        private Node BalanceTree(Node currentNode)
        {
            var balanceFactor = BalanceFactor(currentNode);
            if (balanceFactor > 1)
            {
                if (BalanceFactor(currentNode.Left!) > 0)
                {
                    //currentNode = RotateLL(currentNode);
                }
                else
                {
                    //currentNode = RotateLR(currentNode);
                }
            }
            else if (balanceFactor < -1)
            {
                if (BalanceFactor(currentNode.Right!) > 0)
                {
                    //currentNode = RotateRL(currentNode);
                }
                else
                {
                    currentNode = RotateSingleToLeft(currentNode);
                }
            }
            return currentNode;
        }

        private static int BalanceFactor(Node currentNode)
        {
            var l = GetHeight(currentNode.Left!);
            var r = GetHeight(currentNode.Right!);
            var balanceFactor = l - r;
            return balanceFactor;
        }
        private static int GetHeight(Node? currentNode)
        {
            var height = 0;
            if (currentNode == null) return height;
            var l = GetHeight(currentNode.Left!);
            var r = GetHeight(currentNode.Right!);
            var m = l > r ? l : r;
            height = m + 1;
            return height;
        }

        #region [ROTAÇÕES]
        private static Node RotateSingleToLeft(Node parent)
        {
            parent.Right!.Dad = parent.Dad;
            parent.Dad = parent.Right;
            var pivot = parent.Right;
            parent.Right = pivot!.Left;
            pivot.Left = parent;
            return pivot;
        }
        #endregion

    }
}
