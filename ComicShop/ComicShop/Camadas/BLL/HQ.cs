using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicShop.Camadas.BLL
{
    public class HQ
    {
        public List<MODEL.HQ> Select()
        {
            DAL.HQ dalHQ = new DAL.HQ();
            return dalHQ.Select();
        }

        public List<MODEL.HQ> SelectById(int id)
        {
            DAL.HQ dalHQ = new DAL.HQ();
            return dalHQ.SelectById(id);
        }

        public List<MODEL.HQ> SelectByNome(string nome)
        {
            DAL.HQ dalHQ = new DAL.HQ();
            return dalHQ.SelectByNome(nome);
        }

        public void Insert(MODEL.HQ hq)
        {
            DAL.HQ dalHQ = new DAL.HQ();
            dalHQ.Insert(hq);
        }

        public void Update(MODEL.HQ hq)
        {
            DAL.HQ dalHQ = new DAL.HQ();
            dalHQ.update(hq);
        }

        public void Delete(MODEL.HQ hq)
        {
            DAL.HQ dalHQ = new DAL.HQ();
            dalHQ.Delete(hq);
        }

    }
}
