using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;


namespace PlanosAPI.Models
{
    public static class CargaTeste
    {
        // Como utilizo o in-memory database, segue uma carga inicial de  
        // alguns dados para facilitar testes e uso da API
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<PlanoDBContext>();
            if (!context.Planos.Any())
            {
                Populate(ref context);
            }
        }

        public static void Populate(ref PlanoDBContext context)
        {
                // DDD
                List<PlanoDDD> list_planoddd1 = new List<PlanoDDD>();
                List<PlanoDDD> list_planoddd2 = new List<PlanoDDD>();
                List<PlanoDDD> list_planoddd3 = new List<PlanoDDD>();
                var _021 = context.DDDs.Add(new DDD { Codigo = "021" }).Entity;
                var _027 = context.DDDs.Add(new DDD { Codigo = "027" }).Entity;
                var _011 = context.DDDs.Add(new DDD { Codigo = "011" }).Entity;
                var _031 = context.DDDs.Add(new DDD { Codigo = "031" }).Entity;
                var _041 = context.DDDs.Add(new DDD { Codigo = "041" }).Entity;
                // Operadoras
                var o1 = context.Operadoras.Add(new Operadora { Nome = "Vivo" }).Entity;
                var o2 = context.Operadoras.Add(new Operadora { Nome = "Tim" }).Entity;
                var o3 = context.Operadoras.Add(new Operadora { Nome = "Claro" }).Entity;
                // Tipos de planos
                var t1 = context.TiposPlanos.Add(new TipoPlano { Nome = "Pós" }).Entity;
                var t2 = context.TiposPlanos.Add(new TipoPlano { Nome = "Pré" }).Entity;
                var t3 = context.TiposPlanos.Add(new TipoPlano { Nome = "Controle" }).Entity;
                // Planos
                var p1 = context.Planos.Add(new Plano
                {
                    Minutos = 100,
                    Franquia = 5,
                    UnidadeFranquia = "GB",
                    Valor = Convert.ToDouble("99"),
                    IdTipoPlano = t1.Id,
                    TipoPlano = t1,
                    IdOperadora = o1.Id,
                    Operadora = o1,
                    PlanoDDD = null
                }).Entity;
                var p2 = context.Planos.Add(new Plano
                {
                    Minutos = 50,
                    Franquia = 2,
                    UnidadeFranquia = "GB",
                    Valor = Convert.ToDouble("59"),
                    IdTipoPlano = t2.Id,
                    TipoPlano = t2,
                    IdOperadora = o1.Id,
                    Operadora = o1,
                    PlanoDDD = null
                }).Entity;
            var p3 = context.Planos.Add(new Plano
            {
                Minutos = 120,
                Franquia = 8,
                UnidadeFranquia = "GB",
                Valor = Convert.ToDouble("110"),
                IdTipoPlano = t2.Id,
                TipoPlano = t3,
                IdOperadora = o1.Id,
                Operadora = o1,
                PlanoDDD = null
            }).Entity;
            var pddd1 = context.PlanosDDDs.Add(new PlanoDDD
                {
                    DDD = _021,
                    Plano = p1
                }).Entity;
                var pddd2 = context.PlanosDDDs.Add(new PlanoDDD
                {
                    DDD = _021,
                    Plano = p2
                }).Entity;
                var pddd3 = context.PlanosDDDs.Add(new PlanoDDD
                {
                    DDD = _011,
                    Plano = p2
                }).Entity;
                var pddd4 = context.PlanosDDDs.Add(new PlanoDDD
                {
                    DDD = _021,
                    Plano = p3
                }).Entity;

                list_planoddd1.Add(pddd1);
                list_planoddd2.Add(pddd2);
                list_planoddd2.Add(pddd3);
                list_planoddd3.Add(pddd4);
                p1.PlanoDDD = list_planoddd1;
                p2.PlanoDDD = list_planoddd2;
                p3.PlanoDDD = list_planoddd3;

                context.SaveChanges();
        }
    }
}
