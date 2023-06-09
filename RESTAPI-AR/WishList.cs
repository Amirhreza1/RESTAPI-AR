using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RESTAPI_AR.Models;
using System.Collections.Generic;
using System.Linq;

namespace RESTAPI_AR
{
    public static class WishList
    {
        static List<WishListItem> WishListItems = new();

        //Get All
        [FunctionName("GetWishListItems")]
        public static async Task<IActionResult> GetWishListItems(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "WishListItems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all Items.");
            return new OkObjectResult(WishListItems);
        }


        //Get By Id
        [FunctionName("GetWishListItemById")]
        public static async Task<IActionResult> GetWishListItemById(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = "WishListItems/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Getting a item by Id.");
            var WishListItem = WishListItems.FirstOrDefault(q => q.Id == id);
            if (WishListItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(WishListItem);
        }



        //Create WishList item
        [FunctionName("CreateWishListItem")]
        public static async Task<IActionResult> CreateWishListItem(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "WishListItems")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("Creating a wishlist item.");

            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateWishListItem>(requestData);

            var item = new WishListItem
            {
                ItemName = data.ItemName
            };

            WishListItems.Add(item);


            return new OkObjectResult(item);
        }



        //Update WishList
        [FunctionName("PutWishListItem")]
        public static async Task<IActionResult> PutWishListItem(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = "WishListItems/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Updating an wishlist.");
            var WishListItem = WishListItems.FirstOrDefault(q => q.Id == id);
            if (WishListItem == null)
            {
                return new NotFoundResult();
            }

            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateWishListItem>(requestData);
            WishListItem.Collected = data.Collected;
            return new OkObjectResult(WishListItem);
        }

        //Delete WishList item
        [FunctionName("DeleteWishListItem")]
        public static async Task<IActionResult> DeteleWishListItem(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "WishListItems/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            log.LogInformation("Deleting an Item from the list.");
            var WishListItem = WishListItems.FirstOrDefault(q => q.Id == id);
            if (WishListItem == null)
            {
                return new NotFoundResult();
            }

            WishListItems.Remove(WishListItem);

            return new OkResult();
        }
    }
}
