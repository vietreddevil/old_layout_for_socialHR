[HttpPost]
        public ActionResult list_candidate_after_search(String string_to_req)
        {
            //lay thong tin tu elastic search va tra ve danh sach nguoi dung
            var settings = new ConnectionSettings(new Uri("http://192.168.1.207:9200")).DefaultIndex("fb_author_data");
            var client = new ElasticClient(settings);

            // "_type": "authordata"
            var sResponse = client.Search<AuthorData>(s => s
            .From(0)
            .Size(20)
            .Query(q => q
                .Bool(b => b
                    .Must(mu => mu
                        .Match(m => m.Field("fb_data.mobile_phone").Query("0965991266"))
                    )
                 )
            )
            );
            List<AuthorData> datas = new List<AuthorData>();
            AuthorData data = new AuthorData();
            foreach (var info in sResponse.Hits)
            {
                datas.Add(info.Source);
            }
            ViewData["Message"] = datas;
            ViewData["search_term"] = string_to_req;
            return View();
        }