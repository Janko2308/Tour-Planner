﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using Tour_Planner.Model.Structs;
using System.Drawing;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Tour_Planner.Model.Enums;

namespace Tour_Planner.BL {
    public class MapCreator {
        public async Task<TourCreation> CreateMap(string from, string to, Transport transportType) {
            TourCreation res = new TourCreation();
            var key = "puPOsmfIq48rX6ia0nDeC5VBwr8wX3Po";

            var url = $"http://www.mapquestapi.com/directions/v2/route?key={key}&from={from}&to={to}&unit=k&routeType={transportType.ToString()}";
            
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var rootNode = JsonNode.Parse(content);
            Console.WriteLine(rootNode?.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));

            var sessionId = rootNode["route"]["sessionId"].ToString();
            var boundingBox = rootNode["route"]["boundingBox"];
            var ul_lat = boundingBox["ul"]["lat"].ToString();
            var ul_lng = boundingBox["ul"]["lng"].ToString();
            var lr_lat = boundingBox["lr"]["lat"].ToString();
            var lr_lng = boundingBox["lr"]["lng"].ToString();
            var distance = rootNode["route"]["distance"].ToString();
            var estTime = rootNode["route"]["formattedTime"].ToString();

            res.Distance = int.Parse(distance);
            res.EstimatedTime = int.Parse(estTime);

            url = $"http://www.mapquestapi.com/staticmap/v5/map?key={key}&session={sessionId}&boundingBox={ul_lat},{ul_lng},{lr_lat},{lr_lng}&size=800,600";
            var stream = await client.GetStreamAsync(url);
            
            byte[] bitmapData;
            using (var ms = new MemoryStream()) {
                stream.CopyTo(ms);
                bitmapData = ms.ToArray();
            }

            res.Picture = bitmapData;
            return res;
        }
    }
}
