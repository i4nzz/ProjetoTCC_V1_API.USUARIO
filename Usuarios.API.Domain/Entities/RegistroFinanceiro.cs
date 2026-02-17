namespace Usuarios.API.Domain.Entities;

public class RegistroFinanceiro
{
    public int Id { get; private set; }

    public int FilhoId { get; private set; }
    public Filho Filho { get; private set; }

    public int CategoriaId { get; private set; }
    public CategoriaFinanceira Categoria { get; private set; }

    public int MesadaId { get; private set; }
    public Mesada Mesada { get; private set; }

    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataRegistro { get; private set; }

    protected RegistroFinanceiro() { }

    public RegistroFinanceiro(int filhoId, int categoriaId, int mesadaId, string descricao, decimal valor)
    {
        FilhoId = filhoId;
        CategoriaId = categoriaId;
        MesadaId = mesadaId;
        Descricao = descricao;
        Valor = valor;
        DataRegistro = DateTime.UtcNow;
    }
}
