namespace Usuarios.API.Domain.Entities
{
    public class Filho : Usuario
    {
        public DateTime DataNascimento { get; private set; }

        protected Filho() { }

        public Filho(string nome, string email, string telefone, DateTime dataNascimento)
            : base(nome, email, telefone)
        {
            DataNascimento = dataNascimento;
        }
    }
}
