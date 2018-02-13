﻿using Codes.Service.Interfaces;
using Codes.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClientPortal.Controllers.APIs
{
    [Authorize]
    [Route("api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public IActionResult Get(bool? isPreLogin = null)
        {
            IEnumerable<VideoModel> result;

            if (isPreLogin.HasValue)
            {
                result = _videoService.Get(isPreLogin.Value);
            }
            else
            {
                result = _videoService.Get();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _videoService.Get(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }
}
