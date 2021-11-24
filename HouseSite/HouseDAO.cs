using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseSite
{
    class HouseDAO
    {
        //String strCon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
        MyDBDataContext db = new MyDBDataContext(ConfigurationManager.ConnectionStrings["strcon"].ConnectionString);
        public List<House> SelectAll()
        {
            //List<House> houses = new List<House>();
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "SELECT * FROM House";
            //SqlCommand com = new SqlCommand(strCom, con);
            //SqlDataReader dr = com.ExecuteReader();
            //while (dr.Read())
            //{
            //    House house = new House()
            //    {
            //        ID = (int)dr["ID"],
            //        Owner = (String)dr["Owner"],
            //        Type = (int)dr["Type"],
            //        Price = (int)dr["Price"],
            //        Address = (String)dr["Address"],
            //    };
            //    houses.Add(house);
            //}
            //con.Close();
            //return houses;
            db.ObjectTrackingEnabled = false;
            List<House> houses = db.Houses.ToList();
            return houses;
        }

        public List<House> SelectByKeyword(String keyword)
        {
            //List<House> houses = new List<House>();
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "SELECT * FROM House WHERE Owner LIKE @Keyword";
            //SqlCommand com = new SqlCommand(strCom, con);
            //com.Parameters.Add(new SqlParameter("@Keyword", "%" + keyword + "%"));
            //SqlDataReader dr = com.ExecuteReader();
            //while (dr.Read())
            //{
            //    House house = new House()
            //    {
            //        ID = (int)dr["ID"],
            //        Owner = (String)dr["Owner"],
            //        Type = (int)dr["Type"],
            //        Price = (int)dr["Price"],
            //        Address = (String)dr["Address"],
            //    };
            //    houses.Add(house);
            //}
            //con.Close();
            //return houses;
            List<House> houses = db.Houses.Where(x => x.Owner.Contains(keyword)).ToList();
            return houses;
        }

        public House SelectByCode(int ID)
        {
            //House house = null;
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "SELECT * FROM House WHERE ID=@ID";
            //SqlCommand com = new SqlCommand(strCom, con);
            //com.Parameters.Add(new SqlParameter("@ID", ID));
            //SqlDataReader dr = com.ExecuteReader();
            //if (dr.Read())
            //{
            //    house = new House()
            //    {
            //        ID = (int)dr["ID"],
            //        Owner = (String)dr["Owner"],
            //        Type = (int)dr["Type"],
            //        Price = (int)dr["Price"],
            //        Address = (String)dr["Address"],
            //    };
            //}
            //con.Close();
            //return house;
            House house = db.Houses.SingleOrDefault(x => x.ID == ID);
            return house;
        }

        public bool Insert(House newHouse)
        {
            //bool result = false;
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "INSERT INTO House(Owner,Type,Price,Address) VALUES(@Owner,@Type,@Price,@Address)";
            //SqlCommand com = new SqlCommand(strCom, con);
            //com.Parameters.Add(new SqlParameter("@Owner", newHouse.Owner));
            //com.Parameters.Add(new SqlParameter("@Type", newHouse.Type));
            //com.Parameters.Add(new SqlParameter("@Price", newHouse.Price));
            //com.Parameters.Add(new SqlParameter("@Address", newHouse.Address));

            //try
            //{
            //    result = com.ExecuteNonQuery() > 0;
            //}
            //catch
            //{
            //    result = false;
            //}
            //con.Close();
            //return result;
            try
            {
                db.Houses.InsertOnSubmit(newHouse);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            //bool result = false;
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "DELETE FROM House WHERE ID=@ID";
            //SqlCommand com = new SqlCommand(strCom, con);
            //com.Parameters.Add(new SqlParameter("ID", id));
            //try
            //{
            //    result = com.ExecuteNonQuery() > 0;
            //}
            //catch
            //{
            //    result = false;
            //}
            //con.Close();
            //return result;
            House dbHouse = db.Houses.SingleOrDefault(x => x.ID == id);
            if (dbHouse != null)
            {
                try
                {
                    db.Houses.DeleteOnSubmit(dbHouse);
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool Update(House newHouse)
        {
            //bool result = false;
            //SqlConnection con = new SqlConnection(strCon);
            //con.Open();
            //String strCom = "UPDATE House SET Owner=@Owner,Type=@Type,Price=@Price,Address=@Address WHERE ID=@ID";
            //SqlCommand com = new SqlCommand(strCom, con);
            //com.Parameters.Add(new SqlParameter("ID", newHouse.ID));
            //com.Parameters.Add(new SqlParameter("@Owner", newHouse.Owner));
            //com.Parameters.Add(new SqlParameter("@Type", newHouse.Type));
            //com.Parameters.Add(new SqlParameter("@Price", newHouse.Price));
            //com.Parameters.Add(new SqlParameter("@Address", newHouse.Address));
            //try
            //{
            //    result = com.ExecuteNonQuery() > 0;
            //}
            //catch
            //{
            //    result = false;
            //}
            //con.Close();
            //return result;
            House dbHouse = db.Houses.SingleOrDefault(x => x.ID == newHouse.ID);
            if (dbHouse != null)
            {
                try
                {
                    dbHouse.Owner = newHouse.Owner;
                    dbHouse.Type = newHouse.Type;
                    dbHouse.Price = newHouse.Price;
                    dbHouse.Address = newHouse.Address;
                    db.SubmitChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            return false;
        }
    }
}
