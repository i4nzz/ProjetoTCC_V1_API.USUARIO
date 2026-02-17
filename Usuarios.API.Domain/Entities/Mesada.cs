namespace Usuarios.API.Domain.Entities;

public class Mesada
{
    public int Id { get; private set; }

    public int FilhoId { get; private set; }
    public Filho Filho { get; private set; }

    public decimal Valor { get; private set; }
    public int Mes { get; private set; }
    public int Ano { get; private set; }

    protected Mesada() { }

    public Mesada(int filhoId, decimal valor, int mes, int ano)
    {
        FilhoId = filhoId;
        Valor = valor;
        Mes = mes;
        Ano = ano;
    }
}
