using System;
using Microsoft.Data.Sqlite;
using System.IO;

//Clase que hace el .db para que se use
//lo crea en el directorio XDG ~/.local/share

public static class IniciaBDD {

    //Verifica si existe el .db, sino lo crea.
    public static string Inicia() {
	string ruta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
	string archivo = Path.Combine(ruta, "rolas.db");
	if (!File.Exists(archivo)) {
	    string bdd = $"Data Source={archivo}";
	    using var conexion = new SqliteConnection(bdd);
	    conexion.Open();
	    string SQL = @"
CREATE TABLE types (
  id_type       INTEGER PRIMARY KEY,
  description   TEXT
);

INSERT INTO types VALUES(0, 'Person');
INSERT INTO types VALUES(1, 'Group');
INSERT INTO types VALUES(2, 'Unknown');;

CREATE TABLE performers (
  id_performer  INTEGER PRIMARY KEY,
  id_type       INTEGER,
  name          TEXT,
  FOREIGN KEY   (id_type) REFERENCES types(id_type)
);

CREATE TABLE persons (
  id_person     INTEGER PRIMARY KEY,
  stage_name    TEXT,
  real_name     TEXT,
  birth_date    TEXT,
  death_date    TEXT
);

CREATE TABLE groups (
  id_group      INTEGER PRIMARY KEY,
  name          TEXT,
  start_date    TEXT,
  end_date      TEXT
);

CREATE TABLE in_group (
  id_person     INTEGER,
  id_group      INTEGER,
  PRIMARY KEY   (id_person, id_group),
  FOREIGN KEY   (id_person) REFERENCES persons(id_person)
  FOREIGN KEY   (id_group) REFERENCES groups(id_group)
);

CREATE TABLE albums (
  id_album      INTEGER PRIMARY KEY,
  path          TEXT,
  name          TEXT,
  year          INTEGER
);

INSERT INTO albums VALUES (0, '', 'Unknown', 0);

CREATE TABLE rolas (
  id_rola       INTEGER PRIMARY KEY,
  id_performer  INTEGER,
  id_album      INTEGER,
  path          TEXT,
  title         TEXT,
  track         INTEGER,
  year          INTEGER,
  genre         TEXT,
  FOREIGN KEY   (id_performer) REFERENCES performers(id_performer)
  FOREIGN KEY   (id_album) REFERENCES albums(id_album)
);";
	    using var comandoCrea = new SqliteCommand(SQL, conexion);
	    comandoCrea.ExecuteNonQuery();
	    Console.WriteLine($"La bdd fue hecha en: {archivo}");
	} else {
	    Console.WriteLine($"Se usa la bdd que está en: {archivo}");
	}
	return archivo;
    }
}
