using System;
using System.IO;
using TagLib;
using System.Diagnostics.CodeAnalysis;

 // Clase que tiene al minero  yque
 // busca en los directorios a los
 // archivos .mp3.

public class Minero {

    //El album para probar
    private readonly albumI album;

    //Inicia al minero con el album
    public Minero(albumI albm) {
	this.album = albm;
    }

    // Busca en los directorios solo los archivos .mp3
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(TagLib.Mpeg.AudioFile))]
    public void buscarDirectorio(string ruta, Action<string> mensaje) {
	if (!Directory.Exists(ruta)) {
	    mensaje($"La ruta dada no existe.");
	    return;
	}
	try { 
	    var archivos = Directory.EnumerateFiles(ruta, "*.mp3", SearchOption.AllDirectories);
	    int total = 0;
	    //var bdd = new BDDCanciones();
	    foreach (var rutas in archivos) {
		total++;
		try {
		    using var audio = TagLib.File.Create(rutas);
		    string rutaAlbm = Path.GetDirectoryName(rutas) ?? "Desconocido";
		    // string cancion = Path.GetFileName(rutas);
		    string carpetaAlb = new DirectoryInfo(rutaAlbm).Name;
		    string titulo = audio.Tag.Title ?? Path.GetFileNameWithoutExtension(rutas);
		    string artista = audio.Tag.FirstPerformer ?? "Artista Desconocido";
		    string albumNom = audio.Tag.Album ?? new DirectoryInfo(Path.GetDirectoryName(rutas) ?? "Álbum desconocido").Name;
		    int fecha = (int)audio.Tag.Year;
		    //  bdd.insertaAlbum(rutaAlbum, nombreAlb);
		    var nuevoAlbm = new album {
			rutaAlbum = rutaAlbm,
			nombreAlbum = albumNom,
			fechaAlbum = fecha
		    };
		    int id = album.insertaBusca(nuevoAlbm);
		    mensaje($"Archivo leido: {Path.GetFileName(rutas)}");
		    mensaje($"Album: {nuevoAlbm.nombreAlbum} con id: {id}");
		} catch (TagLib.UnsupportedFormatException) {
		    mensaje($"El archivo no es válido.");
		}
	    }
	    mensaje($"Total de archivos encontrados: {total}");
	} catch (UnauthorizedAccessException) {
	    mensaje("No se pudo leer del directorio.");
	}
    }
}
