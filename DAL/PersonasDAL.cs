using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using BOL.Helpers;

namespace DAL
{
    public  class PersonasDAL
    {

        public static Personas  GetPersona(int rut)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BUSCA_PERSONA";
                cmd.Parameters.AddWithValue("@RUT", rut);         
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                Personas persona = null;
                if (dt.Rows.Count>0)
                {
                    persona = new Personas();
                    persona.Id = Int32.Parse(dt.Rows[0]["id_persona"].ToString());
                    persona.Nombre = dt.Rows[0]["Nombre"].ToString();
                    persona.Rut = Int32.Parse(dt.Rows[0]["rut"].ToString());          
                    persona.Apellido = dt.Rows[0]["APELLIDOPATERNO"].ToString();
                    persona.Inactivo = Boolean.Parse(dt.Rows[0]["INACTIVO"].ToString());
                }
                               
                return persona;
            }
            catch (Exception)
            {
                throw;
            }





        }
		public static PERSONAINDUCCION GetPERSONAINDUCCION(int rut,string pasaporte)
		{

			try
			{
				SqlCommand cmd = new SqlCommand();
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				Coneccion param = Parameter.Leer_parametros();
				cmd.Connection = new SqlConnection(param.ConString);
				cmd.Connection.Open();
				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_BUSCA_PERSONA_INDUCCION";
				if (rut == 0)
				{
					cmd.Parameters.AddWithValue("@RUT", System.DBNull.Value);


				}
				else {
					cmd.Parameters.AddWithValue("@RUT", rut);

				}

				if (pasaporte == "") {
					cmd.Parameters.AddWithValue("@Pasaporte", System.DBNull.Value);

				}
				else
				{

					cmd.Parameters.AddWithValue("@Pasaporte", pasaporte);

				}
				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();
				PERSONAINDUCCION persona = null;
				if (dt.Rows.Count > 0)
				{
					persona = new PERSONAINDUCCION();
					persona.Id = Int32.Parse(dt.Rows[0]["id_persona"].ToString());
					persona.Nombre = dt.Rows[0]["Nombre"].ToString();
					persona.Rut = dt.Rows[0]["RUT"].ToString();
					persona.Dv = dt.Rows[0]["DV"].ToString();
					persona.Pasaporte = dt.Rows[0]["PASAPORTE"].ToString();
					persona.Apellido = dt.Rows[0]["APELLIDOPATERNO"].ToString();
					persona.Inactivo = Boolean.Parse(dt.Rows[0]["INACTIVO"].ToString());
					persona.estadoInduccion = dt.Rows[0]["ESTADOINDUCCION"].ToString();
					persona.fechaInduccion = dt.Rows[0]["FECHAINDUCCION"].ToString();
				}

				return persona;
			}
			catch (Exception)
			{
				throw;
			}





		}
		public static Personas GetPersonaInactiva(int rut)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BUSCA_PERSONA_INACTIVA";
                cmd.Parameters.AddWithValue("@RUT", rut);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                Personas persona = null;
                if (dt.Rows.Count > 0)
                {
                    persona = new Personas();
                    persona.Id = Int32.Parse(dt.Rows[0]["id_persona"].ToString());
                    persona.Nombre = dt.Rows[0]["Nombre"].ToString();
                    persona.Rut = Int32.Parse(dt.Rows[0]["rut"].ToString());
                    persona.Apellido = dt.Rows[0]["APELLIDOPATERNO"].ToString();
                }

                return persona;
            }
            catch (Exception)
            {
                throw;
            }





        }

        public static Personas GetPersonapasaporte(string pasaporte)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BUSCA_PERSONA_PASAPORTE";
                cmd.Parameters.AddWithValue("@pasaporte", pasaporte);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                Personas persona = null;
                if (dt.Rows.Count > 0)
                {
                    persona = new Personas();
                    persona.Id = Int32.Parse(dt.Rows[0]["id_persona"].ToString());
                    persona.Nombre = dt.Rows[0]["Nombre"].ToString();
                    persona.Rut = Int32.Parse(dt.Rows[0]["rut"].ToString());
                    persona.Apellido = dt.Rows[0]["APELLIDOPATERNO"].ToString();
                }

                return persona;
            }
            catch (Exception)
            {
                throw;
            }





        }


    }
}
