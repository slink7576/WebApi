using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace BLL
{
    public class Controller
    {
        private const string APP_PATH = "http://localhost:51834/api/";
   
        public static List<Product> getAll()
        {
            var request = (HttpWebRequest)WebRequest.Create( APP_PATH + "Products");
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                List<Product> deserializedName = JsonConvert.DeserializeObject<List<Product>>(responseString);
                return deserializedName;
            }
            catch(System.Net.WebException)
            {
                return null;
            }
           

        }
        public static bool delItem(int id)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(APP_PATH + "Products/" + id);
                request.Method = "DELETE";
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription.Equals("OK"))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            catch (System.Net.WebException)
            {
                return false;
            }
           
           
        }
        public static bool addItem(Product product)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(APP_PATH + "Products");
                request.Method = "POST";
                request.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(product);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription == "Created")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.WebException)
            {
                return false;
            }
            
           
        }
        public static bool updateItem(int id, Product product)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(APP_PATH + "Products/" + id);
                request.Method = "PUT";
                request.ContentType = "application/json";
                var json = JsonConvert.SerializeObject(product);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {


                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusDescription == "No Content")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.WebException)
            {
                return false;
            }
           
            
        }
        public static Product getItem(int id)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(APP_PATH + "Products/" + id);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Product product = JsonConvert.DeserializeObject<Product>(responseString);
                return product;

            }
            catch (System.Net.WebException)
            {
                return null;
            }
          
        }




    }

}
