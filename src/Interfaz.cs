using System.Collections.Generic;

//Clase que contiene las interfaces para
//cada atributo de la bdd para hacer distintas
//acciones.

//Lo que se puede hacer desde la bdd con el album.
public interface albumI {
    //Inserta desde el minero el álbum a la bdd
    int insertaBusca(album alm);
    //Obtiene los albums que ya están en la bdd
    List<album> mostrarTodo();
}
