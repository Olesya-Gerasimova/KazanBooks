using AutoMapper;
using KazanBooks.BAL.DTO;
using KazanBooks.BAL.Services;

using KazanBooks.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace KazanBooks.WEB.Controllers
{
    public class HomeController : Controller
    {
        KazanBooksService kznBookService;

        public HomeController()
        {
            kznBookService = new KazanBooksService();
        }
        public ActionResult Index()
        {
            IEnumerable<AuthorDTO> authorDtos = kznBookService.GetAuthors();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AuthorDTO, AuthorViewModel>()).CreateMapper();
            var authors = mapper.Map<IEnumerable<AuthorDTO>, List<AuthorViewModel>>(authorDtos);
            return View(authors);
        }

        
        
    }
}
