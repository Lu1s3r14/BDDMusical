using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

//Clase que implementa los metodos
//que se pueden hacer con el album en la bdd

public class Album : albumI {

    //La conexión con la bdd
    private string conexion;

    //Se conecta con la bdd
    public Album(string bdd) {
	conexion = $"Data Source={bdd}";
    }

    //Inserta un álbum en la bdd, antes revisa si existe y regresa su id
    //para no meterlo dos veces.
    public int insertaBusca(album alm) {
	using var conexionA = new SqliteConnection(conexion);
	conexionA.Open();
	string busqueda = "SELECT id_album FROM albums WHERE path = @path LIMIT 1;";
	using var comandoBuscar = new SqliteCommand(busqueda, conexionA);
	comandoBuscar.Parameters.AddWithValue("@path", alm.rutaAlbum);
	var id = comandoBuscar.ExecuteScalar();
	if (id != null) {
	    return Convert.ToInt32(id);
	}
	string insertar = @"INSERT INTO albums (path, name, year) VALUES (@path, @name, @year); SELECT last_insert_rowid();";
	using var comandoInsertar = new SqliteCommand(insertar, conexionA);
	comandoInsertar.Parameters.AddWithValue("@path", alm.rutaAlbum);
	comandoInsertar.Parameters.AddWithValue("@name", alm.nombreAlbum);
	comandoInsertar.Parameters.AddWithValue("@year", alm.fechaAlbum);
	var nuevoId = comandoInsertar.ExecuteScalar();
	return Convert.ToInt32(nuevoId);
    }

    //Regresa el contenido del álbum
    public List<album> mostrarTodo() {
	List<album> lista = new List<album>();
	using var conexionL = new SqliteConnection(conexion);
	conexionL.Open();
	string busca = "SELECT id_album, path, name, year FROM albums;";
	using var comandoBusca = new SqliteCommand(busca, conexionL);
	using var comandoLeer = comandoBusca.ExecuteReader();
	while (comandoLeer.Read()) {
	    var albm = new album {
		idAlbum = comandoLeer.GetInt32(0),
		rutaAlbum = comandoLeer.GetString(1),
		nombreAlbum = comandoLeer.GetString(2),
		fechaAlbum = comandoLeer.GetInt32(3)
	    };
	    lista.Add(albm);
	}
	return lista;
    }
}
