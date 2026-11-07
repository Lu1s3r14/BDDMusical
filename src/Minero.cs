using System;
using System.IO;

 // Clase que tiene al minero  yque
 // busca en los directorios a los
 // archivos .mp3.

public class Minero {

    // Busca en los directorios solo los archivos .mp3
    public void buscarDirectorio(string ruta, Action<string> mensaje) {
	if (!Directory.Exists(ruta)) {
	    mensaje($"La ruta dada no existe.");
	    return;
	}
	try {
	    var archivos = Directory.EnumerateFiles(ruta, "*.mp3", SearchOption.AllDirectories);
	    int total = 0;
	    var bdd = new BDDCanciones();
	    foreach (var rutas in archivos) {
		total++;
		string rutaAlbum = Path.GetDirectoryName(rutas) ?? "Desconocido";
		string cancion = Path.GetFileName(rutas);
		string nombreAlb = new DirectoryInfo(rutaAlbum).Name;
		bdd.insertaAlbum(rutaAlbum, nombreAlb);
		mensaje($"Album: {nombreAlb}");		
	    }
	    mensaje($"Total de archivos encontrados: {total}");
	} catch (UnauthorizedAccessException) {
	    mensaje("No se pudo leer del directorio.");
	}
    }
}
