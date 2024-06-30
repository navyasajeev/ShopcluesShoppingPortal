using ShopcluesShoppingPortal.Data_Access;
using ShopcluesShoppingPortal.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ShopcluesShoppingPortal.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminDashBoard()
        {
            return View();
        }

        public ActionResult GetAllProductDetails()
        {
            ProductRepository productRepository = new ProductRepository();
            ModelState.Clear();
            return View(productRepository.GetAllProduct());
        }

        public ActionResult AddProductDetails()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProductDetails(ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductRepository productRepository = new ProductRepository();
                    if (productRepository.AddProduct(productDetail))
                    {
                        ViewBag.Message = "Product details added successfully";
                    }
                    return RedirectToAction("GetAllProductDetails");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occurred while saving product: " + ex.Message);
                }
            }
            return View();
        }

        public ActionResult EditProductDetails(int id)
        {
            ProductRepository productRepository = new ProductRepository();
            return View(productRepository.GetAllProduct().Find(productDetail => productDetail.ProductID == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProductDetails(ProductDetail productDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                   

                    ProductRepository productRepository = new ProductRepository();
                    if (productRepository.UpdateProduct(productDetail))
                    {
                        return RedirectToAction("GetAllProductDetails");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error occurred while updating product: " + ex.Message);
                }
            }
            return View(productDetail);
        }

        public ActionResult DeleteProductDetails(int id)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository();
                if (productRepository.DeleteProduct(id))
                {
                    ViewBag.AlertMsg = "Product details deleted successfully";
                }
                return RedirectToAction("GetAllProductDetails");
            }
            catch 
            {
                return View();
            }
        }

      
    }
}
