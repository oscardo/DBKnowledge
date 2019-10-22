using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace DBKnow
{
    public static class ClaseVariable
    {
        public static string CadenaConexion { get; set; }
        public static string Usuario { get; set; }

        public static string CadConexion() {
            CadenaConexion = System.Configuration.ConfigurationSettings.AppSettings["Test"].ToString();
            return CadenaConexion;
        }

        /// <summary>
        /// Insertar
        /// </summary>
        /// <param name="PkIncidente"></param>
        /// <param name="FkUsuario"></param>
        /// <param name="FkEstado"></param>
        /// <param name="FkCategoria"></param>
        /// <param name="FkPrioridad"></param>
        /// <param name="FkTecnico"></param>
        /// <param name="FkTipo"></param>
        /// <param name="FkSupervisor"></param>
        /// <param name="FechaInicio"></param>
        /// <param name="FechaFin"></param>
        /// <param name="Titulo"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Solucion"></param>
        /// <param name="Activo"></param>
        public static void Insertar_Incidente(int PkIncidente,
                                              int FkUsuario,
                                              int FkEstado,
                                              int FkCategoria, 
                                              int FkPrioridad, 
                                              int FkTecnico,
                                              int FkTipo,
                                              int FkSupervisor, 
                                              string FechaInicio,
                                              string FechaFin, 
                                              string Titulo,
                                              string Descripcion, 
                                              string Solucion,
                                              bool Activo
                                             )
        {
            try
            {
            StringBuilder sb = new StringBuilder();
            sb.Append("  INSERT INTO Incidentes ");
            sb.Append(" (PkIncidente ");
            sb.Append(" , FkUsuario ");
            sb.Append(" , FkEstado ");
            sb.Append(" , FkCategoria ");
            sb.Append(" , FkPrioridad ");
            sb.Append(" , FkTecnico ");
            sb.Append(" , FkTipo ");
            sb.Append(" , FkSupervisor ");
            sb.Append(" , FechaInicio ");
            sb.Append(" , FechaFin ");
            sb.Append(" , Titulo ");
            sb.Append(" , Descripcion ");
            sb.Append(" , Solucion ");
            sb.Append(" , Activo) ");
            sb.Append(" VALUES ");
            sb.Append(" (" + PkIncidente + " ");
            sb.Append(" ," + FkUsuario + " ");
            sb.Append(" ," + FkEstado + " ");
            sb.Append(" , " +  FkCategoria + " ");
            sb.Append(" , " +  FkPrioridad + " ");
            sb.Append(" , " +  FkTecnico + " ");
            sb.Append(" , " +  FkTipo + " ");
            sb.Append(" , " +  FkSupervisor + " ");
            sb.Append(" , '" +  FechaInicio + "' ");
            sb.Append(" , '" +  FechaFin + "' ");
            sb.Append(" , '" +  Titulo + "' ");
            sb.Append(" , '" +  Descripcion + "' ");
            sb.Append(" , '" +  Solucion + "' ");
            sb.Append(" , " +  (Activo==true? 1 : 0) + " ) ");
            String sql = sb.ToString();
            using (SqlConnection connection = new SqlConnection(CadConexion()))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            }
            catch (Exception ex)
            {
                AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
        }

        public static void AgregarLog(string Mensaje)
        {
            SqlConnection cn = new SqlConnection();
            try
            {
                cn.ConnectionString = CadConexion();
                cn.Open();
                var sql = "insert into Log values('"+ Mensaje.ToString() +"')";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            { if (cn.State == ConnectionState.Open) cn.Close(); }
        }

        public static int Ingreso(string Usuario, string Clave)
        {
            int valor = 0;
            try
            {
            using (SqlConnection connection = new SqlConnection(CadConexion()))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT top 1 Usuario, Clave ");
                sb.Append(" FROM Usuario ");
                sb.Append(" where Usuario = '" + Usuario + "'");
                sb.Append(" and Clave = '" + Clave + "'");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            valor = 1;
                        }
                    }
                }
            }
            }
            catch (Exception ex)
            {
                AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            return valor;
        }

        public static DataSet LlenaDT(string select, string from, string where, string order)
        {
            DataSet dataSet = new DataSet();
            try
            {
            using (SqlConnection connection = new SqlConnection(CadConexion()))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT "+ select + " ");
                sb.Append("FROM " + from + " ");
                if(!where.Equals("")) sb.Append(" where " + where + "");
                if (!order.Equals("")) sb.Append(" order by " + order + "");
                String sql = sb.ToString();
                connection.Open();
                SqlDataAdapter myAdapter = new SqlDataAdapter(sql, CadConexion());
                myAdapter.Fill(dataSet, from.ToUpper().ToString());
                connection.Close();
            }
            }
            catch (Exception ex)
            {
                AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            return dataSet;
        }//fin

        public static DataTable LlenaDT_Grilla(string select, string from, string where, string order)
        {
            DataTable dataTable = new DataTable();
            try
            {
            using (SqlConnection connection = new SqlConnection(CadConexion()))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT " + select + " ");
                sb.Append("FROM " + from + " ");
                if (!where.Equals("")) sb.Append(" where " + where + "");
                if (!order.Equals("")) sb.Append(" order by " + order + "");
                String sql = sb.ToString();
                connection.Open();
                SqlDataAdapter myAdapter = new SqlDataAdapter(sql, CadConexion());
                myAdapter.Fill(dataTable);
                connection.Close();
            }
            }
            catch (Exception ex)
            {
                AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            return dataTable;
        }//fin

        public static ComboBox LlenaComboBox(string select, string from, string where, string order)
        {
            ComboBox comboBox = new ComboBox();
            try
            {
                using (SqlConnection connection = new SqlConnection(CadConexion()))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT " + select + " ");
                sb.Append(" FROM " + from + " ");
                if (!where.Equals("")) sb.Append("where " + where + "");
                if (!order.Equals("")) sb.Append(" order by " + order + "");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                        }
                    }
                }
            }
            }
            catch (Exception ex)
            {
                AgregarLog("Message: " + ex.Message + " Source: " + ex.Source.ToString() + " Target: " + ex.TargetSite.ToString());
            }
            return comboBox;
        }//fin
    }

    
}
