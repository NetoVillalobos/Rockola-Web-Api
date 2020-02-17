using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace Rockola.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SearchList(string keyword)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                //ApiKey = "AIzaSyCEvhnL2M-HB5bv_5w1boEYgSEKd_LsQww",
                //ApiKey = "AIzaSyB-ZEfgT_SeEVmDqRXM9I2jB1nHHict5sI",
                ApiKey = "AIzaSyCDfku4wmVrb39gjc3pDt3T9M1OyCYGlwQ",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = keyword;
            searchListRequest.MaxResults = 10;

            var searchListResponse = searchListRequest.Execute();

            return PartialView(searchListResponse.Items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void Declare()
        {
            List<string> PlayListIds = new List<string>();
            if (Session["Playlist"] == null)
            {
                Session["Playlist"] = PlayListIds;
            }
        }

        [HttpGet]
        public ActionResult AddToPlayList(string IdVideo)
        {
            Declare();
            List<string> ListVideosId = (List<string>)Session["Playlist"];
            ListVideosId.Add(IdVideo);
            Session["Playlist"] = ListVideosId;
            return PartialView("Playlist", ListVideosId);
        }
    }
}