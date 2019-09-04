namespace Senai.Gufos.WebApi.Domains
{
    public class Presencas
    {
        public int IdUsuario { get; set; }
        public Usuarios Usuario { get; set; }

        public int IdEvento { get; set; }
        public Eventos Evento { get; set; }
    }
}
