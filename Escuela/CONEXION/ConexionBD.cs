using Escuela.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Escuela.CONEXION
{
    public class ConexionBD
    {
        SqlConnection conSIASA = new SqlConnection(ConfigurationManager.ConnectionStrings["conSIASA"].ConnectionString);
        public bool Insert_Alum(Alumnos alum)
        {
            using (conSIASA)//iniciamos abriendo la coneccion a la bd
            {
              
                conSIASA.Open();
                SqlTransaction transaction;
                transaction = conSIASA.BeginTransaction();
                SqlCommand command = conSIASA.CreateCommand();
                command.Connection = conSIASA;
                command.Transaction = transaction;
                using (transaction)//abrimos la transaccion de datos
                {

                    try
                    {
                        //
                        command = new SqlCommand("sp_Agregar_alumno", conSIASA, transaction);//CONSUMIMOS EL PROCEDIMIENTO ALMACENADO
                        command.CommandType = CommandType.StoredProcedure;
                        //LE PASAMOS TODOS LOS PARAMETROS PARA EL INSERT O EL UPDATE DE UN alumno
                        command.Parameters.AddWithValue("@Nombre_Completo", alum.nombre);
                        command.Parameters.AddWithValue("@Edad", alum.edad);
                        command.Parameters.AddWithValue("@Telefono", alum.telefono);

                        command.ExecuteNonQuery();//EJECUTAMOS LA CONSULTA, YA SEA UN INSERT O EL UPDATE
                        command.Parameters.Clear();
                        //retornmos 
                        transaction.Commit();//cierre de conexion
                        return true;//RETORNAMOS VERDADERO, SI EL USUARIO SE INSERTO O SE ACTUALIZO
                    }
                    catch (Exception)//SI HAY UN ERROR LO CAPTURAMOS EN EL CATCH
                    {
                        transaction.Rollback();//revierte los cambios si da algun error
                        conSIASA.Close();//CERRAMOS LA CONEXION
                        throw;
                    }
                }
            }
        }
    }
}