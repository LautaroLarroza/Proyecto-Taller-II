namespace Automotors
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; } 
        public string Modelo { get; set; }
        public int? Anio { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CantidadStock { get; set; } 
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public Producto()
        {
            Marca = string.Empty;
            Modelo = string.Empty;
            Descripcion = string.Empty;
        }
    }
}
