using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Rockola.Models;
using Newtonsoft.Json;

namespace Rockola.Controllers
{
    public class VideoYTController : Controller
    {
        //URL de mi API
        string Baseurl = "http://localhost:7020/";

        public async Task<ActionResult> Index()
        {
            List<VideoYT> VideoInfo = new List<VideoYT>();
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //llama todos los videos usando el httpclient
                HttpResponseMessage Res = await client.GetAsync("api/historialt");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Rest = true entra y asigna los datos
                    var VideoResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializar el api y almacenar los datos
                    VideoInfo = JsonConvert.DeserializeObject<List<VideoYT>>(VideoResponse);
                }

                //Muestra la lista de todos los vídeos
                return View(VideoInfo);
            }
        }


        public VideoYT Create(VideoYT video)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/historialt", video).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<VideoYT>().Result;
                }
                return null;
            }
        }
        
    }
}