namespace API_de_Inventario.DTOs.ReporteDtoCarpeta
{
    public class StockActualRespuestaDto
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int StockActual {  get; set; }
        public DateTime? FechaUltimoMovimiento { get; set; }
    }
}
