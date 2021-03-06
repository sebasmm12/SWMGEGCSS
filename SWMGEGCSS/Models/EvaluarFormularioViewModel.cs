﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
namespace SWMGEGCSS.Models
{
    public class EvaluarFormularioViewModel
    {
        public T_actividades_desarrollar actividades_desarrollar { get; set; }
        public List<T_actividades_desarrollar> Lista_actividades_desarrollar { get; set; }
        public IPagedList<T_actividades_desarrollar> Lista_Paginada_actividades_desarrollar { get; set; }
        public T_actividades_desarrollar_gerente actividades_desarrollarGerente { get; set; }
        public T_observacion_actividades observacion_actividad { get; set; }
        public List<T_observacion_actividades> Lista_observaciones_actividades { get; set; }
        public List<T_reporte_rendimientoAUX> Lista_reportes { get; set; }
        public T_reporte_rendimientoAUX reporte { get; set; }
    }
}