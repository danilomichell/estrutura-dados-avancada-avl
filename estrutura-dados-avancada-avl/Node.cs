namespace estrutura_dados_avancada_avl
{
    public class Node
    {
        public int Valor { get; set; }
        public Node? Dad { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int valor)
        {
            Valor = valor;
        }
    }
}
