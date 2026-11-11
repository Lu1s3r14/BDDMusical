using System;
using Microsoft.Data.Sqlite;

// Clase que maneja la base de datos

public class BDDCanciones {
    // La base de datos "fisica"
    private string BDD = "Data Source=rolas.db";

    // Inserta el album en la bdd
    public void insertaAlbum(string ruta, string nombre) {
	using var conexion = new SqliteConnection(BDD);
	conexion.Open();
	string tableQuery = @"
INSERT INTO albums (path, name, year)
VALUES (@path, @name, @year);";
	using var comando = new SqliteCommand(tableQuery, conexion);
	comando.Parameters.AddWithValue("@path", ruta);
	comando.Parameters.AddWithValue("@name", nombre);
	comando.Parameters.AddWithValue("@year", 0);
	comando.ExecuteNonQuery();
    }
}
