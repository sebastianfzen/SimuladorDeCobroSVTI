using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace cotizadorFull.Models
{
    public class CotizarDAL
    {
        private static string o_conexion_svt = System.Configuration.ConfigurationManager.AppSettings["GFSTR_conectorSvti"];

        public CotizarDAL()
        {

        }
        //private string _sqlconn = "Data Source=svtibuildsql5;User=usuariopractica;Password=Usr#D3s4.5";

        public List<Models.ServicioDTO> lista = new List<Models.ServicioDTO>();

        public List<Models.ServicioDTO> calcularServicio(object tar, object ca20, object ca40, object to20, object to40, object if20, object if40, object impor_expor, object tons_otro, object cant_otro, object tipo_carga, object desde, object hasta)
        {
            int cant_20 = 0;
            int cant_40 = 0;
            int ton_20 = 0;
            int ton_40 = 0;
            int otros_ton = 0;
            string tipo_Carga = tipo_carga.ToString();
            char impor_Expor = char.Parse(impor_expor.ToString());

            DateTime dt_desde = DateTime.Parse(desde.ToString());
            DateTime dt_hasta = DateTime.Parse(hasta.ToString());

            bool? i20 = if20 as bool?;
            if (i20 != null)
            {
                cant_20 = int.Parse(ca20.ToString());
                ton_20 = int.Parse(to20.ToString());
            }
         

            bool? i40 = if40 as bool?;
            if (i40 != null)
            {
                cant_40 = int.Parse(ca40.ToString());
                ton_40 = int.Parse(to40.ToString());
            }

            bool? tot = tons_otro as bool?;
            if (tot != null)
            {
                otros_ton = int.Parse(tons_otro.ToString());
            }
            else { otros_ton = 0; }

            //SqlConnection sqlc = new SqlConnection(_sqlconn);
            SqlConnection sqlc = new SqlConnection(o_conexion_svt);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            DataSet dataSet = new DataSet();
            command.Parameters.Clear();
            command.Connection = sqlc;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "up_sxxx_svt_web_calculo_cotizacion";
            // command.Parameters.Add("@tar",SqlDbType.Int).Value = cod;
            command.Parameters.Add(new SqlParameter("@tar", tar ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@impor_expor", impor_Expor ));
            command.Parameters.Add(new SqlParameter("@tipo_carga", tipo_Carga ));
            command.Parameters.Add(new SqlParameter("@tamano40", null ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@tamano20", null ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@cantidad40", ca40 ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@cantidad20", cant_20));
            command.Parameters.Add(new SqlParameter("@peso40", to40 ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@peso20", ton_20));
            command.Parameters.Add(new SqlParameter("@fecha_desde", dt_desde ));
            command.Parameters.Add(new SqlParameter("@fecha_hasta", dt_hasta ));
            command.Parameters.Add(new SqlParameter("@sobredimensionado", null ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@imo", null ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@tons_otras_cargas", tons_otro ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@cant_otros_serv", 10 ));

            adapter.SelectCommand = command;
            dataSet.Clear();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Models.ServicioDTO servi = new Models.ServicioDTO();
                    servi.ser_codigo = (Convert.IsDBNull(row["tar"])) ? 0 : Convert.ToInt32(row["tar"]);
                    if (int.Parse(tar+"")==303)
                    {
                        servi.ser_descripcion = "Almacenaje";
                    }
                    else { servi.ser_descripcion = (Convert.IsDBNull(row["descrip_serv"])) ? string.Empty : Convert.ToString(row["descrip_serv"]); }
                    
                    servi.ser_valor_max = (Convert.IsDBNull(row["valor_total"])) ? 0 : Convert.ToDecimal(row["valor_total"]);
                    servi.ser_ind_vigencia = (Convert.IsDBNull(row["afecto_iva"])) ? string.Empty : Convert.ToString(row["afecto_iva"]);
                    lista.Add(servi);
                }

            }

            return (lista);

        }

        public List<Models.ServicioDTO> retornarServicios(object tar)
        {
            SqlConnection sqlc = new SqlConnection(o_conexion_svt);
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            DataSet dataSet = new DataSet();
            command.Parameters.Clear();
            command.Connection = sqlc;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "up_sxxx_svt_web_lista_serv_cotiza";
            // command.Parameters.Add("@tar",SqlDbType.Int).Value = cod;
            command.Parameters.Add(new SqlParameter("@tar", tar ?? DBNull.Value));


            adapter.SelectCommand = command;
            dataSet.Clear();
            adapter.Fill(dataSet);

            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    Models.ServicioDTO servi = new Models.ServicioDTO();
                    servi.ser_codigo = (Convert.IsDBNull(row["ser_codigo"])) ? 0 : Convert.ToInt32(row["ser_codigo"]);
                    servi.ser_descripcion = (Convert.IsDBNull(row["ser_descripcion"])) ? string.Empty : Convert.ToString(row["ser_descripcion"]);
                    servi.ser_valor_max = (Convert.IsDBNull(row["ser_valor_max"])) ? 0 : Convert.ToDecimal(row["ser_valor_max"]);
                    servi.ser_ind_vigencia = (Convert.IsDBNull(row["ser_ind_vigencia"])) ? string.Empty : Convert.ToString(row["ser_ind_vigencia"]);
                    lista.Add(servi);
                }

            }

            return (lista);
        }
    }
}