public class Producto
{
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Anio { get; set; }
    public decimal Precio { get; set; }
    public int CantidadStock { get; set; }
    public string Descripcion { get; set; } // ✅ Nueva propiedad
    public bool Estado { get; set; }

    // Constructor vacío
    public Producto() { }

    // Constructor con parámetros
    public Producto(int id, string marca, string modelo, int anio, decimal precio, int cantidadStock, string descripcion, bool estado)
    {
        Id = id;
        Marca = marca;
        Modelo = modelo;
        Anio = anio;
        Precio = precio;
        CantidadStock = cantidadStock;
        Descripcion = descripcion;
        Estado = estado;
    }
}