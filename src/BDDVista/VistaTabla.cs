using System;
using Gtk;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using System.Collections.Generic;
using GObject;
using Gio;

//Clase que dibuja las tablas de la bdd
//cuando ya se llenan

public class VistaTabla {
    //La barra para desplazar la tabla
    public ScrolledWindow barraVista {
	get;
	set;
    }
    //El contenido de la tabla
    public ColumnView contenidoTabla {
	get;
	set;
    }
    //Los datos que van en la tabla
    public Gio.ListStore datos {
	get;
	set;
    }
    //Los botones
    public Box Botones {
	get;
	set;
    }
    //La ruta para editar de nuevo la bdd
    private string rutaBDD;

    //Crea la tabla con su contenido
    public VistaTabla() {
	barraVista = ScrolledWindow.New();
	barraVista.Hexpand = true;
	barraVista.Vexpand = true;
	barraVista.HasFrame = true;
	datos = Gio.ListStore.New(FilaTabla.GetGType());
	var seleccionar = SingleSelection.New(datos);
	contenidoTabla = ColumnView.New(seleccionar);
	contenidoTabla.AppendColumn(nuevaColumna("Título", r => r.Titulo));
	contenidoTabla.AppendColumn(nuevaColumna("Artista", r => r.Performer));
        contenidoTabla.AppendColumn(nuevaColumna("Álbum", r => r.Album));
        contenidoTabla.AppendColumn(nuevaColumna("Género", r => r.Genero));
	barraVista.SetChild(contenidoTabla);
	Botones = Box.New(Orientation.Horizontal, 10);
        Botones.Halign = Align.End;
        Botones.MarginTop = 10;
        Botones.MarginBottom = 10;
        Botones.MarginEnd = 13;
        var BPersona = Button.NewWithLabel("Agregar Persona");
        var BBanda = Button.NewWithLabel("Agregar Banda");
        var BUnir = Button.NewWithLabel("Unir a grupo");
        var BEditar = Button.NewWithLabel("Editar");
        Botones.Append(BPersona);
        Botones.Append(BBanda);
        Botones.Append(BUnir);
        Botones.Append(BEditar);
        BPersona.OnClicked += (s, e) => {
            var ventanaPersona = Window.New();
            ventanaPersona.Title = "Nueva Persona";
            ventanaPersona.DefaultWidth = 300;
            var cajaP = Box.New(Orientation.Vertical, 10);
            cajaP.MarginTop = cajaP.MarginBottom = cajaP.MarginStart = cajaP.MarginEnd = 15;
            var textoArtista = Entry.New();
            textoArtista.PlaceholderText = "Nombre Artístico";
            var textoReal = Entry.New();
            textoReal.PlaceholderText = "Nombre Real";
            var textoNacimiento = Entry.New();
            textoNacimiento.PlaceholderText = "(YYYY-MM-DD)";
            var textoMuerte = Entry.New();
            textoMuerte.PlaceholderText = "(YYYY-MM-DD)";
            var BGuardarP = Button.NewWithLabel("Guardar");
            BGuardarP.OnClicked += async (s2, e2) => {
                BGuardarP.Sensitive = false;
                await InsertarPersona(textoArtista.Buffer.Text, textoReal.Buffer.Text, textoNacimiento.Buffer.Text, textoMuerte.Buffer.Text);
                ventanaPersona.Close();
	    };
	    cajaP.Append(Label.New("Nombre Artístico:"));
	    cajaP.Append(textoArtista);
            cajaP.Append(Label.New("Nombre Real:"));
	    cajaP.Append(textoReal);
            cajaP.Append(Label.New("Fecha Nacimiento:"));
	    cajaP.Append(textoNacimiento);
            cajaP.Append(Label.New("Fecha Fallecimiento:"));
	    cajaP.Append(textoMuerte);
            cajaP.Append(BGuardarP);            
            ventanaPersona.SetChild(cajaP);
            ventanaPersona.Show();
	};
	BBanda.OnClicked += (s, e) => {
            var ventanaBanda = Window.New();
            ventanaBanda.Title = "Nueva Banda";
            ventanaBanda.DefaultWidth = 300;
            var cajaB = Box.New(Orientation.Vertical, 10);
            cajaB.MarginTop = cajaB.MarginBottom = cajaB.MarginStart = cajaB.MarginEnd = 15;
            var textoNombre = Entry.New();
	    textoNombre.PlaceholderText = "Nombre de la Banda";
            var textoInicio = Entry.New();
	    textoInicio.PlaceholderText = "(YYYY-MM-DD)";
            var textoFin = Entry.New();
	    textoFin.PlaceholderText = "(YYYY-MM-DD)";
            var BGuardarB = Button.NewWithLabel("Guardar");
            BGuardarB.OnClicked += async (s2, e2) => {
                BGuardarB.Sensitive = false;
                await InsertarBanda(textoNombre.Buffer.Text, textoInicio.Buffer.Text, textoFin.Buffer.Text);
                ventanaBanda.Close();
            };
            cajaB.Append(Label.New("Nombre del grupo:"));
	    cajaB.Append(textoNombre);
            cajaB.Append(Label.New("Fecha Inicio:"));
	    cajaB.Append(textoInicio);
            cajaB.Append(Label.New("Fecha Fin:"));
	    cajaB.Append(textoFin);
            cajaB.Append(BGuardarB);
            ventanaBanda.SetChild(cajaB);
            ventanaBanda.Show();
        };
        BUnir.OnClicked += (s, e) => {
          var ventanaUnion = Window.New();
          ventanaUnion.Title = "Unir a Banda";
          ventanaUnion.DefaultWidth = 300;
          var cajaU = Box.New(Orientation.Vertical, 10);
          cajaU.MarginTop = cajaU.MarginBottom = cajaU.MarginStart = cajaU.MarginEnd = 15;
          var textoPersona = Entry.New();
          textoPersona.PlaceholderText = "Nombre de la persona";
          var textoBanda = Entry.New();
          textoBanda.PlaceholderText = "Nombre de la banda";
          var BGuardarU = Button.NewWithLabel("Unir");
          BGuardarU.OnClicked += async (s2, e2) => {
            BGuardarU.Sensitive = false;
            await UniraBanda(textoPersona.Buffer.Text, textoBanda.Buffer.Text);
            ventanaUnion.Close();
          };
          cajaU.Append(Label.New("Artista:"));
          cajaU.Append(textoPersona);
          cajaU.Append(Label.New("Banda/Grupo al que pertenece:"));
          cajaU.Append(textoBanda);
          cajaU.Append(BGuardarU);
          ventanaUnion.SetChild(cajaU);
          ventanaUnion.Show();
        };
        BEditar.OnClicked += (s, e) => {
            if (seleccionar.SelectedItem == null)
                return;
            var seleccionado = (FilaTabla)seleccionar.SelectedItem;           
            var ventanaEdit = Window.New();
            ventanaEdit.Title = "Editar canción.";
            ventanaEdit.DefaultWidth = 320;
            var caja = Box.New(Orientation.Vertical, 10);
            caja.MarginTop = caja.MarginBottom = caja.MarginStart = caja.MarginEnd = 15;          
            var textoT = Entry.New();
            textoT.Buffer.Text = seleccionado.Titulo;
            var textoF = Entry.New();
            textoF.Buffer.Text = seleccionado.Fecha;         
            Button Guardar = Button.NewWithLabel("Guardar.");
            Guardar.OnClicked += async (s2, e2) => {
                Guardar.Sensitive = false;
                await ActualizarC(seleccionado.IdRola, textoT.Buffer.Text, textoF.Buffer.Text);
                ventanaEdit.Close();
                LlenarTabla(rutaBDD);
            };            
            caja.Append(Label.New("Título de la pista:"));
            caja.Append(textoT);
            caja.Append(Label.New("Año:"));
            caja.Append(textoF);
            caja.Append(Guardar);
            
            ventanaEdit.SetChild(caja);
            ventanaEdit.Show();
        };
    }

