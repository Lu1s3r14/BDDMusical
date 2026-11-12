using System;
using GObject;

//Clase que tiene los atributos
//para llenar la tabla de la vista
//de la bdd.

[Subclass<GObject.Object>]
public partial class FilaTabla {

    //Crea una fila para la vista con los atributos
    public FilaTabla(string titulo, string artista, string album, string genero) : this(Array.Empty<ConstructArgument>()) {
	Titulo = titulo;
	Performer = artista;
	Album = album;
	Genero = genero;
    }

    public static FilaTabla Create(string titulo, string artista, string album, string genero) {
        return new FilaTabla(titulo, artista, album, genero);
    }
    
    //El título de la rola
    public string Titulo {
	get;
	set;
    }
    //El performer de la rola
    public string Performer {
	get;
	set;
    }
    //El album al que pertenece
    public string Album {
	get;
	set;
    }
    //El género de la rola.
    public string Genero {
	get;
	set;
    }    
}
