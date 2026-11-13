using System;
using GObject;

//Clase que tiene los atributos
//para llenar la tabla de la vista
//de la bdd.

[Subclass<GObject.Object>]
public partial class FilaTabla {

    //Crea una fila para la vista con los atributos
    public FilaTabla(int id, string titulo, string artista, string album, string genero, string fecha) : this(Array.Empty<ConstructArgument>()) {
	IdRola = id;
    Titulo = titulo;
	Performer = artista;
	Album = album;
	Genero = genero;
    Fecha = fecha;
    }

    public static FilaTabla Create(int id, string titulo, string artista, string album, string genero, string fecha) {
        return new FilaTabla(id, titulo, artista, album, genero, fecha);
    }
    //El id
    public int IdRola {
      get;
      set;
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
    //La fecha de la rola
    public string Fecha {
      get;
      set;
    }
}
