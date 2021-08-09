﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Paratha.Web.Models;
using Paratha.Web.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paratha.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
