using crud_vehiculos_sqlserver.Datos;
using crud_vehiculos_sqlserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace crud_vehiculos_sqlserver.Controllers
{
    public class VehiculoController : Controller
    {
        // Instanciamos nuestra capa de acceso a datos para usar sus métodos
        private readonly VehiculoDAL vd = new VehiculoDAL();

        // Leer todos los vehiculos (GET: /Vehiculo/Index)
        [HttpGet]
        public ActionResult Index() 
        {
            var listaVehiculos = vd.Listar();
            return View(listaVehiculos);
        }

        // Crea un vehiculo (GET para mostrar el formulario / POST para guardar)
        [HttpGet]
        public ActionResult Create() 
        {
            return View(); // Muestra el formulario vacio
        }

        // [HttpPost] Se ejecuta cuando el usuario envia ek frmulario
        // [ValidateAntiForgeryToken]: Valida un token oculto de seguridad para evitar ataques CSRF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehiculo vehiculo) 
        {
            // Revisa las reglas primero del modelo de la clase Vehiculo.cs
            if (ModelState.IsValid) 
            {
                bool guardado = vd.Registrar(vehiculo);
                if (guardado) 
                {
                    // Nos redirige a la lista principal
                    return RedirectToAction("Index");
                }
            }
            // Si hubo un error nos muestra el formulario vacío
            return View(vehiculo);
        }

        // Editar un vehiculo (GET para cargar datos / POST para actualizar)
        [HttpGet]
        public ActionResult Edit(int? id) 
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Busca el vehiculo especifico por SQL Server
            Vehiculo vehiculo = vd.ObtenerPorId(id.Value);

            if(vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vehiculo vehiculo) 
        {
            if (ModelState.IsValid) 
            {
                bool actualizado = vd.Editar(vehiculo);
                if (actualizado) 
                {
                    return RedirectToAction("Index");
                }
            }

            return View(vehiculo);
        }

        // Eliminar un vehiculo (GET: para confirmar / POST para borrar)
        [HttpGet]
        public ActionResult Delete(int? id) 
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vehiculo vehiculo = vd.ObtenerPorId(id.Value);
            if (vehiculo == null) 
            {
                return HttpNotFound();
            }
            return View(vehiculo); // Muestra la pantalla de confirmación
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) 
        {
            vd.Eliminar(id);
            return RedirectToAction("Index");
        }
    }
}