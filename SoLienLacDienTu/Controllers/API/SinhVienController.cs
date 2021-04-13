using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using SoLienLacDienTu.Models;


namespace SoLienLacDienTu.Controllers
{
    [RoutePrefix("api/SinhVien")]
    public class SinhVienController : ApiController
    {
         DataClasses1DataContext mde = new DataClasses1DataContext();

        [HttpGet]
        [Route("findall")]
        public HttpResponseMessage findAll()
        {

            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject
                    (mde.SinhViens.Select(p => new {
                        id = p.MaSV,
                        tk = p.MaSV,
                        pass = p.Password,
                        ten = p.TenSV,
                    }).ToList()));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpGet]
        [Route("find/{id}")]
        public HttpResponseMessage find(string id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject
                     (mde.SinhViens.Select(p => new {
                         id = p.MaSV,
                         tk = p.MaSV,
                         pass = p.Password,
                         ten = p.TenSV
                     }).Where(p =>p.id == id).FirstOrDefault()));
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        //[HttpPost]
        //[Route("create")]
      /*  public HttpResponseMessage create(Admin product)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                mde.SinhViens.Add(product);
                mde.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage delete(int id)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                mde.Admins.Remove(mde.Admins.SingleOrDefault(p => p.ID == id));
                mde.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }
        [HttpPut]
        [Route("update")]
        public HttpResponseMessage update(Admin product)
        {
            try
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var currentProduct = mde.Admins.SingleOrDefault(p => p.ID == product.ID);
                currentProduct.TaiKhoan = product.TaiKhoan;
                currentProduct.Password = product.Password;
                currentProduct.TenAdmin = product.TenAdmin;
                mde.SaveChanges();
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadGateway);
            }
        }*/
    }
}