    //Crea una nueva columna para la tabla de la bdd
    public ColumnViewColumn nuevaColumna(string encabezado, Func<FilaTabla, string> elegir) {
	var nuevo = SignalListItemFactory.New();
	nuevo.OnSetup += (sender, args) => {
	    var item = (ListItem)args.Object;
	    var etq = Label.New("");
	    etq.Halign = Align.Start;
	    etq.MarginStart = 10;
	    item.SetChild(etq);
	};
	nuevo.OnBind += (sender, args) => {
	    var item = (ListItem)args.Object;
	    var etq = (Label)item.GetChild();
	    var fila = (FilaTabla)item.GetItem();
	    etq.SetText(elegir(fila));
	};
	var columna = ColumnViewColumn.New(encabezado, nuevo);
	columna.Expand = true;
	return columna;
    }
    //Llena la tabla con la bdd en sql
    public async void LlenarTabla(string ruta) {
	rutaBDD = ruta;
	var nuevoContenido = await System.Threading.Tasks.Task.Run(() => {
	    List<Temporal> lista = new List<Temporal>();	
	    try {
		string conexion = $"Data Source={ruta}";
		using var conexionV = new SqliteConnection(conexion);
		conexionV.Open();
		string llenar = @"SELECT r.id_rola, r.title, p.name, a.name, r.genre, r.year FROM rolas r JOIN performers p ON r.id_performer = p.id_performer JOIN albums a ON r.id_album = a.id_album";
		var comandoLlena = new SqliteCommand(llenar, conexionV);
		var leer = comandoLlena.ExecuteReader();
		while(leer.Read()) {
		    lista.Add(new Temporal {
			    IdRola = leer.GetInt32(0),
			    Titulo = leer.IsDBNull(1) ? "Desconocido" : leer.GetString(1),
			    Artista = leer.IsDBNull(2) ? "Desconocido" : leer.GetString(2),
			    Album = leer.IsDBNull(3) ? "Desconocido" : leer.GetString(3),
			    Genero = leer.IsDBNull(4) ? "Desconocido" : leer.GetString(4),
			    Fecha = leer.IsDBNull(5) ? "Desconocido" : leer.GetInt32(5).ToString()
			});
		}
	    } catch (Exception e) {
		Console.WriteLine("Error al leer: " + e.Message);
	    }
	    return lista;
	});
	GLib.Functions.IdleAdd(0, () => {
	    datos.RemoveAll();
	    foreach(var song in nuevoContenido) {
		datos.Append(FilaTabla.Create(song.IdRola, song.Titulo, song.Artista, song.Album, song.Genero, song.Fecha));
	    }
	    return false;
	});
    }
    //Actualiza la bdd con los nuevos datos para las canciones
    public async System.Threading.Tasks.Task ActualizarC(int idRola, string tituloN, string fechaN) {
	string conexion = $"Data Source={rutaBDD}";
	await System.Threading.Tasks.Task.Run(() => {
	    using var conexionA = new SqliteConnection(conexion);
	    conexionA.Open();
	    string actu = "UPDATE rolas SET title = @t, year = @y WHERE id_rola = @id";
	    using var comandoActu = new SqliteCommand(actu, conexionA);
	    comandoActu.Parameters.AddWithValue("@t", tituloN);
	    comandoActu.Parameters.AddWithValue("@y", int.TryParse(fechaN, out int a) ? a : 0);
	    comandoActu.Parameters.AddWithValue("@id", idRola);
	    comandoActu.ExecuteNonQuery();
	    });
    }

