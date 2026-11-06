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
	    foreach (var rutas in archivos) {
		total++;
		string rutaAlbum = Path.GetDirectoryName(rutas) ?? "Desconocido";
		string cancion = Path.GetFileName(rutas);
		mensaje($"Cancion: {cancion}");		
	    }
	    mensaje($"Total de archivos encontrados: {total}");
	} catch (UnauthorizedAccessException) {
	    mensaje("No se pudo leer del directorio.");
	}
    }
}
