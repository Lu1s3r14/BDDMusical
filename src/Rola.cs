using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

//Clase que implementa los metodos para
//las rolas en la bdd

public class Rola : rolaI {

    //La conexión con la bdd
    private string conexion;

    //Se conecta con la bdd
    public Rola(string bdd) {
	conexion = $"Data Source={bdd}";
    }

    //Inserta la rola en la bdd, antes revisa que exista para no
    //tener duplicados, y regresa su id.
    public int insertaBuscaR(rola cancion) {
	using var conexionR = new SqliteConnection(conexion);
	conexionR.Open();
	string busqueda = "SELECT id_rola FROM rolas WHERE path = @path LIMIT 1;";
	using var comandoBuscar = new SqliteCommand(busqueda, conexionR);
	comandoBuscar.Parameters.AddWithValue("@path", cancion.rutaRola);
	var id = comandoBuscar.ExecuteScalar();
	if (id != null) {
	    return Convert.ToInt32(id);
	}
	string insertar = @"INSERT INTO rolas (id_performer, id_album, path, title, track, year, genre) VALUES (@id_performer, @id_album, @path, @title, @track, @year, @genre); SELECT last_insert_rowid();";
	using var comandoInsertar = new SqliteCommand(insertar, conexionR);
	comandoInsertar.Parameters.AddWithValue("@id_performer", cancion.idRperf);
	comandoInsertar.Parameters.AddWithValue("@id_album", cancion.idRalbum);
	comandoInsertar.Parameters.AddWithValue("@path", cancion.rutaRola);
	comandoInsertar.Parameters.AddWithValue("@title", cancion.tituloRola);
	comandoInsertar.Parameters.AddWithValue("@track", cancion.pistaRola);
	comandoInsertar.Parameters.AddWithValue("@year", cancion.fechaRola);
	comandoInsertar.Parameters.AddWithValue("@genre", cancion.generoRola);
	var nuevoId = comandoInsertar.ExecuteScalar();
	return Convert.ToInt32(nuevoId);
    }

    //Regresa todas las rolas que estén en la bdd
    public List<rola> mostrarTodoR() {
	List<rola> lista = new List<rola>();
	using var conexionL = new SqliteConnection(conexion);
	conexionL.Open();
	string busca = "SELECT id_rola, id_performer, id_album, path, title, track, year, genre FROM rolas;";
	using var comandoBusca = new SqliteCommand(busca, conexionL);
	using var comandoLeer = comandoBusca.ExecuteReader();
	while (comandoLeer.Read()) {
	    var song = new rola {
		idRola = comandoLeer.GetInt32(0),
		idRperf = comandoLeer.GetInt32(1),
		idRalbum = comandoLeer.GetInt32(2),
		rutaRola = comandoLeer.GetString(3),
		tituloRola = comandoLeer.GetString(4),
		pistaRola = comandoLeer.GetInt32(5),
		fechaRola = comandoLeer.GetInt32(6),
		generoRola = comandoLeer.GetString(7)
	    };
	    lista.Add(song);
	}
	return lista;
    }
}
