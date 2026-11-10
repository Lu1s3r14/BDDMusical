using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;

//Clase que tiene las implementaciones
//de la interfaz para performers en la bdd

public class Performer : performerI{

    //La conexión con la bdd
    private string conexion;

    //Se conecta con la bdd
    public Performer(string bdd) {
	conexion = $"Data Source={bdd}";
    }

    //Inserta al performer a la bdd, pero revisa que exista y regresa
    //su id para no meterlo dos veces.
    public int insertaBuscaP(performer perf) {
	using var conexionP = new SqliteConnection(conexion);
	conexionP.Open();
	string busqueda = "SELECT id_performer FROM performers WHERE name = @name LIMIT 1;";
	using var comandoBuscar = new SqliteCommand(busqueda, conexionP);
	comandoBuscar.Parameters.AddWithValue("@name", perf.nombrePerf);
	var id = comandoBuscar.ExecuteScalar();
	if (id != null) {
	    return Convert.ToInt32(id);
	}
	string insertar = @"INSERT INTO performers (id_type, name) VALUES (@id_type, @name); SELECT last_insert_rowid();";
	using var comandoInsertar = new SqliteCommand(insertar, conexionP);
	comandoInsertar.Parameters.AddWithValue("@id_type", 2);
	comandoInsertar.Parameters.AddWithValue("@name", perf.nombrePerf);
	var nuevoId = comandoInsertar.ExecuteScalar();
	return Convert.ToInt32(nuevoId);
    }

    //Regresa a los performers existentes en la bdd
    public List<performer> mostrarTodoP() {
	List<performer> lista = new List<performer>();
	using var conexionL = new SqliteConnection(conexion);
	conexionL.Open();
	string busca = "SELECT id_performer, id_type, name FROM performers;";
	using var comandoBusca = new SqliteCommand(busca, conexionL);
	using var comandoLeer = comandoBusca.ExecuteReader();
	while (comandoLeer.Read()) {
	    var perfm = new performer {
		idPerf = comandoLeer.GetInt32(0),
		idTyp = comandoLeer.GetInt32(1),
		nombrePerf = comandoLeer.GetString(2)
	    };
	    lista.Add(perfm);
	}
	return lista;
    }
}