    //Mete personas a la bdd
    private async System.Threading.Tasks.Task InsertarPersona(string artista, string real, string naci, string muerte) {
	string conexion = $"Data Source={rutaBDD}";
	await System.Threading.Tasks.Task.Run(() => {
            using var conexionI = new SqliteConnection(conexion);
            conexionI.Open();
            string insertarP = "INSERT INTO persons (stage_name, real_name, birth_date, death_date) VALUES (@a, @r, @n, @m)";
            using var comandoInsertar = new SqliteCommand(insertarP, conexionI);
            comandoInsertar.Parameters.AddWithValue("@a", string.IsNullOrWhiteSpace(artista) ? DBNull.Value : artista);
            comandoInsertar.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(real) ? DBNull.Value : real);
            comandoInsertar.Parameters.AddWithValue("@n", string.IsNullOrWhiteSpace(naci) ? DBNull.Value : naci);
            comandoInsertar.Parameters.AddWithValue("@m", string.IsNullOrWhiteSpace(muerte) ? DBNull.Value : muerte);
            comandoInsertar.ExecuteNonQuery();
        });
    }
    //Une personas a un grupo/banda
    private async System.Threading.Tasks.Task UniraBanda(string nombrePersona, string nombreBanda) {
      string conexion = $"Data Source={rutaBDD}";
      await System.Threading.Tasks.Task.Run(() => {
        try {
          using var conexionG = new SqliteConnection(conexion);
          conexionG.Open();
          string unir = @"
          INSERT INTO in_group (id_person, id_group) VALUES ((SELECT id_person FROM persons WHERE stage_name = @p OR real_name = @p LIMIT 1),(SELECT id_group FROM groups WHERE name = @b LIMIT 1))";
          using var comandoUnir = new SqliteCommand(unir, conexionG);
          comandoUnir.Parameters.AddWithValue("@p", string.IsNullOrWhiteSpace(nombrePersona) ? DBNull.Value : nombrePersona);
          comandoUnir.Parameters.AddWithValue("@b", string.IsNullOrWhiteSpace(nombreBanda) ? DBNull.Value : nombreBanda);
          comandoUnir.ExecuteNonQuery();
        } catch (Exception ex) {
          Console.WriteLine("Error al unir a la banda");
        }
      });
    }
    //Crea bandas en la bdd
    private async System.Threading.Tasks.Task InsertarBanda(string nombre, string inicio, string fin) {
        await System.Threading.Tasks.Task.Run(() => {
            using var conexion = new SqliteConnection($"Data Source={rutaBDD}");
            conexion.Open();
            string query = "INSERT INTO groups (name, start_date, end_date) VALUES (@n, @i, @f)";
            using var cmd = new SqliteCommand(query, conexion);
            cmd.Parameters.AddWithValue("@n", string.IsNullOrWhiteSpace(nombre) ? DBNull.Value : nombre);
            cmd.Parameters.AddWithValue("@i", string.IsNullOrWhiteSpace(inicio) ? DBNull.Value : inicio);
            cmd.Parameters.AddWithValue("@f", string.IsNullOrWhiteSpace(fin) ? DBNull.Value : fin);
            cmd.ExecuteNonQuery();
        });
    }

    //Clase que se usa para obtener datos de la bdd
    private class Temporal {
	public int IdRola {
	    get;
	    set;
	}
	public string Titulo {
	    get;
	    set;
	}
	public string Artista {
	    get;
	    set;
	}
	public string Album {
	    get;
	    set;
	}
	public string Fecha {
	    get;
	    set;
	}
	public string Genero {
	    get;
	    set;
	}
    }
}
