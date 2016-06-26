using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicShop.Camadas.BLL
{
    public class Autor
    {
        public List<MODEL.Autor> Select()
        {
            DAL.Autor dalAut = new DAL.Autor();
            return dalAut.Select();
        }

        public void Insert(MODEL.Autor aut)
        {
            DAL.Autor dalAut = new DAL.Autor();
            dalAut.Insert(aut);
        }

        public void Update(MODEL.Autor aut)
        {
            DAL.Autor dalAut = new DAL.Autor();
            dalAut.Update(aut);
        }
        public void Delete(MODEL.Autor aut)
        {
            DAL.Autor dalAut = new DAL.Autor();
            dalAut.Delete(aut);
        }
    }
}
