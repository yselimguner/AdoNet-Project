using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkADOnet
{
    public class ProductDAL
    {
        //SQLConnection'u elle yazarız. Yılaroda data.sqlClient'a eklenir.
        SqlConnection _connection = new SqlConnection(@"server =(localdb)\mssqllocaldb; Initial Catalog = ETrade; Integrated Security = True");

        public DataTable GetAll()
        {
            //DataTable pahalı bir nesnedir.Memory açısından ve serileştirme özelliği
            //yoktur. Bu yüzden profesyonel projelerde kullanılmaz fazlaca.


            //Bunu elle yazıyoruz.
            //ConnectionState'te yazınca sstem.Data kütüphanesi ekleyelim
            if (_connection.State==ConnectionState.Closed)
            {
                _connection.Open();
            }//eğer bağlantı kapalı ise onu aç demek istedik.
            SqlCommand command = new SqlCommand("Select* from Products",_connection);
            SqlDataReader reader=   command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            _connection.Close();
            return dataTable;
        }//En Üstte Belirttiğim nedenlerden ötürü aşağıda asıl kullanacağımız metodu yazarım.

        public List<Product> GetAll2()
        {
            
            //ConnectionState'te yazınca sstem.Data kütüphanesi ekleyelim
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }//eğer bağlantı kapalı ise onu aç demek istedik.

            SqlCommand command = new SqlCommand("Select* from Products", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(reader["Id"]);
                product.Name = reader["Name"].ToString();
                product.StockAmount = Convert.ToInt32(reader["StockAmount"]);
                product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                products.Add(product);
            }

            reader.Close();
            _connection.Close();
            return products;
        }

        public void Add(Product product)
        {

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            //Buraya kadar olan kısmı yukarıdan kopyaladık fakat bundan sonrasını elle yazarız.
            SqlCommand command = new SqlCommand("Insert into Products values(@name,@unitPrice,@stockAmount)", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void Update(Product product)
        {
            if (_connection.State==ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlCommand command = new SqlCommand("Update Products set Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount where Id=@id", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public void Delete(int id)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }

            SqlCommand command = new SqlCommand("Delete from Products where Id=@id", _connection);
            //sadece id kullanacağımız için diğerlerini sileriz.
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
