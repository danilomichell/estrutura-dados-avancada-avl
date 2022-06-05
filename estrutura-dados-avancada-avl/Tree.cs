namespace estrutura_dados_avancada_avl
{
    public class Tree
    {
        private Node? _root;

        #region [PRINT]
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
        #endregion

        #region [INSERT]
        public void Insert(int valor)
        {
            var newItem = new Node(valor);
            _root = _root == null ? newItem : RecursiveInsert(_root, newItem);
        }
        private static Node RecursiveInsert(Node? currentNode, Node insertNode)
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
        #endregion

        #region [FATOR DE BALANCEAMENTO]
        private static Node BalanceTree(Node currentNode)
        {
            var balanceFactor = BalanceFactor(currentNode);
            currentNode = balanceFactor switch
            {
                > 1 => BalanceFactor(currentNode.Left!) > 0
                    ? RotateSingleToRight(currentNode)
                    : RotateDoubleToLeft(currentNode),
                < -1 => BalanceFactor(currentNode.Right!) > 0
                    ? RotateDoubleToRight(currentNode)
                    : RotateSingleToLeft(currentNode),
                _ => currentNode
            };
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
        #endregion

        #region [ROTAÇÕES]
        private static Node RotateSingleToLeft(Node parent)
        {
            parent.Right!.Dad = parent.Dad;
            if (parent.Right.Left is not null)
            {
                if (parent.Right.Left.Valor > parent.Valor)
                    parent.Right.Left.Dad = parent;
            }
            parent.Dad = parent.Right;
            var pivot = parent.Right;
            parent.Right = pivot!.Left;
            pivot.Left = parent;
            return pivot;
        }
        private static Node RotateSingleToRight(Node parent)
        {
            parent.Left!.Dad = parent.Dad;
            if (parent.Left.Right is not null)
            {
                if (parent.Left.Right.Valor < parent.Valor)
                    parent.Left.Right.Dad = parent;
            }
            parent.Dad = parent.Left;
            var pivot = parent.Left;
            parent.Left = pivot!.Right;
            pivot.Right = parent;
            return pivot;
        }
        private static Node RotateDoubleToLeft(Node parent)
        {
            var pivot = parent.Left;
            parent.Left = RotateSingleToLeft(pivot!);
            return RotateSingleToRight(parent);
        }
        private static Node RotateDoubleToRight(Node parent)
        {
            var pivot = parent.Right;
            parent.Right = RotateSingleToRight(pivot!);
            return RotateSingleToLeft(parent);
        }
        #endregion
    }
}
