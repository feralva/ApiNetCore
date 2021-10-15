using ApiHigieneYSeguridad.Constants;
using Data.EF;
using Data.Entities;
using Data.Entities.Seguridad;
using Data.Repositories;
using Data.Repositories.Contracts;
using Encriptacion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Services;
using Services.Contracts;
using System;

namespace Testing
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            //new ReportingService(null,null, null).GenerarReporteVisita(2026, "");
            //new EmailService().enviarEmailBienvenida(new Usuario() { IdUsuario = "alvarez_fernando_l@hotmail.com" }, "1234");
            //Assert.Pass();
        }
    }
}