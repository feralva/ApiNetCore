namespace Model
{
    public class DireccionDTO:IDTO
    {
        public string Calle { get; set; }
        public int Altura { get; set; }
        public int PartidoId { get; set; }
        public string PartidoNombre { get; set; }
        public int ProvinciaId { get; set; }
        public string ProvinciaNombre { get; set; }
    }
}