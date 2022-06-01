using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Escuela.CONEXION;
using Escuela.Models;

namespace Escuela.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertarAlum(string Nombre, int edad, int telefono)
        {
            Alumnos InsertA = new Alumnos();
            InsertA.nombre = Nombre;
            InsertA.edad = edad;
            InsertA.telefono = telefono;

            bool i = false;
            i = new ConexionBD().Insert_Alum(InsertA);
            String mensaje = "";

            if (i == true)
            {
                mensaje = "Datos Agregados correctamente";
                //return Json(new { title = "EXITO", icon = "success", success = true, mensaje = "", codigo = CodCliente, nombre = nuevo.NOMBRE, cedula = nuevo.NUM_RUC }, JsonRequestBehavior.AllowGet);
                return Json(new { title = "LISTO", icon = "success", success = true, mensaje = mensaje + " Nombre: " + InsertA.nombre, InsertA.edad, InsertA.telefono }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                mensaje = "No se actualizaron los datos";
                return Json(new { title = "ERROR", icon = "error", success = false, mensaje = mensaje + " Nombre: " + InsertA.nombre, InsertA.edad, InsertA.telefono }, JsonRequestBehavior.AllowGet);

            }


        }

    }
}
