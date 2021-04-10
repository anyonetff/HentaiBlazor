using HentaiBlazor.Data.Comic;
using HentaiBlazor.Services;
using HentaiBlazor.Services.Comic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HentaiBlazor.Controllers
{
    // 漫画服务控制器
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookService bookService;

        private readonly CoverService coverService;

        public BookController(
            BookService bookService, 
            CoverService coverService)
        {
            this.bookService = bookService;
            this.coverService = coverService;
        }

        [HttpGet("Cover/{id}")]
        public async Task Cover(string id)
        {
            byte[] cover = coverService.GetCached(id);

            if (cover == null)
            {
                CBookEntity book = await bookService.FindAsync(id);

                cover = await coverService.GetAsync(book);
            }

            if (cover == null || cover.Length == 0)
            {
                return;
            }

            using (Stream stream = this.Response.BodyWriter.AsStream())
            {
                await stream.WriteAsync(cover, 0, cover.Length);
            }
        }

    }
}
