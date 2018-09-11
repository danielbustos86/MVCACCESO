using BOL;
using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CaboFrowardMVC.Models
{
    public class SingleConnection

    {

        private SingleConnection() { }
        private static SingleConnection _ConsString = null;
        private String _String = null;

        public static string ConString
        {

            get
            {

                if (_ConsString == null)
                {
                    _ConsString = new SingleConnection { _String = SingleConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }

        }



        public static string Connect()
        {

            Coneccion Cnn = new Coneccion();
            Cnn = Parameter.Leer_parametros();
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = Cnn.Servidor,
                InitialCatalog = Cnn.Bd,
                UserID = Cnn.Usuario,
                Password = Cnn.Clave,
            };

            EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                Metadata = "res://*/Models.BDModel.csdl|res://*/Models.BDModel.ssdl|res://*/Models.BDModel.msl",
                ProviderConnectionString = sqlString.ToString()
            };
            return entityString.ConnectionString;

        }

    }
}