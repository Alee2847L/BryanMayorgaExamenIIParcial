using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace BryanMayorgaExamenIIParcial
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Clase_conectar conexion = new Clase_conectar();
        private DataTable tabla;
        private string nomemp;

        public string Nomemp { get => nomemp; set => nomemp = value; }

        public MainWindow()
        {
            InitializeComponent();
            tabla = new DataTable();
            nomemp = txtnombres.Text;
        }

        private void Btnagrerar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnombres.Text != "")
            {
                try
                {
                    conexion.Abrirconexion();
                    if (conexion.Estado == 1)
                    {

                        string query = string.Format("AGREGARUSUARIO");
                        SqlCommand comando = new SqlCommand(query, conexion.Conexion);
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        using (adaptador)
                        {
                            comando.Parameters.AddWithValue("@nombres", txtnombres.Text);
                            comando.Parameters.AddWithValue("@apellidos", txtapellidos.Text);
                            comando.Parameters.AddWithValue("@nombreUsuario", txtnomusu.Text);
                            comando.Parameters.AddWithValue("@contrasena", txtcontra.Text);
                            comando.Parameters.AddWithValue("@correroelectronico", txtcorreo.Text);
                            comando.Parameters.AddWithValue("@fechacreacion", txtfecha.Text);
                            comando.Parameters.AddWithValue("@ultimaconexion", txtultimaconexion.Text);
                            comando.ExecuteNonQuery();
                            MessageBox.Show(" Datos Insertado");
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(" Datos No Insertado" + ex.Message);
                }
            }
        }

        private void Btneliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnombres.Text != "")
            {
                try
                {
                    conexion.Abrirconexion();
                    if (conexion.Estado == 1)
                    {

                        string query = string.Format("ELIMINARUSUARIO");
                        SqlCommand comando = new SqlCommand(query, conexion.Conexion);
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        using (adaptador)
                        {
                            comando.Parameters.AddWithValue("@nombres", txtnombres.Text);
                            comando.ExecuteNonQuery();
                            MessageBox.Show(" Datos Eliminados");
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(" Datos No Eliminados" + ex.Message);
                }
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtnombres.Text != "")
            {

                try
                {
                    conexion.Abrirconexion();

                    if (conexion.Estado == 1)
                    {

                        string query = string.Format("ACTUALIZARUSUARIO");
                        SqlCommand comando = new SqlCommand(query, conexion.Conexion);
                        comando.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        using (adaptador)
                        {
                            comando.Parameters.AddWithValue("@nombres", txtnombres.Text);
                            comando.Parameters.AddWithValue("@apellidos", txtapellidos.Text);
                            comando.Parameters.AddWithValue("@nombreUsuario", txtnomusu.Text);
                            comando.Parameters.AddWithValue("@contrasena", txtcontra.Text);
                            comando.Parameters.AddWithValue("@correroelectronico", txtcorreo.Text);
                            comando.Parameters.AddWithValue("@fechacreacion", txtfecha.Text);
                            comando.Parameters.AddWithValue("@ultimaconexion", txtultimaconexion.Text);
                            comando.ExecuteNonQuery();
                            MessageBox.Show(" Datos Actualizados");
                        }

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(" Datos No Actualizados" + ex.Message);
                }
            }
        }

        private void Btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conexion.Abrirconexion();
                if (conexion.Estado == 1)
                {
                    tabla.Reset();
                    SqlDataAdapter adaptador = new SqlDataAdapter(string.Format("select * from Usuarios.usuario where nombres ='{0}'", Nomemp), conexion.Conexion);
                    adaptador.Fill(tabla);
                    if (tabla.Rows.Count > 0)
                    {
                        txtnombres.Text  = tabla.Rows[0][1].ToString();
                        txtapellidos.Text = tabla.Rows[0][2].ToString();
                        txtnomusu.Text = tabla.Rows[0][3].ToString();
                        txtcontra.Text = tabla.Rows[0][4].ToString();
                        txtcorreo.Text = tabla.Rows[0][5].ToString();
                        txtfecha.Text = tabla.Rows[0][6].ToString();
                        txtultimaconexion.Text = tabla.Rows[0][7].ToString();
                        txtestado.Text = tabla.Rows[0][8].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Empleado no encontrado");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
