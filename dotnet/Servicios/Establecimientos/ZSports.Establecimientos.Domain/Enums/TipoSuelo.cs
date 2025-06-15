namespace ZSports.Establecimientos.Domain.Enums;

public enum TipoSuelo
{
    SinDefinir = 0,
    Cemento = 1,
    PolvoLadrillo = 2,
    Cesped = 3,
    SinteticoArena = 4,
    SinteticoAgua = 5,
    SinteticoCaucho = 6,
    Parquet = 7
}

public static class TipoSueloExtensions
{
    public static string AsString(this TipoSuelo tipoSuelo)
    {
        return tipoSuelo switch
        {
            TipoSuelo.SinDefinir => "Sin Definir",
            TipoSuelo.Cemento => "Cemento",
            TipoSuelo.PolvoLadrillo => "Polvo de Ladrillo",
            TipoSuelo.Cesped => "Cesped",
            TipoSuelo.SinteticoArena => "Sintetico de Arena",
            TipoSuelo.SinteticoAgua => "Sintetico de Agua",
            TipoSuelo.SinteticoCaucho => "Sintetico de Caucho",
            TipoSuelo.Parquet => "Parquet",
            _ => "Invalido",
        };
    }
}
