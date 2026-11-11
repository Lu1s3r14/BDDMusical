// ESta clase guarda los "modelos"
// de cada parte de la BDD, para que
// puedan ser modificados (agregar, cambiar
// y/o eliminar) y ser pedidos.

//El tipo de la BDD
public class tipos {
    //el id del tipo
    public int idType {
	get;
	set;
    }
    //La descripción del tipo
    public string descripcion {
	get;
	set;
    }
}

//El performer de la canción.
public class performer {
    //Su id
    public int idPerf {
	get;
	set;
    }
    //El id de su tipo
    public int idTyp {
	get;
	set;
    }
    //Su nombre
    public string nombrePerf {
	get;
	set;
    }
}

//La persona que puede tener o no una canción relacionada
public class person {
    //Su id como persona
    public int idPerson {
	get;
	set;
    }
    //Su nombre como artista
    public string artistaPerson {
	get;
	set;
    }
    //Su nombre real
    public string nombrePerson {
	get;
	set;
    }
    //Su fecha de nacimiento.
    public string fechaPerson {
	get;
	set;
    }
    //Su fecha de muerte (si aplica)
    public string muertePerson {
	get;
	set;
    }
}

//La banda o grupo que toca canciones
public class grooup {
    //El id de la banda
    public int idGroup {
	get;
	set;
    }
    //El nombre de la banda/grupo
    public string nombreGroup{
	get;
	set;
    }
    //La fecha en que inicio el grupo/banda
    public string inicioGroup {
	get;
	set;
    }
    //La fecha en que terminó el grupo/banda. (si aplica)
    public string finGroup {
	get;
	set;
    }
}

//El álbum de una canción
public class album {
    //Su id
    public int idAlbum {
	get;
	set;
    }
    //La ruta en directorio donde está el álbum
    public string rutaAlbum {
	get;
	set;
    }
    //El nombre del álbum
    public string nombreAlbum {
	get;
	set;
    }
    //El año del álbum
    public int fechaAlbum {
	get;
	set;
    }
}

//La rola
public class rola {
    //Su id
    public int idRola {
	get;
	set;
    }
    //El id de su performer
    public int idRperf{
	get;
	set;
    }
    //El id del álbum al que pertenece
    public int idRalbum {
	get;
	set;
    }
    //La ruta en directorio
    public string rutaRola {
	get;
	set;
    }
    //El título de la rola
    public string tituloRola {
	get;
	set;
    }
    //El número de pista en el álbum
    public int pistaRola {
	get;
	set;
    }
    //El año de la rola
    public int fechaRola {
	get;
	set;
    }
    //El género de la rola
    public string generoRola {
	get;
	set;
    } 
}
