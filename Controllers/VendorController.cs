﻿using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JwtAuthentication_Relations_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [HttpPost]
        public vendorResponse VendorRegister(VendorBodyRequest vendorBodyRequest)
        {
            var Record = _vendorService.VendorRegistration(vendorBodyRequest);
            return Record;
        }
    }
}
