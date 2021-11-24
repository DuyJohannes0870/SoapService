using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace HouseSite
{
    /// <summary>
    /// Summary description for HouseService
    /// </summary>
    [WebService(Namespace = "http://duyhpk.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HouseService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<House> GetAll()
        {
            List<House> houses = new HouseDAO().SelectAll();
            return houses;
        }

        [WebMethod]
        public List<House> Search(String keyword)
        {
            List<House> houses = new HouseDAO().SelectByKeyword(keyword);
            return houses;
        }

        [WebMethod]
        public House GetDetails(int id)
        {
            House house = new HouseDAO().SelectByCode(id);
            return house;
        }

        [WebMethod]
        public bool AddNew(House newHouse)
        {
            bool result = new HouseDAO().Insert(newHouse);
            return result;
        }

        [WebMethod]
        public bool Delete(int ID)
        {
            bool res = new HouseDAO().Delete(ID);
            return res;
        }

        [WebMethod]
        public bool Update(House newHouse)
        {
            bool res = new HouseDAO().Update(newHouse);
            return res;
        }
    }
}
