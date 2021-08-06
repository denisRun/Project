using AutoMapper;
using HotelBLL.DTO;
using HotelBLL.Interfaces;
using HotelWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelWEB.Controllers
{
    public class UserController : Controller
    {
        IUserService service;
        IMapper mapperModelToDTO;
        IMapper mapperDtoToModel;

        public UserController(IUserService service)
        {
            this.service = service;
            this.mapperDtoToModel = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserDTO, UserModel>()).CreateMapper();
            this.mapperModelToDTO = new MapperConfiguration(cfg =>
                cfg.CreateMap<UserModel, UserDTO>()).CreateMapper();
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {           
            if (ModelState.IsValid)
            {
                var userDTO = mapperModelToDTO.Map<UserModel, UserDTO>(user);
                var result = service.Login(userDTO);

                if (result != null)
                {
                    FormsAuthentication.SetAuthCookie(result.Id.ToString(), true);
                    return RedirectToAction("Index", "Booking");
                }

                ModelState.AddModelError("", "Incorrect Login or Password");
            }

            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password == model.RepeatPassword)
                {
                    IMapper mapperRegisterToDTO = new MapperConfiguration(cfg =>
                        cfg.CreateMap<RegisterModel, UserDTO>()).CreateMapper();
                    var modelDTO = mapperRegisterToDTO.Map<RegisterModel, UserDTO>(model);
                    var result = service.Register(modelDTO);

                    if (result == null)
                    {
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Password didn`t match");
                }
            }

            return View();
        }
    }
}