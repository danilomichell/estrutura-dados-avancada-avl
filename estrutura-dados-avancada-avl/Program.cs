using estrutura_dados_avancada_avl;

Tree tree = new();
for (int i = 40; i > 29; i--)
{
    tree.Insert(i);
    tree.ShowTree();
}
tree.ShowTree();