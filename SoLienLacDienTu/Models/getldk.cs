using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoLienLacDienTu.Models
{
    public class getldk
    {
        DataClasses1DataContext db = null;
        public getldk()
        {
            db = new DataClasses1DataContext();
        }
        public List<LoaiDK> ListAll()
        {
            return db.LoaiDKs.ToList();
        }
    }
}