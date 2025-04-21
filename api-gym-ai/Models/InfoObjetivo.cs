namespace api_gym_ai.Models
{
    public class InfoObjetivo
    {
        public string Objetivo { get; set; }
        public IEnumerable<string> PartesDoCorpoEmFoco { get; set; }
        public string TempoDeTreino { get; set; }
        public string VariacaoTreino { get; set; }
    }
}
