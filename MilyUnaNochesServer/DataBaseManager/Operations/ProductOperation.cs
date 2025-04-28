using MilyUnaNochesService.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace DataBaseManager.Operations {
    public static class ProductOperation {

        public static bool SaveProduct(Producto producto) {
            LoggerManager logger = new LoggerManager(typeof(ProductOperation));
            bool isInserted = false;

            try {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities()) {
                    db.Producto.Add(producto);
                    db.SaveChanges();
                    isInserted = true;
                }
            } catch (EntityException entityException) {
                logger.LogError($"DbEntityValidationException: Error trying to register product. Exception: {entityException.Message}", entityException);
            } catch (Exception exception) {
                logger.LogError($"Exception: Error trying to register product. Exception: {exception.Message}", exception);
            }

            return isInserted;
        }

        public static List<Producto> GetProducts() 
        {
            try {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities()) {
                    var productosDb = db.Producto.ToList();
                    //
                    List<Producto> productos = productosDb.Select(productoDb => new Producto {
                        idProducto = productoDb.idProducto,
                        codigoProducto = productoDb.codigoProducto,
                        nombreProducto = productoDb.nombreProducto,
                        descripcion = productoDb.descripcion,
                        categoria = productoDb.categoria,
                        cantidadStock = productoDb.cantidadStock,
                        precioVenta = productoDb.precioVenta,
                        precioCompra = productoDb.precioCompra,
                        imagen = productoDb.imagen
                    }).ToList();

                    return productos;
                }
            } catch (Exception ex) {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                throw;
            }
        }

        public static Producto GetProductByCode(string code) {
            var logger = new LoggerManager(typeof(ProductOperation));

            try {
                using (var db = new MilYUnaNochesEntities()) {
                    return db.Producto.FirstOrDefault(p => p.codigoProducto == code);
                }
            } catch (EntityException ex) {
                logger.LogError($"Error al buscar producto: {code}", ex);
                return null;
            } catch (Exception ex) {
                logger.LogError($"Error inesperado: {code}", ex);
                return null;
            }
        }

        public static bool CheckStockByCode(string productCode, int requiredQuantity) {
            var logger = new LoggerManager(typeof(ProductOperation));

            try {
                using (var db = new MilYUnaNochesEntities()) {
                    var producto = db.Producto.FirstOrDefault(p => p.codigoProducto == productCode);

                    if (producto == null) {
                        Console.WriteLine($"Producto no encontrado: {productCode}");
                        return false;
                    }

                    logger.LogInfo($"Verificaci�n stock - C�digo: {productCode}, Stock: {producto.cantidadStock}, Requerido: {requiredQuantity}");
                    return producto.cantidadStock >= requiredQuantity;
                }
            } catch (Exception ex) {
                logger.LogError($"Error al verificar stock: {productCode}", ex);
                return false;
            }
        }
        public static bool ValidateProductName(string productName)
        {
            try
            {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities())
                {
                    bool exist = db.Producto
                                  .Any(p => p.nombreProducto.ToLower() == productName.ToLower());

                    return !exist;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al validar el nombre del producto: {ex.Message}");
                throw;
            }
        }
        public static bool UpdateProduct(Producto producto, string oldProductName)
        {
            LoggerManager logger = new LoggerManager(typeof(ProductOperation));
            bool isUpdated = false;

            try
            {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities())
                {
                    // Buscar el producto existente en la base de datos
                    var existingProduct = db.Producto.FirstOrDefault(p => p.nombreProducto == oldProductName);
                    Debug.WriteLine("HOLA");
                    Debug.WriteLine(oldProductName);
                    if (existingProduct != null)
                    {
                        Debug.WriteLine("HOLA");
                        // Actualizar solo las propiedades modificables
                        existingProduct.nombreProducto = producto.nombreProducto;
                        existingProduct.codigoProducto = producto.codigoProducto;
                        existingProduct.descripcion = producto.descripcion;
                        existingProduct.precioCompra = producto.precioCompra;
                        existingProduct.precioVenta = producto.precioVenta;
                        existingProduct.categoria = producto.categoria;
                        existingProduct.cantidadStock = producto.cantidadStock;
                        existingProduct.imagen = producto.imagen;

                        db.SaveChanges();
                        Debug.WriteLine("HOLA");
                        isUpdated = true;
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                logger.LogError($"DbEntityValidationException: Error al actualizar el producto. Detalles: {string.Join(", ", ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors).Select(e => e.ErrorMessage))}", ex);
            }
            catch (EntityException entityException)
            {
                logger.LogError($"EntityException: Error al actualizar el producto. Exception: {entityException.Message}", entityException);
            }
            catch (Exception exception)
            {
                logger.LogError($"Exception: Error al actualizar el producto. Exception: {exception.Message}", exception);
            }

            return isUpdated;
        }

        public static bool DeleteProduct(string productName)
        {
            LoggerManager logger = new LoggerManager(typeof(ProductOperation));
            bool isDeleted = false;

            try
            {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities())
                {
                    var producto = db.Producto.FirstOrDefault(p => p.nombreProducto == productName);

                    if (producto != null)
                    {
                        db.Producto.Remove(producto);
                        db.SaveChanges();
                        isDeleted = true;
                    }
                }
            }
            catch (EntityException entityException)
            {
                logger.LogError($"DbEntityValidationException: Error trying to delete product. Exception: {entityException.Message}", entityException);
            }
            catch (Exception exception)
            {
                logger.LogError($"Exception: Error trying to delete product. Exception: {exception.Message}", exception);
            }

            return isDeleted;
        }

        public static int NumProducts()
        {
            LoggerManager logger = new LoggerManager(typeof(ProductOperation));
            int dbCount = 0;

            try
            {
                using (MilYUnaNochesEntities db = new MilYUnaNochesEntities())
                {
                    dbCount = db.Producto.Count();
                }
            }
            catch (EntityException entityException)
            {
                logger.LogError($"DbEntityValidationException: Error comparing product counts. Exception: {entityException.Message}", entityException);
            }
            catch (Exception exception)
            {
                logger.LogError($"Exception: Error comparing product counts. Exception: {exception.Message}", exception);
            }

            return dbCount;
        }

    }
}
